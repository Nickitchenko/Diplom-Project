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
    public partial class Main_Admin : Form
    {
        private string PathDb = Properties.Settings.Default.ConnectionUSer;
        private string znak = "=";
        private string checker = "";
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData,oledbData2;
        string name, work;

        int id_statistic = 0;

        public Main_Admin(string name1,string work1)
        {
            InitializeComponent();
            name = name1; work = work1;
            textBox4.Text = work + ": " + name;
            button1.BackColor = Color.White;
        }

        private void func_datagridFind(string oldb, string table, DataGridView dataGridView)
        {
            using (DataSet1 dataset = new DataSet1())
            {
                oledbData = new OleDbDataAdapter(oldb, con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset, table);
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset;
                binding.DataMember = table;
                dataGridView.DataSource = binding;
            }
        }

        private void Main_Admin_Load(object sender, EventArgs e)
        {
            button4.BackColor = Color.Blue;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;

            //combobutton
            
            using (DataSet dataSet = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Orders.Order_Id as Код, Client.Client_Surname + ' ' + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id=Client.Client_Id WHERE Orders.Orders_Status=1", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataSet, "Orders");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataSet;
                binding.DataMember = "Orders";
                dataGridView1.DataSource = binding;
            }
            int ord_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            using (DataSet dataset = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Name as Назва, OrderItem.OrderItem_Count as Кількість, Tovar.Tovar_SellingPrice as Ціна FROM Tovar INNER JOIN OrderItem ON Tovar.[Tovar_Id] = OrderItem.[Tovar_Id] where Order_Id = " + ord_id, con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset, "OrderItem");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset;
                binding.DataMember = "OrderItem";
                dataGridView2.DataSource = binding;
            }
            using (DataSet data = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Id as Код, Tovar.Tovar_Name as Назва, Tovar.Tovar_Color as Колір, Type.Type_Name as Тип, Tovar.Tovar_SellingPrice as Ціна, Sclad.Sclad_Count as 'Кількість на складі', Provider.Provider_CompanyName as Виробник, Units.Units_Name as 'Одиниці вимірювання' FROM(Units INNER JOIN(Type INNER JOIN Tovar ON Type.[Type_Id] = Tovar.[Type_Id]) ON Units.[Units_Id] = Tovar.[Units_Id]) INNER JOIN(Provider INNER JOIN Sclad ON Provider.[Provider_Id] = Sclad.[Provider_Id]) ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id]", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(data, "Tovar");
                BindingSource binding = new BindingSource();
                binding.DataSource = data;
                binding.DataMember = "Tovar";
                dataGridView3.DataSource = binding;
            }

            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Client.Client_Id as 'Код', (Client.Client_Surname + ' ' + Client.Client_Name + ' ' + Client.Client_Patronymic) as 'ПІБ Клієнта', Client.Client_Telephone as №Телефону, Client.Client_Address as Адреса, Client_Birthday as 'Дата народження', Sex.Sex_Name as 'Стать' FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id WHERE Client.Client_Status=1", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Client");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Client";
                dataGridView4.DataSource = binding;
            }

            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Admin.Admin_id as 'Код', (Admin.Admin_login + ' ' + Admin.Admin_Name + ' ' + Admin.Admin_Patronymic) as 'ПІБ Користувача', Sex.Sex_Name as 'Стать', Admin.Admin_DR as 'Дата народження', Admin.Admin_Password as Пароль, Status.Status_Name as Посада FROM Sex INNER JOIN (Status INNER JOIN Admin ON Status.[Status_Id] = Admin.[Status_Id]) ON Sex.[Sex_Id] = Admin.[Sex_Id] WHERE Admin.Admin_status=1", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Admin");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Admin";
                dataGridView5.DataSource = binding;
            }

            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Provider.Provider_Id as 'Код', Provider.Provider_CompanyName as 'Назва компанії', Provider.Provider_Owner as 'Власник', Provider.Provider_Address as 'Адреса', Provider.Provider_OwnerPhone as '№ Телефона' FROM Provider WHERE Provider.Provider_Status=1", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Provider");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Provider";
                dataGridView6.DataSource = binding;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.BackColor = Color.Blue;
            button4.BackColor = Color.White;
            button3.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
            znak = "<";
            string b = textBox2.Text;
            if (button1.BackColor == Color.Blue)
            {
                find_duo();
            }
            else
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id = Client.Client_Id where Orders.Order_Price" + znak + b, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3.BackColor = Color.Blue;
            button2.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
            znak = "<=";
            string b = textBox2.Text;
            if (button1.BackColor == Color.Blue)
            {
                find_duo();
            }
            else
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id = Client.Client_Id where Orders.Order_Price" + znak + b, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            button4.BackColor = Color.Blue;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
            znak = "=";
            string b = textBox2.Text;
            if (button1.BackColor == Color.Blue)
            {
                find_duo();
            }
            else
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id = Client.Client_Id where Orders.Order_Price" + znak + b, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            button5.BackColor = Color.Blue;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button6.BackColor = Color.White;
            znak = ">=";
            string b = textBox2.Text;
            if (button1.BackColor == Color.Blue)
            {
                find_duo();
            }
            else
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id = Client.Client_Id where Orders.Order_Price" + znak + b, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            button6.BackColor = Color.Blue;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button5.BackColor = Color.White;
            button4.BackColor = Color.White;
            znak = ">";
            string b = textBox2.Text;
            if (button1.BackColor == Color.Blue)
            {
                find_duo();
            }
            else
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id = Client.Client_Id where Orders.Order_Price" + znak + b, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e) //комбо поиск
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Поля 'Ціна' та 'Пошук' повинні бути заповнені.");
            }
            else
            {
                if (button1.BackColor == Color.White)
                {
                    button1.BackColor = Color.Blue;
                    find_duo();
                }
                else if (button1.BackColor == Color.Blue)
                {
                    button1.BackColor = Color.White;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    if(checker=="price")
                    {
                        find_price();
                    }
                    else if(checker=="find")
                    {
                        find_find();
                    }
                }
            }
        }

        private void find_duo()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            string a = textBox1.Text;
            string b = textBox2.Text;
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id = Client.Client_Id where (Orders.Order_Id like '%" + a + "%' or Client.Client_Surname + Client.Client_Name like '%" + a + "%' or Orders.Order_Date like '%" + a + "%' or Orders.Order_Price like '%" + a + "%') and Orders.Order_Price" + znak + b, con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Orders");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Orders";
                dataGridView1.DataSource = binding;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ord_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (DataSet ds = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Name as Назва, OrderItem.OrderItem_Count as Кількість, Tovar.Tovar_SellingPrice as Ціна FROM Tovar INNER JOIN OrderItem ON Tovar.[Tovar_Id] = OrderItem.[Tovar_Id] where Order_Id = " + ord_id, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(ds, "OrderItem");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = ds;
                    binding.DataMember = "OrderItem";
                    dataGridView2.DataSource = binding;
                }
            }
            catch { }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Активне вікно відповідає за відображення історії замовлення, деталей замовлень та створення нового замовлення");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddOrder ad = new AddOrder(name,work);
            ad.Text = "Нове замовлення 2";
            ad.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddOrder ad = new AddOrder(name, work);
            ad.StartPosition = FormStartPosition.CenterScreen;
            ad.Text = "Нове замовлення 3";
            ad.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddOrder ad = new AddOrder(name, work);
            ad.StartPosition = FormStartPosition.CenterScreen;
            ad.Text = "Нове замовлення 4";
            ad.Show();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            find_find();
            checker = "find";
        }

        private void find_find()
        {
            string a = textBox1.Text;
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + ' ' + Client.Client_Name as 'ПІБ Клієнта'," +
" Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення'" +
" from Orders inner join Client on Orders.Client_Id = Client.Client_Id where" +
" Orders.Order_Id like '%" + a + "%' or Client.Client_Surname + ' ' + Client.Client_Name like '%" + a + "%' or " +
" Orders.Order_Date like '%" + a + "%' or Orders.Order_Price like '%" + a + "%'", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Orders");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Orders";
                dataGridView1.DataSource = binding;
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            find_price();
            checker = "price";
        }

        private void find_price()
        {
            string a = textBox2.Text;
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Orders.Order_Id as '№ Замовлення', Client.Client_Surname + ' ' + Client.Client_Name as 'ПІБ Клієнта'," +
" Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення'" +
" from Orders inner join Client on Orders.Client_Id = Client.Client_Id where Orders.Order_Price like '%" + a + "%'", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Orders");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Orders";
                dataGridView1.DataSource = binding;
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            string TovarInfo="";
            string TovarPhoto = "";
            int TovId = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
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
                TovarInfo = (string)dr["Tovar_Description"];
                try
                {
                    TovarPhoto = (string)dr["Tovar_Photo_URL"];
                }
                catch { }
            }

            string tov_name = "", tov_color = "", tov_type = "", units = "", company = ""; 
            int tov_count, tovar_price, tov_id, check;
            tov_id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value); //код товару
            tov_name = (string)dataGridView3.CurrentRow.Cells[1].Value; //назва товару
            tov_color = (string)dataGridView3.CurrentRow.Cells[2].Value;
            tov_type = (string)dataGridView3.CurrentRow.Cells[3].Value;
            tovar_price = Convert.ToInt32(dataGridView3.CurrentRow.Cells[4].Value);//ціна товару
            tov_count = Convert.ToInt32(dataGridView3.CurrentRow.Cells[5].Value); //кількість товарів
            company = (string)dataGridView3.CurrentRow.Cells[6].Value; //назва компанії виробника
            units = (string)dataGridView3.CurrentRow.Cells[7].Value; //одиниці вимірювання
            AdminPanel.TovarInfoAdmin ti = new AdminPanel.TovarInfoAdmin(tov_name, tov_color, tov_type, tov_count, tovar_price, tov_id, units, company, TovarInfo, TovarPhoto);
            ti.ShowDialog();
            obnova();

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string a = textBox3.Text;
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
                    dataGridView3.DataSource = binding;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int order_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            AdminPanel.EditOrder eo = new AdminPanel.EditOrder(order_id);
            eo.Show();
        }

        private void func_del_order(int order_id)
        {
            using (OleDbConnection con = new OleDbConnection(PathDb))
            {
                con.Open();
                OleDbCommand command = new OleDbCommand("DELETE * FROM OrderItem WHERE Order_Id = "+order_id, con); //TableName - имя таблицы, из которой удаляете запись
                command.ExecuteNonQuery();
                OleDbCommand command1 = new OleDbCommand("DELETE * FROM Orders WHERE Order_Id = " + order_id, con); //TableName - имя таблицы, из которой удаляете запись
                command1.ExecuteNonQuery();
            }
        }

        private void del_datagrid(DataGridView d1)
        {
            int delet = d1.SelectedCells[0].RowIndex;
            d1.Rows.RemoveAt(delet);
        }

        private void change_active_cells()
        {
            int ord_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            using (DataSet1 dataset = new DataSet1())
            {
                oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Name as Назва, OrderItem.OrderItem_Count as Кількість, Tovar.Tovar_SellingPrice as Ціна FROM Tovar INNER JOIN OrderItem ON Tovar.[Tovar_Id] = OrderItem.[Tovar_Id] where Order_Id = " + ord_id, con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset, "OrderItem");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset;
                binding.DataMember = "OrderItem";
                dataGridView2.DataSource = binding;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int tov_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            DialogResult dialogResult = MessageBox.Show("Ви точно хочете видалити виділене замовлення", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string oledbUpdateCommand = "Update Orders Set Orders_Status=0 Where Order_Id=" + tov_id;
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }

                //обновление таблици с товарами
                using (DataSet dataSet9 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as Код, Client.Client_Surname + ' ' + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id=Client.Client_Id WHERE Orders.Orders_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataSet9, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataSet9;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            int tov_id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
            DialogResult dialogResult = MessageBox.Show("Ви точно хочете видалити виділений аксесуар", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string oledbUpdateCommand = "Update Tovar Set Tovar_Status=0 Where Tovar_Id=" + tov_id;
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }
                obnova();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string a = textBox3.Text;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("Select Tovar.Tovar_Id as Код, Tovar.Tovar_Name  as Назва, Tovar.Tovar_Color  as Колір, Type.Type_Name  as Тип, Tovar.Tovar_SellingPrice  as Ціна, Sclad.Sclad_Count as 'Кількість на складі', Provider.Provider_CompanyName as Виробник, Units.Units_Name as 'Одиниці вимірювання' FROM(Units INNER JOIN(Type INNER JOIN Tovar ON Type.[Type_Id] = Tovar.[Type_Id]) ON Units.[Units_Id] = Tovar.[Units_Id]) INNER JOIN(Provider INNER JOIN Sclad ON Provider.[Provider_Id] = Sclad.[Provider_Id]) ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id]" +
                    " WHERE (Tovar.Tovar_Id like '%" + a + "%' or Tovar.Tovar_Name like '%" + a +
                    "%' or Type.Type_Name like '%" + a + "%' or Tovar.Tovar_SellingPrice like '%" +
                    a + "%' or Sclad.Sclad_Count like '%" + a + "%' or Tovar.Tovar_Color like '%" + a + "%' or Provider.Provider_CompanyName like '%" + a + "%' or Units.Units_Name like '%" + a + "%') AND Tovar.Tovar_Status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Tovar");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Tovar";
                    dataGridView3.DataSource = binding;
                }
            }
        }

        private void find_client()
        {
            string a = textBox5.Text;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Client.Client_Id as 'Код', (Client.Client_Surname + ' ' + Client.Client_Name + ' ' + Client.Client_Patronymic) as 'ПІБ Клієнта', Client.Client_Telephone as №Телефону, Client.Client_Address as Адреса, Client_Birthday as 'Дата народження', Sex.Sex_Name as 'Стать' FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id" +
                    " WHERE Client.Client_Status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Client");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Client";
                    dataGridView4.DataSource = binding;
                }
            }
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string a = textBox5.Text;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Client.Client_Id as 'Код', (Client.Client_Surname + ' ' + Client.Client_Name + ' ' + Client.Client_Patronymic) as 'ПІБ Клієнта', Client.Client_Telephone as №Телефону, Client.Client_Address as Адреса, Client_Birthday as 'Дата народження', Sex.Sex_Name as 'Стать' FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id" +
                    " WHERE (Client.Client_Id like '%" + a + "%' or Client.Client_Surname like '%" + a + "%' or Client.Client_Name like '%" + a +
                    "%' or Client.Client_Patronymic like '%" + a + "%' or Client.Client_Telephone like '%" +
                    a + "%' or Client.Client_Address like '%" + a + "%' or Sex.Sex_Name like '%" + a + "%' or Client.Client_Birthday like '%" + a + "%') AND Client.Client_Status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Client");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Client";
                    dataGridView4.DataSource = binding;
                }
            }
        }

        private void but15()
        {
            textBox5.Text = string.Empty;
            find_client();
        }
        
        private void button15_Click(object sender, EventArgs e)
        {
            but15();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int Client_Id = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value);
            DialogResult dialogResult = MessageBox.Show("Ви точно хочете видалити виділений аксесуар", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string oledbUpdateCommand = "Update Client Set Client_Status=0 Where Client_Id=" + Client_Id;
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }

                obnova();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            AdminPanel.NewClient client = new AdminPanel.NewClient(textBox4.Text);
            client.ShowDialog();
            obnova();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int client_id = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value);
            AdminPanel.NewClient nc = new AdminPanel.NewClient(textBox4.Text,client_id);
            nc.ShowDialog();
            obnova();
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string a = textBox6.Text;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Admin.Admin_id as 'Код', (Admin.Admin_login + ' ' + Admin.Admin_Name + ' ' + Admin.Admin_Patronymic) as 'ПІБ Користувача', Sex.Sex_Name as 'Стать', Admin.Admin_DR as 'Дата народження', Admin.Admin_Password as Пароль, Status.Status_Name as Посада FROM Sex INNER JOIN (Status INNER JOIN Admin ON Status.[Status_Id] = Admin.[Status_Id]) ON Sex.[Sex_Id] = Admin.[Sex_Id]" +
                    " WHERE (Admin.Admin_id like '%" + a + "%' or Admin.Admin_login like '%" + a + "%' or Admin.Admin_Name like '%" + a +
                    "%' or Admin.Admin_Patronymic like '%" + a + "%' or Admin.Admin_DR like '%" +
                    a + "%' or Admin.Admin_Password like '%" + a + "%' or Sex.Sex_Name like '%" + a + "%') AND Admin.Admin_status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Admin");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Admin";
                    dataGridView5.DataSource = binding;
                }
            }
        }

        private void but22()
        {
            textBox6.Text = string.Empty;
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Admin.Admin_id as 'Код', (Admin.Admin_login + ' ' + Admin.Admin_Name + ' ' + Admin.Admin_Patronymic) as 'ПІБ Користувача', Sex.Sex_Name as 'Стать', Admin.Admin_DR as 'Дата народження', Admin.Admin_Password as Пароль, Status.Status_Name as Посада FROM Sex INNER JOIN (Status INNER JOIN Admin ON Status.[Status_Id] = Admin.[Status_Id]) ON Sex.[Sex_Id] = Admin.[Sex_Id] WHERE Admin.Admin_status=1", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Admin");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Admin";
                dataGridView5.DataSource = binding;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            but22();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            AdminPanel.RegWork rw = new AdminPanel.RegWork(textBox4.Text);
            rw.ShowDialog();
            obnova();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int Admin_Id = Convert.ToInt32(dataGridView5.CurrentRow.Cells[0].Value);
            AdminPanel.RegWork rw = new AdminPanel.RegWork(textBox4.Text, Admin_Id);
            rw.ShowDialog();
            obnova();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int Admin_Id = Convert.ToInt32(dataGridView5.CurrentRow.Cells[0].Value);
            DialogResult dialogResult = MessageBox.Show("Ви точно хочете видалити виділеного користувача", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string oledbUpdateCommand = "Update Admin Set Admin_status=0 Where Admin_id=" + Admin_Id;
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }

                //обновление таблици с товарами
                obnova();
            }
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            string a = textBox7.Text;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Provider.Provider_Id as 'Код', Provider.Provider_CompanyName as 'Назва компанії', Provider.Provider_Owner as 'Власник', Provider.Provider_Address as 'Адреса', Provider.Provider_OwnerPhone as '№ Телефона' FROM Provider" +
                    " WHERE (Provider.Provider_Id like '%" + a + "%' or Provider.Provider_CompanyName like '%" + a + "%' or Provider.Provider_Owner like '%" + a +
                    "%' or Provider.Provider_Address like '%" + a + "%' or Provider.Provider_OwnerPhone like '%" +
                    a + "%') AND Provider.Provider_Status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Provider");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Provider";
                    dataGridView6.DataSource = binding;
                }
            }
        }

        private void but26()
        {
            textBox7.Text = string.Empty;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Provider.Provider_Id as 'Код', Provider.Provider_CompanyName as 'Назва компанії', Provider.Provider_Owner as 'Власник', Provider.Provider_Address as 'Адреса', Provider.Provider_OwnerPhone as '№ Телефона' FROM Provider" +
                    " WHERE Provider.Provider_Status=1", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Provider");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Provider";
                    dataGridView6.DataSource = binding;
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            but26();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int Provider_Id = Convert.ToInt32(dataGridView6.CurrentRow.Cells[0].Value);
            DialogResult dialogResult = MessageBox.Show("Ви точно хочете видалити виділеного постачальника", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string oledbUpdateCommand = "Update Provider Set Provider_Status=0 Where Provider_Id=" + Provider_Id;
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }

                //обновление таблици
                obnova();
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            AdminPanel.NewProvider np = new AdminPanel.NewProvider(textBox4.Text);
            np.ShowDialog();
            obnova();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            AdminPanel.NewProvider np = new AdminPanel.NewProvider(textBox4.Text, Convert.ToInt32(dataGridView6.CurrentRow.Cells[0].Value));
            np.ShowDialog();
            obnova();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                using (DataSet dataSet = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as Код, Client.Client_Surname + ' ' + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id=Client.Client_Id WHERE Orders.Orders_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataSet, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataSet;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
                int ord_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (DataSet dataset = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Name as Назва, OrderItem.OrderItem_Count as Кількість, Tovar.Tovar_SellingPrice as Ціна FROM Tovar INNER JOIN OrderItem ON Tovar.[Tovar_Id] = OrderItem.[Tovar_Id] where Order_Id = " + ord_id, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset, "OrderItem");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset;
                    binding.DataMember = "OrderItem";
                    dataGridView2.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                using (DataSet data = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Id as Код, Tovar.Tovar_Name  as Назва, Tovar.Tovar_Color  as Колір, Type.Type_Name  as Тип, Tovar.Tovar_SellingPrice  as Ціна, Sclad.Sclad_Count as 'Кількість на складі', Provider.Provider_CompanyName as Виробник, Units.Units_Name as 'Одиниці вимірювання' FROM(Units INNER JOIN(Type INNER JOIN Tovar ON Type.[Type_Id] = Tovar.[Type_Id]) ON Units.[Units_Id] = Tovar.[Units_Id]) INNER JOIN(Provider INNER JOIN Sclad ON Provider.[Provider_Id] = Sclad.[Provider_Id]) ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id]", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(data, "Tovar");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = data;
                    binding.DataMember = "Tovar";
                    dataGridView3.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Client.Client_Id as 'Код', (Client.Client_Surname + ' ' + Client.Client_Name + ' ' + Client.Client_Patronymic) as 'ПІБ Клієнта', Client.Client_Telephone as №Телефону, Client.Client_Address as Адреса, Client_Birthday as 'Дата народження', Sex.Sex_Name as 'Стать' FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id WHERE Client.Client_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Client");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Client";
                    dataGridView4.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Admin.Admin_id as 'Код', (Admin.Admin_login + ' ' + Admin.Admin_Name + ' ' + Admin.Admin_Patronymic) as 'ПІБ Користувача', Sex.Sex_Name as 'Стать', Admin.Admin_DR as 'Дата народження', Admin.Admin_Password as Пароль, Status.Status_Name as Посада FROM Sex INNER JOIN (Status INNER JOIN Admin ON Status.[Status_Id] = Admin.[Status_Id]) ON Sex.[Sex_Id] = Admin.[Sex_Id] WHERE Admin.Admin_status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Admin");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Admin";
                    dataGridView5.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Provider.Provider_Id as 'Код', Provider.Provider_CompanyName as 'Назва компанії', Provider.Provider_Owner as 'Власник', Provider.Provider_Address as 'Адреса', Provider.Provider_OwnerPhone as '№ Телефона' FROM Provider WHERE Provider.Provider_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Provider");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Provider";
                    dataGridView6.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                radioButton1.Checked = true;
                radiobutton_check();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            AdminPanel.NewTovar nt = new AdminPanel.NewTovar();
            nt.ShowDialog();
            obnova();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("Select Tovar.Tovar_Id as Код, Tovar.Tovar_Name  as Назва, Tovar.Tovar_Color  as Колір, Type.Type_Name  as Тип, Tovar.Tovar_SellingPrice  as Ціна, Sclad.Sclad_Count as 'Кількість на складі', Provider.Provider_CompanyName as Виробник, Units.Units_Name as 'Одиниці вимірювання' FROM(Units INNER JOIN(Type INNER JOIN Tovar ON Type.[Type_Id] = Tovar.[Type_Id]) ON Units.[Units_Id] = Tovar.[Units_Id]) INNER JOIN(Provider INNER JOIN Sclad ON Provider.[Provider_Id] = Sclad.[Provider_Id]) ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id]" +
                    " WHERE Tovar.Tovar_Status=0", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Tovar");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Tovar";
                    dataGridView3.DataSource = binding;
                }
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Client.Client_Id as 'Код', (Client.Client_Surname + ' ' + Client.Client_Name + ' ' + Client.Client_Patronymic) as 'ПІБ Клієнта', Client.Client_Telephone as №Телефону, Client.Client_Address as Адреса, Client_Birthday as 'Дата народження', Sex.Sex_Name as 'Стать' FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id" +
                    " WHERE Client.Client_Status=0", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Client");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Client";
                    dataGridView4.DataSource = binding;
                }
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            textBox6.Text = string.Empty;
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("SELECT Admin.Admin_id as 'Код', (Admin.Admin_login + ' ' + Admin.Admin_Name + ' ' + Admin.Admin_Patronymic) as 'ПІБ Користувача', Sex.Sex_Name as 'Стать', Admin.Admin_DR as 'Дата народження', Admin.Admin_Password as Пароль, Status.Status_Name as Посада FROM Sex INNER JOIN (Status INNER JOIN Admin ON Status.[Status_Id] = Admin.[Status_Id]) ON Sex.[Sex_Id] = Admin.[Sex_Id] WHERE Admin.Admin_status=0", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Admin");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Admin";
                dataGridView5.DataSource = binding;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            textBox7.Text = string.Empty;
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Provider.Provider_Id as 'Код', Provider.Provider_CompanyName as 'Назва компанії', Provider.Provider_Owner as 'Власник', Provider.Provider_Address as 'Адреса', Provider.Provider_OwnerPhone as '№ Телефона' FROM Provider" +
                    " WHERE Provider.Provider_Status=0", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Provider");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Provider";
                    dataGridView6.DataSource = binding;
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            AdminPanel.Supply sp = new AdminPanel.Supply();
            sp.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            tabControl1.SelectedTab = tabPage6;
        }

        void radiobutton_check()
        {
            if (radioButton1.Checked == true)
            {
                pictureBox1.Image = Properties.Resources.blank;
                button35.Text = "Всі товари";
                Class.StaticticClass sc = new Class.StaticticClass(2);
                List<string> tovar_name = sc.tovar_name;
                List<int> tovar_id = sc.tovar_id;
                List<int> tovar_count = sc.tovar_count;
                List<int> tovar_price = sc.tovar_price;

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Назва Товару", typeof(string)));
                dt.Columns.Add(new DataColumn("Кількість продаж", typeof(int)));
                dt.Columns.Add(new DataColumn("Загальна Ціна", typeof(int)));

                for (int i = 0; i < tovar_id.Count; i++)
                {
                    dt.Rows.Add(tovar_name[i], tovar_count[i], tovar_price[i]);
                }
                dataGridView7.DataSource = dt;
            }
            else if (radioButton2.Checked == true)
            {
                pictureBox1.Image = Properties.Resources.statistic_client;
                button35.Text = "Всі клієнти";
                Class.StaticticClass sc = new Class.StaticticClass(3);
                List<string> Client = sc.Client;
                List<int> Orders_Count = sc.Orders_Count;
                List<int> Orders_Price = sc.Orders_Price;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ПІБ Клієнта", typeof(string)));
                dt.Columns.Add(new DataColumn("Кількість Замовлень", typeof(int)));
                dt.Columns.Add(new DataColumn("Загальна Сума", typeof(int)));

                for (int i = 0; i < Client.Count; i++)
                {
                    dt.Rows.Add(Client[i], Orders_Count[i], Orders_Price[i]);
                }
                dataGridView7.DataSource = dt;
            }
            else if (radioButton3.Checked == true)
            {
                pictureBox1.Image = Properties.Resources.statistik_worker;
                button35.Text = "Всі Співробітники";
                Class.StaticticClass sc = new Class.StaticticClass(4);
                List<string> Admin = sc.Admin;
                List<int> Price = sc.Price;
                List<int> Count = sc.Count;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ПІБ Співробітника", typeof(string)));
                dt.Columns.Add(new DataColumn("Кількість Замовлень", typeof(int)));
                dt.Columns.Add(new DataColumn("Загальна Сума", typeof(int)));

                for (int i = 0; i < Admin.Count; i++)
                {
                    dt.Rows.Add(Admin[i], Count[i], Price[i]);
                }
                dataGridView7.DataSource = dt;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radiobutton_check();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radiobutton_check();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radiobutton_check();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            tabControl1.SelectedTab = tabPage6;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
            tabControl1.SelectedTab = tabPage6;
        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                dataGridView7.ClearSelection();

                if (string.IsNullOrWhiteSpace(textBox8.Text))
                    return;
                var values = textBox8.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dataGridView7.RowCount - 1; i++)
                {
                    foreach (string value in values)
                    {
                        var row = dataGridView7.Rows[i];

                        if (row.Cells["ПІБ Клієнта"].Value.ToString().Contains(value) || row.Cells["Кількість Замовлень"].Value.ToString().Contains(value) || row.Cells["Загальна Сума"].Value.ToString().Contains(value))
                        {
                            row.Selected = true;
                        }
                    }
                }
            }
            else if (radioButton1.Checked == true)
            {
                dataGridView7.ClearSelection();

                if (string.IsNullOrWhiteSpace(textBox8.Text))
                    return;
                var values = textBox8.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dataGridView7.RowCount - 1; i++)
                {
                    foreach (string value in values)
                    {
                        var row = dataGridView7.Rows[i];

                        if (row.Cells["Назва Товару"].Value.ToString().Contains(value) || row.Cells["Кількість продаж"].Value.ToString().Contains(value) || row.Cells["Загальна Ціна"].Value.ToString().Contains(value))
                        {
                            row.Selected = true;
                        }
                    }
                }
            }
            else if (radioButton3.Checked == true)
            {
                dataGridView7.ClearSelection();

                if (string.IsNullOrWhiteSpace(textBox8.Text))
                    return;
                var values = textBox8.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dataGridView7.RowCount - 1; i++)
                {
                    foreach (string value in values)
                    {
                        var row = dataGridView7.Rows[i];

                        if (row.Cells["ПІБ Співробітника"].Value.ToString().Contains(value) || row.Cells["Кількість Замовлень"].Value.ToString().Contains(value) || row.Cells["Загальна Сума"].Value.ToString().Contains(value))
                        {
                            row.Selected = true;
                        }
                    }
                }
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            textBox8.Text = String.Empty;
        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {
            AddOrder ad = new AddOrder(name,work);
            ad.Text = "Нове замовлення 1";
            ad.Show();
        }

        private void obnova()
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                using (DataSet dataSet = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("select Orders.Order_Id as Код, Client.Client_Surname + ' ' + Client.Client_Name as 'ПІБ Клієнта', Orders.Order_Date as 'Дата замовлення', Orders.Order_Price as 'Ціна замовлення' from Orders inner join Client on Orders.Client_Id=Client.Client_Id WHERE Orders.Orders_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataSet, "Orders");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataSet;
                    binding.DataMember = "Orders";
                    dataGridView1.DataSource = binding;
                }
                int ord_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (DataSet dataset = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Name as Назва, OrderItem.OrderItem_Count as Кількість, Tovar.Tovar_SellingPrice as Ціна FROM Tovar INNER JOIN OrderItem ON Tovar.[Tovar_Id] = OrderItem.[Tovar_Id] where Order_Id = " + ord_id, con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset, "OrderItem");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset;
                    binding.DataMember = "OrderItem";
                    dataGridView2.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                using (DataSet data = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Tovar.Tovar_Id as Код, Tovar.Tovar_Name  as Назва, Tovar.Tovar_Color  as Колір, Type.Type_Name  as Тип, Tovar.Tovar_SellingPrice  as Ціна, Sclad.Sclad_Count as 'Кількість на складі', Provider.Provider_CompanyName as Виробник, Units.Units_Name as 'Одиниці вимірювання' FROM(Units INNER JOIN(Type INNER JOIN Tovar ON Type.[Type_Id] = Tovar.[Type_Id]) ON Units.[Units_Id] = Tovar.[Units_Id]) INNER JOIN(Provider INNER JOIN Sclad ON Provider.[Provider_Id] = Sclad.[Provider_Id]) ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id]", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(data, "Tovar");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = data;
                    binding.DataMember = "Tovar";
                    dataGridView3.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Client.Client_Id as 'Код', (Client.Client_Surname + ' ' + Client.Client_Name + ' ' + Client.Client_Patronymic) as 'ПІБ Клієнта', Client.Client_Telephone as №Телефону, Client.Client_Address as Адреса, Client_Birthday as 'Дата народження', Sex.Sex_Name as 'Стать' FROM Client INNER JOIN Sex on Client.Sex_Id=Sex.Sex_Id WHERE Client.Client_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Client");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Client";
                    dataGridView4.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Admin.Admin_id as 'Код', (Admin.Admin_login + ' ' + Admin.Admin_Name + ' ' + Admin.Admin_Patronymic) as 'ПІБ Користувача', Sex.Sex_Name as 'Стать', Admin.Admin_DR as 'Дата народження', Admin.Admin_Password as Пароль, Status.Status_Name as Посада FROM Sex INNER JOIN (Status INNER JOIN Admin ON Status.[Status_Id] = Admin.[Status_Id]) ON Sex.[Sex_Id] = Admin.[Sex_Id] WHERE Admin.Admin_status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Admin");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Admin";
                    dataGridView5.DataSource = binding;
                }
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                using (DataSet dataset3 = new DataSet())
                {
                    oledbData = new OleDbDataAdapter("SELECT Provider.Provider_Id as 'Код', Provider.Provider_CompanyName as 'Назва компанії', Provider.Provider_Owner as 'Власник', Provider.Provider_Address as 'Адреса', Provider.Provider_OwnerPhone as '№ Телефона' FROM Provider WHERE Provider.Provider_Status=1", con);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                    oledbData.Fill(dataset3, "Provider");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataset3;
                    binding.DataMember = "Provider";
                    dataGridView6.DataSource = binding;
                }
            }
        }
    }
}
