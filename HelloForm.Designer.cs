
namespace BAM
{
    partial class HelloForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.Location = new System.Drawing.Point(666, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Авторизуватись";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button2.Location = new System.Drawing.Point(350, 201);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 41);
            this.button2.TabIndex = 1;
            this.button2.Text = "Довідка";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(305, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Розробник: Нікітченко Євгеній Сергійович";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(637, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "email: nickitchenkoevgen@gmail.com";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button3.Location = new System.Drawing.Point(514, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 41);
            this.button3.TabIndex = 4;
            this.button3.Text = "Вийти";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // HelloForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BAM.Properties.Resources.фон_арм1;
            this.ClientSize = new System.Drawing.Size(1184, 311);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "HelloForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "АРМ \"Магазин Аксесуарів\" - Вітальне вікно";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
    }
}