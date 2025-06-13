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
    public partial class SubjectInfo : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        private bool isEditMode = false;
        public SubjectInfo()
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
        private void Form2_Load(object sender, EventArgs e)
        {
            query = "select sub_name as 'Назва предмету',sub_times as 'Кількість годин',sub_control as 'Тип атестації'," +
                "concat(teach_surname,' ',teach_name, ' ',teach_middlename) as 'Викладач'from subjectt join teacher using (teach_id) ";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            AddSubjects frm = new AddSubjects();
            frm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            query = "select sub_name as 'Назва предмету',sub_times as 'Кількість годин',sub_control as 'Тип атестації'," +
                "concat(teach_surname,' ',teach_name, ' ',teach_middlename) as 'Викладач'from subjectt join teacher using (teach_id) ";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            textBox2.Text = "";
            radioButton3.Checked = false;
            radioButton1.Checked = false;
            textBox1.Text = "";
        }
        private void ValidateCyrillicAndUkrainianInput(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != 'і' && l != 'ї' && l != 'І' && l != 'Ї' && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateCyrillicAndUkrainianInput(sender, e);
            query = "select sub_name as 'Назва предмету',sub_times as 'Кількість годин',sub_control as 'Тип атестації'," +
                "concat(teach_surname,' ',teach_name, ' ',teach_middlename) as 'Викладач'from subjectt join teacher using (teach_id) " +
                "where sub_name like '%" + textBox1.Text+"%'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            textBox2.Text = "";
            radioButton3.Checked = false;
            radioButton1.Checked = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateCyrillicAndUkrainianInput(sender, e);
            query = "select sub_name as 'Назва предмету',sub_times as 'Кількість годин',sub_control as 'Тип атестації'," +
                "concat(teach_surname,' ',teach_name, ' ',teach_middlename) as 'Викладач'from subjectt join teacher using (teach_id) " +
                "where teach_surname like '%" + textBox2.Text + "%' or teach_name like '%"+textBox2.Text+"%' or teach_middlename like '%"+textBox2.Text+"%'" ;
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            textBox1.Text = "";
            radioButton3.Checked = false;
            radioButton1.Checked = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            query = "select sub_name as 'Назва предмету',sub_times as 'Кількість годин',sub_control as 'Тип атестації'," +
                "concat(teach_surname,' ',teach_name, ' ',teach_middlename) as 'Викладач'from subjectt join teacher using (teach_id) " +
                "where sub_control='" + radioButton1.Text + "'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            textBox1.Text = "";
            textBox2.Text = "";
            radioButton3.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            query = "select sub_name as 'Назва предмету',sub_times as 'Кількість годин',sub_control as 'Тип атестації'," +
                "concat(teach_surname,' ',teach_name, ' ',teach_middlename) as 'Викладач'from subjectt join teacher using (teach_id) " +
                "where sub_control='" + radioButton3.Text + "'";
            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            textBox1.Text = "";
            textBox2.Text = "";
            radioButton1.Checked = false;

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            SubjectsDeleate frm = new SubjectsDeleate();
            frm.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                SetReadOnlyMode(true);
                Form2_Load(sender, e); 
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
                    if (row.IsNewRow || row.Cells["Назва предмету"].Value == null) continue;

                    var subName = row.Cells["Назва предмету"].Value?.ToString();
                    var subTimes = row.Cells["Кількість годин"].Value?.ToString();
                    var subControl = row.Cells["Тип атестації"].Value?.ToString();
                    var teacherFullName = row.Cells["Викладач"].Value?.ToString();

                    var teacherParts = teacherFullName.Split(' ');
                    string teachSurname = teacherParts.Length > 0 ? teacherParts[0] : "";
                    string teachName = teacherParts.Length > 1 ? teacherParts[1] : "";
                    string teachMiddlename = teacherParts.Length > 2 ? teacherParts[2] : "";

                    var teachIdQuery = $@"SELECT teach_id FROM teacher 
                                WHERE teach_surname = '{MySqlHelper.EscapeString(teachSurname)}' 
                                AND teach_name = '{MySqlHelper.EscapeString(teachName)}' 
                                AND teach_middlename = '{MySqlHelper.EscapeString(teachMiddlename)}'";
                    var teachIdDs = fn.getData(teachIdQuery);

                    if (teachIdDs.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception($"Викладач {teacherFullName} не знайдений у базі даних.");
                    }
                    var teachId = teachIdDs.Tables[0].Rows[0][0].ToString();

                    var existingSubject = fn.getData(
                        $"SELECT sub_id FROM subjectt WHERE sub_name = '{MySqlHelper.EscapeString(subName)}'");

                    if (existingSubject.Tables[0].Rows.Count > 0)
                    {
                        var subId = existingSubject.Tables[0].Rows[0][0].ToString();
                        fn.setData(
                            $@"UPDATE subjectt SET 
                      sub_times = '{MySqlHelper.EscapeString(subTimes)}',
                      sub_control = '{MySqlHelper.EscapeString(subControl)}',
                      teach_id = '{teachId}'
                      WHERE sub_id = {subId}");
                    }
                    else
                    {
                        fn.setData(
                            "INSERT INTO subjectt (sub_name, sub_times, sub_control, teach_id) " +
                            $"VALUES ('{MySqlHelper.EscapeString(subName)}', '{MySqlHelper.EscapeString(subTimes)}', " +
                            $"'{MySqlHelper.EscapeString(subControl)}', '{teachId}')");
                    }
                }

                MessageBox.Show("Дані збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetReadOnlyMode(true);
                Form2_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
