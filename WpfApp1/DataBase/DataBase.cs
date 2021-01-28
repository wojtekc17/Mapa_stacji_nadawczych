using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp1
{
    static partial class DataBase
    {
        public static SqlConnection connection;
        
        static bool _p = false;
        public static bool Polaczono
        {
            get
            {
                return _p;
            }
        }

        public static bool Open()
        {
            try
            {

                String conn = string.Format("Server = {0}; Database = {3}; User ID = {1}; Password = {2};Connection Timeout=30000; multisubnetfailover = false;", "radiowesystemy.database.windows.net", "przemo", "Radiowe2020", "radiowe"); 

                connection = new SqlConnection(conn);

                connection.Open();
                _p = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie można połączyć z serwerem baz danych.\n Skontaktuj się z Administratorem bazy danych." + ex.Message, "Radio");

                _p = false;
                return false;
            }
        }

        public static void Close()
        {
            connection.Close();
            _p = false;
        }

        public static void Command(string ask)
        {
            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(ask, connection, transaction);
            cmd.ExecuteNonQuery();

            transaction.Commit();

        }
        public static void Command(string ask, out DataTable result)
        {
            DataTable dt = new DataTable();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(ask, connection, transaction);

            using (SqlDataReader r = cmd.ExecuteReader())
            {
                dt.BeginLoadData();
                dt.Load(r);
                dt.EndLoadData();
            }
            transaction.Commit();
            result = dt;
        }
        public static DataTable BaseTable(string TableName)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {

                cmd.CommandText = string.Format("SELECT * FROM {0}", TableName);
                DataTable dt = new DataTable();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    dt.BeginLoadData();
                    dt.Load(r);
                    dt.EndLoadData();
                }
                return dt;
            }
        }

        public static void AddUser(string NazwaUzytkownika, int LokalizacjaX, int LokalizajaY, string MocNadawcza, string ZyskAntenyO, string ZyskAntenyN, int NumerKanalu, double aclr1, double aclr2, string statuss)
        {
            var a = string.Format("INSERT dbo.Users2(Nazwa,X,Y,Moc,[Zysk Nadawczej],[Zysk Odbiorczej],[Numer Kanalu], [ACLR+1], [ACLR+2], [Status]) VALUES ('{0}', {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, '{9}');", NazwaUzytkownika, LokalizacjaX, LokalizajaY, MocNadawcza, ZyskAntenyO, ZyskAntenyN, NumerKanalu, aclr1, aclr2, statuss);
            Command(a);
            //Command(string.Format("INSERT dbo.Users2() VALUES ('{0}', {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9});", NazwaUzytkownika, LokalizacjaX, LokalizajaY, NumerKanalu, MocNadawcza, ZyskAntenyN, ZyskAntenyO, aclr1, aclr2, statuss));
        }
        public static void addName(string name, string cell, string vall, int id)
        {
            Command(string.Format("dbo.{0} set \"{1}\"='{2}' where ID= ", name, cell, vall, id));
        }

        public static void ClearDB(string name)
        {
                DataBase.Command(string.Format("TRUNCATE TABLE {0}; " +
                    "DECLARE @intCounter as INT = 1; WHILE @intCounter <= 200 BEGIN INSERT INTO {0} VALUES(" +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null," +
                    "null)SET @IntCounter = @IntCounter + 1;END; ", name));
                //DataBase.Command(string.Format("", i + 1));
                //DataBase.Command(string.Format("TRUNCATE TABLE dbo.Name{0}", i + 1));

        }
    }

}
