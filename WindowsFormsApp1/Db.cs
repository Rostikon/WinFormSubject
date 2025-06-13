using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace func
{
    class function
    {
        protected MySqlConnection getConnection()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=localhost;database=academic_performance;port=3306;User Id=root;password=secret;";
            return con;
        }

        public DataSet getData(String query)
        {
            MySqlConnection con = getConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void setData(String query, String msg)
        {
            MySqlConnection con = getConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void setData(String query)
        {
            MySqlConnection con = getConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }
        
    } 
}
