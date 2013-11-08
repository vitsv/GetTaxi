using Data.Domain;
using Managers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class UserManager : GlobalManager
    {
        public User GetUserByLogin(string login)
        {
            return RepoGeneric.FindOne<User>(c => c.Login == login);
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

        public int GetUserRightsSum(int id)
        {
            return 0;
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

        public void DeleteUser(int id)
        {
            var repo = RepoGeneric;
            User user = repo.FindOne<User>(c => c.UserId == id);

            if (user != null)
                repo.Delete<User>(user);

            repo.UnitOfWork.SaveChanges();
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
    }
}
