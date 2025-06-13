using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using func;
namespace WindowsFormsApp1
{
    public partial class AddTeachers : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        public AddTeachers()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            String n, s, m, c, id;
            query = "select teach_id+1 from teacher order by 1 desc limit 1";
            ds = fn.getData(query);
            id = ds.Tables[0].Rows[0][0].ToString();
            n = textBox2.Text;
            s = textBox1.Text;
            m = textBox3.Text;
            c = textBox4.Text;
            if (n != "" && s != "" && m != "" && c != "")
            {
                query = "insert into teacher values('" + id + "','" + s + "','" + n + "','" + m + "','" + c + "')";
                fn.setData(query);
                MessageBox.Show("Готово!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Введіть усі дані!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidateCyrillicAndUkrainianInput(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 'і' && l != 'ї' && l != 'І' && l != 'Ї' && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateCyrillicAndUkrainianInput(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateCyrillicAndUkrainianInput(sender, e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateCyrillicAndUkrainianInput(sender, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateCyrillicAndUkrainianInput(sender, e);
        }
    }
}
