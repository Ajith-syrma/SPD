
namespace SPD_Write_Bot
{
    partial class SPD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPD));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pos_x = new System.Windows.Forms.Label();
            this.lbl_pos_y = new System.Windows.Forms.Label();
            this.lbl_resol = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_Filepath = new System.Windows.Forms.Label();
            this.lbl_Path = new System.Windows.Forms.Label();
            this.lbl_Part = new System.Windows.Forms.Label();
            this.lbl_Mdl = new System.Windows.Forms.Label();
            this.part = new System.Windows.Forms.Label();
            this.lbl_Model = new System.Windows.Forms.Label();
            this.lbl_Release = new System.Windows.Forms.Label();
            this.lbl_Cap = new System.Windows.Forms.Label();
            this.lbl_capacity = new System.Windows.Forms.Label();
            this.lbl_Ver = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.Label();
            this.SW = new System.Windows.Forms.Label();
            this.lbl__Freq = new System.Windows.Forms.Label();
            this.Frequence = new System.Windows.Forms.Label();
            this.cbm_FG = new System.Windows.Forms.ComboBox();
            this.lbl_FG = new System.Windows.Forms.Label();
            this.cbm_Cus = new System.Windows.Forms.ComboBox();
            this.lbl_Cus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_App_ver = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(178, 240);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.Visible = false;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "SCAN HERE";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbl_pos_x
            // 
            this.lbl_pos_x.AutoSize = true;
            this.lbl_pos_x.Location = new System.Drawing.Point(45, 239);
            this.lbl_pos_x.Name = "lbl_pos_x";
            this.lbl_pos_x.Size = new System.Drawing.Size(0, 13);
            this.lbl_pos_x.TabIndex = 2;
            this.lbl_pos_x.Click += new System.EventHandler(this.lbl_pos_x_Click);
            // 
            // lbl_pos_y
            // 
            this.lbl_pos_y.AutoSize = true;
            this.lbl_pos_y.Location = new System.Drawing.Point(51, 243);
            this.lbl_pos_y.Name = "lbl_pos_y";
            this.lbl_pos_y.Size = new System.Drawing.Size(0, 13);
            this.lbl_pos_y.TabIndex = 3;
            this.lbl_pos_y.Click += new System.EventHandler(this.lbl_pos_y_Click);
            // 
            // lbl_resol
            // 
            this.lbl_resol.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_resol.ForeColor = System.Drawing.Color.Navy;
            this.lbl_resol.Location = new System.Drawing.Point(249, 107);
            this.lbl_resol.Name = "lbl_resol";
            this.lbl_resol.Size = new System.Drawing.Size(79, 21);
            this.lbl_resol.TabIndex = 4;
            this.lbl_resol.Text = "---";
            this.lbl_resol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(455, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(17, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "X";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MistyRose;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cbm_FG);
            this.panel1.Controls.Add(this.lbl_FG);
            this.panel1.Controls.Add(this.cbm_Cus);
            this.panel1.Controls.Add(this.lbl_Cus);
            this.panel1.Location = new System.Drawing.Point(3, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 204);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lbl_Filepath);
            this.panel2.Controls.Add(this.lbl_Path);
            this.panel2.Controls.Add(this.lbl_Part);
            this.panel2.Controls.Add(this.lbl_Mdl);
            this.panel2.Controls.Add(this.part);
            this.panel2.Controls.Add(this.lbl_resol);
            this.panel2.Controls.Add(this.lbl_Model);
            this.panel2.Controls.Add(this.lbl_Release);
            this.panel2.Controls.Add(this.lbl_Cap);
            this.panel2.Controls.Add(this.lbl_capacity);
            this.panel2.Controls.Add(this.lbl_Ver);
            this.panel2.Controls.Add(this.Date);
            this.panel2.Controls.Add(this.SW);
            this.panel2.Controls.Add(this.lbl__Freq);
            this.panel2.Controls.Add(this.Frequence);
            this.panel2.Font = new System.Drawing.Font("Lucida Bright", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.SandyBrown;
            this.panel2.Location = new System.Drawing.Point(0, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 160);
            this.panel2.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(193, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "Resol : ";
            // 
            // lbl_Filepath
            // 
            this.lbl_Filepath.AutoSize = true;
            this.lbl_Filepath.BackColor = System.Drawing.Color.MistyRose;
            this.lbl_Filepath.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Filepath.ForeColor = System.Drawing.Color.Black;
            this.lbl_Filepath.Location = new System.Drawing.Point(52, 136);
            this.lbl_Filepath.Name = "lbl_Filepath";
            this.lbl_Filepath.Size = new System.Drawing.Size(19, 15);
            this.lbl_Filepath.TabIndex = 17;
            this.lbl_Filepath.Text = "---";
            // 
            // lbl_Path
            // 
            this.lbl_Path.AutoSize = true;
            this.lbl_Path.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Path.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Path.ForeColor = System.Drawing.Color.Black;
            this.lbl_Path.Location = new System.Drawing.Point(3, 132);
            this.lbl_Path.Name = "lbl_Path";
            this.lbl_Path.Size = new System.Drawing.Size(49, 19);
            this.lbl_Path.TabIndex = 16;
            this.lbl_Path.Text = "Path :";
            // 
            // lbl_Part
            // 
            this.lbl_Part.AutoSize = true;
            this.lbl_Part.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Part.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Part.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Part.Location = new System.Drawing.Point(263, 77);
            this.lbl_Part.Name = "lbl_Part";
            this.lbl_Part.Size = new System.Drawing.Size(33, 19);
            this.lbl_Part.TabIndex = 15;
            this.lbl_Part.Text = "---";
            // 
            // lbl_Mdl
            // 
            this.lbl_Mdl.AutoSize = true;
            this.lbl_Mdl.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Mdl.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mdl.ForeColor = System.Drawing.Color.Black;
            this.lbl_Mdl.Location = new System.Drawing.Point(4, 15);
            this.lbl_Mdl.Name = "lbl_Mdl";
            this.lbl_Mdl.Size = new System.Drawing.Size(64, 19);
            this.lbl_Mdl.TabIndex = 3;
            this.lbl_Mdl.Text = "Model : ";
            // 
            // part
            // 
            this.part.AutoSize = true;
            this.part.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.part.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.part.ForeColor = System.Drawing.Color.Black;
            this.part.Location = new System.Drawing.Point(192, 77);
            this.part.Name = "part";
            this.part.Size = new System.Drawing.Size(80, 19);
            this.part.TabIndex = 14;
            this.part.Text = "Part NO : ";
            this.part.Click += new System.EventHandler(this.part_Click);
            // 
            // lbl_Model
            // 
            this.lbl_Model.AutoSize = true;
            this.lbl_Model.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Model.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Model.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Model.Location = new System.Drawing.Point(70, 16);
            this.lbl_Model.Name = "lbl_Model";
            this.lbl_Model.Size = new System.Drawing.Size(33, 19);
            this.lbl_Model.TabIndex = 5;
            this.lbl_Model.Text = "---";
            // 
            // lbl_Release
            // 
            this.lbl_Release.AutoSize = true;
            this.lbl_Release.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Release.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Release.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Release.Location = new System.Drawing.Point(281, 47);
            this.lbl_Release.Name = "lbl_Release";
            this.lbl_Release.Size = new System.Drawing.Size(33, 19);
            this.lbl_Release.TabIndex = 13;
            this.lbl_Release.Text = "---";
            this.lbl_Release.Click += new System.EventHandler(this.label12_Click);
            // 
            // lbl_Cap
            // 
            this.lbl_Cap.AutoSize = true;
            this.lbl_Cap.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Cap.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Cap.ForeColor = System.Drawing.Color.Black;
            this.lbl_Cap.Location = new System.Drawing.Point(4, 47);
            this.lbl_Cap.Name = "lbl_Cap";
            this.lbl_Cap.Size = new System.Drawing.Size(80, 19);
            this.lbl_Cap.TabIndex = 6;
            this.lbl_Cap.Text = "Capacity : ";
            // 
            // lbl_capacity
            // 
            this.lbl_capacity.AutoSize = true;
            this.lbl_capacity.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_capacity.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_capacity.ForeColor = System.Drawing.Color.Navy;
            this.lbl_capacity.Location = new System.Drawing.Point(78, 47);
            this.lbl_capacity.Name = "lbl_capacity";
            this.lbl_capacity.Size = new System.Drawing.Size(33, 19);
            this.lbl_capacity.TabIndex = 7;
            this.lbl_capacity.Text = "---";
            // 
            // lbl_Ver
            // 
            this.lbl_Ver.AutoSize = true;
            this.lbl_Ver.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl_Ver.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Ver.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Ver.Location = new System.Drawing.Point(77, 77);
            this.lbl_Ver.Name = "lbl_Ver";
            this.lbl_Ver.Size = new System.Drawing.Size(33, 19);
            this.lbl_Ver.TabIndex = 11;
            this.lbl_Ver.Text = "---";
            // 
            // Date
            // 
            this.Date.AutoSize = true;
            this.Date.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Date.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date.ForeColor = System.Drawing.Color.Black;
            this.Date.Location = new System.Drawing.Point(192, 47);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(96, 19);
            this.Date.TabIndex = 12;
            this.Date.Text = "Release Date:";
            this.Date.Click += new System.EventHandler(this.Date_Click);
            // 
            // SW
            // 
            this.SW.AutoSize = true;
            this.SW.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.SW.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SW.ForeColor = System.Drawing.Color.Black;
            this.SW.Location = new System.Drawing.Point(4, 77);
            this.SW.Name = "SW";
            this.SW.Size = new System.Drawing.Size(85, 19);
            this.SW.TabIndex = 10;
            this.SW.Text = "S/W Ver : ";
            // 
            // lbl__Freq
            // 
            this.lbl__Freq.AutoSize = true;
            this.lbl__Freq.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lbl__Freq.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl__Freq.ForeColor = System.Drawing.Color.Black;
            this.lbl__Freq.Location = new System.Drawing.Point(192, 15);
            this.lbl__Freq.Name = "lbl__Freq";
            this.lbl__Freq.Size = new System.Drawing.Size(55, 19);
            this.lbl__Freq.TabIndex = 8;
            this.lbl__Freq.Text = "Freq : ";
            // 
            // Frequence
            // 
            this.Frequence.AutoSize = true;
            this.Frequence.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Frequence.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frequence.ForeColor = System.Drawing.Color.Navy;
            this.Frequence.Location = new System.Drawing.Point(242, 15);
            this.Frequence.Name = "Frequence";
            this.Frequence.Size = new System.Drawing.Size(33, 19);
            this.Frequence.TabIndex = 9;
            this.Frequence.Text = "---";
            // 
            // cbm_FG
            // 
            this.cbm_FG.BackColor = System.Drawing.Color.MistyRose;
            this.cbm_FG.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbm_FG.ForeColor = System.Drawing.Color.Black;
            this.cbm_FG.FormattingEnabled = true;
            this.cbm_FG.Location = new System.Drawing.Point(328, 9);
            this.cbm_FG.Name = "cbm_FG";
            this.cbm_FG.Size = new System.Drawing.Size(136, 24);
            this.cbm_FG.TabIndex = 1;
            this.cbm_FG.SelectedIndexChanged += new System.EventHandler(this.cbm_FG_SelectedIndexChanged);
            // 
            // lbl_FG
            // 
            this.lbl_FG.AutoSize = true;
            this.lbl_FG.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FG.ForeColor = System.Drawing.Color.Black;
            this.lbl_FG.Location = new System.Drawing.Point(282, 12);
            this.lbl_FG.Name = "lbl_FG";
            this.lbl_FG.Size = new System.Drawing.Size(40, 18);
            this.lbl_FG.TabIndex = 2;
            this.lbl_FG.Text = "FG  :";
            // 
            // cbm_Cus
            // 
            this.cbm_Cus.BackColor = System.Drawing.Color.MistyRose;
            this.cbm_Cus.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbm_Cus.ForeColor = System.Drawing.Color.Black;
            this.cbm_Cus.FormattingEnabled = true;
            this.cbm_Cus.Location = new System.Drawing.Point(100, 9);
            this.cbm_Cus.Name = "cbm_Cus";
            this.cbm_Cus.Size = new System.Drawing.Size(127, 24);
            this.cbm_Cus.TabIndex = 0;
            this.cbm_Cus.SelectedIndexChanged += new System.EventHandler(this.cbm_Cus_SelectedIndexChanged);
            // 
            // lbl_Cus
            // 
            this.lbl_Cus.AutoSize = true;
            this.lbl_Cus.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Cus.ForeColor = System.Drawing.Color.Black;
            this.lbl_Cus.Location = new System.Drawing.Point(10, 12);
            this.lbl_Cus.Name = "lbl_Cus";
            this.lbl_Cus.Size = new System.Drawing.Size(86, 18);
            this.lbl_Cus.TabIndex = 0;
            this.lbl_Cus.Text = "Customer :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 27);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(121, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "SPD - AUTOMATION";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbl_App_ver
            // 
            this.lbl_App_ver.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_App_ver.Location = new System.Drawing.Point(386, 2);
            this.lbl_App_ver.Name = "lbl_App_ver";
            this.lbl_App_ver.Size = new System.Drawing.Size(61, 21);
            this.lbl_App_ver.TabIndex = 9;
            this.lbl_App_ver.Text = "1.0.0";
            this.lbl_App_ver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(478, 272);
            this.Controls.Add(this.lbl_App_ver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_pos_y);
            this.Controls.Add(this.lbl_pos_x);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1035, 45);
            this.Name = "SPD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pos_x;
        private System.Windows.Forms.Label lbl_pos_y;
        private System.Windows.Forms.Label lbl_resol;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Cap;
        private System.Windows.Forms.Label lbl_Model;
        private System.Windows.Forms.ComboBox cbm_FG;
        private System.Windows.Forms.Label lbl_Mdl;
        private System.Windows.Forms.Label lbl_FG;
        private System.Windows.Forms.ComboBox cbm_Cus;
        private System.Windows.Forms.Label lbl_Cus;
        private System.Windows.Forms.Label lbl_Part;
        private System.Windows.Forms.Label part;
        private System.Windows.Forms.Label lbl_Release;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.Label lbl_Ver;
        private System.Windows.Forms.Label SW;
        private System.Windows.Forms.Label Frequence;
        private System.Windows.Forms.Label lbl__Freq;
        private System.Windows.Forms.Label lbl_capacity;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Filepath;
        private System.Windows.Forms.Label lbl_Path;
        private System.Windows.Forms.Label lbl_App_ver;
        private System.Windows.Forms.Label label3;
    }
}


















