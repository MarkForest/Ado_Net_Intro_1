using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParametrizidQueryDemo
{
    public partial class Form1 : Form
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        SqlConnection sqlConnection = null;
        SqlDataReader sqlDataReader = null;
        SqlCommand sqlCommand = null;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 50 && textBox2.Text.Length <= 50) {
                sqlConnection = new SqlConnection(connectionString);
                string query = "insert into Author values(@firstName, @lastName)";
                sqlCommand = new SqlCommand(query, sqlConnection);
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@lastName";
                param2.SqlDbType = SqlDbType.VarChar;
                param2.Value = textBox2.Text;
                sqlCommand.Parameters.Add(param2);
                //sqlCommand.Parameters.Add("@firstName", SqlDbType.VarChar).Value = textBox1.Text;
                sqlCommand.Parameters.AddWithValue("@firstName", textBox1.Text);


                try
                {
                    sqlConnection.Open();
                    int rows = sqlCommand.ExecuteNonQuery();
                    MessageBox.Show($"Count working rows = {rows}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlConnection?.Close();
                }
               
            }
            else
            {
                MessageBox.Show("Не тупи пользователь");
            }

        }
    }
}
