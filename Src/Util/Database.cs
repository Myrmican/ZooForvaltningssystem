using MySql.Data.MySqlClient;
using System;

namespace Zoo_forvaltningssystem.Src.Util
{
    static class Database
    {
        const string credentials = "server=127.0.0.1;uid=root;pwd=;database=test";
        private static MySqlConnection? connection = null;

        public static MySqlConnection? Connect()
        {

            if (connection != null) return connection;

            try
            {
                connection = new MySqlConnection(credentials);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to database: " + e.Message);
                return null;
            }
        }
    }
}