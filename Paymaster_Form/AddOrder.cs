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
using System.Diagnostics;

namespace BAM
{

    public partial class AddOrder : Form
    {
        string thisday;
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        private DataSet dataset2;
        private List<int> list = new List<int>();
        string name, work;

        public AddOrder(string name1, string work1)
        {
            InitializeComponent();
            name = name1;
            work = work1;
            textBox4.Text = work + ": " + name;
            dataset2 = new DataSet();
        }

        private void listboxheight()
        {
            listBox1.Height = (listBox1.Items.Count) * 24;
            listBox2.Height = (listBox2.Items.Count) * 24;
            listBox3.Height = (listBox3.Items.Count) * 24;
            listBox4.Height = (listBox4.Items.Count) * 24;
            listBox5.Height = (listBox5.Items.Count) * 24;
            listBox6.Height = (listBox6.Items.Count) * 24;
        }

        private void clear()
        {
            textBox1.Text = string.Empty;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            checkBox1.Checked = false;
            textBox2.Text = string.Empty;
            textBox3.Text = "0";
        }

        private void AddOrder_Shown(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            con.Open();
            using (OleDbConnection oledbConnection = new OleDbConnection(PathDb))
            {
                oledbConnection.Open();
                using (DataSet dataSet = new DataSet())
                {
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter("select Client_Id, Client_Surname from Client", oledbConnection);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);
                    dataAdapter.Fill(dataSet, "Client");
                    BindingSource binding = new BindingSource();
                    binding.DataSource = dataSet;
                    binding.DataMember = "Client";
                    comboBox1.DataSource = binding;
                    comboBox1.DisplayMember = "Client_Surname";
                    comboBox1.ValueMember = "Client_Id";
                    comboBox1.SelectedItem = 1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //редагування кількості товарів в замовленні
        {
            int idTov = 0; //id виділеного лістбоксаПла
            if (Convert.ToString(listBox1.SelectedItem) != String.Empty)
            {
                idTov = listBox1.SelectedIndex;
            }
            else if (Convert.ToString(listBox2.SelectedItem) != String.Empty)
            {
                idTov = listBox2.SelectedIndex;
            }
            else if (Convert.ToString(listBox4.SelectedItem) != String.Empty)
            {
                idTov = listBox4.SelectedIndex;
            }
            else if (Convert.ToString(listBox5.SelectedItem) != String.Empty)
            {
                idTov = listBox5.SelectedIndex;
            }
            else if (Convert.ToString(listBox6.SelectedItem) != String.Empty)
            {
                idTov = listBox6.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Замовлення пусте або жоден елемент не був обраний"); return;
            }
            TovarCountEdit t = new TovarCountEdit();
            t.ShowDialog();
            if(t.CountTextBox.Text==string.Empty)
            {
                return;
            }
            int new_count = Convert.ToInt32(t.CountTextBox.Text); //змінена кількість товарів
            string new_znak = t.ZnakTextBox.Text;
            int TovarPrice=Convert.ToInt32(listBox4.Items[idTov]);
            //порівняння заданої кількості в лістбокс та в TovarCountEdit
            int new_count2 = 0;
            string DBznak = ""; //нова кількість товарів на складі
            if(Convert.ToInt32(listBox2.Items[idTov])>new_count && new_znak=="")
            {
                new_count2 = Convert.ToInt32(listBox2.Items[idTov]) - new_count; 
                DBznak = "+"; 
                listBox2.Items[idTov] = new_count;
            }
            else if(Convert.ToInt32(listBox2.Items[idTov])<new_count && new_znak=="")
            {
                new_count2 = new_count - Convert.ToInt32(listBox2.Items[idTov]);
                DBznak = "-";
                listBox2.Items[idTov] = new_count;
            }
            else if (Convert.ToInt32(listBox2.Items[idTov]) > new_count && new_znak == "-")
            {
                new_count2 = Convert.ToInt32(listBox2.Items[idTov]) - new_count;
                DBznak = "+";
                listBox2.Items[idTov] = new_count2;
                new_count2 = new_count;
            }
            else if (Convert.ToInt32(listBox2.Items[idTov]) < new_count && new_znak == "-")
            {
                MessageBox.Show("Введена кількість товарів не можлива. Введене число більше заданого. Результат " + (new_count2 - Convert.ToInt32(listBox2.Items[idTov]))); return;
            }
            int codeTovar = Convert.ToInt32(listBox6.Items[idTov]); //знаходження айді виділеного товару
            int TovCount = 0;
            string findcount = "SELECT Sclad.Sclad_Id, Sclad.Tovar_Id, Sclad.Sclad_Count, Tovar.Tovar_SellingPrice FROM Tovar INNER JOIN Sclad ON Tovar.[Tovar_Id] = Sclad.[Tovar_Id] where Tovar.Tovar_Id =" + codeTovar;
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
            }
            if (DBznak == "-")
            {
                if (TovCount-new_count < 0)
                {
                    MessageBox.Show("На складі немає стільки товарів!!! Доступна кількість: " + TovCount); return;
                }
            }
            if (Convert.ToInt32(listBox2.Items[idTov]) == 0)
            {
                func_clear_order(idTov);
                listboxheight();
            }
            else
            {
                string oledbUpdateCommand = "";
                if (DBznak == "+")
                {
                    oledbUpdateCommand = "Update Sclad Set Sclad_Count=Sclad_Count+" + new_count2 + " Where Tovar_Id=" + codeTovar;
                    //dataGridView1.Rows[codeTovar-2].Cells[5].Value = (int)dataGridView1.Rows[codeTovar - 2].Cells[5].Value + new_count2;
                }
                else
                {
                    oledbUpdateCommand = "Update Sclad Set Sclad_Count=Sclad_Count-" + new_count2 + " Where Tovar_Id=" + codeTovar;
                    //dataGridView1.Rows[codeTovar - 2].Cells[5].Value = (int)dataGridView1.Rows[codeTovar - 2].Cells[5].Value - new_count2;
                }
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }
                //
                //змінення листбокс3
                listBox3.Items[idTov] = Convert.ToInt32(listBox2.Items[idTov]) * Convert.ToInt32(listBox4.Items[idTov]);
                //
                //розрахунок нової вартості замовлення
                int NewPrice = 0;
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    NewPrice = NewPrice + Convert.ToInt32(listBox3.Items[i]);
                }
                textBox3.Text = Convert.ToString(NewPrice);
                listboxheight();
            }
            //
        }

