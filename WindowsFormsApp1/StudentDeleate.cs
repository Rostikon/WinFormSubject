using System;
using System.Data;
using System.Windows.Forms;
using func;

namespace WindowsFormsApp1
{
    public partial class StudentDeleate : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        String n, s, m, c, id;

        public StudentDeleate()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents(); 
        }

        private void LoadStudents()
        {
            listBox1.Items.Clear(); 
            listBox2.Items.Clear();

            query = "select stud_code, stud_name, stud_surname, stud_middlename from student_form";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                id = ds.Tables[0].Rows[i][0].ToString();
                n = ds.Tables[0].Rows[i][1].ToString();
                s = ds.Tables[0].Rows[i][2].ToString();
                m = ds.Tables[0].Rows[i][3].ToString();
                c = s + " " + n + " " + m;
                listBox1.Items.Add(c);
                listBox2.Items.Add(id);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Будь ласка, виберіть студента для видалення", "Попередження",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = listBox1.SelectedIndex;
            string studentId = listBox2.Items[selectedIndex].ToString();

            try
            {
                query = "delete from student_form where stud_code='" + studentId + "'";
                fn.setData(query);

                listBox1.Items.RemoveAt(selectedIndex);
                listBox2.Items.RemoveAt(selectedIndex);

                MessageBox.Show("Студента успішно видалено!", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}