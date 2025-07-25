﻿using MySql.Data.MySqlClient;

namespace Cumulative_Assignment.Models
{
    //Configuring the Database School needed for the API and web application.
    public class SchoolDbContext
    {
        private static string User {  get { return "root";  } }

        private static string Password { get { return ""; } }

        private static string Database { get { return "school"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }


        protected static string ConnectionString
        {
            get
            {

                return "server =  " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
