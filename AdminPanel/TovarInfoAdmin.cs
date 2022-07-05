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
    public partial class TovarInfoAdmin : Form
    {
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        public string tov_name1 = "", tov_color1 = "", tov_type1 = "", units1 = "", company1 = "";
        public int answer_zmina=0;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap image;
            string name = "";
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 
                    this.pictureBox1.Size = image.Size;
                    string Nameimage = open_dialog.FileName;
                    int idx = Nameimage.LastIndexOf("\\") +1;
                    int size_name_image = Nameimage.Length - idx;
                    name = Nameimage.Substring(idx, size_name_image);
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Неможливо відкрити новий файл",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Edit_TovarPhoto(name);
            }
        }

        private void Edit_TovarPhoto(string name)
        {
            string oledbUpdateCommand = "";
            oledbUpdateCommand = "Update Tovar Set Tovar_Photo_URL='" + name + "' Where Tovar_Id=" + tov_id1;
            using (con)
            {
                con.Open();
                using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                {
                    oledbupdate.ExecuteNonQuery();
                }
            }
        }

        public int tov_count1, tovar_price1, tov_id1, check1 = -1;
        string TovarInfo1 = "";

        public TovarInfoAdmin(string tov_name, string tov_color, string tov_type, int tov_count, int tovar_price, int tov_id, string units, string company, string TovarInfo, string tovarphoto)
        {
            InitializeComponent();
            textBox1.Text = TovarInfo;
            TovarInfo1 = TovarInfo;
            TovarInfoAdmin_Load(tov_name, tov_color, tov_type, tov_count, tovar_price, tov_id, units, company, tovarphoto);
        }

        private void TovarInfoAdmin_Load(string tov_name, string tov_color, string tov_type, int tov_count, int tovar_price, int tov_id, string units, string company, string tovarphoto)
        {
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            DataGridViewCell cell1 = new DataGridViewTextBoxCell();

            DataGridViewColumn col = new DataGridViewColumn();
            col.ReadOnly = true;
            col.CellTemplate = cell;
            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "Інформація про товар";
            col1.ReadOnly = false;
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
            //завантаження фото товару
            int i = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string new_tovarname = Convert.ToString(dataGridView1.Rows[1].Cells[1].Value);
            string new_tovarunits = Convert.ToString(dataGridView1.Rows[2].Cells[1].Value);
            string new_tovartype = Convert.ToString(dataGridView1.Rows[3].Cells[1].Value);
            string new_tovarcolor = Convert.ToString(dataGridView1.Rows[4].Cells[1].Value);
            string new_tovarbrand = Convert.ToString(dataGridView1.Rows[5].Cells[1].Value);
            try
            {
                int new_tovarpurchaseprice = Convert.ToInt32(dataGridView1.Rows[6].Cells[1].Value);
            }
            catch
            {
                MessageBox.Show("Введені дані некоректні, в поле ціна можна вводити лише цифри","Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            try
            {
                int new_tovarsellingprice = Convert.ToInt32(dataGridView1.Rows[7].Cells[1].Value);
            }
            catch
            {
                MessageBox.Show("Введені дані некоректні, в поле ціна можна вводити лише цифри", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            string new_tovardescription = textBox1.Text;
            string UpdateCommand = "";
            if(new_tovarbrand!=company1)
            {
                UpdateCommand = "Update Tovar Set Tovar_Brand='" + new_tovarbrand + "' Where Tovar_Id=" + tov_id1;
                updatecomand(UpdateCommand);
                answer_zmina = 1;
                this.Close();
            }
            if(new_tovarcolor!=tov_color1)
            {
                UpdateCommand = "Update Tovar Set Tovar_Color='" + new_tovarcolor + "' Where Tovar_Id=" + tov_id1;
                updatecomand(UpdateCommand);
                answer_zmina = 1;
                this.Close();
            }
            if(new_tovardescription!=TovarInfo1)
            {
                UpdateCommand = "Update Tovar Set Tovar_Description='" + new_tovardescription + "' Where Tovar_Id=" + tov_id1;
                updatecomand(UpdateCommand);
                answer_zmina = 1;
                this.Close();
            }
            if(new_tovarname!=tov_name1)
            {
                UpdateCommand = "Update Tovar Set Tovar_Name='" + new_tovarname + "' Where Tovar_Id=" + tov_id1;
                updatecomand(UpdateCommand);
                answer_zmina = 1;
                this.Close();
            }
        }

        private void phototovar()
        {
            int selectedindex = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string put = "";
            string findcount = "SELECT Tovar_Id, Tovar_Photo_URL FROM Tovar where Tovar.Tovar_Id =" + selectedindex;
            OleDbCommand Command1 = con.CreateCommand();
            Command1.CommandText = findcount;
            OleDbDataAdapter odata1 = new OleDbDataAdapter();
            odata1.SelectCommand = Command1;
            DataSet data1 = new DataSet();
            string datatablename = "Tovar";
            odata1.Fill(data1, datatablename);

            DataTable datatable = data1.Tables[datatablename];
            int i = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                try
                {
                    put = (string)dr["Tovar_Photo_URL"];
                }
                catch { }
            }
            try
            {
                Class.Photo photo = new Class.Photo();
                string Put = photo.put;
                pictureBox1.BackgroundImage = Image.FromFile(Put + put); i = 1;
            }
            catch
            {
                if (i == 0)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.NoPhoto as Bitmap;
                }
            }
        }
    }
}
