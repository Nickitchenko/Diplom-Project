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
    public partial class EditOrder : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;

        int orderid;
        
        public EditOrder(int order_id)
        {
            InitializeComponent();
            orderid = order_id;
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
                string tov_color = sf.tov_color, tov_type = sf.tov_type, company = sf.company, units = sf.units, TovarInfo = sf.TovarInfo;
                Paymaster_Form.TovarInfo ti = new Paymaster_Form.TovarInfo(tov_name, tov_color, tov_type, tov_count, tovar_price, tov_id, check, units, company, TovarInfo, tovarphoto);
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
                        listBox6.Items.Insert(0, tov_id);
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

        private void listboxheight()
        {
            listBox1.Height = (listBox1.Items.Count) * 24;
            listBox2.Height = (listBox2.Items.Count) * 24;
            listBox3.Height = (listBox3.Items.Count) * 24;
            listBox4.Height = (listBox4.Items.Count) * 24;
            listBox5.Height = (listBox5.Items.Count) * 24;
            listBox6.Height = (listBox6.Items.Count) * 24;
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
                    listBox5.Items[0] = 1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
            if (t.CountTextBox.Text == string.Empty)
            {
                return;
            }
            int new_count = Convert.ToInt32(t.CountTextBox.Text); //змінена кількість товарів
            string new_znak = t.ZnakTextBox.Text;
            int TovarPrice = Convert.ToInt32(listBox4.Items[idTov]);
            //порівняння заданої кількості в лістбокс та в TovarCountEdit
            int new_count2 = 0;
            string DBznak = ""; //нова кількість товарів на складі
            if (Convert.ToInt32(listBox2.Items[idTov]) > new_count && new_znak == "")
            {
                new_count2 = Convert.ToInt32(listBox2.Items[idTov]) - new_count;
                DBznak = "+";
                listBox2.Items[idTov] = new_count;
            }
            else if (Convert.ToInt32(listBox2.Items[idTov]) < new_count && new_znak == "")
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
                if (TovCount - new_count < 0)
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idTov = -1;
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void EditOrder_Load(object sender, EventArgs e)
        {
            using (OleDbConnection con = new OleDbConnection(PathDb))
            {
                int client_id=0, order_price=0;
                string order_date="";
                //достаем данные из бд для составления заказа
                string findcount = "SELECT Order_Id, Client_Id, Order_Date, Order_Price FROM Orders WHERE Order_Id=" + orderid;
                OleDbCommand Command1 = con.CreateCommand();
                Command1.CommandText = findcount;
                OleDbDataAdapter odata1 = new OleDbDataAdapter();
                odata1.SelectCommand = Command1;
                DataSet data1 = new DataSet();
                string datatablename = "Orders";
                odata1.Fill(data1, datatablename);

                DataTable datatable = data1.Tables[datatablename];

                foreach (DataRow dr in datatable.Rows)
                {
                    client_id = Convert.ToInt32(dr["Client_id"]);
                    order_date = Convert.ToString(dr["Order_Date"]);
                    order_price = Convert.ToInt32(dr["Order_Price"]);
                }

                string findcount2 = "SELECT Tovar_Id, OrderItem_Count FROM OrderItem WHERE Order_Id=" + orderid;
                OleDbCommand Command2 = con.CreateCommand();
                Command2.CommandText = findcount2;
                OleDbDataAdapter odata2 = new OleDbDataAdapter();
                odata2.SelectCommand = Command2;
                DataSet data2 = new DataSet();
                string datatablename2 = "OrderItem";
                odata2.Fill(data2, datatablename2);

                DataTable datatable2 = data2.Tables[datatablename2];
                List<int> tovar_Id = new List<int>();
                List<int> tovar_count = new List<int>();
                foreach (DataRow dr in datatable2.Rows)
                {
                    tovar_Id.Add(Convert.ToInt32(dr["Tovar_Id"]));
                    tovar_count.Add(Convert.ToInt32(dr["OrderItem_Count"]));
                }

                //поиск названий товара

                List<string> tovar_name = new List<string>();
                List<int> tovar_price = new List<int>();
                for (int i=0; i<tovar_Id.Count;i++)
                {
                    string findcount3 = "SELECT Tovar_Name, Tovar_SellingPrice FROM Tovar WHERE Tovar_Id=" + tovar_Id[i];
                    OleDbCommand Command3 = con.CreateCommand();
                    Command3.CommandText = findcount3;
                    OleDbDataAdapter odata3 = new OleDbDataAdapter();
                    odata3.SelectCommand = Command3;
                    DataSet data3 = new DataSet();
                    string datatablename3 = "Tovar";
                    odata3.Fill(data3, datatablename3);

                    DataTable datatable3 = data3.Tables[datatablename3];
                    foreach (DataRow dr in datatable3.Rows)
                    {
                        tovar_name.Add(Convert.ToString(dr["Tovar_Name"]));
                        tovar_price.Add(Convert.ToInt32(dr["Tovar_SellingPrice"]));
                    }
                }

                for(int i=0;i<tovar_name.Count;i++)
                {
                    listBox5.Items.Insert(i,i+1);
                    listBox6.Items.Insert(0, format_tovar_id(tovar_Id[i]));
                    listBox1.Items.Insert(i, tovar_name[i]);
                    listBox4.Items.Insert(i, tovar_price[i]);
                    listBox2.Items.Insert(i, tovar_count[i]);
                    listBox3.Items.Insert(i, Convert.ToInt32(listBox2.Items[i]) * Convert.ToInt32(listBox4.Items[i]));
                }
                listboxheight();

                using (DataSet dataSet = new DataSet())
                {
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter("select Client_Id, Client_Surname from Client", con);
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
                comboBox1.SelectedValue = client_id;
                textBox3.Text = Convert.ToString(order_price);
            }
        }

    }
}
