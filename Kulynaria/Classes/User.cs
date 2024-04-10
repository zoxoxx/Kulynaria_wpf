using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulynaria.Classes
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public int RoleId { get; set; }

        public User(int userId, string firstName, string lastName, string patronymic, DateTime dateOfBirthday,
            string login, string password, string phone, string adress, int roleId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            DateOfBirthday = dateOfBirthday;
            Login = login;
            Password = password;
            Phone = phone;
            Adress = adress;
            RoleId = roleId;
        }

        public User(string login, string password)
        {
            Password = password;
            Login = login;
        }
    }
}
