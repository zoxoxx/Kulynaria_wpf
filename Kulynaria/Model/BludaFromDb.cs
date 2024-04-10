using Kulynaria.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kulynaria.Model
{
    internal class BludaFromDb
    {
        public List<Bludo> loadBludos()
        {
            List<Bludo> bludos = new List<Bludo>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "SELECT identityb, bludo, vids_blud, osnova, vyhod, bludo_image "
                    + "FROM public.bluda, public.vid_blud where public.vid_blud.id =public.bluda.numbo order by identityb;";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bludos.Add(new Bludo((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], reader[5].ToString()));
                    }
                }
                reader.Close();
                return bludos;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return bludos;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Bludo> FiltrBludosByCategory(int idCategoriya)
        {
            List<Bludo> bludos = new List<Bludo>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "SELECT identityb, bludo, vids_blud, osnova, vyhod, bludo_image "
                    + "FROM public.bluda, public.vid_blud where public.vid_blud.id =public.bluda.numbo and id = @categoriya_id order by identityb;";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                command.Parameters.AddWithValue("@categoriya_id", idCategoriya);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bludos.Add(new Bludo((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], reader[5].ToString()));
                    }
                }
                reader.Close();
                return bludos;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return bludos;
            }
            finally
            {
                connection.Close();
            }
        }


        public static List<SostavBluda> SostavBludFromDB(int idBluda)
        {
            List<SostavBluda> sostavBlud = new List<SostavBluda>();

            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "SELECT product, ves1 " +
                    "FROM public.sostyav_blud, public.products " +
                    "where public.sostyav_blud.producty1=public.products.identificator and sostyav_blud.bluda1=@idBluda;";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                command.Parameters.AddWithValue("@idBluda", idBluda);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sostavBlud.Add(new SostavBluda(idBluda, reader[0].ToString(), (int)reader[1]));
                    }
                }
                reader.Close();
                return sostavBlud;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return sostavBlud;
            }
            finally
            {
                connection.Close();
            }
        }

        //public void AddNewBludo(Bludo newBludo, List<SostavBluda> sostavBludas, int idCategoriya, string picPath)
        //{
        //    NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
        //    connection.Open();

        //    NpgsqlTransaction transaction = connection.BeginTransaction();
        //    NpgsqlCommand cmd = connection.CreateCommand();
        //    cmd.Transaction = transaction;

        //    try
        //    {
        //        cmd.CommandText = " insert into public.bluda (bludo, numbo, osnova, vyhod, bludo_image) " +
        //           "values (@bludoName, @idCat, @osnova, @vyhod, @picPath )";
        //        cmd.Parameters.AddWithValue("@bludoName", newBludo.bludo);
        //        cmd.Parameters.AddWithValue("@idCat", idCategoriya);
        //        cmd.Parameters.AddWithValue("@osnova", newBludo.Osnova);
        //        cmd.Parameters.AddWithValue("@vyhod", newBludo.Vyhod);
        //        cmd.Parameters.AddWithValue("@picPath", picPath);
        //        cmd.ExecuteNonQuery();

        //        cmd.CommandText = "select MAX(identityb) from public.bluda";
        //        int idBluda = Convert.ToInt32(cmd.ExecuteScalar());
        //        MessageBox.Show(idBluda.ToString());

        //        for (int i = 0; i < sostavBludas.Count; i++)
        //        {
        //            cmd.CommandText = $"INSERT INTO public.sostyav_blud( bluda1, producty1, ves1) " +
        //                $"VALUES ({idBluda},(select public.products.identificator from public.products where product='{sostavBludas[i].ProductName}'),"
        //                + $"{sostavBludas[i].Weight})";
        //            cmd.ExecuteNonQuery();
        //        }
        //        MessageBox.Show($"Блюдо добавлено!");

        //        transaction.Commit();
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        transaction.Rollback();
        //    }
        //}




        public int Schetchik(List<Bludo> bludos)
        {
            int b;
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "select count(*) from rezepti";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                b = Convert.ToInt32(command.ExecuteScalar());
                return b;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 0;

        }

        public static int CounterBludos()
        {
            int b;
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.connectionStr);
            try
            {
                connection.Open();
                string sqlExp = "select count(*) from public.bluda";
                NpgsqlCommand command = new NpgsqlCommand(sqlExp, connection);
                b = Convert.ToInt32(command.ExecuteScalar());
                return b;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 0;

        }
    }
}

