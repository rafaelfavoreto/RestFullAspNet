using RestFullAspNet.Data.VO;
using RestFullAspNet.Model;
using RestFullAspNet.Model.Context;
using RestFullAspNet.Repository.Generic;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestFullAspNet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MysqlContext _context;

        public UserRepository(MysqlContext context)
        {
            _context = context;
        }
        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(p => p.Id.Equals(user.Id))) return null;


            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
          
          return result;
           
        }

        public bool RevokeToken(string UserName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == UserName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == username));
        }

        private object ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[]  hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