        private string format_tovar_id(int tov_id)
        {
            string id = Convert.ToString(tov_id);
            int a = id.Length;
            if (a == 4)
                return id;
            else if (a == 3)
            {
                return "0" + id;
            }
            else if (a == 2)
            {
                return "00" + id;
            }
            else if (a == 1)
            {
                return "000" + id;
            }
            else { MessageBox.Show("Error format id"); return ""; }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Paymaster_Form.SelectTovar sf = new Paymaster_Form.SelectTovar(textBox1.Text);
            sf.ShowDialog();
            int answer = 0;
            string tov_name = sf.tov_name;
            if (tov_name != String.Empty)
            {
                string tovarphoto = sf.TovarPhoto;
                int tov_count = sf.tov_count, tovar_price = sf.tovar_price, tov_id = sf.tov_id, check = sf.check;
                string tov_color = sf.tov_color, tov_type = sf.tov_type, company = sf.company, units = sf.units, TovarInfo=sf.TovarInfo;
                Paymaster_Form.TovarInfo ti = new Paymaster_Form.TovarInfo(tov_name, tov_color, tov_type, tov_count, tovar_price, tov_id, check, units, company, TovarInfo,tovarphoto);
                ti.ShowDialog();
                answer = ti.answer;
                if (answer != 0)
                {
                    for (int i = 0; i < listBox1.Items.Count; i++) //порівнює в датагридвью виділений елемент таблиці з листбоксом з іменами
                    {
                        if ((string)listBox1.Items[i] == tov_name)
                        {
                            check = i; break;
                        }
                    }
                    if (check == -1) // якщо порівняння не знайдено тоді додає новий товар і всю інфу в лістбокси
                    {
                        listBox1.Items.Insert(0, tov_name);
                        listBox2.Items.Insert(0, 1);
                        listBox3.Items.Insert(0, tovar_price);
                        listBox4.Items.Insert(0, tovar_price);
                        textBox3.Text = Convert.ToString(Math.Round(Convert.ToDouble(textBox3.Text) + tovar_price, 2));
                        listBox5.Items.Insert(0, listBox1.Items.Count);
                        listBox6.Items.Insert(0, format_tovar_id(tov_id));
                        listboxheight();
                        //int Tovar_Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value); //айди товару
                        //list.Add(Tovar_Id); //додавання до списку айди товару
                    }
                    else // якщо знайшов вже такий товар тоді додає сумму товару та кількість
                    {
                        listBox2.Items[check] = (int)listBox2.Items[check] + 1;
                        listBox3.Items[check] = (int)listBox3.Items[check] + tovar_price;
                        int NewPrice = 0;
                        for (int i = 0; i < listBox3.Items.Count; i++)
                        {
                            NewPrice = NewPrice + Convert.ToInt32(listBox3.Items[i]);
                        }
                        textBox3.Text = Convert.ToString(NewPrice);
                    }

                    //віднімання на складі кількості товарів в бд
                    string oledbUpdateCommand = "Update Sclad Set Sclad_Count=Sclad_Count-1 Where Tovar_Id=" + tov_id;
                    using (OleDbConnection con = new OleDbConnection(PathDb))
                    {
                        con.Open();
                        using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                        {
                            oledbupdate.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Paymaster_Form.CopyCheck cp = new Paymaster_Form.CopyCheck(saveFileDialog1);
            cp.Show();
        }

        

        private void func_clear_order(int idTov)
        {
            if (idTov != -1)
            {
                int tovCount = Convert.ToInt32(listBox2.Items[idTov]);
                int codeTovar = Convert.ToInt32(listBox6.Items[idTov]);
                string oledbUpdateCommand = "";
                oledbUpdateCommand = "Update Sclad Set Sclad_Count=Sclad_Count+" + tovCount + " Where Tovar_Id=" + codeTovar;
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    con.Open();
                    using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                    {
                        oledbupdate.ExecuteNonQuery();
                    }
                }
                //розрахунок нової вартості замовлення
                int NewPrice = 0;
                if (listBox1.Items.Count > 1)
                {
                    for (int i = 0; i < listBox3.Items.Count; i++)
                    {
                        NewPrice = NewPrice + Convert.ToInt32(listBox3.Items[i]);
                    }
                    listboxheight();
                }
                else if (listBox1.Items.Count == 1)
                {
                    NewPrice = Convert.ToInt32(listBox3.Items[0]);
                    listboxheight();
                }
                else
                {
                    NewPrice = 0;
                    listboxheight();
                }
                textBox3.Text = Convert.ToString(NewPrice);
                listBox1.Items.RemoveAt(idTov);
                listBox2.Items.RemoveAt(idTov);
                listBox3.Items.RemoveAt(idTov);
                listBox4.Items.RemoveAt(idTov);
                listBox5.Items.RemoveAt(idTov);
                listBox6.Items.RemoveAt(idTov);
                if (listBox1.Items.Count > 1)
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        listBox5.Items.Insert(i, i + 1);
                    }
                }
                else if (listBox1.Items.Count == 1)
                {
                    listBox5.Items[0]= 1;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idTov=-1;
            if (Convert.ToString(listBox1.SelectedItem) != String.Empty)
            {
                idTov = listBox1.SelectedIndex;
            }
            else if (Convert.ToString(listBox2.SelectedItem) != String.Empty)
            {
                idTov = listBox2.SelectedIndex;
            }
            else if (Convert.ToString(listBox4.SelectedItem) != String.Empty)
            {
                idTov = listBox4.SelectedIndex;
            }
            else if (Convert.ToString(listBox5.SelectedItem) != String.Empty)
            {
                idTov = listBox5.SelectedIndex;
            }
            else if (Convert.ToString(listBox6.SelectedItem) != String.Empty)
            {
                idTov = listBox6.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Замовлення пусте або жоден елемент не був обраний"); return;
            }
            func_clear_order(idTov);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox1.SelectedIndex;
            listBox2.SelectedIndex = a;
            listBox3.SelectedIndex = a;
            listBox4.SelectedIndex = a;
            listBox5.SelectedIndex = a;
            listBox6.SelectedIndex = a;
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox5.SelectedIndex;
            listBox2.SelectedIndex = a;
            listBox3.SelectedIndex = a;
            listBox4.SelectedIndex = a;
            listBox1.SelectedIndex = a;
            listBox6.SelectedIndex = a;
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox6.SelectedIndex;
            listBox2.SelectedIndex = a;
            listBox3.SelectedIndex = a;
            listBox4.SelectedIndex = a;
            listBox5.SelectedIndex = a;
            listBox1.SelectedIndex = a;
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox4.SelectedIndex;
            listBox2.SelectedIndex = a;
            listBox3.SelectedIndex = a;
            listBox1.SelectedIndex = a;
            listBox5.SelectedIndex = a;
            listBox6.SelectedIndex = a;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox2.SelectedIndex;
            listBox1.SelectedIndex = a;
            listBox3.SelectedIndex = a;
            listBox4.SelectedIndex = a;
            listBox5.SelectedIndex = a;
            listBox6.SelectedIndex = a;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox3.SelectedIndex;
            listBox2.SelectedIndex = a;
            listBox1.SelectedIndex = a;
            listBox4.SelectedIndex = a;
            listBox5.SelectedIndex = a;
            listBox6.SelectedIndex = a;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count!=0)
            {
                //for(int i=0;i<listBox1.Items.Count;i++)
                //{
                //    func_clear_order(i);
                //}

                DialogResult dialogResult = MessageBox.Show("Закрити чек?", "Чек", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    textBox1.Text = string.Empty;
                    textBox3.Text = string.Empty;
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Вийти з програми?", "Вихід з програми", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (dialogResult == DialogResult.No)
                {
                    
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddOrder ao = new AddOrder(name,work);
            ao.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddOrder ao = new AddOrder(name,work);
            ao.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string DayZvit = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
            string DMY = DayZvit.Substring(0, 10);
            DayZvit dz=new DayZvit();
            dz.func_FindOrder(DMY,saveFileDialog1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("Dovidka_Paymaster.chm");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminPanel.NewClient nc = new AdminPanel.NewClient(textBox4.Text);
            nc.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Paymaster_Form.ReturnOrder ro = new Paymaster_Form.ReturnOrder();
            ro.ShowDialog();
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e) //оформлення замовлення
        {
            int x = 0;
            if(textBox3.Text=="0" || textBox3.Text==string.Empty)
            {
                MessageBox.Show("Замовлення пусте. Для оплати товара потрібно додати хоч один аксесуар."); return;
            }
            Paymaster_Form.PaymentForm pf = new Paymaster_Form.PaymentForm(textBox3.Text);
            pf.ShowDialog();
            bool answerPayment = pf.answer;
            if(answerPayment==true)
            {
                int TotalPrice = Convert.ToInt32(textBox3.Text);
                int ClientId;
                int typepay = pf.typepay;
                if(checkBox1.Checked==true)
                {
                    ClientId = 3;
                }
                else
                {
                    ClientId = Convert.ToInt32(comboBox1.SelectedValue);
                }
                thisday = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
                int admin_id=0;
                string selectString2 = "Select Admin.Admin_id from Admin WHERE Admin_login='"+name+"'";
                OleDbCommand Command2 = con.CreateCommand();
                Command2.CommandText = selectString2;
                OleDbDataAdapter odata2 = new OleDbDataAdapter();
                odata2.SelectCommand = Command2;
                DataSet data2 = new DataSet();
                string datatablename2 = "Admin";
                odata2.Fill(data2, datatablename2);
                DataTable datatable2 = data2.Tables[datatablename2];
                //знаходження тільки створеного замовлення та його айді
                foreach (DataRow dr in datatable2.Rows)
                {
                    admin_id = Convert.ToInt32(dr["Admin_id"]);
                }

                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    //вставка в таблицу заказы информации про заказ
                    con.Open();
                    string InsertOledbCommand1 = "insert into Orders(Client_Id, Order_Date, Order_Price, TypePay_Id, Admin_id) values(@Client_Id, @Order_Date, @Order_Price, @TypePay_Id, @Admin_id)";
                    using (OleDbCommand oledbInsert1 = new OleDbCommand(InsertOledbCommand1, con))
                    {
                        oledbInsert1.Parameters.AddWithValue("@Client_Id", ClientId);
                        oledbInsert1.Parameters.AddWithValue("@Order_Date", thisday);
                        oledbInsert1.Parameters.AddWithValue("@Order_Price", TotalPrice);
                        oledbInsert1.Parameters.AddWithValue("@TypePay_Id", typepay);
                        oledbInsert1.Parameters.AddWithValue("@Admin_id", admin_id);
                        oledbInsert1.ExecuteNonQuery();
                    }
                    // створення таблиці з тільки створеним замовленням
                    string selectString = "Select Order_Id from Orders where Client_Id = " + ClientId + " and Order_Price = " + TotalPrice;
                    OleDbCommand Command1 = con.CreateCommand();
                    Command1.CommandText = selectString;
                    OleDbDataAdapter odata1 = new OleDbDataAdapter();
                    odata1.SelectCommand = Command1;
                    DataSet data1 = new DataSet();
                    string datatablename = "Orders";
                    odata1.Fill(data1, datatablename);
                    DataTable datatable = data1.Tables[datatablename];
                    //знаходження тільки створеного замовлення та його айді
                    foreach (DataRow dr in datatable.Rows)
                    {
                        x = Convert.ToInt32(dr["Order_Id"]);
                    }
                    //вставка в таблицю деталі замовлення списку всіх куплених товарів
                    string InsertOledbCommand2 = "insert into OrderItem(Order_Id,Tovar_Id,OrderItem_Count) values(@Order_Id, @Tovar_Id, @OrderItem_Count)";
                    for (int j = 0; j < listBox5.Items.Count; j++)
                    {
                        using (OleDbCommand oledbInsert2 = new OleDbCommand(InsertOledbCommand2, con))
                        {
                            oledbInsert2.Parameters.AddWithValue("@Order_id", x);
                            oledbInsert2.Parameters.AddWithValue("@Tovar_Id", listBox6.Items[j]);
                            oledbInsert2.Parameters.AddWithValue("@OrderItem_Count", listBox2.Items[j]);
                            oledbInsert2.ExecuteNonQuery();
                        }
                    }
                }
                //печатать чек
                DialogResult dialogResult = MessageBox.Show("Друкувати чек?", "Друк", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string[] tovar_name = new string[listBox1.Items.Count];
                    int[] tovar_code = new int[listBox6.Items.Count];
                    int[] tovar_price = new int[listBox4.Items.Count];
                    int[] tovar_Allprice = new int[listBox3.Items.Count];
                    int[] tovar_count = new int[listBox2.Items.Count];
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        tovar_name[i] = Convert.ToString(listBox1.Items[i]);
                    }
                    for (int i = 0; i < listBox6.Items.Count; i++)
                    {
                        tovar_code[i] = Convert.ToInt32(listBox6.Items[i]);
                    }
                    for (int i = 0; i < listBox4.Items.Count; i++)
                    {
                        tovar_price[i] = Convert.ToInt32(listBox4.Items[i]);
                    }
                    for (int i = 0; i < listBox3.Items.Count; i++)
                    {
                        tovar_Allprice[i] = Convert.ToInt32(listBox3.Items[i]);
                    }
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        tovar_count[i] = Convert.ToInt32(listBox2.Items[i]);
                    }
                    Pay p = new Pay();
                    p.func_for_check(tovar_name, tovar_code, tovar_price, tovar_Allprice, tovar_count,x,saveFileDialog1);
                    clear();
                }
                else if (dialogResult == DialogResult.No)
                {
                    clear();
                }
            }
        }
    }
}
