namespace SPD_Write_Bot
{
    partial class ResultDisplay
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
            this.dgvEntryValue = new System.Windows.Forms.DataGridView();
            this.dgvHexValue = new System.Windows.Forms.DataGridView();
            this.Customer_Name = new System.Windows.Forms.Label();
            this.Serial_Number = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntryValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHexValue)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEntryValue
            // 
            this.dgvEntryValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntryValue.Location = new System.Drawing.Point(17, 32);
            this.dgvEntryValue.Name = "dgvEntryValue";
            this.dgvEntryValue.Size = new System.Drawing.Size(346, 180);
            this.dgvEntryValue.TabIndex = 0;
            // 
            // dgvHexValue
            // 
            this.dgvHexValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHexValue.Location = new System.Drawing.Point(12, 227);
            this.dgvHexValue.Name = "dgvHexValue";
            this.dgvHexValue.Size = new System.Drawing.Size(351, 296);
            this.dgvHexValue.TabIndex = 1;
            // 
            // Customer_Name
            // 
            this.Customer_Name.AutoSize = true;
            this.Customer_Name.Location = new System.Drawing.Point(14, 9);
            this.Customer_Name.Name = "Customer_Name";
            this.Customer_Name.Size = new System.Drawing.Size(85, 13);
            this.Customer_Name.TabIndex = 2;
            this.Customer_Name.Text = "Customer_Name";
            // 
            // Serial_Number
            // 
            this.Serial_Number.AutoSize = true;
            this.Serial_Number.Location = new System.Drawing.Point(150, 9);
            this.Serial_Number.Name = "Serial_Number";
            this.Serial_Number.Size = new System.Drawing.Size(76, 13);
            this.Serial_Number.TabIndex = 3;
            this.Serial_Number.Text = "Serial_Number";
            // 
            // ResultDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 535);
            this.Controls.Add(this.Serial_Number);
            this.Controls.Add(this.Customer_Name);
            this.Controls.Add(this.dgvHexValue);
            this.Controls.Add(this.dgvEntryValue);
            this.Name = "ResultDisplay";
            this.Text = "ResultDisplay";
            this.Load += new System.EventHandler(this.ResultDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntryValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHexValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEntryValue;
        private System.Windows.Forms.DataGridView dgvHexValue;
        private System.Windows.Forms.Label Customer_Name;
        private System.Windows.Forms.Label Serial_Number;
    }
}