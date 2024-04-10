using Kulynaria.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kulynaria.Model
{
     class UserFromDb
    {
        public User GetUser(string login, string password)
        {
            User user = null;
            try
            {
                using (NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr))
                {
                    connect.Open();

                    string sqlExp = "SELECT*FROM users WHERE login=@login ";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlExp, connect);
                    cmd.Parameters.AddWithValue("login", login);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (password != "")
                        {
                            if (Verification.VerifySHA512Hash(password, (string)reader["password_"]))
                            {
                                DateTime birthday = DateTime.Now;
                                if (!(reader[4] is DBNull))
                                {
                                    birthday = Convert.ToDateTime(reader[4]);
                                }
                                user = new User((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), birthday,
                                    reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), (int)reader[9]);
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет такого пользователя");
                    }
                    return user;
                }
            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return user; }
        }



        public static List<Role> loadRoles()
        {
            List<Role> roles = new List<Role>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "SELECT role_id, rolename "
                    + "FROM public.role";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role((int)reader[0], reader[1].ToString()));
                    }
                }
                reader.Close();
                return roles;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return roles;
            }
            finally
            {
                connection.Close();
            }
        }


        public static List<User> loadUsers()
        {
            List<User> users = new List<User>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "SELECT user_id, firstname, lastname, patronymic, dateofbirthday, login, password_, phone, adress, role_id "
                    + "FROM public.users;";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime birthday = DateTime.Now;
                        if (!(reader[4] is DBNull))
                        {
                            birthday = Convert.ToDateTime(reader[4]);
                        }
                        users.Add(new User((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), birthday,
                                  reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), (int)reader[9]));
                    }
                }
                reader.Close();
                return users;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return users;
            }
            finally
            {
                connection.Close();
            }
        }



        public static void GetPassword(string newpassword, string oldpassword, User user)
        {
            NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connect.Open();
                if (user.Password == Verification.GetSHA512Hash(oldpassword))
                {
                    string sqlExp = "UPDATE users " +
                        "set password_ = @newpassword " +
                         "where \"user_id\" = @user_id ";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlExp, connect);
                    cmd.Parameters.AddWithValue("newpassword", Verification.GetSHA512Hash(newpassword));
                    cmd.Parameters.AddWithValue("user_id", user.UserId);
                    int vyp = cmd.ExecuteNonQuery();
                    if (vyp == 1) { MessageBox.Show("Пароль изменен"); }
                }
                else MessageBox.Show("Пароль неверный");
            }

            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return; }
            connect.Close();
        }


        public static bool CheckPassword(string password, string passRepeat)
        {
            if (password.Length < 6)
            {
                MessageBox.Show("Длина пароля не может быть больше 6 символов");
                return false;
            }
            else
            {
                bool f, f1, f2;
                f = f1 = f2 = false;
                for (int i = 0; i < password.Length; i++)
                {
                    if (Char.IsDigit(password[i])) f1 = true;
                    if (Char.IsUpper(password[i])) f2 = true;
                    if (f1 && f2) break;
                }
                if (!(f1 && f2))
                {
                    MessageBox.Show("Пароль должен содержать хотя бы одну цифру и одну заглавную букву!");
                    return f1 && f2;
                }
                else
                {
                    string simbol = "!@#$%^";
                    for (int i = 0; i < password.Length; i++)
                    {
                        for (int j = 0; j < simbol.Length; j++)
                            if (password[i] == simbol[j]) { f = true; break; }
                    }
                    if (!f) MessageBox.Show("Пароль должен содержать один из символов !#$%^");
                    if ((password == passRepeat) && f) return true; else { MessageBox.Show("Неверно подтвержден пароль"); return false; }
                }
            }
        }




        public static bool CheckUser(string login)
        {
            try
            {
                using (NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr))
                {
                    connect.Open();
                    string sqlExp = "SELECT login from Users where login=@login";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlExp, connect);
                    cmd.Parameters.AddWithValue("@login", login);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Такой логин уже есть"); return false;
                    }
                    else
                    {
                        reader.Close();
                        return true;
                    }
                }
            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return false; }
        }




        public static void UserAdd(string login, string password, string firstName, string lastName)
        {
            NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connect.Open();
                string sqlExp = "INSERT INTO public.users(firstname, lastname, patronymic, login, password_, adress, role_id, phone) " +
                    "VALUES (@FirstName,@LastName, ' ',@login,@password,' ',default,' ')";
                NpgsqlCommand cmd1 = new NpgsqlCommand(sqlExp, connect);
                cmd1.Parameters.AddWithValue("login", login);
                cmd1.Parameters.AddWithValue("password", Verification.GetSHA512Hash(password));
                cmd1.Parameters.AddWithValue("FirstName", firstName);
                cmd1.Parameters.AddWithValue("LastName", lastName);
                int i = cmd1.ExecuteNonQuery();
                if (i == 1) { MessageBox.Show("Вы успешно зарегистрированы "); }
                else MessageBox.Show("Ошибка записи");
            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return; }
            connect.Close();
        }

        public static void update_personal_data(int seria, string email, string kem, string kogda, string login)
        {
            NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connect.Open();
                string sqlExp = "call update_user_data(@login, @seria, @kem, @kogda, @email)";
                NpgsqlCommand cmd1 = new NpgsqlCommand(sqlExp, connect);
                cmd1.Parameters.AddWithValue("email", email);
                cmd1.Parameters.AddWithValue("login", login);
                cmd1.Parameters.AddWithValue("seria", seria);
                cmd1.Parameters.AddWithValue("kem", kem);
                cmd1.Parameters.AddWithValue("kogda", kogda);
                int i = cmd1.ExecuteNonQuery();
                if (i == 1) { MessageBox.Show("Персональные данные успешно заполнены "); }
                else MessageBox.Show("Ошибка записи");
            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return; }
            connect.Close();
        }


        public static void UserUpdateProfil(User user)
        {
            NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connect.Open();
                string sqlExp = "UPDATE public.users SET firstname =@FirstName, lastname =@LastName, patronymic =@patronymic, dateofbirthday =@date_of_birthday," +
                    " adress =@adress, phone =@phone where user_id=@user_id";
                NpgsqlCommand cmd1 = new NpgsqlCommand(sqlExp, connect);
                cmd1.Parameters.AddWithValue("FirstName", user.FirstName);
                cmd1.Parameters.AddWithValue("LastName", user.LastName);
                cmd1.Parameters.AddWithValue("patronymic", user.Patronymic);
                cmd1.Parameters.AddWithValue("date_of_birthday", user.DateOfBirthday);
                cmd1.Parameters.AddWithValue("adress", user.Adress);
                cmd1.Parameters.AddWithValue("phone", user.Phone);
                cmd1.Parameters.AddWithValue("user_id", user.UserId);

                int i = cmd1.ExecuteNonQuery();
                if (i == 1) { MessageBox.Show("Данные обновлены"); }
                else MessageBox.Show("Ошибка записи");

            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return; }
            connect.Close();


        }


        public static void DeleteUser(int user_id)
        {
            NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connect.Open();
                string sqlExp = "DELETE FROM public.users where user_id = @user_id ";

                NpgsqlCommand cmd1 = new NpgsqlCommand(sqlExp, connect);
                cmd1.Parameters.AddWithValue("user_id", user_id);
                int i = cmd1.ExecuteNonQuery();
                if (i == 1) { MessageBox.Show("Пользователь удален"); }
                else MessageBox.Show("Ошибка удаления");

            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return; }
            connect.Close();


        }


        public static void ChangeRole(int user_id, int role_id)
        {
            NpgsqlConnection connect = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connect.Open();
                string sqlExp = "UPDATE users " +
                               "set role_id = @role_id " +
                                "where \"user_id\" = @user_id ";

                NpgsqlCommand cmd1 = new NpgsqlCommand(sqlExp, connect);
                cmd1.Parameters.AddWithValue("user_id", user_id);
                cmd1.Parameters.AddWithValue("role_id", role_id);
                int i = cmd1.ExecuteNonQuery();
                if (i == 1) { MessageBox.Show("Роль изменена"); }
                else MessageBox.Show("Ошибка изменения роли");

            }
            catch (NpgsqlException ex)
            { MessageBox.Show(ex.Message); return; }
            connect.Close();


        }
    }
}
