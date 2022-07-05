using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace BAM.AdminPanel
{
    public partial class NewProvider : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        int prov_id = -1;
        public NewProvider(string info)
        {
            InitializeComponent();
            toolStripMenuItem1.Text = info;
            comboBox2.SelectedIndex = 1;
        }

        public NewProvider(string info, int id)
        {
            InitializeComponent();
            toolStripMenuItem1.Text = info;
            prov_id = id;
            string find = "SELECT Provider.Provider_CompanyName, Provider.Provider_Owner, Provider.Provider_Address, Provider.Provider_OwnerPhone FROM Provider WHERE Provider.Provider_Id = " + prov_id;
            OleDbCommand Command1 = con.CreateCommand();
            Command1.CommandText = find;
            OleDbDataAdapter odata1 = new OleDbDataAdapter();
            odata1.SelectCommand = Command1;
            DataSet data1 = new DataSet();
            string datatablename = "Provider";
            odata1.Fill(data1, datatablename);

            DataTable datatable = data1.Tables[datatablename];

            foreach (DataRow dr in datatable.Rows)
            {
                textBox1.Text = (string)dr["Provider_CompanyName"];
                textBox2.Text = (string)dr["Provider_Owner"];
                textBox3.Text = (string)dr["Provider_Address"];
                maskedTextBox1.Text = (string)dr["Provider_OwnerPhone"];
            }
            comboBox2.Visible = false;
            comboBox2.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Назва компанії' пусте. Заповніть його для збереження даних.");
                return;
            }
            else if (textBox2.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Власник' пусте. Заповніть його для збереження даних.");
                return;
            }
            else if (textBox3.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Адреса' пусте. Заповніть його для збереження даних.");
                return;
            }
            else if (maskedTextBox1.Text == "+38(  )")
            {
                MessageBox.Show("Поле '№ Телефона' пусте. Заповніть його для збереження даних.");
                return;
            }
            else
            {
                if (prov_id == -1)
                {
                    using (OleDbConnection con = new OleDbConnection(PathDb))
                    {
                        //вставка в таблицу заказы информации про заказ
                        con.Open();
                        string InsertOledbCommand1 = "insert into Provider(Provider_CompanyName, Provider_Owner, Provider_Address, Provider_OwnerPhone, Provider_Status) values(@Provider_CompanyName, @Provider_Owner, @Provider_Address, @Provider_OwnerPhone, @Provider_Status)";
                        using (OleDbCommand oledbInsert1 = new OleDbCommand(InsertOledbCommand1, con))
                        {
                            oledbInsert1.Parameters.AddWithValue("@Provider_CompanyName", textBox1.Text);
                            oledbInsert1.Parameters.AddWithValue("@Provider_Owner", textBox2.Text);
                            oledbInsert1.Parameters.AddWithValue("@Provider_Address", comboBox2.SelectedValue + textBox3.Text);
                            oledbInsert1.Parameters.AddWithValue("@Provider_OwnerPhone", maskedTextBox1.Text);
                            oledbInsert1.Parameters.AddWithValue("@Provider_Status", 1);
                            oledbInsert1.ExecuteNonQuery();
                        }
                    }
                    this.Close();
                }
                else
                {
                    string UpdateCommand = "Update Provider Set Provider_CompanyName='" + textBox1.Text + "', Provider_Owner='" + textBox2.Text + "', Provider_Address='" + textBox3.Text + "', Provider_OwnerPhone='" + maskedTextBox1.Text + "' Where Provider_Id=" + prov_id;
                    updatecomand(UpdateCommand);
                    MessageBox.Show("Зміни набули чинності!");
                    this.Close();
                }
            }
        }

        private void updatecomand(string command)
        {
            using (OleDbConnection con2 = new OleDbConnection(Properties.Settings.Default.ConnectionUSer))
            {
                con2.Open();
                using (OleDbCommand oledbupdate = new OleDbCommand(command, con2))
                {
                    oledbupdate.ExecuteNonQuery();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
