using Data.Domain;
using Managers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Data.Enumerators;

namespace Managers
{
    public class ClientManager : GlobalManager
    {

        public Client GetClientByPhone(string phone)
        {
            return RepoGeneric.FindOne<Client>(c => c.Phone.Equals(phone));
        }

        public IEnumerable<Client> GetAllClients()
        {
            return RepoGeneric.GetAll<Client>();
        }

        public IUnitOfWorkResult Add(Client client)
        {
            string _salt = GenerateSalt(32);
            client.Salt = _salt;
            client.Password = CreatePasswordHash(client.Password, _salt);

            var repo = RepoGeneric;
            repo.Add<Client>(client);
            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        public IUnitOfWorkResult AddByCode(string phone)
        {
            var repo = RepoGeneric;
            var client = repo.FindOne<Client>(c => c.Phone.Equals(phone));

            if (client == null)
            {
                client = new Client();

                client.Phone = phone;
                client.CreationDate = DateTime.Now;

                client.IsActive = true;

                string _salt = GenerateSalt(32);
                client.Salt = _salt;
                client.Password = CreatePasswordHash(GenerateSmsCode(10).ToString(), _salt);


                repo.Add<Client>(client);
            }

            client.ActivateCode = GenerateSmsCode();
            client.SmsSentCount += 1;

            //TODO wysłac kod

            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        public Client GetUserById(int id)
        {
            return RepoGeneric.FindOne<Client>(c => c.ClientId == id);
        }

        public IUnitOfWorkResult EditUser(Client client)
        {
            var repo = RepoGeneric;
            Client editedClient = RepoGeneric.FindOne<Client>(c => c.ClientId == client.ClientId);

            editedClient.FirstName = client.FirstName;
            editedClient.LastName = client.LastName;
            editedClient.Email = client.Email;

            if (!String.IsNullOrEmpty(client.Password))
            {
                string _salt = GenerateSalt(32);
                editedClient.Salt = _salt;
                editedClient.Password = CreatePasswordHash(client.Password, _salt);
            }

            var res = repo.UnitOfWork.SaveChanges();

            return res;
        }


        public IUnitOfWorkResult Delete(int id)
        {
            var repo = RepoGeneric;
            var model = repo.FindOne<Client>(c => c.ClientId == id);

            if (model == null)
                throw new Exception(string.Format("Can't find user with id {0}", id));

            repo.Delete<Client>(model);

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult UpdateLastLoginDate(int id)
        {
            var repo = RepoGeneric;
            var client = repo.FindOne<Client>(c => c.ClientId == id);
            client.LastLoginDate = DateTime.Now;
            client.SmsSentCount = 0;

            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }


        public bool IsValidLogin(string phone, string password)
        {

            var client = this.GetClientByPhone(phone);

            if (client != null)
            {
                string passHash = CreatePasswordHash(password, client.Salt);
                if (client.Password == passHash && client.IsActive == true)
                {
                    UpdateLastLoginDate(client.ClientId);
                    return true;
                }
            }


            return false;
        }

        public bool IsPasswordPass(string login, string password)
        {
            var user = this.GetClientByPhone(login);
            if (user != null)
            {
                string passHash = CreatePasswordHash(password, user.Salt);
                if (user.Password == passHash)
                {
                    return true;
                }
            }

            return false;
        }

        public string GenerateSalt(int length)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[length];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public virtual string CreatePasswordHash(string password, string saltkey)
        {
            string saltAndPassword = String.Concat(password, saltkey);
            string hashedPassword = GetMD5Hash(saltAndPassword);
            return hashedPassword;
        }

        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public IUnitOfWorkResult ChangePassword(string phone, string password)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<Client>(c => c.Phone == phone && c.IsActive == true);
            if (user != null)
            {
                if (!String.IsNullOrEmpty(password))
                {
                    string _salt = GenerateSalt(32);
                    user.Salt = _salt;
                    user.Password = CreatePasswordHash(password, _salt);

                }
            }

            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        /// <summary>
        /// Check if user with given phone exist in DB
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Return True if user doesn't exist</returns>
        public bool CheckIfPhoneUniq(string phone)
        {
            return (RepoGeneric.FindOne<Client>(c => c.Phone.Equals(phone)) == null);
        }

        public bool ActivateNewUser(int clientId, string code)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<Client>(c => c.ClientId == clientId);
            if (user != null)
            {
                if (user.ActivateCode.Equals(code))
                {
                    user.IsActive = true;

                    var res = repo.UnitOfWork.SaveChanges();
                    if (res.IsError)
                        throw new Exception(res.ErrorMessage);
                    return true;
                }
            }

            return false;
        }

        public bool ActivateNewUserByPhone(string phone, string code)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<Client>(c => c.Phone == phone);
            if (user != null)
            {
                if (user.ActivateCode.Equals(code))
                {
                    user.IsActive = true;

                    var res = repo.UnitOfWork.SaveChanges();
                    if (res.IsError)
                        throw new Exception(res.ErrorMessage);
                    return true;
                }
            }

            return false;
        }

        public string GenerateSmsCode(int size = 5)
        {
            Random _rng = new Random((int)DateTime.Now.Ticks);
            string _chars = "0123456789";

            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        public bool ResendActivateSms(int userId)
        {
            var repo = RepoGeneric;

            var user = repo.FindOne<User>(c => c.UserId == userId);

            if (user == null)
                return false;

            if (user.SmsSentCount >= 3)
                return false;

            var kod = GenerateSmsCode();

            //TODO Wyslac SMS

            user.SmsSentCount = user.SmsSentCount + 1;
            user.ActivateCode = kod;

            var res = repo.UnitOfWork.SaveChanges();

            if (res.IsError)
                return false;

            return true;

        }
    }
}
