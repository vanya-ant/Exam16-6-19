using Exam.Data;
using Exam.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Exam.Services
{
    public class UsersService : IUsersService
    {
        private readonly ExamDbContext context;

        public UsersService(ExamDbContext examDbContext)
        {
            this.context = examDbContext;
        }

        public string CreateUser(string username, string email, string password)
        {

            if (this.context.Users.Any(x => x.Username == username || 
                    this.context.Users.Any(u => u.Email == email)))
            {
                return null;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = this.HashPassword(password),
            };
            this.context.Users.Add(user);
            this.context.SaveChanges();
            return user.Id;
        }

        public User GetUserOrNull(string username, string password)
        {
            var passwordHash = this.HashPassword(password);
            var user = this.context.Users.FirstOrDefault(
                x => x.Username == username
                && x.Password == passwordHash);
            return user;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
