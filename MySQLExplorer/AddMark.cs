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
    public partial class AddMark : Form
    {
        private SqlConnection connection;
        public AddMark(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length == 0)
                return;
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO [Marks] (Name) VALUES (N'{textBoxName.Text}')",connection);
                command.ExecuteNonQuery();
                toolStripStatusLabel.Text = "Успешно добавлено!";
                textBoxName.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Ошибка!";
            }
        }
    }
}
