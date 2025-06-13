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
    public partial class AddSubjects : Form
    {
        function fn = new function();
        String n, h,id,z;
        int temp_i;
        String query;
        DataSet ds;

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            z = "Іспит";
            radioButton1.Checked = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            String t_id="";
            n = textBox2.Text;
            h = textBox1.Text;
            temp_i = comboBox1.SelectedIndex;
            if(temp_i!=-1)
                t_id = listBox1.Items[temp_i].ToString();
          
            query = "select sub_id+1 from subjectt order by 1 desc limit 1";
            ds = fn.getData(query);
            id = ds.Tables[0].Rows[0][0].ToString();
            if (n != "" && h != "" && t_id != "")
            {
             
                    query = "insert into subjectt values('" + id + "','" + n + "','" + h + "','" + z + "','" + t_id + "')";
                    fn.setData(query);
                    MessageBox.Show("Готово!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
            else
                MessageBox.Show("Введіть усю інформацію!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {
                if (textBox1.Text.Length >= 3 && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                    MessageBox.Show("Максимальна довжина - 3 символи", "Попередження",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            z = "Залік";
            radioButton3.Checked = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 'і' && l != 'ї' && l != '\'' && l != '-' && l != '\b')
            {
                e.Handled = true;
            }
        }


        public AddSubjects()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            String n1, h1, m1, c1,id1;
            query = "select teach_id,teach_name,teach_surname,teach_middlename from teacher";
            ds = fn.getData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                id1 = ds.Tables[0].Rows[i][0].ToString();
                n1 = ds.Tables[0].Rows[i][1].ToString();
                h1 = ds.Tables[0].Rows[i][2].ToString();
                m1 = ds.Tables[0].Rows[i][3].ToString();
                c1 = h1 + " " + n1 + " " + m1;
                comboBox1.Items.Add(c1);
                listBox1.Items.Add(id1);

            }
        }

    }
}
