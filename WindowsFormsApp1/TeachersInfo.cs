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
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    public partial class TeachersInfo : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        private bool isEditMode = false;
        public TeachersInfo()
        {
            InitializeComponent();
            SetReadOnlyMode(true);
        }
        private void SetReadOnlyMode(bool readOnly)
        {
            dataGridView1.ReadOnly = readOnly;
            dataGridView1.AllowUserToAddRows = !readOnly;
            dataGridView1.AllowUserToDeleteRows = !readOnly;
            isEditMode = !readOnly;

            pictureBox4.Enabled = !readOnly;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            query = "select teach_name as 'Імя',teach_surname as 'Прізвище',teach_middlename as 'По-батькові'," +
                "teach_category as 'Категорія' from teacher";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            query = "select teach_name as 'Імя',teach_surname as 'Прізвище',teach_middlename as 'По-батькові'," +
                "teach_category as 'Категорія' from teacher where teach_name like '%" + textBox1.Text+ "%' or teach_surname like '%" + textBox1.Text + "%' or teach_middlename like '%" + textBox1.Text + "%'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            query = "select teach_name as 'Імя',teach_surname as 'Прізвище',teach_middlename as 'По-батькові'," +
                "teach_category as 'Категорія'  from teacher";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            AddTeachers frm = new AddTeachers();
            frm.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            TeachersDeleate frm = new TeachersDeleate();
            frm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                SetReadOnlyMode(true);
                Form3_Load(sender, e); 
            }
            else
            {
                SetReadOnlyMode(false);
                dataGridView1.AllowUserToAddRows = false; 
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow || row.Cells["Прізвище"].Value == null) continue;

                    var surname = row.Cells["Прізвище"].Value?.ToString();
                    var name = row.Cells["Імя"].Value?.ToString();
                    var middlename = row.Cells["По-батькові"].Value?.ToString();
                    var category = row.Cells["Категорія"].Value?.ToString();

                    var existingTeacher = fn.getData(
                        $"SELECT teach_id FROM teacher WHERE teach_surname = '{MySqlHelper.EscapeString(surname)}'");

                    if (existingTeacher.Tables[0].Rows.Count > 0)
                    {
                        var teacherId = existingTeacher.Tables[0].Rows[0][0].ToString();
                        fn.setData(
                            $"UPDATE teacher SET " +
                            $"teach_name = '{MySqlHelper.EscapeString(name)}', " +
                            $"teach_middlename = '{MySqlHelper.EscapeString(middlename)}', " +
                            $"teach_category = '{MySqlHelper.EscapeString(category)}' " +
                            $"WHERE teach_id = {teacherId}");
                    }
                    else
                    {
                        fn.setData(
                            "INSERT INTO teacher (teach_name, teach_surname, teach_middlename, teach_category) " +
                            $"VALUES ('{MySqlHelper.EscapeString(name)}', '{MySqlHelper.EscapeString(surname)}', " +
                            $"'{MySqlHelper.EscapeString(middlename)}', '{MySqlHelper.EscapeString(category)}')");
                    }
                }

                MessageBox.Show("Дані збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetReadOnlyMode(true);
                Form3_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
