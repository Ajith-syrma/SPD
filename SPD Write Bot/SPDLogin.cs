using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPD_Write_Bot
{
    public partial class frmSPDLogin : Form
    {
        public string Error_filepath = ConfigurationManager.AppSettings["FilePath1"];
        public frmSPDLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmSPDLogin_Load(object sender, EventArgs e)
        {
            txtEmpid.Height = 30;
            txtEmpName.Height = 30;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    GraphicsPath path = new GraphicsPath();
        //    path.AddEllipse(0, 0, this.Width, this.Height);
        //    this.Region = new Region(path);
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool login = false;
            try
            {
                if (txtEmpid.Text.ToString() != string.Empty && txtEmpName.Text.ToString() != string.Empty)
                {
                    if (txtEmpid.Text.Length > 4 && txtEmpName.Text.Length > 3)
                        login = true;
                    else
                    {
                        login = false;
                        MessageBox.Show("Emp Id should be greater than 4 and Emp Name should be greater than 3");
                    }
                }
                else
                {
                    login = false;
                    MessageBox.Show("Please Enter Valid input");
                }

                if (login)
                {
                    writeErrorMessage("Login successfull", txtEmpid.Text.ToString(), txtEmpName.Text.ToString());
                    SPD sPD = new SPD();
                    sPD.Show();
                }
                else
                    writeErrorMessage("Login Failed", txtEmpid.Text.ToString(), txtEmpName.Text.ToString());

            }
            catch
            {

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpid.Text=string.Empty;
            txtEmpName.Text=string.Empty;
        }

        public void writeErrorMessage(string Message, string userid, string username)
        {
            // Ensure the directory exists
            string systemPath = Error_filepath;
            if (!Directory.Exists(systemPath))
            {
                Directory.CreateDirectory(systemPath);
            }

            // Prepare log file path
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            string errorLogFileName = $"ErrorLog_{currentDate}.txt";
            string errorLogPath = Path.Combine(systemPath, errorLogFileName);

            // Write to log file
            using (StreamWriter errLogs = new StreamWriter(errorLogPath, true))
            {
                errLogs.WriteLine("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                errLogs.WriteLine("---------------------------------------------------" + DateTime.Now + "----------------------------------------------" + Environment.NewLine);
                errLogs.WriteLine($"Log Message: {Message}" + Environment.NewLine);
                errLogs.WriteLine($"User ID: {userid}" + Environment.NewLine);
                errLogs.WriteLine($"User Name: {username}");
                errLogs.Close();
            }
        }
    }
}
