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
    public partial class CopyCheck : Form
    {
        OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;

        public CopyCheck(SaveFileDialog saveFileDialog)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==string.Empty)
            {
                MessageBox.Show("Код чека не введений");
            }
            else
            {
                List<string> tovar_name = new List<string>();
                List<int> tovar_code = new List<int>();
                List<int> tovar_price = new List<int>();
                List<int> tovar_count = new List<int>();
                int order_id = Convert.ToInt32(textBox1.Text);
                string selectString = "SELECT Orders.Order_Id, Orders.Order_Date, Tovar.Tovar_Id, Tovar.Tovar_Name, OrderItem.OrderItem_Count, Tovar.Tovar_SellingPrice FROM Tovar INNER JOIN(Orders INNER JOIN OrderItem ON Orders.[Order_Id] = OrderItem.[Order_Id]) ON Tovar.[Tovar_Id] = OrderItem.[Tovar_Id] WHERE Orders.Order_Id=" + order_id;
                OleDbCommand Command1 = con.CreateCommand();
                Command1.CommandText = selectString;
                OleDbDataAdapter odata1 = new OleDbDataAdapter();
                odata1.SelectCommand = Command1;
                DataSet data1 = new DataSet();
                string datatablename = "Tovar";
                odata1.Fill(data1, datatablename);
                DataTable datatable = data1.Tables[datatablename];
                foreach (DataRow dr in datatable.Rows)
                {
                    tovar_name.Add(Convert.ToString(dr["Tovar_Name"]));
                    tovar_code.Add(Convert.ToInt32(dr["Tovar_Id"]));
                    tovar_price.Add(Convert.ToInt32(dr["Tovar_SellingPrice"]));
                    tovar_count.Add(Convert.ToInt32(dr["OrderItem_Count"]));
                }
                string[] tovar_name2 = tovar_name.ToArray();
                int[] tovar_code2 = tovar_code.ToArray();
                int[] tovar_price2 = tovar_price.ToArray();
                int[] tovar_count2 = tovar_count.ToArray();
                int[] tovar_allPrice = new int[tovar_price2.Length];
                for(int i=0;i<tovar_price2.Length;i++)
                {
                    tovar_allPrice[i] = tovar_count2[i] * tovar_price2[i];
                }
                Pay p = new Pay();
                this.Visible = false;
                p.func_for_check(tovar_name2, tovar_code2, tovar_price2, tovar_allPrice, tovar_count2, order_id, saveFileDialog1);
                this.Visible = false;
            }
        }
    }
}
