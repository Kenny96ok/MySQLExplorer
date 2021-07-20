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
using System.Configuration;

namespace MySQLExplorer
{
    public partial class MainWindow : Form
    {
        //string connectString = ;
        private SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDataBase"].ConnectionString);
            try
            {
                textBox1.Text += "Openning Connection ...";

                connection.Open();

                textBox1.Text += "\nConnection successful!";
            }
            catch (Exception ex)
            {
                textBox1.Text += ("Error: " + ex.Message);
            }

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
        }

        private void AddMarkItem_Click(object sender, EventArgs e)
        {
            
            AddMark addMarkWindow = new AddMark(connection);
            addMarkWindow.ShowDialog();
            
        }
    }
}
