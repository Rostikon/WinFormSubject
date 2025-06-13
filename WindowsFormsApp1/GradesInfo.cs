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
using globalValue;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Frm : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        String id, n, s, m, c;
        private bool isEditMode = false;

        public Frm()
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                query = "SELECT subjectt.sub_name as 'Назва предмету', " +
                       "Acadperform_mark as 'Оцінка', " +
                       "DATE(Acadperform_datemark) as 'Дата оцінки', " +
                       "teacher.teach_surname as 'Вчитель' " +
                       "FROM subjectt, academic_performance, student_form, teacher " +
                       "WHERE student_form.stud_id = academic_performance.stud_id " +
                       "AND academic_performance.sub_id = subjectt.sub_id " +
                       "AND subjectt.teach_id = teacher.teach_id " +
                       $"AND stud_code = '{listBox1.Items[comboBox1.SelectedIndex]}'";

                ds = fn.getData(query);
                dataGridView1.DataSource = ds.Tables[0];

                if (dataGridView1.Columns["Дата оцінки"] != null)
                {
                    dataGridView1.Columns["Дата оцінки"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                Class2.Value = comboBox1.SelectedItem.ToString();
                Class2.Value1 = listBox1.Items[comboBox1.SelectedIndex].ToString();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form10 frm = new Form10();
            frm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            query = "select stud_code,stud_name,stud_surname,stud_middlename from student_form";
            ds = fn.getData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                id = ds.Tables[0].Rows[i][0].ToString();
                n = ds.Tables[0].Rows[i][1].ToString();
                s = ds.Tables[0].Rows[i][2].ToString();
                m = ds.Tables[0].Rows[i][3].ToString();
                c = s + " " + n + " " + m;
                comboBox1.Items.Add(c);
                listBox1.Items.Add(id);
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            fn.Clear(dataGridView1);
            comboBox1.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && row.Cells["Назва предмету"].Value != null)
                        {
                            var subName = row.Cells["Назва предмету"].Value?.ToString();
                            var mark = row.Cells["Оцінка"].Value?.ToString();
                            var dateValue = row.Cells["Дата оцінки"].Value;

                            string date = "";
                            if (dateValue != null && DateTime.TryParse(dateValue.ToString(), out DateTime parsedDate))
                            {
                                date = parsedDate.ToString("yyyy-MM-dd");
                            }

                            var subId = fn.getData(
                                $"SELECT sub_id FROM subjectt WHERE sub_name = '{MySqlHelper.EscapeString(subName)}'")
                                .Tables[0].Rows[0][0].ToString();

                            var acadIdQuery = $"SELECT Acadperform_id FROM academic_performance WHERE stud_id = " +
                                              $"(SELECT stud_id FROM student_form WHERE stud_code = '{Class2.Value1}') " +
                                              $"AND sub_id = '{subId}' AND Acadperform_datemark = '{date}'";
                            var acadIdDs = fn.getData(acadIdQuery);

                            if (acadIdDs.Tables[0].Rows.Count > 0)
                            {
 
                                var acadId = acadIdDs.Tables[0].Rows[0][0].ToString();
                                fn.setData(
                                    $"UPDATE academic_performance SET Acadperform_mark = '{MySqlHelper.EscapeString(mark)}' " +
                                    $"WHERE Acadperform_id = '{acadId}'");
                            }
                            else
                            {
                                fn.setData(
                                    "INSERT INTO academic_performance (stud_id, sub_id, Acadperform_mark, Acadperform_datemark) " +
                                    $"VALUES ((SELECT stud_id FROM student_form WHERE stud_code = '{Class2.Value1}'), " +
                                    $"{subId}, '{MySqlHelper.EscapeString(mark)}', '{date}')");
                            }
                        }
                    }

                    MessageBox.Show("Дані збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetReadOnlyMode(true);
                    comboBox1_SelectedIndexChanged(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                SetReadOnlyMode(true);
                comboBox1_SelectedIndexChanged(sender, e);
            }
            else
            {
                SetReadOnlyMode(false);
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {

        }

        private void close_MouseLeave(object sender, EventArgs e)
        {

        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            query = "SELECT stud_code, stud_name, stud_surname, stud_middlename FROM student_form";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                id = ds.Tables[0].Rows[i][0].ToString();
                n = ds.Tables[0].Rows[i][1].ToString();
                s = ds.Tables[0].Rows[i][2].ToString();
                m = ds.Tables[0].Rows[i][3].ToString();
                c = $"{s} {n} {m}";
                comboBox1.Items.Add(c);
                listBox1.Items.Add(id);
            }
        }
    }
}