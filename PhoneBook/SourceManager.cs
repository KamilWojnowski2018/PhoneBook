using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PhoneBook.Models;

namespace PhoneBook
{
    public class SourceManager : Connection
    {
        public List<PersonModel> GetAll()
        {
            List<PersonModel> person = new List<PersonModel>();


            Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "SELECT * FROM People",
                CommandType = CommandType.Text,
                Connection = _connection
            };
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PersonModel personModel = new PersonModel();
                            personModel.ID = Convert.ToInt32(reader.GetValue(0));
                            personModel.FirstName = reader.GetValue(1).ToString();
                            personModel.LastName = reader.GetValue(2).ToString();
                            personModel.Phone = Convert.ToString(reader.GetValue(3));
                            personModel.Email = reader.GetValue(4).ToString();
                            personModel.Created = Convert.ToDateTime(reader.GetValue(5));
                            string temp = reader.GetValue(6).ToString().ToLower();
                            if (temp != "")
                            {
                                personModel.Updated = Convert.ToDateTime(temp);
                            }
                            person.Add(personModel);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            return person;
        }

        //// przygotowane pod paginację.
        public List<PersonModel> Get(int start, int take)
        {
            List<PersonModel> person = new List<PersonModel>();

            Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "SELECT * FROM People WHERE id BETWEEN @start AND @take",
                CommandType = CommandType.Text,
                Connection = _connection
            };
            SqlParameter sqlParameterStart = new SqlParameter()
            {
                ParameterName = "@start",
                Value = start,
                DbType = DbType.Int32
            };
            SqlParameter sqlParameterTake = new SqlParameter()
            {
                ParameterName = "@take",
                Value = take,
                DbType = DbType.Int32
            };
            command.Parameters.Add(sqlParameterStart);
            command.Parameters.Add(sqlParameterTake);
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PersonModel personModel = new PersonModel();
                            personModel.ID = Convert.ToInt32(reader.GetValue(0));
                            personModel.FirstName = reader.GetValue(1).ToString();
                            personModel.LastName = reader.GetValue(2).ToString();
                            personModel.Phone = Convert.ToString(reader.GetValue(3));
                            personModel.Email = reader.GetValue(4).ToString();
                            personModel.Created = Convert.ToDateTime(reader.GetValue(5));
                            string temp = reader.GetValue(6).ToString().ToLower();
                            if (temp != "")
                            {
                                personModel.Updated = Convert.ToDateTime(temp);
                            }
                            person.Add(personModel);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            return person;
        }

        public PersonModel GetByID(int id)
        {
            PersonModel personModel = new PersonModel();

            Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "SELECT * FROM People WHERE id=@id",
                CommandType = CommandType.Text,
                Connection = _connection
            };
            SqlParameter sqlParameterID = new SqlParameter()
            {
                ParameterName = "@id",
                Value = id,
                DbType = DbType.Int32
            };
            command.Parameters.Add(sqlParameterID);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            personModel.ID = Convert.ToInt32(reader.GetValue(0));
                            personModel.FirstName = reader.GetValue(1).ToString();
                            personModel.LastName = reader.GetValue(2).ToString();
                            personModel.Phone = Convert.ToString(reader.GetValue(3));
                            personModel.Email = reader.GetValue(4).ToString();
                            personModel.Created = Convert.ToDateTime(reader.GetValue(5));
                            string temp = reader.GetValue(6).ToString().ToLower();
                            if (temp != "")
                            {
                                personModel.Updated = Convert.ToDateTime(temp);
                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            return personModel;
        }

        public int Add(PersonModel personModel)
        {
            int id = 0;
            try
            {
                Open();
                SqlCommand command = new SqlCommand()
                {
                    CommandText = "INSERT INTO People (FirstName, LastName, Phone, Email, Created) VALUES (@FirstName, @LastName, @Phone, @Email, @Created)",
                    CommandType = CommandType.Text,
                    Connection = _connection
                };
                SqlParameter sqlParameterFirstName = new SqlParameter()
                {
                    ParameterName = "@FirstName",
                    Value = personModel.FirstName,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterLastName = new SqlParameter()
                {
                    ParameterName = "@LastName",
                    Value = personModel.LastName,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterPhone = new SqlParameter()
                {
                    ParameterName = "@Phone",
                    Value = personModel.Phone,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterEmail = new SqlParameter()
                {
                    ParameterName = "@Email",
                    Value = personModel.Email,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterCreated = new SqlParameter()
                {
                    ParameterName = "@Created",
                    Value = DateTime.Now,
                    DbType = DbType.DateTime
                };

                command.Parameters.Add(sqlParameterFirstName);
                command.Parameters.Add(sqlParameterLastName);
                command.Parameters.Add(sqlParameterPhone);
                command.Parameters.Add(sqlParameterEmail);
                command.Parameters.Add(sqlParameterCreated);
                command.ExecuteNonQuery();



                SqlCommand command1 = new SqlCommand()
                {
                    CommandText = "SELECT TOP 1 * FROM People ORDER BY id DESC",
                    CommandType = CommandType.Text,
                    Connection = _connection
                };
                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader.GetValue(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            return id;
        }

        public void Update(PersonModel personModel)
        {
            try
            {
                Open();
                SqlCommand command = new SqlCommand()
                {
                    CommandText = "UPDATE People SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email =  @Email, Updated = @Updated WHERE id = @id",
                    CommandType = CommandType.Text,
                    Connection = _connection
                };
                SqlParameter sqlParameterFirstName = new SqlParameter()
                {
                    ParameterName = "@FirstName",
                    Value = personModel.FirstName,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterLastName = new SqlParameter()
                {
                    ParameterName = "@LastName",
                    Value = personModel.LastName,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterPhone = new SqlParameter()
                {
                    ParameterName = "@Phone",
                    Value = personModel.Phone,
                    DbType = DbType.Int32
                };
                SqlParameter sqlParameterEmail = new SqlParameter()
                {
                    ParameterName = "@Email",
                    Value = personModel.Email,
                    DbType = DbType.String
                };
                SqlParameter sqlParameterUpdated = new SqlParameter()
                {
                    ParameterName = "@Updated",
                    Value = DateTime.Now,
                    DbType = DbType.DateTime
                };
                SqlParameter sqlParameterID = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = personModel.ID,
                    DbType = DbType.Int32
                };
                command.Parameters.Add(sqlParameterFirstName);
                command.Parameters.Add(sqlParameterLastName);
                command.Parameters.Add(sqlParameterPhone);
                command.Parameters.Add(sqlParameterEmail);
                command.Parameters.Add(sqlParameterUpdated);
                command.Parameters.Add(sqlParameterID);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        public void Remove(long id)
        {
            Open();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM People WHERE id = @id";
                command.CommandType = CommandType.Text;
                command.Connection = _connection;

                SqlParameter sqlParameterID = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id,
                    DbType = DbType.Int32
                };
                command.Parameters.Add(sqlParameterID);
                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        public List<PersonModel> Search(string searchString)
        {

            List<PersonModel> person = new List<PersonModel>();
            Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM People WHERE LastName LIKE '@LastName'";
            command.CommandType = CommandType.Text;
            command.Connection = _connection;

            SqlParameter sqlParameterLastName = new SqlParameter()
            {
                ParameterName = "@LastName",
                Value = "%" + searchString + "%",
                DbType = DbType.String
            };
            command.Parameters.Add(sqlParameterLastName);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PersonModel personModel = new PersonModel();
                            personModel.ID = Convert.ToInt32(reader.GetValue(0));
                            personModel.FirstName = reader.GetValue(1).ToString();
                            personModel.LastName = reader.GetValue(2).ToString();
                            personModel.Phone = Convert.ToString(reader.GetValue(3));
                            personModel.Email = reader.GetValue(4).ToString();
                            personModel.Created = Convert.ToDateTime(reader.GetValue(5));
                            string temp = reader.GetValue(6).ToString().ToLower();
                            if (temp != "")
                            {
                                personModel.Updated = Convert.ToDateTime(temp);
                            }
                            person.Add(personModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            return person;
        }
    }
}