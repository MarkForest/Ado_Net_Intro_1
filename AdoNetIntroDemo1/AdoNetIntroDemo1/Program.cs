using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace AdoNetIntroDemo1
{
    class Program
    {
        static SqlConnection sqlConnection = null;
        static string connectionString;
        static SqlCommand sqlCommand = null;
        static SqlDataReader sqlDataReader = null;
        static void Main(string[] args)
        {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            //InsertAuthor();
            SelectAuthor();
            
            Console.ReadKey();
            
        }

        private static void SelectAuthor()
        {
            sqlConnection = new SqlConnection(connectionString);

            string querySelect = "select * from books; select * from Author;";
            int line = 0;
           

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                do
                {
                    while (sqlDataReader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                Console.Write(sqlDataReader.GetName(i) + " ");
                            }
                            Console.WriteLine();
                        }
                        line++;
                        Console.WriteLine(sqlDataReader[0] + " " + sqlDataReader[1] + " " + sqlDataReader[2]+ " "+ sqlDataReader[3] + " ");
                    }
                } while (sqlDataReader.NextResult());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection?.Close();
                sqlDataReader?.Close();
            }

        }

        private static void InsertAuthor()
        {
            
            //1....
            //sqlConnection = new SqlConnection();
            //sqlConnection.ConnectionString = connectionString;
            //2....
            sqlConnection = new SqlConnection(connectionString);
            string query = "insert into Author values('Roger', 'Zelazny')";
           
            //sqlCommand = new SqlCommand();
            //sqlCommand.Connection = sqlConnection;
            //sqlCommand.CommandText = query;
            sqlCommand = new SqlCommand(query, sqlConnection);


            try
            {
                sqlConnection.Open();
                int rows = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"Count working rows = {rows}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //if (sqlConnection != null) {
                //  sqlConnection.Close();
                //}
                sqlConnection?.Close();
            }
        }
    }
}
