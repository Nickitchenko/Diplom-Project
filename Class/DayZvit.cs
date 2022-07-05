using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace BAM
{
    public class DayZvit
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        private DataSet dataset2;

        public void func_FindOrder(string date, SaveFileDialog saveFileDialog1)
        {
            //розрахунки всіх типів оплати за задану дату
            string findcount = "SELECT Client_Id, Order_Date, Order_Price, TypePay_Id FROM Orders where Order_Date like '" + date + "%'";
            OleDbCommand Command1 = con.CreateCommand();
            Command1.CommandText = findcount;
            OleDbDataAdapter odata1 = new OleDbDataAdapter();
            odata1.SelectCommand = Command1;
            DataSet data1 = new DataSet();
            string datatablename = "Orders";
            odata1.Fill(data1, datatablename);

            DataTable datatable = data1.Tables[datatablename];
            string allclient1 = "", allclient2 = "";
            int sum = 0; int average_check; int client_count; int beznal; int nal = 0; int countstring = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                countstring += 1;
                sum += Convert.ToInt32(dr["Order_Price"]);
                allclient2 = allclient1.Insert(allclient1.Length, Convert.ToString(dr["Client_Id"]));
                allclient1 = allclient2;
            }
            if (countstring == 0)
            {
                MessageBox.Show("За цей день не було жодного замовлення. День - "+date); return;
            }
            allclient2 = new string(allclient2.Distinct().ToArray());
            client_count = allclient2.Length;
            average_check = sum / countstring;
            //
            nal = func_Nal(date);
            beznal = Func_beznal(nal,sum);
            func_DayZvit(sum, average_check, client_count, beznal, nal, date, saveFileDialog1);
        }

        public int func_Nal(string date)
        {
            //розрахунок касси за видом оплати готівка
            int nal = 0;
            string findcount2 = "SELECT Client_Id, Order_Date, Order_Price, TypePay_Id FROM Orders where Order_Date like '" + date + "%' AND TypePay_Id=1";
            OleDbCommand Command2 = con.CreateCommand();
            Command2.CommandText = findcount2;
            OleDbDataAdapter odata2 = new OleDbDataAdapter();
            odata2.SelectCommand = Command2;
            DataSet data2 = new DataSet();
            string datatablename2 = "Orders";
            odata2.Fill(data2, datatablename2);

            DataTable datatable2 = data2.Tables[datatablename2];
            foreach (DataRow dr in datatable2.Rows)
            {
                nal += Convert.ToInt32(dr["Order_Price"]);
            }
            return nal;
        }

        private int Func_beznal(int nal, int sum)
        {
            return sum - nal;
        }

        public void func_DayZvit(int sum, int average_check, int client_count, int beznal, int nal, string date, SaveFileDialog saveFileDialog1)
        {
            var builder = new StringBuilder();
            var buyerList = new List<Goods>();
            string day = "";
            string filename = "D:\\учеба\\Дипломна робота\\4 Проект\\BAM\\Zvit\\Денний звіт " + date + ".txt";
            buyerList.Add(new Goods(sum, average_check, client_count, beznal, nal, date));
            builder.AppendLine($"{"".PadRight(25, ' ')}Касса");
            builder.AppendLine($"{"".PadRight(25, ' ')}Денний звіт");
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Title = "Друк звіта";
            saveFileDialog1.FileName = filename;
            saveFileDialog1.InitialDirectory = "D:\\учеба\\Дипломна робота\\4 Проект\\BAM\\Zvit\\";
            foreach (var product in buyerList)
            {
                builder.AppendLine($"{"".PadRight(25, ' ')}Сума продажів:{product.sum}");
                builder.AppendLine($"{"".PadRight(25, ' ')}Середній чек:{product.average_check}");
                builder.AppendLine($"{"".PadRight(25, ' ')}Кількість клієнтів:{product.client_count}");
                builder.AppendLine($"{"".PadRight(25, ' ')}Безготівкова каса:{product.sum}");
                builder.AppendLine($"{"".PadRight(25, ' ')}Касове місце:{product.nal}");
                builder.AppendLine($"{"".PadRight(25, ' ')}-----------------------------");
                builder.AppendLine($"{"".PadRight(25, ' ')}Звіт виконано:{product.date}");
                day = product.date;
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, builder.ToString());
            Process.Start(filename);
        }

        class Goods
        {
            public int sum;
            public int average_check;
            public int client_count;
            public int beznal;
            public int nal;
            public string date;
            public Goods(int sum, int average_check, int client_count, int beznal, int nal, string date)
            {
                this.average_check = average_check;
                this.sum = sum;
                this.client_count = client_count;
                this.beznal = beznal;
                this.nal = nal;
                this.date = date;
            }
        }
    }
}
