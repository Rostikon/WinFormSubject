using System;
using System.Data;
using System.Windows.Forms;
using func;

namespace WindowsFormsApp1
{
    public partial class SubjectsDeleate : Form
    {
        function fn = new function();
        String query;
        DataSet ds;

        public SubjectsDeleate()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            RefreshSubjectList();
        }

        private void RefreshSubjectList()
        {
            listBox1.Items.Clear();
            query = "SELECT sub_name FROM subjectt";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Будь ласка, оберіть предмет для видалення", "Попередження",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string subjectName = listBox1.SelectedItem.ToString();
                query = "DELETE FROM subjectt WHERE sub_name='" + subjectName + "'";
                fn.setData(query, "Предмет успішно видалено!");

                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                MessageBox.Show("Предмет успішно видалено!", "Успіх",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні предмету: {ex.Message}", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}