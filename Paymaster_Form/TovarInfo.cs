using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAM.Paymaster_Form
{
    public partial class TovarInfo : Form
    {
        public string tov_name1 = "", tov_color1 = "", tov_type1 = "", units1 = "", company1 = ""; public int tov_count1, tovar_price1, tov_id1, check1=-1;

        private void TovarInfo_Load(object sender, EventArgs e)
        {

        }

        public int answer = 0;
        public TovarInfo(){}

        public TovarInfo(string tov_name, string tov_color, string tov_type, int tov_count, int tovar_price, int tov_id, int check, string units, string company, string TovarInfo, string tovarphoto)
        {
            InitializeComponent();
            textBox1.Text = TovarInfo;
            TovarInfo_Load(tov_name, tov_color, tov_type,tov_count, tovar_price, tov_id, check, units, company,tovarphoto);
        }

        private void TovarInfo_Load(string tov_name, string tov_color, string tov_type, int tov_count, int tovar_price, int tov_id, int check, string units, string company,string tovarphoto)
        {
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            DataGridViewCell cell1 = new DataGridViewTextBoxCell();

            DataGridViewColumn col = new DataGridViewColumn();
            col.ReadOnly = true;
            col.CellTemplate = cell;
            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "Інформація про товар";
            col1.ReadOnly = true;
            col1.CellTemplate = cell1;

            dataGridView1.Columns.Add(col);
            dataGridView1.Columns.Add(col1);

            DataGridViewCell cel1 = new DataGridViewTextBoxCell();
            DataGridViewCell cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            DataGridViewRow row = new DataGridViewRow();
            cel1.Value = "Код";
            cel2.Value = tov_id;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            tov_id1 = tov_id;
            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Назва товару";
            cel2.Value = tov_name;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            tov_name1 = tov_name;

            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Одиниці вимірювання (Ш/К)";
            cel2.Value = units;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            units1 = units;

            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Тип товару";
            cel2.Value = tov_type;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            tov_type1 = tov_type;

            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Колір";
            cel2.Value = tov_color;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            tov_color1 = tov_color;

            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Виробник";
            cel2.Value = company;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            company1 = company;

            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Ціна";
            cel2.Value = tovar_price;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            tovar_price1 = tovar_price;

            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            cel1.Style.BackColor = Color.LightGray;
            row = new DataGridViewRow();
            cel1.Value = "Кількість";
            cel2.Value = tov_count;
            row.Cells.AddRange(cel1, cel2);
            dataGridView1.Rows.Add(row);
            tov_count1 = tov_count;
            int i = 0;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            try
            {
                Class.Photo photo = new Class.Photo();
                string Put = photo.put;
                pictureBox1.BackgroundImage = Image.FromFile(Put + tovarphoto);
                //pictureBox1.Image = Image.FromFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + Put + tovarphoto);
                i = 1;
            }
            catch
            {
                if (i == 0)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.NoPhoto as Bitmap;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            answer = 1;
            this.Visible = false;
        }
    }
}
