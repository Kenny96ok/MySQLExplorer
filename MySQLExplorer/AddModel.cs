using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQLExplorer
{
    public partial class AddModel : Form
    {
        private SqlConnection connection;
        private Dictionary<string, int> marksId;
        public AddModel(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            marksId = new Dictionary<string, int>();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBoxModel.Text.Length == 0)
                return;
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO [Models] (mark_id, name) VALUES ({marksId[comboBoxMarks.Text]} ,N'{textBoxModel.Text}')", connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel.Text = "Успешно добавлено!";
                textBoxModel.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Ошибка!";
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddModel_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT id, name FROM Marks", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                foreach (IDataRecord record in sqlDataReader)
                {
                    marksId.Add((string)record[1], (int)record[0]);
                    comboBoxMarks.Items.Add(record[1]);
                }

                sqlDataReader.Close();
                comboBoxMarks.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
