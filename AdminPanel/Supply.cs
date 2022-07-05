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
    public partial class Supply : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;

        public Supply()
        {
            InitializeComponent();
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Tovar_Id, Tovar_Name from Tovar", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Tovar");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Tovar";
                comboBox1.DataSource = binding;
                comboBox1.DisplayMember = "Tovar_Name";
                comboBox1.ValueMember = "Tovar_Id";
            }
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Provider_Id, Provider_CompanyName from Provider", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Provider");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Provider";
                comboBox2.DataSource = binding;
                comboBox2.DisplayMember = "Provider_CompanyName";
                comboBox2.ValueMember = "Provider_Id";
            }

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (numericUpDown1.Value==0)
            {
                MessageBox.Show("Поле 'Кількість' обов'язкове і воно повинно бути більше нуля"); return;
            }
            else
            {
                string find = "SELECT Tovar_Id, Provider_Id FROM Sclad WHERE Tovar_Id="+((int)comboBox1.SelectedValue);
                OleDbCommand Command1 = con.CreateCommand();
                Command1.CommandText = find;
                OleDbDataAdapter odata1 = new OleDbDataAdapter();
                odata1.SelectCommand = Command1;
                DataSet data1 = new DataSet();
                string datatablename = "Sclad";
                odata1.Fill(data1, datatablename);

                DataTable datatable = data1.Tables[datatablename];
                int tovar_id = 0, prov_id = 0;
                foreach (DataRow dr in datatable.Rows)
                {
                    tovar_id = (int)dr["Tovar_Id"];
                    prov_id = (int)dr["Provider_Id"];
                }
                if(prov_id==((int)comboBox2.SelectedValue+1))
                {
                    string UpdateCommand = "Update Sclad Set Sclad_Count=Sclad_Count+" + numericUpDown1.Value + " Where Tovar_Id=" + comboBox1.SelectedValue + " AND Provider_Id="+comboBox2.SelectedValue;
                    updatecomand(UpdateCommand);
                    MessageBox.Show("Зміни набули чинності!");
                    this.Close();
                }
                else
                {
                    using (OleDbConnection con = new OleDbConnection(PathDb))
                    {
                        //вставка в таблицу заказы информации про заказ
                        con.Open();
                        string InsertOledbCommand1 = "insert into Sclad(Tovar_Id, Sclad_Count, Provider_Id) values(@Tovar_Id, @Sclad_Count, @Provider_Id)";
                        using (OleDbCommand oledbInsert1 = new OleDbCommand(InsertOledbCommand1, con))
                        {
                            oledbInsert1.Parameters.AddWithValue("@Tovar_Id", comboBox1.SelectedValue);
                            oledbInsert1.Parameters.AddWithValue("@Sclad_Count", numericUpDown1.Value);
                            oledbInsert1.Parameters.AddWithValue("@Provider_Id", comboBox2.SelectedValue);
                            oledbInsert1.ExecuteNonQuery();
                        }
                    }
                    this.Close();
                }
            }
        }
    }
}
