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

namespace BAM
{
    public partial class Registration : Form
    {
        private string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData;

        public Registration()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            con.Open();

            using (DataSet dataSet = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select * from Admin", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataSet, "Admin");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataSet;
                binding.DataMember = "Admin";
                dataGridView1.DataSource = binding;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int status2 = 0;
            string login = "";
            string selectString = "Select Admin_login, Admin_password, Status_Id from Admin";
            OleDbCommand Command1 = con.CreateCommand();
            Command1.CommandText = selectString;
            OleDbDataAdapter odata1 = new OleDbDataAdapter();
            odata1.SelectCommand = Command1;
            DataSet data1 = new DataSet();
            string datatablename = "Users";
            odata1.Fill(data1, datatablename);

            DataTable datatable = data1.Tables[datatablename];

            foreach (DataRow dr in datatable.Rows)
            {
                if((string)dr["Admin_password"]==textBox2.Text)
                {
                    login = (string)dr["Admin_login"];
                    status2 = (int)dr["Status_Id"];
                    break;
                }
            }
            if (status2 == 0)
            {
                MessageBox.Show("Логін або пароль не вірний!" +
                    " Перевірте правильність вводу");
            }
            else
            {
                if (status2 == 1)
                {
                    Main_Admin a = new Main_Admin(login,"Адміністратор"); a.Show(); this.Visible = false;
                }
                else if (status2 == 2)
                {
                    AddOrder p = new AddOrder(login,"Касир"); p.Show(); this.Visible = false;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HelloForm hf = new HelloForm();
            hf.Show();
            this.Close();
        }
        void EditTextbox(TextBox t, string number)
        {
            t.Text = t.Text + number;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "0");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "3");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "2");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "1");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "9");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "8");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox2, "7");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            AdminExam ae = new AdminExam();
            ae.ShowDialog();
            this.Visible = true;
        }
    }
}
