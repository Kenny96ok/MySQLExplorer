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
        private SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            

        }
        
        private void uploadMainTable()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select 'n' as N'№', mark as N'Марка', model as N'Модель', color as N'Цвет', number as N'Гос. Номер', vin as 'VIN' " +
                    "from cars inner join (select models.id, marks.name as 'mark', models.name as 'model' " +
                    "from models left join marks on mark_id = marks.id) as models on model_id = models.id",
                    connection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            mainTable.DataSource = dataSet.Tables[0];
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDataBase"].ConnectionString);
            try
            {
                toolStripStatusLabel.Text = "Openning Connection ...";

                connection.Open();

                toolStripStatusLabel.Text = "Connection successful!";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select 'n' as N'№', mark as N'Марка', model as N'Модель', color as N'Цвет', number as N'Гос. Номер', vin as 'VIN' " +
                    "from cars inner join (select models.id, marks.name as 'mark', models.name as 'model' " +
                    "from models left join marks on mark_id = marks.id) as models on model_id = models.id", 
                    connection);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                mainTable.DataSource = dataSet.Tables[0];
                mainTable.Columns[0].Width = 40;
                mainTable.Columns[5].Width = mainTable.Columns[5].Width + 45;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Ошибка!";
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

        private void addModelItem_Click(object sender, EventArgs e)
        {
            AddModel addModelWindow = new AddModel(connection);
            addModelWindow.ShowDialog();
        }

        private void mainTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex+1;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddCar addCar = new AddCar(connection);
            addCar.ShowDialog();
            uploadMainTable();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(mainTable.SelectedRows.Count==0)
            {
                return;
            }
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM [Cars] WHERE vin = N'{mainTable.SelectedRows[0].Cells[5].Value}'", connection);
                command.ExecuteNonQuery();
                uploadMainTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при удалении!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //MessageBox.Show(mainTable.SelectedRows[0].Cells[5].Value.ToString());
        }

        //private void 
    }
}
