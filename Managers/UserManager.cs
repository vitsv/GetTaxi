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
    public class UserManager : GlobalManager
    {
        public User GetUserByLogin(string login)
        {
            return RepoGeneric.FindOne<User>(c => c.Login == login);
        }

        public User GetUserByPhone(string phone)
        {
            return RepoGeneric.FindOne<User>(c => c.Phone.Equals(phone));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return RepoGeneric.GetAll<User>();
        }

        public IUnitOfWorkResult AddUser(User user)
        {
            string _salt = GenerateSalt(32);
            user.Salt = _salt;
            user.Password = CreatePasswordHash(user.Password, _salt);

            var repo = RepoGeneric;
            repo.Add<User>(user);
            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        public User GetUserById(int id)
        {
            return RepoGeneric.FindOne<User>(c => c.UserId == id);
        }

        public IUnitOfWorkResult EditUser(User user)
        {
            var repo = RepoGeneric;
            User editedUser = RepoGeneric.FindOne<User>(c => c.UserId == user.UserId);

            editedUser.FirstName = user.FirstName;
            editedUser.LastName = user.LastName;
            editedUser.Email = user.Email;

            if (!String.IsNullOrEmpty(user.Password))
            {
                string _salt = GenerateSalt(32);
                editedUser.Salt = _salt;
                editedUser.Password = CreatePasswordHash(user.Password, _salt);
            }

            var res = repo.UnitOfWork.SaveChanges();

            return res;
        }

        public IUnitOfWorkResult UpdateRoles(User user, List<int> roles)
        {
            var repo = RepoGeneric;
            var userRoles = repo.Find<UserRole>(c => c.UserId == user.UserId);
            foreach (var ur in userRoles)
                repo.Delete<UserRole>(ur);

            foreach (var role in roles)
            {
                repo.Add<UserRole>(new UserRole { UserId = user.UserId, RoleId = role });
            }

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult Delete(int id)
        {
            var repo = RepoGeneric;
            var model = repo.FindOne<User>(c => c.UserId == id);

            if (model == null)
                throw new Exception(string.Format("Can't find user with id {0}", id));

            var userRoles = repo.Find<UserRole>(c => c.UserId == model.UserId);
            foreach (var ur in userRoles)
                repo.Delete<UserRole>(ur);

            repo.Delete<User>(model);

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult UpdateLastLoginDate(int id)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<User>(c => c.UserId == id);
            user.LastLoginDate = DateTime.Now;
            user.SuspendDate = null;

            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return RepoGeneric.GetAll<Role>();
        }

        public bool IsValidLogin(string login, string password)
        {

            var user = this.GetUserByLogin(login);

            if (user != null)
            {
                string passHash = CreatePasswordHash(password, user.Salt);
                if (user.Password == passHash && user.Active == true)
                {
                    UpdateLastLoginDate(user.UserId);
                    return true;
                }
            }


            return false;
        }

        public bool IsValidPhone(string phone, string password)
        {

            var user = this.GetUserByPhone(phone);

            if (user != null)
            {
                string passHash = CreatePasswordHash(password, user.Salt);
                if (user.Password == passHash && user.Active == true)
                {
                    UpdateLastLoginDate(user.UserId);
                    return true;
                }
            }


            return false;
        }

        public bool IsPasswordPass(string login, string password)
        {
            var user = this.GetUserByLogin(login);
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

        public string[] GetRolesForUser(string login)
        {
            var user = this.GetUserByLogin(login);
            if (user != null)
            {
                List<string> roles = new List<string>();
                foreach (var role in user.UserRole)
                {
                    roles.Add(role.Role.Name);
                }

                return roles.ToArray();
            }

            return null;
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

        public IUnitOfWorkResult ChangePassword(string login, string password)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<User>(c => c.Login == login && c.Active == true);
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
        /// Unblock suspended user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public IUnitOfWorkResult UnlockUser(int id)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<User>(c => c.UserId == id);
            if (user != null)
            {
                user.SuspendDate = null;

            }

            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        public IUnitOfWorkResult UnlockUser(string login)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<User>(c => c.Login == login);
            if (user != null)
            {
                user.SuspendDate = null;

            }

            var res = repo.UnitOfWork.SaveChanges();
            return res;
        }

        public int GetRightsSum(int id)
        {
            var user = RepoGeneric.FindOne<User>(c => c.UserId == id);
            if (user == null)
                throw new Exception(string.Format("User not found", id));

            int sum = 0;
            foreach (var role in user.UserRole)
            {
                sum = sum + role.Role.Right.Sum(c => c.Value);
            }

            return sum;
        }

        /// <summary>
        /// Check if user with given phone exist in DB
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Return True if user doesn't exist</returns>
        public bool CheckIfPhoneUniq(string phone)
        {
            return (RepoGeneric.FindOne<User>(c => c.Phone.Equals(phone)) == null);
        }

        public bool ActivateNewUser(int userId, string code)
        {
            var repo = RepoGeneric;
            var user = repo.FindOne<User>(c => c.UserId == userId);
            if (user.ActivateCode.Equals(code))
            {
                var userRoles = user.UserRole.ToList();

                foreach (var ur in userRoles)
                    repo.Delete<UserRole>(ur);

                user.UserRole.Add(new UserRole { RoleId = (int)GlobalEnumerator.UserRoleId.User });

                var res = repo.UnitOfWork.SaveChanges();
                if (res.IsError)
                    throw new Exception(res.ErrorMessage);
                return true;
            }
            else
                return false;
        }

        public string GenerateSmsCode()
        {
            Random _rng = new Random((int)DateTime.Now.Ticks);
            string _chars = "0123456789";
            int size = 5;

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
