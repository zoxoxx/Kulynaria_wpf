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
    internal class CategoryFromDb
    {
        public List<Categoriya> LoadCategories()
        {

            List<Categoriya> categoriyes = new List<Categoriya>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "SELECT id, vids_blud "
                    + "FROM public.vid_blud";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categoriyes.Add(new Categoriya((int)reader[0], reader[1].ToString()));
                    }
                }
                reader.Close();
                return categoriyes;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return categoriyes;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
