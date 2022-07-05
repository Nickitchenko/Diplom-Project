using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace BAM.Class
{
    public class StaticticClass
    {
        private string PathDb = Properties.Settings.Default.ConnectionUSer;
        private string znak = "=";
        private string checker = "";
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        string name, work;

        public StaticticClass(int id)
        {
            if(id==2)
            {
                tovar_statictic();
            }
            else if(id==3)
            {
                client_statistic();
            }
            else if(id==4)
            {
                worker_statistic();
            }
        }
        
        private DataTable create_table(string zap, string name)
        {
            string findcount2 = zap;
            OleDbCommand Command2 = con.CreateCommand();
            Command2.CommandText = findcount2;
            OleDbDataAdapter odata2 = new OleDbDataAdapter();
            odata2.SelectCommand = Command2;
            DataSet data2 = new DataSet();
            string datatablename2 = name;
            odata2.Fill(data2, datatablename2);
            DataTable datatable2 = data2.Tables[datatablename2];

            return datatable2;
        }

        public List<string> tovar_name = new List<string>();
        public List<int> tovar_id = new List<int>();
        public List<int> tovar_count = new List<int>();
        public List<int> tovar_price = new List<int>();

        public void tovar_statictic()
        {
            DataTable datatable2 = create_table("SELECT Tovar_Name, Tovar_Id, Tovar_Color FROM Tovar", "Tovar");

            foreach (DataRow dr in datatable2.Rows)
            {
                tovar_name.Add(Convert.ToString(dr["Tovar_Name"]) + " " + Convert.ToString(dr["Tovar_Color"]));
                tovar_id.Add(Convert.ToInt32(dr["Tovar_Id"]));
            }
            for(int i=0;i<tovar_id.Count;i++)
            {
                string zap = "SELECT OrderItem.Tovar_Id, OrderItem.OrderItem_Count, Tovar.Tovar_SellingPrice FROM OrderItem INNER JOIN Tovar ON OrderItem.Tovar_Id=Tovar.Tovar_Id WHERE Tovar.Tovar_Id=" + tovar_id[i];
                DataTable datatable3 = create_table(zap, "OrderItem");
                int count = 0, price=0;
                foreach (DataRow dr in datatable3.Rows)
                {                        
                    count += Convert.ToInt32(dr["OrderItem_Count"]);
                    price = Convert.ToInt32(dr["Tovar_SellingPrice"]);
                }
                tovar_count.Add(count);
                if (count == 0)
                {
                    tovar_price.Add(0);
                }
                else
                {
                    tovar_price.Add(price * count);
                }
            }
        }

        public List<string> Client = new List<string>();
        public List<int> Client_Id = new List<int>();
        public List<int> Orders_Count = new List<int>();
        public List<int> Orders_Price = new List<int>();

        private void client_statistic()
        {
            string zap1 = "Select Client_Id, Client_Surname, Client_Name, Client_Patronymic FROM Client";
            DataTable datatable2 = create_table(zap1, "Client");

            foreach (DataRow dr in datatable2.Rows)
            {
                Client.Add(Convert.ToString(dr["Client_Surname"]) + " " + Convert.ToString(dr["Client_Name"]) + " " + Convert.ToString(dr["Client_Patronymic"]));
                Client_Id.Add(Convert.ToInt32(dr["Client_Id"]));
            }
            
            for(int i=0; i<Client_Id.Count; i++)
            {
                string zap2 = "Select Client_Id, Order_Id, Order_Price FROM Orders Where Client_Id=" + Client_Id[i];
                DataTable datatable3 = create_table(zap2, "Orders");
                int count1 = 0;
                int price1 = 0;
                foreach (DataRow dr in datatable3.Rows)
                {
                    count1 += 1;
                    price1 += Convert.ToInt32(dr["Order_Price"]);
                }
                Orders_Count.Add(count1);
                Orders_Price.Add(price1);
            }
        }

        public List<string> Admin = new List<string>();
        public List<int> Admin_id = new List<int>();
        public List<int> Price = new List<int>();
        public List<int> Count = new List<int>();

        private void worker_statistic()
        {
            string zap1 = "Select Admin_id, Admin_Login, Admin_Name, Admin_Patronymic FROM Admin";
            DataTable datatable2 = create_table(zap1, "Admin");

            foreach (DataRow dr in datatable2.Rows)
            {
                Admin.Add(Convert.ToString(dr["Admin_Login"]) + " " + Convert.ToString(dr["Admin_Name"]) + " " + Convert.ToString(dr["Admin_Patronymic"]));
                Admin_id.Add(Convert.ToInt32(dr["Admin_id"]));
            }
            for(int i=0;i<Admin_id.Count;i++)
            {
                string zap2 = "Select Order_Id, Order_Price FROM Orders Where Admin_id=" + Admin_id[i];
                DataTable datatable3 = create_table(zap2, "Orders");
                int count1 = 0;
                int price1 = 0;
                foreach (DataRow dr in datatable3.Rows)
                {
                    count1 += 1;
                    price1+= Convert.ToInt32(dr["Order_Price"]);
                }
                Count.Add(count1);
                Price.Add(price1);
            }
        }
    }
}
