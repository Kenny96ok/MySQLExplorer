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
    public partial class AddCar : Form
    {
        private SqlConnection connection;
        private Dictionary<string, int> marksId, modelsId;
        public AddCar(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            marksId = new Dictionary<string, int>();
            modelsId = new Dictionary<string, int>();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxMarks_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxModels.Items.Clear();
            modelsId.Clear();
            try
            {
                SqlCommand command = new SqlCommand($"SELECT id, name FROM Models WHERE mark_id={marksId[comboBoxMarks.Text]}", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                foreach (IDataRecord record in sqlDataReader)
                {
                    modelsId.Add((string)record[1], (int)record[0]);
                    comboBoxModels.Items.Add(record[1]);
                }

                sqlDataReader.Close();
                if (comboBoxModels.Items.Count != 0)
                {
                    comboBoxModels.SelectedIndex = 0;
                    comboBoxModels.Enabled = true;
                    addButton.Enabled = true;
                }
                else
                {
                    comboBoxModels.Enabled = false;
                    addButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBoxColor.Text.Length == 0 || textBoxNumber.Text.Length == 0 || textBoxVIN.Text.Length == 0)
                return;
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO [Cars] (model_id, number, color, VIN) VALUES ({modelsId[comboBoxModels.Text]} ,N'{textBoxNumber.Text}',N'{textBoxColor.Text}',N'{textBoxVIN.Text}')", connection);
                command.ExecuteNonQuery();
                this.Close();
                MessageBox.Show("Успешно добавлено!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCar_Load(object sender, EventArgs e)
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
