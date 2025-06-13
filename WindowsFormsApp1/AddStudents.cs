using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using func;

namespace WindowsFormsApp1
{
    public partial class AddStudents : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        String state;
        public AddStudents()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton3.Checked = false;
            state = radioButton1.Text;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            String n, s, m, st, d, p, id;
            n = textBox2.Text;
            s = textBox1.Text;
            m = textBox3.Text;
            st = textBox4.Text;
            d = maskedTextBox1.Text;
            p = maskedTextBox2.Text;
            var parsedDate = DateTime.Parse(d);
            int checking = parsedDate.Year;
            if (DateTime.Now.Year - checking >= 16 && DateTime.Now.Year - checking <= 65)
            {
                query = "select stud_id+1 from student_form order by 1 desc limit 1";
                ds = fn.getData(query);
                id = ds.Tables[0].Rows[0][0].ToString();
                if (n != "" && s != "" && m != "" && st != "" && d != "")
                {
                    query = "select stud_code from student_form where stud_code='" + st + "'";
                    ds = fn.getData(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into student_form values('" + id + "','" + n + "','" + s + "','" + m + "','" + st + "','" + d + "','" + state + "','" + p + "')";
                        fn.setData(query);
                        MessageBox.Show("Готово!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Студент з таким студентським квитком вже є!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Введіть усю інформацію!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Студенту може бути тільки від 16 до 65 років!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            state = radioButton3.Text;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.' && l != 'і' && l != 'І')
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.' && l != 'і' && l != 'І')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.' && l != 'і' && l != 'І')
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string input = textBox4.Text;
            string validInput = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c) || (char.IsLetter(c) && c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    validInput += c;
                }
            }

            if (validInput.Length > 8)
            {
                validInput = validInput.Substring(0, 8);
            }

            if (textBox4.Text != validInput)
            {
                int cursorPosition = textBox4.SelectionStart;
                textBox4.Text = validInput;
                textBox4.SelectionStart = Math.Max(0, cursorPosition - 1);
            }
        }

        private void AddStudents_Load(object sender, EventArgs e)
        {

        }
    }
}
