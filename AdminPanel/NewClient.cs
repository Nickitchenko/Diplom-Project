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
    public partial class NewClient : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        string surname = "", name = "", patronymic = "", sex_name = "", DR = "", telephone = "", address = "";
        int status = 0;
        int sex_id=0;
        int Client_id = 0;

        public NewClient(string admin_name)
        {
            InitializeComponent();
            toolStripMenuItem1.Text = admin_name;
            status = 0;
        }

        public NewClient(string admin_name, int client_id)
        {
            InitializeComponent();
            Client_id = client_id;
            toolStripMenuItem1.Text = admin_name;
            status = 1;
            string find = "SELECT Client.Client_Id, Client.Client_Surname, Client.Client_Name, Client.Client_Patronymic, Client.Client_Telephone, Client.Client_Address, Client.Client_Birthday, Sex.Sex_Id, Sex.Sex_Name FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id WHERE Client_Id = " + client_id;
            OleDbCommand Command1 = con.CreateCommand();
            Command1.CommandText = find;
            OleDbDataAdapter odata1 = new OleDbDataAdapter();
            odata1.SelectCommand = Command1;
            DataSet data1 = new DataSet();
            string datatablename = "Client";
            odata1.Fill(data1, datatablename);

            DataTable datatable = data1.Tables[datatablename];

            foreach (DataRow dr in datatable.Rows)
            {
                surname = (string)dr["Client_Surname"];
                name = (string)dr["Client_Name"];
                patronymic = (string)dr["Client_Patronymic"];
                sex_name = (string)dr["Sex_Name"];
                DR = (string)dr["Client_Birthday"];
                telephone = (string)dr["Client_Telephone"];
                address = (string)dr["Client_Address"];
                sex_id= (int)dr["Sex_Id"];
            }
            textBox1.Text = surname;
            textBox2.Text = name;
            textBox3.Text = patronymic;
            dateTimePicker1.Text = DR;
            maskedTextBox1.Text = telephone;
            comboBox2.Visible = false;
            textBox5.Text = address;
        }

        private void NewClient_Load(object sender, EventArgs e)
        {
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Sex_Id, Sex_Name from Sex", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Sex");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Sex";
                comboBox1.DataSource = binding;
                comboBox1.DisplayMember = "Sex_Name";
                comboBox1.ValueMember = "Sex_Id";
            }
            if (status == 0)
            {
                comboBox2.SelectedIndex = 1;
            }
            else
            {
                comboBox2.SelectedIndex = 1;
                comboBox1.SelectedIndex = sex_id;
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Прізвище' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox2.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Ім'я' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox3.Text == String.Empty)
            {
                MessageBox.Show("Поле 'По-Батькові' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else
            {
                DateTime datemin = new DateTime(2004, 1, 1);
                if (dateTimePicker1.Value > DateTime.Now)
                {
                    MessageBox.Show("Дата народження не може бути майбутньою");
                }
                else
                {
                    if (status == 0)
                    {
                        using (OleDbConnection con = new OleDbConnection(PathDb))
                        {
                            //вставка в таблицу заказы информации про заказ
                            con.Open();
                            string InsertOledbCommand1 = "insert into Client(Client_Surname, Client_Name, Client_Patronymic, Client_Telephone, Client_Address, Client_Birthday, Client_Status, Sex_Id) values(@Client_Surname, @Client_Name, @Client_Patronymic, @Client_Telephone, @Client_Address, @Client_Birthday, @Client_Status, @Sex_Id)";
                            using (OleDbCommand oledbInsert1 = new OleDbCommand(InsertOledbCommand1, con))
                            {
                                oledbInsert1.Parameters.AddWithValue("@Client_Surname", textBox1.Text);
                                oledbInsert1.Parameters.AddWithValue("@Client_Name", textBox2.Text);
                                oledbInsert1.Parameters.AddWithValue("@Client_Patronymic", textBox3.Text);
                                oledbInsert1.Parameters.AddWithValue("@Client_Telephone", maskedTextBox1.Text);
                                oledbInsert1.Parameters.AddWithValue("@Client_Address", comboBox2.SelectedItem + " " + textBox5.Text);
                                oledbInsert1.Parameters.AddWithValue("@Client_Birthday", dateTimePicker1.Text);
                                oledbInsert1.Parameters.AddWithValue("@Client_Status", 1);
                                oledbInsert1.Parameters.AddWithValue("@Sex_Id", comboBox1.SelectedValue);
                                oledbInsert1.ExecuteNonQuery();
                            }
                        }
                        this.Close();
                    }
                    else if (status == 1)
                    {
                        string UpdateCommand = "Update Client Set Client_Surname='" + textBox1.Text + "', Client_Name='" + textBox2.Text + "', Client_Patronymic='" + textBox3.Text + "', Client_Telephone='" + maskedTextBox1.Text + "', Client_Address='" + textBox5.Text + "', Client_Birthday='" + dateTimePicker1.Text + "', Client_Status='" + 1 + "', Sex_Id=" + comboBox1.SelectedValue + " Where Client_Id=" + Client_id;
                        updatecomand(UpdateCommand);
                        MessageBox.Show("Зміни набули чинності!");
                        this.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
