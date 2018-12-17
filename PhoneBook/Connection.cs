using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PhoneBook
{
    public class Connection
    {
        protected SqlConnection _connection;

        protected void Open()
        {
            try
            {
                _connection = new SqlConnection
                {
                    ConnectionString = "Integrated Security=SSPI;" +
                                       "Data Source=DESKTOP-0CM0K4S\\SQLEXPRESS01;" +
                                       "Initial Catalog=PhoneBook;"
                };
                _connection.Open();
                var connectState = _connection.State;
                var nameOfDB = _connection.Database;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        protected void Close()
        {
            _connection.Close();
        }
    }
}