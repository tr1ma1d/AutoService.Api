using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Core.Models
{
    public class Users
    {
        public Guid Id { get; }
        public string Username { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Email { get; } = string.Empty;


        private Users(Guid id, string username, string password, string email)
        {
            Id = id;
            Username = username;               
            Password = password;
            Email = email;
        }
        private Users(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public static (Users User, string error) Create(Guid id, string username, string password, string email)
        {
            string error = string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email)) {
                error = "Введите данные // Enter a data";
            }
            var user = new Users(id, username, password, email);

            return (user, error);
        }
        public static (Users User, string error) Create(string username, string password, string email)
        {
            string error = string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                error = "Введите данные // Enter a data";
            }
            var user = new Users(username, password, email);

            return (user, error);
        }
    }
}
