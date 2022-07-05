using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace BAM
{
    public class Pay
    {
        public void func_for_check(string[] tovar_name, int[] tovar_code, int[] tovar_price, int[] tovar_Allprice, int[] tovar_count, int order_id,SaveFileDialog saveFileDialog1)
        {
            int counter = 0;
            double sum = 0;
            var builder = new StringBuilder();
            var buyerList = new List<Goods>();
            string filename = "D:\\учеба\\Дипломна робота\\4 Проект\\BAM\\Checks\\Чек №" + order_id + ".txt";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Title = "Друк чека";
            saveFileDialog1.FileName = filename;
            saveFileDialog1.InitialDirectory = "D:\\учеба\\Дипломна робота\\4 Проект\\BAM\\Checks\\";
            for (int i=0;i<tovar_name.Length;i++)
            {
                buyerList.Add(new Goods(tovar_name[i], tovar_code[i], tovar_price[i], tovar_count[i], tovar_Allprice[i]));
            }
            builder.AppendLine($"{"".PadRight(25, ' ')}ТОВ 'BAM-Маркет'");
            builder.AppendLine($"{"".PadRight(25, ' ')}Магазин Мобільних аксесуарів");
            builder.AppendLine($"{"".PadRight(25, ' ')}М.Дніпро");
            builder.AppendLine($"{"".PadRight(25, ' ')}бул. Слави. 17а/20, прим 183");
            builder.AppendLine($"{"".PadRight(25, ' ')}Гаряча лінія 095 879 88 60");
            builder.AppendLine($"{"".PadRight(25, ' ')}email:in@atbmarket.com");
            builder.AppendLine($"{"".PadRight(25, ' ')}Чек №"+order_id);
            foreach (var product in buyerList)
            {
                counter++;
                builder.AppendLine($"{counter}.{product.name}");
                builder.AppendLine($"  Код:{product.code}");
                builder.AppendLine($"  Вартість за одиницю{"".PadRight(40 - product.price.ToString().Length, '.')}{product.price}");
                builder.AppendLine($"  Кількість:{product.count}");
                builder.AppendLine($"  Вартість за {product.count} одиниць{"".PadRight(40 - product.tovar_Allprice.ToString().Length, '.')}{product.tovar_Allprice}");
            }
            for(int i=0;i<tovar_Allprice.Length;i++)
            {
                sum = sum + tovar_Allprice[i];
            }

            builder.AppendLine("".PadRight(51, '='));
            builder.AppendLine($"Загалом{"".PadRight(46 - sum.ToString().Length, '.')}{sum}");
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, builder.ToString());
            Process.Start(filename);
        }

        class Goods
        {
            public string name;
            public int code;
            public double price;
            public int count;
            public int tovar_Allprice;
            public Goods(string name, int code, double price, int count, int tovar_Allprice)
            {
                this.name = name;
                this.code = code;
                this.price = price;
                this.count = count;
                this.tovar_Allprice = tovar_Allprice;
            }
        }
    }
}
