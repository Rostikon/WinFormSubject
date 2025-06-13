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
    public partial class StudentsInfo : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        private bool isEditMode = false;
        public StudentsInfo()
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
        private void Form6_Load(object sender, EventArgs e)
        {
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            radioButton1.Checked = false;
            radioButton3.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form where Stud_state like '%Ч%'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form where Stud_state like '%Ж%'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox2.Text = "";
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form where Stud_name like '%" + textBox1.Text + "%' or Stud_surname like '%" + textBox1.Text + "%' or Stud_middlename like '%" + textBox1.Text + "%'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton1.Checked = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.Text = "";
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form where Stud_phone like '%" + textBox2.Text + "%'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton1.Checked = false;
        }

       
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            AddStudents frm = new AddStudents();
            frm.ShowDialog();
        }

        

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            StudentDeleate frm = new StudentDeleate();
            frm.ShowDialog();
        }

      

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            query = "select Stud_name as 'Імя',Stud_surname as 'Прізвище',stud_middlename as 'По-батькові'," +
                "Stud_code as 'Номер студ.квитка',stud_datebirth as 'Дата народження',stud_state as 'Стать'," +
                "stud_phone as 'Номер телефону' from student_form";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton1.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                SetReadOnlyMode(true);
                Form6_Load(sender, e);
            }
            else
            {
                SetReadOnlyMode(false);
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    var originalData = fn.getData("SELECT Stud_code, Stud_name, Stud_surname, stud_middlename, " +
                                                "stud_datebirth, stud_state, stud_phone FROM student_form")
                                      .Tables[0];

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && row.Cells["Номер студ.квитка"].Value != null)
                        {
                            var studCode = row.Cells["Номер студ.квитка"].Value?.ToString();
                            var name = row.Cells["Імя"].Value?.ToString();
                            var surname = row.Cells["Прізвище"].Value?.ToString();
                            var middlename = row.Cells["По-батькові"].Value?.ToString();
                            var dateBirth = row.Cells["Дата народження"].Value;
                            var state = row.Cells["Стать"].Value?.ToString();
                            var phone = row.Cells["Номер телефону"].Value?.ToString();

                            string date = "";
                            if (dateBirth != null && DateTime.TryParse(dateBirth.ToString(), out DateTime parsedDate))
                            {
                                date = parsedDate.ToString("yyyy-MM-dd");
                            }

                            bool recordExists = originalData.AsEnumerable()
                                .Any(r => r["Stud_code"].ToString() == studCode);

                            if (recordExists)
                            {
                                query = "UPDATE student_form SET " +
                                       $"Stud_name = '{MySqlHelper.EscapeString(name)}', " +
                                       $"Stud_surname = '{MySqlHelper.EscapeString(surname)}', " +
                                       $"stud_middlename = '{MySqlHelper.EscapeString(middlename)}', " +
                                       $"stud_datebirth = '{date}', " +
                                       $"stud_state = '{MySqlHelper.EscapeString(state)}', " +
                                       $"stud_phone = '{MySqlHelper.EscapeString(phone)}' " +
                                       $"WHERE Stud_code = '{MySqlHelper.EscapeString(studCode)}'";
                            }
                            else
                            {
                                query = "INSERT INTO student_form " +
                                       "(Stud_name, Stud_surname, stud_middlename, Stud_code, stud_datebirth, stud_state, stud_phone) " +
                                       $"VALUES ('{MySqlHelper.EscapeString(name)}', '{MySqlHelper.EscapeString(surname)}', " +
                                       $"'{MySqlHelper.EscapeString(middlename)}', '{MySqlHelper.EscapeString(studCode)}', " +
                                       $"'{date}', '{MySqlHelper.EscapeString(state)}', '{MySqlHelper.EscapeString(phone)}')";
                            }

                            fn.setData(query);
                        }
                    }

                    MessageBox.Show("Дані успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetReadOnlyMode(true);
                    Form6_Load(sender, e); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при оновленні даних: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

