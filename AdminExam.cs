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
    public partial class AdminExam : Form
    {
        private string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData;

        public AdminExam()
        {
            InitializeComponent();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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
                if ((string)dr["Admin_password"] == textBox2.Text)
                {
                    login = (string)dr["Admin_login"];
                    status2 = (int)dr["Status_Id"];
                    break;
                }
            }
            if (status2 == 0)
            {
                MessageBox.Show("Введені данні не вірні!" +
                    " Перевірте правильність вводу");
            }
            else
            {
                if (status2 == 1)
                {
                    this.Visible = false;
                    AdminPanel.RegWork r = new AdminPanel.RegWork(login,"Адміністратор"); r.ShowDialog(); this.Close();
                }
                else 
                {
                    MessageBox.Show("Користувач - "+login + " ( Касир ) не має потрібних прав доступу!");
                }
            }
        }
    }
}
