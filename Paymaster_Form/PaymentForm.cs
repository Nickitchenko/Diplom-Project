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
    public partial class PaymentForm : Form
    {
        public bool answer = false;
        public int typepay;
        private double Round(double pay)//окргулення
        {
            double round = Math.Round(pay);
            return round;
        }

        void EditTextbox(TextBox t, string number)//функція для кнопок на формі (1-9)
        {
            t.Text = t.Text + number;
        }

        public PaymentForm(string pay)
        {
            InitializeComponent();
            textBox2.Text = pay;
            this.ActiveControl = textBox5;
            button16.Enabled = false;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox5.Text = String.Empty;
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e) // розрахунок решти
        {
            int client_money;
            if (textBox5.Text == String.Empty)
            {
                client_money = 0;
            }
            else
            {
                client_money = Convert.ToInt32(textBox5.Text);
            }
            textBox6.Text = Convert.ToString(client_money - Convert.ToInt32(textBox2.Text));
        }

        private void textBox5_TextChanged(object sender, EventArgs e) //розрахунок решти №2
        {
            int client_money;
            if (textBox5.Text==String.Empty)
            {
                client_money = 0;
            }
            else
            {
                client_money = Convert.ToInt32(textBox5.Text);
            }
            textBox6.Text = Convert.ToString(client_money-Convert.ToInt32(textBox2.Text));
            if(Convert.ToInt32(textBox6.Text)>=0)
            {
                button16.Enabled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e) //ввід тілько чисел в готівку
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "7");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "8");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "9");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "5");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "6");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "1");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "2");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "3");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EditTextbox(textBox5, "0");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox5.Text.Remove(textBox5.Text.Length - 1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox2.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            answer = false;
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            answer = true;
            typepay = 2;
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            answer = true;
            typepay = 1;
            this.Close();
        }
    }
}
