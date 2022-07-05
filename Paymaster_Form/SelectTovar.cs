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

namespace BAM.Paymaster_Form
{
    public partial class SelectTovar : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbDataAdapter oledbData;
        public int answer=0;
        public string TovarInfo = "";
        public string TovarPhoto = "";
        public string tov_name="",tov_color="", tov_type="", units="", company=""; public int tov_count, tovar_price, tov_id, check;

        private void SelectTovar_Load(object sender, EventArgs e)
        {

        }

        OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public SelectTovar(string code)
        {
            InitializeComponent();
            FindTovar(code);
        }

        private void FindTovar(string a)
        {
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Id, Tovar.Tovar_Name, Tovar.Tovar_Color, Type.Type_Name, Tovar.Tovar_SellingPrice, Sclad.Sclad_Count, Provider.Provider_CompanyName, Units.Units_Name FROM(Units INNER JOIN(Type INNER JOIN Tovar ON Type.[Type_Id] = Tovar.[Type_Id]) ON Units.[Units_Id] = Tovar.[Units_Id]) INNER JOIN(Provider INNER JOIN Sclad ON Provider.[Provider_Id] = Sclad.[Provider_Id]) ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id]" +
                    " WHERE (Tovar.Tovar_Id like '%" + a + "%' or Tovar.Tovar_Name like '%" + a +
                    "%' or Type.Type_Name like '%" + a + "%' or Tovar.Tovar_SellingPrice like '%" +
                    a + "%' or Sclad.Sclad_Count like '%" + a + "%' or Tovar.Tovar_Color like '%" + a + "%' or Provider.Provider_CompanyName like '%" + a + "%' or Units.Units_Name like '%" + a + "%') AND Tovar.Tovar_Status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Tovar");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Tovar";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible=false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int TovCount = 0;
            int TovId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string findcount = "SELECT Sclad.Sclad_Id, Sclad.Tovar_Id, Sclad.Sclad_Count, Tovar.Tovar_SellingPrice, Tovar.Tovar_Description, Tovar.Tovar_Photo_URL FROM Tovar INNER JOIN Sclad ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id] where Tovar.Tovar_Id =" + TovId;
            OleDbCommand Command1 = con.CreateCommand();
            Command1.CommandText = findcount;
            OleDbDataAdapter odata1 = new OleDbDataAdapter();
            odata1.SelectCommand = Command1;
            DataSet data1 = new DataSet();
            string datatablename = "Sclad";
            odata1.Fill(data1, datatablename);

            DataTable datatable = data1.Tables[datatablename];

            foreach (DataRow dr in datatable.Rows)
            {
                TovCount = (int)dr["Sclad_Count"];
                TovarInfo = (string)dr["Tovar_Description"];
                try
                {
                    TovarPhoto = (string)dr["Tovar_Photo_URL"];
                }
                catch{ }
            }
            if (TovCount <= 0)
            {
                MessageBox.Show("На складі немає стільки товарів!!! Доступна кількість: " + TovCount); this.Visible=false;
            }
            else
            {
                //oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Id, Tovar.Tovar_Name, Tovar.Tovar_Color, Type.Type_Name, Tovar.Tovar_SellingPrice, Sclad.Sclad_Count 
                check = -1;
                tov_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value); //код товару
                tov_name = (string)dataGridView1.CurrentRow.Cells[1].Value; //назва товару
                tov_color = (string)dataGridView1.CurrentRow.Cells[2].Value;
                tov_type = (string)dataGridView1.CurrentRow.Cells[3].Value;
                tovar_price = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);//ціна товару
                tov_count = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value); //кількість товарів
                company = (string)dataGridView1.CurrentRow.Cells[6].Value; //назва компанії виробника
                units = (string)dataGridView1.CurrentRow.Cells[7].Value; //одиниці вимірювання
                this.Visible = false;
            }
        }
    }
}
