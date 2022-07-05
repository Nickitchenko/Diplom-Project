using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAM
{
    public partial class TovarCountEdit : Form
    {
        public TovarCountEdit()
        {
            InitializeComponent();
        }

        private void TovarCountEdit_Load(object sender, EventArgs e)
        {
        }

        void EditTextbox(TextBox t, string number)
        {
            t.Text = t.Text + number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "7");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "8");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "9");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "5");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "6");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "1");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "2");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "3");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EditTextbox(CountTextBox, "0");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ZnakTextBox.Text = "+";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ZnakTextBox.Text = String.Empty;
            CountTextBox.Text = String.Empty;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            CountTextBox.Text = CountTextBox.Text.Remove(CountTextBox.Text.Length - 1); 
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
