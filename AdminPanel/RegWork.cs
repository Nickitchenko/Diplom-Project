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
    public partial class RegWork : Form
    {
        string PathDb = Properties.Settings.Default.ConnectionUSer;
        public OleDbCommand com = new OleDbCommand();
        public OleDbConnection con = new OleDbConnection(Properties.Settings.Default.ConnectionUSer);
        public OleDbDataAdapter oledbData, oledbData2;

        int status_form = 0;
        string surname = "", Name = "", patr = "", DR = "", passwd = "", photo = "";
        int sex_id = 0, status_id = 0;
        int work_id = 0;

        public string name = "";
        public RegWork(string login, string status)
        {
            InitializeComponent();
            toolStripMenuItem1.Text = status + ": " + login;
        }
        public RegWork(string info)
        {
            InitializeComponent();
            toolStripMenuItem1.Text = info;
        }

        public RegWork(string info,int id)
        {
            InitializeComponent();
            toolStripMenuItem1.Text = info;
            work_id = id;
            status_form = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime datemin = new DateTime(2004, 1, 1);
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Дата народження не може бути майбутньою");
            }
            else if(dateTimePicker1.Value.Date > datemin)
            {
                MessageBox.Show("Дата народження незадовільна. Співробітнику менше 18 років.");
            }
            else
            {
                if (textBox1.Text == String.Empty)
                {
                    MessageBox.Show("Поле 'Прізвище' пусте. Заповніть його для збереження даних.'");
                    return;
                }
                else if (textBox2.Text == String.Empty)
                {
                    MessageBox.Show("Поле 'Ім'я' пусте. Заповніть його для збереження даних.'");
                    return;
                }
                else if (textBox3.Text == String.Empty)
                {
                    MessageBox.Show("Поле 'По-Батькові' пусте. Заповніть його для збереження даних.'");
                    return;
                }
                else if (dateTimePicker1.Value == DateTime.Now)
                {
                    MessageBox.Show("Поле 'Дата народження' некоректне. Змініть його для збереження даних.'");
                    return;
                }
                else if (textBox4.Text == String.Empty)
                {
                    MessageBox.Show("Поле 'Пароль' пусте. Заповніть його для збереження даних.'");
                    return;
                }
                else if (textBox4.TextLength < 4)
                {
                    MessageBox.Show("Поле 'Пароль' має складатись з 4-ох цифр. Заповніть його для збереження даних.'");
                    return;
                }
                else
                {
                    if (status_form == 0)
                    {
                        if (name == string.Empty)
                        {
                            name = "worker.png";
                        }
                        using (OleDbConnection con = new OleDbConnection(PathDb))
                        {
                            //вставка в таблицу заказы информации про заказ
                            con.Open();
                            string InsertOledbCommand1 = "insert into Admin(Admin_login, Admin_Name, Admin_Patronymic, Sex_Id, Admin_DR, Status_Id, Admin_password, Admin_Photo, Admin_status) values(@Admin_login, @Admin_Name, @Admin_Patronymic, @Sex_Id, @Admin_DR, @Status_Id, @Admin_password, @Admin_Photo, @Admin_status)";
                            using (OleDbCommand oledbInsert1 = new OleDbCommand(InsertOledbCommand1, con))
                            {
                                oledbInsert1.Parameters.AddWithValue("@Admin_login", textBox1.Text);
                                oledbInsert1.Parameters.AddWithValue("@Admin_Name", textBox2.Text);
                                oledbInsert1.Parameters.AddWithValue("@Admin_Patronymic", textBox3.Text);
                                oledbInsert1.Parameters.AddWithValue("@Sex_Id", comboBox1.SelectedValue);
                                oledbInsert1.Parameters.AddWithValue("@Admin_DR", dateTimePicker1.Text);
                                oledbInsert1.Parameters.AddWithValue("@Status_Id", comboBox2.SelectedValue);
                                oledbInsert1.Parameters.AddWithValue("@Admin_password", textBox4.Text);
                                oledbInsert1.Parameters.AddWithValue("@Admin_Photo", name);
                                oledbInsert1.Parameters.AddWithValue("@Admin_status", 1);
                                oledbInsert1.ExecuteNonQuery();
                            }
                        }
                        this.Close();
                    }
                    else
                    {
                        string oledbUpdateCommand = "Update Admin Set Admin_login='" + textBox1.Text + "', Admin_Name='" + textBox2.Text + "', Admin_Patronymic='" + textBox3.Text + "', Sex_Id=" + (comboBox1.SelectedIndex + 1) + ", Admin_DR='" + dateTimePicker1.Text + "', Admin_password='" + textBox4.Text + "', Admin_Photo='" + name + "', Status_Id= " + (comboBox2.SelectedIndex + 1) + " Where Admin_id=" + work_id;
                        using (OleDbConnection con = new OleDbConnection(PathDb))
                        {
                            con.Open();
                            using (OleDbCommand oledbupdate = new OleDbCommand(oledbUpdateCommand, con))
                            {
                                oledbupdate.ExecuteNonQuery();
                            }
                        }
                        this.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void RegWork_Load(object sender, EventArgs e)
        {
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Sex_Id, Sex_Name from Sex", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Sex");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Sex";
                comboBox1.DataSource = binding;
                comboBox1.DisplayMember = "Sex_Name";
                comboBox1.ValueMember = "Sex_Id";
            }
            using (DataSet dataset3 = new DataSet())
            {
                oledbData = new OleDbDataAdapter("select Status_Id, Status_Name from Status", con);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(oledbData);
                oledbData.Fill(dataset3, "Status");
                BindingSource binding = new BindingSource();
                binding.DataSource = dataset3;
                binding.DataMember = "Status";
                comboBox2.DataSource = binding;
                comboBox2.DisplayMember = "Status_Name";
                comboBox2.ValueMember = "Status_Id";
            }
            if(status_form==1)
            {
                string findcount = "SELECT Admin.Admin_login, Admin.Admin_Name, Admin.Admin_Patronymic, Admin.Sex_Id, Admin.Admin_DR, Admin.Admin_password, Admin.Status_Id, Admin.Admin_Photo FROM Admin where Admin.Admin_id =" + work_id;
                OleDbCommand Command1 = con.CreateCommand();
                Command1.CommandText = findcount;
                OleDbDataAdapter odata1 = new OleDbDataAdapter();
                odata1.SelectCommand = Command1;
                DataSet data1 = new DataSet();
                string datatablename = "Admin";
                odata1.Fill(data1, datatablename);
                DataTable datatable = data1.Tables[datatablename];

                foreach (DataRow dr in datatable.Rows)
                {
                    surname = (string)dr["Admin_login"];
                    Name = (string)dr["Admin_Name"];
                    patr = (string)dr["Admin_Patronymic"];
                    sex_id = (int)dr["Sex_Id"];
                    DR = (string)dr["Admin_DR"];
                    status_id = (int)dr["Status_Id"];
                    passwd = (string)dr["Admin_password"];
                    try
                    {
                        photo = (string)dr["Admin_Photo"];
                    }
                    catch { }
                }
                textBox1.Text = surname;
                textBox2.Text = Name;
                textBox3.Text = patr;
                comboBox1.SelectedIndex = sex_id;
                dateTimePicker1.Text = DR;
                textBox4.Text = passwd;
                comboBox2.SelectedIndex = status_id;
                int i = 0;
                try
                {
                    Class.Photo ph = new Class.Photo();
                    string Put = ph.put_worker;
                    pictureBox1.BackgroundImage = Image.FromFile(Put + photo);
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
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
