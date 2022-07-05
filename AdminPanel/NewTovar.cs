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
    public partial class NewTovar : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;
        public string name = "";
        public NewTovar()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Bitmap image;
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
                    int idx = Nameimage.LastIndexOf("\\") + 1;
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Назва аксесуара' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox4.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Компанія виробник' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox5.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Колір аксесуара' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox6.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Ціна постачальника' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox3.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Ціна продажі' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else if (textBox8.Text == String.Empty)
            {
                MessageBox.Show("Поле 'Детальний опис аксесаура' пусте. Заповніть його для збереження даних.'");
                return;
            }
            else
            {
                using (OleDbConnection con = new OleDbConnection(PathDb))
                {
                    //вставка в таблицу заказы информации про заказ
                    con.Open();
                    string InsertOledbCommand1 = "insert into Tovar(Tovar_Name, Type_Id, Tovar_Brand, Tovar_Color, Tovar_PurchasePrice, Tovar_SellingPrice, Units_Id, Tovar_Description, Tovar_Photo_URL, Tovar_Status) values(@Tovar_Name, @Type_Id, @Tovar_Brand, @Tovar_Color, @Tovar_PurchasePrice, @Tovar_SellingPrice, @Units_Id, @Tovar_Description, @Tovar_Photo_URL, @Tovar_Status)";
                    using (OleDbCommand oledbInsert1 = new OleDbCommand(InsertOledbCommand1, con))
                    {
                        oledbInsert1.Parameters.AddWithValue("@Tovar_Name", textBox1.Text);
                        oledbInsert1.Parameters.AddWithValue("@Type_Id", comboBox1.SelectedValue);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_Brand", textBox4.Text);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_Color", textBox5.Text);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_PurchasePrice", textBox6.Text);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_SellingPrice", textBox3.Text);
                        oledbInsert1.Parameters.AddWithValue("@Units_Id", comboBox2.SelectedValue);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_Description", textBox8.Text);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_Photo_URL", name);
                        oledbInsert1.Parameters.AddWithValue("@Tovar_Status", 1);
                        oledbInsert1.ExecuteNonQuery();
                    }
                }
                this.Close();
            }
        }

        private void NewTovar_Load(object sender, EventArgs e)
        {
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Type_Id, Type_Name from Type", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Type");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Type";
                comboBox1.DataSource = binding;
                comboBox1.DisplayMember = "Type_Name";
                comboBox1.ValueMember = "Type_Id";
            }
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Units_Id, Units_Name from Units", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Units");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Units";
                comboBox2.DataSource = binding;
                comboBox2.DisplayMember = "Units_Name";
                comboBox2.ValueMember = "Units_Id";
            }

        }
    }
}
