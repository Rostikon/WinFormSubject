using System;
using System.Data;
using System.Windows.Forms;
using func;

namespace WindowsFormsApp1
{
    public partial class TeachersDeleate : Form
    {
        String query;
        DataSet ds;
        function fn = new function();
        String n, s, m, c, id;

        public TeachersDeleate()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            query = "select teach_id, teach_name, teach_surname, teach_middlename from teacher";
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
                MessageBox.Show("Будь ласка, виберіть викладача для видалення", "Попередження",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = listBox1.SelectedIndex;
            string teacherId = listBox2.Items[selectedIndex].ToString();

            try
            {
                query = "delete from teacher where teach_id='" + teacherId + "'";
                fn.setData(query);

                listBox1.Items.RemoveAt(selectedIndex);
                listBox2.Items.RemoveAt(selectedIndex);

                MessageBox.Show("Викладача успішно видалено!", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}