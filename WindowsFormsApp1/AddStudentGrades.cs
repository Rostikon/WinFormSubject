using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using globalValue;
using func;
namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        String id;
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            String n, s,c;
            label4.Text = Class2.Value;
            query = "select sub_id,sub_name,teach_surname from subjectt,teacher where subjectt.teach_id=teacher.teach_id";
            ds = fn.getData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                id= ds.Tables[0].Rows[i][0].ToString();
                n = ds.Tables[0].Rows[i][1].ToString();
                s = ds.Tables[0].Rows[i][2].ToString();
                c = n + " " + s;
                comboBox1.Items.Add(c);
                listBox1.Items.Add(id);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            String a_id, s_id, mark, date,stud_id;
            query = "select stud_id from student_form where stud_code ='" + Class2.Value1 + "'";
            ds = fn.getData(query);
            stud_id= ds.Tables[0].Rows[0][0].ToString();
            date = DateTime.Now.ToString("yyyy-MM-dd");
            query = "select acadperform_id+1 from academic_performance order by 1 desc limit 1";
            ds = fn.getData(query);
            a_id = ds.Tables[0].Rows[0][0].ToString();
            query="select sub_id from subjectt where sub_id='"+ listBox1.Items[comboBox1.SelectedIndex].ToString()+"'";
            ds = fn.getData(query);
            s_id = ds.Tables[0].Rows[0][0].ToString();
            mark = textBox1.Text;
            if (a_id != "" && stud_id != "" && s_id != "" && mark != "")
            {
                query = "insert into academic_performance.academic_performance values('" + a_id + "','" + stud_id + "','" + s_id + "','" + mark + "','" + date + "')";
                fn.setData(query);
                MessageBox.Show("Готово!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Введіть усю інформацію!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '2' && e.KeyChar <= '5') || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
