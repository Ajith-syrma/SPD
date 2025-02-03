using Microsoft.Win32.SafeHandles;
using SPD_Write_Bot.DisableDevice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.IO;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SPD_Write_Bot
{
    public partial class SPD : Form
    {
        public SPD()
        {
            EnableMouse(true);
            InitializeComponent();
        }


        private readonly Configuration _configuration;
        public string Serial;
        public string SerialFull;
        public string dec_serial;
        public string server_datetime;
        public DateTime currentDate;
        public int weekNumber,year;
        public string input,data;
        public string PCB_ID;
        // Connection string for SQL Server
        //public string conn_str1 = "Data Source=192.168.1.181;Initial Catalog=SFCS;User ID=sa;Password=Syrma@2022;MultipleActiveResultSets=true";
        public String conn_str1 = "Data Source=192.168.1.183;Initial Catalog=DIVYA;User ID=Dhivya;Password=1996Divya;MultipleActiveResultSets=True";
        public SqlConnection cnn;
        public SqlCommand cmd;
        public string Resol;
        public string system_datetime;
        public int AppResol=0;
        public string NetworkPath;
        public string username = "Sfcs1@syrmatech.local";
        public string Password = "Syrma@2023";
        static string SPD_Value = "";
        static string App_ver = ConfigurationManager.AppSettings["App_Ver"];
        static string errorMessage = string.Empty;
        public string Error_filepath = ConfigurationManager.AppSettings["FilePath1"];
        checkBoxAutomation clsCheckboxValivation = new checkBoxAutomation();
        TableValue clsTableValue=new TableValue();
        Process process = new Process();
        genclass rowDetails = new genclass();
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        public string combinedValue;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        static string CaseSelection(string value)
        {
            string val;

            switch (value.ToUpper())
            {
                case "A" :
                    val = "41";
                    break;
                case "B":
                    val = "42";
                    break;
                case "C":
                    val = "43";
                    break;
                case "D":
                    val = "44";
                    break;
                case "E":
                    val = "45";
                    break;
                case "F":
                    val = "46";
                    break;
                default:
                    val = "3" + value;
                    break;
            }
            SPD_Value += val + ","; // Accumulate the values in SPD_Value
            return val;
        }
        //private void Form1_Load(object sender, EventArgs e)
        //{

        //    cnn = new SqlConnection(conn_str1);


        //    /////// extra inclued for all resolution screens

        //    //Get screen resolution
        //    Rectangle res = Screen.PrimaryScreen.Bounds;
        //    // Calculate location (etc. 1366 Width - form size...)
        //    this.Location = new Point(res.Width - Size.Width);


        //    if (ConfigurationManager.AppSettings["Resolution"]=="1366x768")
        //    {
        //        lbl_resol.Text =  ConfigurationManager.AppSettings["Resolution"];
        //        AppResol = 1;
        //    }
        //    else if (ConfigurationManager.AppSettings["Resolution"] == "1024x768")
        //    {
        //        lbl_resol.Text =  ConfigurationManager.AppSettings["Resolution"];
        //        AppResol = 2;
        //    }
        //    else
        //    {
        //        MessageBox.Show("UnSupported Resolution. Please Change Resolution");
        //        Application.Exit();
        //    }

        //    cbm_Cus.Items.Clear(); 
        //    cbm_Cus.Items.Add("--Select--");
        //    cbm_Cus.Items.Add("BIWIN");
        //    cbm_Cus.Items.Add("ESSENCORE");

        //    cbm_Cus.SelectedIndex = 0;
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            if (App_ver == lbl_App_ver.Text)

            {
                //string resolution = GetScreenResolution(screen);
                var screen = Screen.PrimaryScreen;
                var width = screen.Bounds.Width;
                var height = screen.Bounds.Height;
                string resolution = $"{width}x{height}";

                if (resolution != ConfigurationManager.AppSettings["Resolution"])
                {
                    MessageBox.Show("Check the resolution set it to 1366x768");                   
                    Application.Exit(); 
                }

                // Variable to store error messages
                try
                {
                    cnn = new SqlConnection(conn_str1);

                    // Get screen resolution
                    Rectangle res = Screen.PrimaryScreen.Bounds;

                    // Calculate location (etc. 1366 Width - form size...)
                    this.Location = new Point(res.Width - Size.Width);

                    if (ConfigurationManager.AppSettings["Resolution"] == "1366x768")
                    {
                        lbl_resol.Text = ConfigurationManager.AppSettings["Resolution"];
                        AppResol = 1;
                    }
                    else if (ConfigurationManager.AppSettings["Resolution"] == "1024x768")
                    {
                        lbl_resol.Text = ConfigurationManager.AppSettings["Resolution"];
                        AppResol = 2;

                    }
                    else
                    {
                        throw new Exception("Unsupported Resolution. Please Change Resolution");
                    }

                    // Populate ComboBox
                    cbm_Cus.Items.Clear();
                    cbm_Cus.Items.Add("--Select--");
                    cbm_Cus.Items.Add("BIWIN");
                    cbm_Cus.Items.Add("ESSENCORE");
                    cbm_Cus.Items.Add("HITACHI");
                    cbm_Cus.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message; // Save error in the variable
                    MessageBox.Show(errorMessage); // Display error message
                    Application.Exit(); // Exit the application if an error occurs
                }


            }
            else 
            {
                MessageBox.Show("You are not running latest application. Please download the same from STAC Applicatin Portal"); // Display error message
                Application.Exit();
            }


        }
        public string GetScreenResolution(Screen screen)
        {
            var width = screen.Bounds.Width;
            var height = screen.Bounds.Height;
            return $"{width} x {height}";
        }

        public int getYrCode(char YRC)
        {
            if (YRC == 'M') { return 22; }
            else if (YRC == 'N') { return 23; }
            else if (YRC == 'O') { return 24; }
            else if (YRC == 'P') { return 25; }
            else if (YRC == 'Q') { return 26; }
            else return 0;

        }

        public int getYrCodeBW(char YRC)
        {
            if (YRC == '1') { return 21; }
            else if (YRC == '2') { return 22; }
            else if (YRC == '3') { return 23; }
            else if (YRC == '4') { return 24; }
            else if (YRC == '5') { return 25; }
            else return 0;

        }

        public int getWKCode()
        {
            currentDate = DateTime.Now;
            CultureInfo culture = CultureInfo.CurrentCulture;
            Calendar calendar = culture.Calendar;
           weekNumber = calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            year = currentDate.Year;
           return weekNumber;
        }

        [DllImport("user32.dll")]
        static extern bool BlockInput(bool fBlockIt);
        [DllImport("user32.dll")]
        static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ReleaseCapture();

        public void EnableMouse(bool flag)
        {
            if(ConfigurationManager.AppSettings["ClassGuid"]!="")
            {
                Guid mouseGuid = new Guid(ConfigurationManager.AppSettings["ClassGuid"]);

                // get this from the properties dialog box of this device in Device Manager
                string instancePath = ConfigurationManager.AppSettings["InstancePath"];

                DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, flag);
            }
            if(ConfigurationManager.AppSettings["ClassGuidK"]!="")
            {
                Guid KeyBoardGuid = new Guid(ConfigurationManager.AppSettings["ClassGuidK"]);

                // get this from the properties dialog box of this device in Device Manager
                string KinstancePath = ConfigurationManager.AppSettings["InstancePathK"];

                DeviceHelper.SetDeviceEnabled(KeyBoardGuid, KinstancePath, flag);
            }
        }
        private void get_server_datetime()
        {
            cnn.Close();
            SqlCommand cmd = new SqlCommand("SELECT FORMAT (getdate(), 'dd-MM-yyyy HH:mm:ss.ffff') as date ;", cnn);
            if (cnn.State == ConnectionState.Closed) cnn.Open();
            server_datetime = Convert.ToString(cmd.ExecuteScalar());
            cnn.Close();
            system_datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ffff");
        }

       

        public void BotStart1()
        {
                        
            EnableMouse(false);
            SimulateLeftMouseClick(143, 124);
            Thread.Sleep(1000);

            IntPtr hwnd = FindWindow(null, "Load SPD Date File From Library");

            // Move the window to (100, 100)
            SetWindowPos(hwnd, IntPtr.Zero, 434, 246, 0, 0, SWP_NOSIZE | SWP_NOZORDER);


            SimulateLeftMouseClick(573, 480);
            Thread.Sleep(100);
            SimulateLeftMouseClick(573, 480);
            Thread.Sleep(100);
            SendKeys.SendWait(ConfigurationManager.AppSettings["FilePath"]);
            Thread.Sleep(800);
            SimulateLeftMouseClick(817,475);
            Thread.Sleep(100);


            SimulateLeftMouseClick(514, 353);
            Thread.Sleep(100);
          
            SimulateLeftMouseClick(817, 475);
            Thread.Sleep(100);
            
            //1.Write SPD DATA to Module buttClick 378,124.
            SimulateLeftMouseClick(375, 120);
            Thread.Sleep(500);
           
            //2.Edit Configuration buttClick 681,343.
            SimulateLeftMouseClick(681, 343);
            Thread.Sleep(500);
            //3.1st cfg ButtClick 722,395.
            SimulateLeftMouseClick(722, 395);
            Thread.Sleep(500);
           
            //4.Serial number textbox click 688,355
            SimulateLeftMouseClick(688, 355);
            Thread.Sleep(500);

            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            MessageBox.Show("check");
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);

            SendKeys.SendWait(Serial);

            // Wait for the text to be entered
            Thread.Sleep(500);


            //5.OK buttClick 856,397
            SimulateLeftMouseClick(856, 397);
            Thread.Sleep(500);

            //2nd cfg ButtClick 719,426
            SimulateLeftMouseClick(719, 426);
            Thread.Sleep(500);
            // USER WEEK BALLOON
            SimulateLeftMouseClick(733, 307);
            Thread.Sleep(500);
            //yr buttClick 781,381
            SimulateLeftMouseClick(781, 381);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait(getYrCode(char.Parse(SerialFull.Substring(1, 1))).ToString());
            Thread.Sleep(500);
            //Wk buttClick 781,412
            SimulateLeftMouseClick(781, 412);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait(int.Parse(SerialFull.Substring(2, 2)).ToString());
            Thread.Sleep(500);
            //ok buttClick 787,464
            SimulateLeftMouseClick(787, 464);
            Thread.Sleep(500);

            //6.OK buttClick 854,157
            SimulateLeftMouseClick(854, 157);
            Thread.Sleep(500);
            //7.OK buttClick 595,482
            //SimulateLeftMouseClick(595, 482);
            //Thread.Sleep(250);
            //8.Edit SPD DATA buttClick 675,381
            SimulateLeftMouseClick(675, 381);
            Thread.Sleep(800);
            //9.Jump to Text Click 528,553
            SimulateLeftMouseClick(528, 553);
            SendKeys.SendWait("353");
            Thread.Sleep(50);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(800);

            SimulateLeftMouseClick(640, 246);
            Thread.Sleep(50);
            SimulateLeftMouseClick(640, 246);
            Thread.Sleep(50);
            SendKeys.SendWait("^C");
            Thread.Sleep(50);
            if (Clipboard.GetText() == "Data")
            {
                //10.
                SimulateLeftMouseClick(760, 265);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                //MessageBox.Show(SerialFull.Substring(1, 1));
                if (Control.IsKeyLocked(Keys.CapsLock))
                {
                    SendKeys.SendWait((Convert.ToInt32(Char.Parse(SerialFull.Substring(1, 1))).ToString("X2").ToString()).ToLower());

                }
                else
                {
                    SendKeys.SendWait((Convert.ToInt32(Char.Parse(SerialFull.Substring(1, 1))).ToString("X2").ToString()).ToUpper());

                }


                //SendKeys.SendWait((Encoding.ASCII.GetBytes(SerialFull.Substring(1,1))).ToString());
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 287);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + SerialFull.Substring(2, 1));

                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 308);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + SerialFull.Substring(3, 1));

                Thread.Sleep(500);

                //760,390
                SimulateLeftMouseClick(760, 390);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(0, 1));
                Thread.Sleep(500);

                //760,410
                SimulateLeftMouseClick(760, 410);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(1, 1));
                Thread.Sleep(500);
                //760,430
                SimulateLeftMouseClick(760, 430);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(2, 1));
                Thread.Sleep(500);
                //760,450
                SimulateLeftMouseClick(760, 450);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(3, 1));
                Thread.Sleep(500);
                //760,470
                SimulateLeftMouseClick(760, 470);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(4, 1));
                Thread.Sleep(500);
                SimulateLeftMouseClick(935, 395);
                Thread.Sleep(500);
                SimulateLeftMouseClick(635, 505);
                Thread.Sleep(2000);

                textBox1.Clear();
                textBox1.Select();
                SerialFull = "";
                Serial = "";
            }
            else
            {
                this.BackColor = Color.Red;
            }
            
            EnableMouse(true);
           
        }
        public void BotStart2()
        {
           
            EnableMouse(false);

            SimulateLeftMouseClick(144, 122);
            Thread.Sleep(1000);

            IntPtr hwnd = FindWindow(null, "Load SPD Date File From Library");

            // Move the window to (100, 100)
            SetWindowPos(hwnd, IntPtr.Zero, 451, 253, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
            Thread.Sleep(500);
            SimulateLeftMouseClick(588, 487);
            Thread.Sleep(100);
            SimulateLeftMouseClick(588, 487);
            Thread.Sleep(100);
            SendKeys.SendWait(ConfigurationManager.AppSettings["FilePath"]);
            Thread.Sleep(500);
            SimulateLeftMouseClick(837,485);
            Thread.Sleep(800);


            SimulateLeftMouseClick(527, 342);
            Thread.Sleep(500);
            SimulateLeftMouseClick(837, 485);
            Thread.Sleep(500);


            //1.Write SPD DATA to Module buttClick 378,124.
            SimulateLeftMouseClick(380, 123);
            Thread.Sleep(500);
            //2.Edit Configuration buttClick 681,343.
            SimulateLeftMouseClick(510, 342);
            Thread.Sleep(500);
            //3.1st cfg ButtClick 722,395.
            SimulateLeftMouseClick(548, 395);
            Thread.Sleep(500);
            //4.Serial number textbox click 688,355
            SimulateLeftMouseClick(504, 356);
            Thread.Sleep(500);

            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);

            SendKeys.SendWait(Serial);

            // Wait for the text to be entered
            Thread.Sleep(500);


            //5.OK buttClick 856,397
            SimulateLeftMouseClick(686, 395);
            Thread.Sleep(500);

            //2nd cfg ButtClick 719,426
            SimulateLeftMouseClick(550, 426);
            Thread.Sleep(500);
            // USER WEEK BALLOON
            SimulateLeftMouseClick(564, 307);
            Thread.Sleep(500);
            //yr buttClick 781,381
            SimulateLeftMouseClick(610, 382);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait(getYrCode(char.Parse(SerialFull.Substring(1, 1))).ToString());
            Thread.Sleep(500);
            //Wk buttClick 781,412
            SimulateLeftMouseClick(611, 413);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait(int.Parse(SerialFull.Substring(2, 2)).ToString());
            Thread.Sleep(500);
            //ok buttClick 787,464
            SimulateLeftMouseClick(615, 466);
            Thread.Sleep(500);

            //6.OK buttClick 854,157
            SimulateLeftMouseClick(683, 160);
            Thread.Sleep(500);
            //7.OK buttClick 595,482
            //SimulateLeftMouseClick(595, 482);
            //Thread.Sleep(250);
            //8.Edit SPD DATA buttClick 675,381
            SimulateLeftMouseClick(507, 380);
            Thread.Sleep(800);
            //9.Jump to Text Click 528,553
            SimulateLeftMouseClick(358, 555);
            SendKeys.SendWait("353");
            Thread.Sleep(50);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(800);


            SimulateLeftMouseClick(476, 246);
            Thread.Sleep(50);
            SimulateLeftMouseClick(476, 246);
            Thread.Sleep(50);
            SendKeys.SendWait("^C");
            Thread.Sleep(50);


            if (Clipboard.GetText() == "Data")
            {
                SimulateLeftMouseClick(591, 267);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                //MessageBox.Show(SerialFull.Substring(1, 1));
                if (Control.IsKeyLocked(Keys.CapsLock))
                {
                    SendKeys.SendWait((Convert.ToInt32(Char.Parse(SerialFull.Substring(1, 1))).ToString("X2").ToString()).ToLower());

                }
                else
                {
                    SendKeys.SendWait((Convert.ToInt32(Char.Parse(SerialFull.Substring(1, 1))).ToString("X2").ToString()).ToUpper());
                }


                //SendKeys.SendWait((Encoding.ASCII.GetBytes(SerialFull.Substring(1,1))).ToString());
                Thread.Sleep(500);

                SimulateLeftMouseClick(591, 287);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);

                SendKeys.SendWait("3" + SerialFull.Substring(2, 1));

                Thread.Sleep(500);

                SimulateLeftMouseClick(591, 308);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);

                SendKeys.SendWait("3" + SerialFull.Substring(3, 1));

                Thread.Sleep(500);

                //760,390
                SimulateLeftMouseClick(591, 390);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);

                SendKeys.SendWait("3" + Serial.Substring(0, 1));
                Thread.Sleep(500);

                //760,410
                SimulateLeftMouseClick(591, 410);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(1, 1));
                Thread.Sleep(500);
                //760,430
                SimulateLeftMouseClick(591, 430);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(2, 1));
                Thread.Sleep(500);
                //760,450
                SimulateLeftMouseClick(591, 450);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);

                SendKeys.SendWait("3" + Serial.Substring(3, 1));
                Thread.Sleep(500);
                //760,470
                SimulateLeftMouseClick(591, 470);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(50);
                SendKeys.SendWait("3" + Serial.Substring(4, 1));
                Thread.Sleep(500);
                SimulateLeftMouseClick(765, 393);
                Thread.Sleep(500);
                SimulateLeftMouseClick(463, 505);
                Thread.Sleep(2000);

                textBox1.Clear();
                textBox1.Select();
                SerialFull = "";
                Serial = "";
            }
            else
            {
                this.BackColor = Color.Red;
            }

                //10.
            
            EnableMouse(true);
            
        }
        public void BotStart3(string serial_number, string customer_name)
        {
            string result = "PASS";// Default result is success unless an error occurs
            string checkbx1status = "Default checkbox ticked";  // Initialize default values
            string checkbx2status = "Default checkbox ticked";// Initialize default values
                                                              // string serial_value = string.Empty;
            string errorfilepath = Error_filepath;
            try
            { 
           
            get_server_datetime();
            EnableMouse(false);
            SimulateLeftMouseClick(143, 124);
            Thread.Sleep(1000);

            IntPtr hwnd = FindWindow(null, "Load SPD Date File From Library");

            // Move the window to (100, 100)
            SetWindowPos(hwnd, IntPtr.Zero, 434, 246, 0, 0, SWP_NOSIZE | SWP_NOZORDER);


            SimulateLeftMouseClick(573, 480);
            Thread.Sleep(100);
            SimulateLeftMouseClick(573, 480);
            Thread.Sleep(100);
                SendKeys.SendWait(lbl_Filepath.Text);
                //SendKeys.SendWait(NetworkPath);
            Thread.Sleep(800);
            SimulateLeftMouseClick(817, 475);
            Thread.Sleep(100);


            SimulateLeftMouseClick(514, 353);
            Thread.Sleep(100);
            SimulateLeftMouseClick(817, 475);
            Thread.Sleep(100);

            // Write SPD DATA to Module button click
            SimulateLeftMouseClick(375, 125);
            Thread.Sleep(500);

            // Edit Configuration button click
            SimulateLeftMouseClick(680, 340);
            Thread.Sleep(500);


           

                var serialnumber = clsCheckboxValivation.checkBoxValidation();
                // 1st cfg button click
                if (serialnumber != null && serialnumber.Count > 0)
                {
                    var fristSerialNumber = serialnumber[0].ToString();
                    var secondSerialNumber = serialnumber[1].ToString();
                    if (fristSerialNumber != string.Empty)
                    {
                        if (fristSerialNumber == "false")
                        {
                            // 1st cfg button click
                            checkbx1status = "Manual check box ticked";
                            SimulateLeftMouseClick(488, 402);
                            Thread.Sleep(500);

                        }
                    }
                }
                SimulateLeftMouseClick(720, 395);
                Thread.Sleep(500);

                // Hex/Decimal (HEX) selection 
                SimulateLeftMouseClick(478, 395);
                Thread.Sleep(500);

                SimulateLeftMouseClick(646, 502);
                Thread.Sleep(500);

                // Serial number text box click
                 SimulateLeftMouseClick(660, 355);
                 Thread.Sleep(100);
                SimulateLeftMouseClick(678, 355);
                Thread.Sleep(100);

                SimulateLeftMouseClick(678, 355);
                Thread.Sleep(100);

                ////1.Write SPD DATA to Module buttClick 378,124.
                //SimulateLeftMouseClick(375, 120);
                //Thread.Sleep(500);
                ////2.Edit Configuration buttClick 681,343.
                //SimulateLeftMouseClick(681, 343);
                //Thread.Sleep(500);
                ////3.1st cfg ButtClick 722,395.
                //SimulateLeftMouseClick(722, 395);
                //Thread.Sleep(500);
                ////4.Serial number textbox click 688,355
                //SimulateLeftMouseClick(688, 355);
                //Thread.Sleep(500);

                SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);

            SendKeys.SendWait(Serial);

            // Wait for the text to be entered
            Thread.Sleep(500);


            //5.OK buttClick 856,397
            SimulateLeftMouseClick(856, 397);
            Thread.Sleep(500);

           

                if (serialnumber != null && serialnumber.Count > 0)
                {
                    var seoundSerialNumber = serialnumber[1].ToString();
                    if (seoundSerialNumber != string.Empty)
                    {
                        if (seoundSerialNumber == "false")
                        {
                            //2nd cfg ButtClick 719,426
                            checkbx2status = "Manual check box ticked";
                            SimulateLeftMouseClick(488, 427);
                            Thread.Sleep(500);
                        }
                    }
                }
                SimulateLeftMouseClick(719, 426);
                Thread.Sleep(500);
                string errorMessage = "Check Box status of the Serial Number";  // Default error message
                string functionName = "Biwin";
                string serial_value = textBox1.Text;
                // string errorfilepath = Error_filepath;
                writeErrorMessage(serial_value, checkbx1status, checkbx2status, Error_filepath, errorMessage, functionName);

                // USER WEEK BALLOON
                SimulateLeftMouseClick(733, 307);
            Thread.Sleep(500);
            //yr buttClick 781,381
            SimulateLeftMouseClick(781, 381);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait(getYrCodeBW(char.Parse(SerialFull.Substring(4, 1))).ToString());
            Thread.Sleep(500);
            //Wk buttClick 781,412
            SimulateLeftMouseClick(781, 412);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(50);
            SendKeys.SendWait(int.Parse(SerialFull.Substring(5, 2)).ToString());
            Thread.Sleep(500);
            //ok buttClick 787,464
            SimulateLeftMouseClick(787, 464);
            Thread.Sleep(500);

            //6.OK buttClick 854,157
            SimulateLeftMouseClick(854, 157);
            Thread.Sleep(500);
            //7.OK buttClick 595,482
            //SimulateLeftMouseClick(595, 482);
            //Thread.Sleep(250);
            //8.Edit SPD DATA buttClick 675,381
            SimulateLeftMouseClick(675, 381);
            Thread.Sleep(800);


            // Jump to Text button click
            SimulateLeftMouseClick(530, 551);
            Thread.Sleep(100);
            SimulateLeftMouseClick(530, 551);
            Thread.Sleep(100);
            SendKeys.SendWait("{BACKSPACE}");
            Thread.Sleep(100);
            SendKeys.SendWait("353");
            Thread.Sleep(50);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(50);


            SimulateLeftMouseClick(640, 246);
            Thread.Sleep(50);
            SimulateLeftMouseClick(640, 246);
            Thread.Sleep(50);
            SendKeys.SendWait("^C");
            Thread.Sleep(50);
            if (Clipboard.GetText() == "Data")
            {
                //10.
                //SimulateLeftMouseClick(760, 265);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                ////MessageBox.Show(SerialFull.Substring(1, 1));
                //if (Control.IsKeyLocked(Keys.CapsLock))
                //{
                //    SendKeys.SendWait((Convert.ToInt32(Char.Parse(SerialFull.Substring(1, 1))).ToString("X2").ToString()).ToLower());

                //}
                //else
                //{
                //    SendKeys.SendWait((Convert.ToInt32(Char.Parse(SerialFull.Substring(1, 1))).ToString("X2").ToString()).ToUpper());

                //}


                ////SendKeys.SendWait((Encoding.ASCII.GetBytes(SerialFull.Substring(1,1))).ToString());
                //Thread.Sleep(500);

                SimulateLeftMouseClick(760, 247);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("42");

                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 267);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("57");

                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 287);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                if (SerialFull.Substring(2, 1) == "B")
                {
                    SendKeys.SendWait("42");

                }
                else if (SerialFull.Substring(2, 1) == "A")
                {
                    SendKeys.SendWait("41");

                }
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 307);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(3, 1));

                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 327);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(4, 1));

                Thread.Sleep(500);

                //760,390
                SimulateLeftMouseClick(760, 347);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(5, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 367);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(6, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 387);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(7, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 407);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(8, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 427);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(9, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 447);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(10, 1));
                Thread.Sleep(500);


                SimulateLeftMouseClick(760, 467);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(11, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 487);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(12, 1));
                Thread.Sleep(500);

                SimulateLeftMouseClick(760, 507);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("3" + SerialFull.Substring(13, 1));
                Thread.Sleep(500);

                //SimulateLeftMouseClick(760, 527);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + SerialFull.Substring(14, 1));
                //Thread.Sleep(500);

                //760,410
                //SimulateLeftMouseClick(760, 410);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + Serial.Substring(1, 1));
                //Thread.Sleep(500);
                ////760,430
                //SimulateLeftMouseClick(760, 430);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + Serial.Substring(2, 1));
                //Thread.Sleep(500);
                ////760,450
                //SimulateLeftMouseClick(760, 450);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + Serial.Substring(3, 1));
                //Thread.Sleep(500);
                ////760,470
                //SimulateLeftMouseClick(760, 470);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + Serial.Substring(4, 1));
                //Thread.Sleep(500);

                //SimulateLeftMouseClick(760, 490);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + Serial.Substring(4, 1));
                //Thread.Sleep(500);

                //SimulateLeftMouseClick(760, 510);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("{BACKSPACE}");
                //Thread.Sleep(50);
                //SendKeys.SendWait("3" + Serial.Substring(4, 1));
                //Thread.Sleep(500);


                SimulateLeftMouseClick(935, 395);
                Thread.Sleep(500);
                SimulateLeftMouseClick(635, 505);
                Thread.Sleep(5000);

                    //Result Button Click
                    SimulateLeftMouseClick(683, 441);
                    Thread.Sleep(2000);

                    //compare icon Click
                    SimulateLeftMouseClick(305, 123);
                    Thread.Sleep(2000);

                    //alert message ok button Click
                    SimulateLeftMouseClick(698, 416);
                    Thread.Sleep(2000);

                    //buffer value and o0riginal value campare ok button Click
                    // SimulateLeftMouseClick(914, 641);
                    // Thread.Sleep(100);
                    EnableMouse(true);
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is ResultDisplay)
                        {
                            form.Close();
                            break;
                        }
                    }
                    rowDetails = clsTableValue.Program(customer_name, serial_number);
                   
                    ResultDisplay resultdisplay = new ResultDisplay(rowDetails, customer_name, serial_number);
                    resultdisplay.StartPosition = FormStartPosition.Manual;
                    resultdisplay.WindowState = FormWindowState.Normal;
                    resultdisplay.Location = new Point(0, 100);
                    resultdisplay.Show();
                   // resultdisplay.BringToFront();
                    resultdisplay.Activate();


                textBox1.Clear();
                textBox1.Select();
                SerialFull = "";
                Serial = "";
            }
            else
            {
                    result = "FAIL: Clipboard text was not 'Data'";
                    this.BackColor = Color.Red;
            }

            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }
            finally
            {
                EnableMouse(true);

            }
            // Ensure the database connection is closed even if an error occurs
            if (cnn.State == ConnectionState.Open) cnn.Close();

            SqlCommand cmd = new SqlCommand("INSERT INTO SPD_PROG_Log VALUES (@Customer, @FG, @Model,@PCBA_ID,@Capacity, @Frequency, @Version, @Release, @Part, @Result, @ServerDateTime, HOST_NAME(), @Running_Converse, @SPD_Value, @File_Path, @System_Datetime, '')", cnn);
            cmd.Parameters.AddWithValue("@Customer", cbm_Cus.Text);
            cmd.Parameters.AddWithValue("@FG", cbm_FG.Text);
            cmd.Parameters.AddWithValue("@Model", lbl_Model.Text);
            cmd.Parameters.AddWithValue("@PCBA_ID", PCB_ID);
            cmd.Parameters.AddWithValue("@Capacity", lbl_capacity.Text);
            cmd.Parameters.AddWithValue("@Frequency", Frequence.Text);
            cmd.Parameters.AddWithValue("@Version", lbl_Ver.Text);
            cmd.Parameters.AddWithValue("@Release", lbl_Release.Text);
            cmd.Parameters.AddWithValue("@Part", lbl_Part.Text);
            cmd.Parameters.AddWithValue("@Result", result);
            cmd.Parameters.AddWithValue("@ServerDateTime", server_datetime);
            cmd.Parameters.AddWithValue("@Running_Converse", combinedValue);
            cmd.Parameters.AddWithValue("@SPD_Value", SPD_Value);
            cmd.Parameters.AddWithValue("@File_Path", lbl_Filepath.Text);
            cmd.Parameters.AddWithValue("@System_Datetime", system_datetime);

            if (cnn.State == ConnectionState.Closed) cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            //cnn.Close();
            //SqlCommand cmd = new SqlCommand("INSERT INTO SPD_PROG_Log VALUES ('" + cbm_Cus.Text + "','" + cbm_FG.Text + "','" + lbl_Model.Text + "','" + PCB_ID + "','" + lbl_capacity.Text + "','" + Frequence.Text + "','" + lbl_Ver.Text + "','" + lbl_Release.Text + "','" + lbl_Part.Text + "','" + result + "','" + server_datetime + "',HOST_NAME(),'','','','','')", cnn);
            //if (cnn.State == ConnectionState.Closed) cnn.Open();
            //cmd.ExecuteNonQuery();
            //cnn.Close();
            SPD_Value = "";
            
        }
        public string BotStart4(string serial_number, string customer_name)
        {
            string result = "PASS"; // Default result is success unless an error occurs
           
            string checkbx1status = "Default checkbox ticked";  // Initialize default values
            string checkbx2status = "Default checkbox ticked";// Initialize default values
           // string serial_value = string.Empty;
           string errorfilepath = Error_filepath;
            try
            {
                get_server_datetime();
                EnableMouse(false);

                Thread.Sleep(300);

                SimulateLeftMouseClick(140, 120);
                Thread.Sleep(1000);

                IntPtr hwnd = FindWindow(null, "Load SPD Date File From Library");

                // Move the window to (451, 253)
                SetWindowPos(hwnd, IntPtr.Zero, 451, 253, 0, 0, SWP_NOSIZE | SWP_NOZORDER);

                // File path text box click
                SimulateLeftMouseClick(588, 487);
                Thread.Sleep(100);

                SimulateLeftMouseClick(588, 487);
                Thread.Sleep(100);
                //SendKeys.SendWait(NetworkPath);
                SendKeys.SendWait(lbl_Filepath.Text);
                Thread.Sleep(500);
                SimulateLeftMouseClick(840, 485);
                Thread.Sleep(100);

                // Click Open button
                SimulateLeftMouseClick(807, 492);
                Thread.Sleep(500);

                // Write SPD DATA to Module button click
                SimulateLeftMouseClick(375, 125);
                Thread.Sleep(500);

               // textBox1.Text = "Config value";

                // Edit Configuration button click
                SimulateLeftMouseClick(680, 340);
                Thread.Sleep(500);
             
                var serialnumber = clsCheckboxValivation.checkBoxValidation();
                // 1st cfg button click
                if(serialnumber != null && serialnumber.Count > 0)
                {
                    var fristSerialNumber = serialnumber[0].ToString();
                    var secondSerialNumber = serialnumber[1].ToString();
                    if (fristSerialNumber != string.Empty)
                    {
                        if(fristSerialNumber == "false")
                        {
                            //textBox1.Text = "checkboxvalue  " + fristSerialNumber;
                            SimulateLeftMouseClick(488, 398);
                            checkbx1status = "Manual check box ticked";
                            Thread.Sleep(500);
                      
                        }

                    }
                    //if (secondSerialNumber != string.Empty)
                    //{
                    //    if (secondSerialNumber == "false")
                    //    {
                    //        //textBox1.Text = "checkboxvalue  " + fristSerialNumber;
                    //        SimulateLeftMouseClick(488, 398);
                    //        Thread.Sleep(1000);
                    //    }

                    //}
                }
                SimulateLeftMouseClick(720, 395);
                Thread.Sleep(500);
              
                    // Hex/Decimal (HEX) selection 
                    SimulateLeftMouseClick(478, 395);
                    Thread.Sleep(500);
                           
               
                    //// Hex / Decimal (Decimal) selection
                    //SimulateLeftMouseClick(478, 420);
              

                // Serial number text box click
                SimulateLeftMouseClick(660, 355);
                Thread.Sleep(100);
                SimulateLeftMouseClick(660, 355);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
              
                SendKeys.SendWait(dec_serial);
                Thread.Sleep(100);

                SimulateLeftMouseClick(645, 501);
                Thread.Sleep(100);

                // OK button click
                SimulateLeftMouseClick(855, 395);
                Thread.Sleep(100);


                // 2nd cfg button click
                if (serialnumber != null && serialnumber.Count > 0)
                {
                    var seoundSerialNumber = serialnumber[1].ToString();
                    if (seoundSerialNumber != string.Empty)
                    {
                        if (seoundSerialNumber == "false")
                        {
                           // textBox1.Text = "checkboxvalueTwo  " + seoundSerialNumber;
                           
                            checkbx2status = "Manual check box ticked";
                            SimulateLeftMouseClick(488, 427);
                            Thread.Sleep(500);

                        }

                    }
                   
                }
                SimulateLeftMouseClick(716, 425);
                Thread.Sleep(100);
                string errorMessage = "Check Box status of the Serial Number";  // Default error message
                string functionName = "Essencore";
                string serial_value = textBox1.Text;
               // string errorfilepath = Error_filepath;
                writeErrorMessage(serial_value, checkbx1status, checkbx2status, Error_filepath, errorMessage, functionName);


                // Click User Input
                SimulateLeftMouseClick(699, 307);
                Thread.Sleep(100);
              
                getWKCode();

                SimulateLeftMouseClick(780, 380);
                Thread.Sleep(100);
                SimulateLeftMouseClick(780, 380);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(200);
                SendKeys.SendWait(year.ToString().Substring(2, 2));
                Thread.Sleep(500);

                // Year button click
                SimulateLeftMouseClick(780, 410);
                Thread.Sleep(100);
                SimulateLeftMouseClick(780, 410);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(200);
                SendKeys.SendWait(weekNumber.ToString());
                Thread.Sleep(500);

                // OK button click
                SimulateLeftMouseClick(780, 463);
                Thread.Sleep(500);

                // OK button click
                SimulateLeftMouseClick(854, 156);
                Thread.Sleep(250);

                // Edit SPD DATA button click
                SimulateLeftMouseClick(680, 380);
                Thread.Sleep(500);

                // Jump to Text button click
                SimulateLeftMouseClick(530, 551);
                Thread.Sleep(100);
                SimulateLeftMouseClick(530, 551);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("353");
                Thread.Sleep(50);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(50);

                SimulateLeftMouseClick(646, 246);
                Thread.Sleep(50);
                SimulateLeftMouseClick(646, 246);
                Thread.Sleep(50);
                SendKeys.SendWait("^C");
                Thread.Sleep(50);

                if (Clipboard.GetText() == "Data")
                {
                    // Line 1
                    SimulateLeftMouseClick(760, 245);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("30");
                    Thread.Sleep(100);

                    // Line 2
                    SimulateLeftMouseClick(760, 265);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("30");
                    Thread.Sleep(100);

                    // Line 3
                    SimulateLeftMouseClick(760, 285);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("30");
                    Thread.Sleep(100);

                    // Line 4
                    input = textBox1.Text.Substring(3, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 305);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 5
                    input = textBox1.Text.Substring(4, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 328);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 6
                    input = textBox1.Text.Substring(5, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 348);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 7
                    input = textBox1.Text.Substring(6, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 368);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 8
                    input = textBox1.Text.Substring(7, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 388);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    SimulateLeftMouseClick(935, 390);
                    Thread.Sleep(100);

                    SimulateLeftMouseClick(630, 500);
                    Thread.Sleep(5000);

                    //Result Button Click
                    SimulateLeftMouseClick(683, 441);
                    Thread.Sleep(2000);

                    //compare icon Click
                    SimulateLeftMouseClick(305, 123);
                    Thread.Sleep(2000);

                    //alert message ok button Click
                    SimulateLeftMouseClick(698, 416);
                    Thread.Sleep(2000);

                    //buffer value and o0riginal value campare ok button Click
                  //  SimulateLeftMouseClick(914, 641);
                    Thread.Sleep(100);
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is ResultDisplay)
                        {
                            form.Close();
                            break;
                        }
                    }
                    rowDetails = clsTableValue.Program(customer_name, serial_number);
                    ResultDisplay resultdisplay = new ResultDisplay(rowDetails, customer_name, serial_number);
                    resultdisplay.StartPosition = FormStartPosition.Manual;
                    resultdisplay.WindowState = FormWindowState.Normal;
                    resultdisplay.Location = new Point(0, 100);
                    resultdisplay.Show();
                   // resultdisplay.BringToFront();
                    resultdisplay.Activate();


                    textBox1.Clear();
                    textBox1.Select();
                    SerialFull = "";
                    Serial = "";
                    dec_serial = "";
                    input = "";
                    data = "";
                }
                else
                {
                    result = "FAIL: Clipboard text was not 'Data'";
                    this.BackColor = SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error -" + ex.Message.ToString());
                result = $"Error: {ex.Message}";
            }
            finally
            {
            EnableMouse(true);
             
            }
            // Ensure the database connection is closed even if an error occurs
         
            if (cnn.State == ConnectionState.Open) cnn.Close();

            cmd = new SqlCommand("pro_SPD_Write_Bot",cnn);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Customer", cbm_Cus.Text);
            cmd.Parameters.AddWithValue("@FG", cbm_FG.Text);
            cmd.Parameters.AddWithValue("@Model", lbl_Model.Text);
            cmd.Parameters.AddWithValue("@PCBA_ID", PCB_ID);
            cmd.Parameters.AddWithValue("@Capacity", lbl_capacity.Text);
            cmd.Parameters.AddWithValue("@Frequency", Frequence.Text);
            cmd.Parameters.AddWithValue("@Version", lbl_Ver.Text);
            cmd.Parameters.AddWithValue("@Release", lbl_Release.Text);
            cmd.Parameters.AddWithValue("@Part", lbl_Part.Text);
            cmd.Parameters.AddWithValue("@Result", result);
            cmd.Parameters.AddWithValue("@ServerDateTime", server_datetime);
            cmd.Parameters.AddWithValue("@Running_Converse", combinedValue);
            cmd.Parameters.AddWithValue("@SPD_Value", SPD_Value);
            cmd.Parameters.AddWithValue("@File_Path", lbl_Filepath.Text);
            cmd.Parameters.AddWithValue("@System_Datetime", system_datetime);

            if (cnn.State == ConnectionState.Closed) cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            SPD_Value = "";
            return result;


        }
        public void writeErrorMessage(string Serialnumber, string checkbx1status, string checkbx2status, string errorfilepath, string errorMessage, string functionName)
        {
            // Ensure the directory exists
            string systemPath = errorfilepath;
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
                errLogs.WriteLine($"Log Message: {errorMessage}" + Environment.NewLine);
                errLogs.WriteLine($"Product Type: {functionName}" + Environment.NewLine);
                errLogs.WriteLine($"Serial Number: {Serialnumber}" + Environment.NewLine  + $"CheckBox 1 Status: {checkbx1status}" + $"|| CheckBox 2 Status: {checkbx2status}");             
                errLogs.Close();
            }
        }

        //Hitachi
        public string BotStart5(string serial_number,string customer_name)
        {
            string result = "PASS"; // Default result is success unless an error occurs
            string checkbx1status = "Default checkbox ticked";  // Initialize default values
            string checkbx2status = "Default checkbox ticked";
            string errorfilepath = Error_filepath;
            try
            {
                get_server_datetime();
                EnableMouse(false);

                Thread.Sleep(300);

                SimulateLeftMouseClick(140, 120);
                Thread.Sleep(1000);

                IntPtr hwnd = FindWindow(null, "Load SPD Date File From Library");

                // Move the window to (451, 253)
                SetWindowPos(hwnd, IntPtr.Zero, 451, 253, 0, 0, SWP_NOSIZE | SWP_NOZORDER);

                // File path text box click
                SimulateLeftMouseClick(588, 487);
                Thread.Sleep(100);

                SimulateLeftMouseClick(588, 487);
                Thread.Sleep(100);
                //SendKeys.SendWait(NetworkPath);
                SendKeys.SendWait(lbl_Filepath.Text);
                Thread.Sleep(500);
                SimulateLeftMouseClick(840, 485);
                Thread.Sleep(100);

                // Click Open button
                SimulateLeftMouseClick(807, 492);
                Thread.Sleep(500);

                // Write SPD DATA to Module button click
                SimulateLeftMouseClick(375, 125);
                Thread.Sleep(500);

                // Edit Configuration button click
                SimulateLeftMouseClick(680, 340);
                Thread.Sleep(500);

                //SimulateLeftMouseClick(488, 398);
                //Thread.Sleep(500);

                var serialnumber = clsCheckboxValivation.checkBoxValidation();
                // 1st cfg button click
                if (serialnumber != null && serialnumber.Count > 0)
                {
                    var fristSerialNumber = serialnumber[0].ToString();
                    var secondSerialNumber = serialnumber[1].ToString();
                    if (fristSerialNumber != string.Empty)
                    {
                        if (fristSerialNumber == "false")
                        {
                            // 1st cfg button click
                            // SimulateLeftMouseClick(720, 395);
                            checkbx1status = "Manual check box ticked";
                            SimulateLeftMouseClick(488, 398);
                            Thread.Sleep(500);
                        }
                    }
                }
                 SimulateLeftMouseClick(720, 395);
                Thread.Sleep(500);
                //Write MSB Byte 
                SimulateLeftMouseClick(568, 285);
                Thread.Sleep(100);

                SimulateLeftMouseClick(568, 285);
                Thread.Sleep(100);

                SendKeys.SendWait("325");
                Thread.Sleep(100);



                // Hex/Decimal (HEX) selection 
                SimulateLeftMouseClick(478, 395);
                Thread.Sleep(500);


                //// Hex / Decimal (Decimal) selection
                //SimulateLeftMouseClick(478, 420);


                // Serial number text box click
                SimulateLeftMouseClick(660, 355);
                Thread.Sleep(100);
                SimulateLeftMouseClick(660, 355);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);

                SendKeys.SendWait(dec_serial);
                Thread.Sleep(500);

                SimulateLeftMouseClick(645, 501);
                Thread.Sleep(500);

                // OK button click
                SimulateLeftMouseClick(855, 395);
                Thread.Sleep(500);

                SimulateLeftMouseClick(488, 427);
                Thread.Sleep(500);

                if (serialnumber != null && serialnumber.Count > 0)
                {
                    var seoundSerialNumber = serialnumber[1].ToString();
                    if (seoundSerialNumber != string.Empty)
                    {
                        if (seoundSerialNumber == "false")
                        {
                            // 2nd cfg button click
                            checkbx2status = "Manual check box ticked";
                            SimulateLeftMouseClick(716, 425);
                            Thread.Sleep(500);
                        }
                    }
                }
                string errorMessage = "Check Box status of the Serial Number";  // Default error message
                string functionName = "Hitachi";
                string serial_value = textBox1.Text;
                // string errorfilepath = Error_filepath;
                writeErrorMessage(serial_value, checkbx1status, checkbx2status, Error_filepath, errorMessage, functionName);

                // Click User Input
                SimulateLeftMouseClick(699, 307);
                Thread.Sleep(500);

                getWKCode();

                SimulateLeftMouseClick(780, 380);
                Thread.Sleep(100);
                SimulateLeftMouseClick(780, 380);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(200);
                SendKeys.SendWait(year.ToString().Substring(2, 2));
                Thread.Sleep(500);

                // Year button click
                SimulateLeftMouseClick(780, 410);
                Thread.Sleep(100);
                SimulateLeftMouseClick(780, 410);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(200);
                SendKeys.SendWait(weekNumber.ToString());
                Thread.Sleep(500);

                // OK button click
                SimulateLeftMouseClick(780, 463);
                Thread.Sleep(500);

                // OK button click
                SimulateLeftMouseClick(854, 156);
                Thread.Sleep(250);

                // Edit SPD DATA button click
                SimulateLeftMouseClick(680, 380);
                Thread.Sleep(500);

                // Jump to Text button click
                SimulateLeftMouseClick(530, 551);
                Thread.Sleep(100);
                SimulateLeftMouseClick(530, 551);
                Thread.Sleep(100);
                SendKeys.SendWait("{BACKSPACE}");
                Thread.Sleep(100);
                SendKeys.SendWait("370");
                Thread.Sleep(50);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(50);

                SimulateLeftMouseClick(646, 246);
                Thread.Sleep(50);
                SimulateLeftMouseClick(646, 246);
                Thread.Sleep(50);
                SendKeys.SendWait("^C");
                Thread.Sleep(50);

                if (Clipboard.GetText() == "Data")
                {
                    // Line 1
                    SimulateLeftMouseClick(760, 245);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("30");
                    Thread.Sleep(100);

                    // Line 2
                    SimulateLeftMouseClick(760, 265);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("30");
                    Thread.Sleep(100);

                    // Line 3
                    SimulateLeftMouseClick(760, 285);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("30");
                    Thread.Sleep(100);

                    // Line 4
                    input = textBox1.Text.Substring(18, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 305);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 5
                    input = textBox1.Text.Substring(19, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 328);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 6
                    input = textBox1.Text.Substring(20, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 348);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 7
                    input = textBox1.Text.Substring(21, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 368);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    // Line 8
                    input = textBox1.Text.Substring(22, 1);
                    data = CaseSelection(input);

                    Thread.Sleep(100);
                    SimulateLeftMouseClick(760, 388);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(50);
                    SendKeys.SendWait(data);
                    Thread.Sleep(100);

                    SimulateLeftMouseClick(935, 390);
                    Thread.Sleep(100);

                    SimulateLeftMouseClick(630, 500);
                    Thread.Sleep(5000);

                    //Result Button Click
                    SimulateLeftMouseClick(683, 441);
                    Thread.Sleep(2000);

                    //compare icon Click
                    SimulateLeftMouseClick(305, 123);
                    Thread.Sleep(2000);

                    //alert message ok button Click
                    SimulateLeftMouseClick(698, 416);
                    Thread.Sleep(2000);
                    EnableMouse(true);
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is ResultDisplay)
                        {
                            form.Close();
                            break;
                        }
                    }
                    rowDetails =  clsTableValue.Program(customer_name,serial_number);
                    ResultDisplay resultdisplay = new ResultDisplay(rowDetails, customer_name, serial_number);
                    resultdisplay.StartPosition = FormStartPosition.Manual;
                    resultdisplay.WindowState = FormWindowState.Normal;
                    resultdisplay.Location = new Point(0, 100);
                    resultdisplay.Show();
                  //  resultdisplay.BringToFront();
                    resultdisplay.Activate();

                    //buffer value and o0riginal value campare ok button Click
                    // SimulateLeftMouseClick(914, 641);
                    //Thread.Sleep(100);

                    textBox1.Clear();
                    textBox1.Select();
                    SerialFull = "";
                    Serial = "";
                    dec_serial = "";
                    input = "";
                    data = "";
                }
                else
                {
                    result = "FAIL: Clipboard text was not 'Data'";
                    this.BackColor = SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }
            finally
            {
                EnableMouse(true);

            }
            // Ensure the database connection is closed even if an error occurs
            if (cnn.State == ConnectionState.Open) cnn.Close();

            SqlCommand cmd = new SqlCommand("INSERT INTO SPD_PROG_Log VALUES (@Customer, @FG, @Model,@PCBA_ID,@Capacity, @Frequency, @Version, @Release, @Part, @Result, @ServerDateTime, HOST_NAME(), @Running_Converse, @SPD_Value, @File_Path, @System_Datetime, '')", cnn);
            cmd.Parameters.AddWithValue("@Customer", cbm_Cus.Text);
            cmd.Parameters.AddWithValue("@FG", cbm_FG.Text);
            cmd.Parameters.AddWithValue("@Model", lbl_Model.Text);
            cmd.Parameters.AddWithValue("@PCBA_ID", PCB_ID);
            cmd.Parameters.AddWithValue("@Capacity", lbl_capacity.Text);
            cmd.Parameters.AddWithValue("@Frequency", Frequence.Text);
            cmd.Parameters.AddWithValue("@Version", lbl_Ver.Text);
            cmd.Parameters.AddWithValue("@Release", lbl_Release.Text);
            cmd.Parameters.AddWithValue("@Part", lbl_Part.Text);
            cmd.Parameters.AddWithValue("@Result", result);
            cmd.Parameters.AddWithValue("@ServerDateTime", server_datetime);
            cmd.Parameters.AddWithValue("@Running_Converse", combinedValue);
            cmd.Parameters.AddWithValue("@SPD_Value", SPD_Value);
            cmd.Parameters.AddWithValue("@File_Path", lbl_Filepath.Text);
            cmd.Parameters.AddWithValue("@System_Datetime", system_datetime);
            if (cnn.State == ConnectionState.Closed) cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            SPD_Value = "";
            return result;
        }



        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")]

        static extern bool SetCursorPos(int x, int y);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public void SimulateLeftMouseClick(int x, int y)
        {
            //// Get current cursor position
            //int x = Cursor.Position.X;
            //int y = Cursor.Position.Y;

            // Simulate left mouse button down
            SetCapture(IntPtr.Zero);
            SetCursorPos(x, y);

            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);

            // Simulate left mouse button up
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
            ReleaseCapture();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Clipboard.Clear();

                    //NetworkPath = lbl_Filepath.Text;

                    //// Set up process for network path mapping
                    //process.StartInfo.FileName = "cmd.exe";
                    //process.StartInfo.Arguments = $"/C net use {NetworkPath} /user:{username} {Password} /persistent:yes";
                    //process.StartInfo.RedirectStandardOutput = true;
                    //process.StartInfo.UseShellExecute = false;
                    //process.StartInfo.CreateNoWindow = true;

                    PCB_ID = textBox1.Text;

                    if (cbm_Cus.SelectedIndex > 0 && cbm_FG.SelectedIndex > 0)
                    {
                        if (textBox1.TextLength == 12)
                        {
                            Point mousePosition = Cursor.Position;
                            lbl_pos_x.Text = mousePosition.X.ToString();
                            lbl_pos_y.Text = mousePosition.Y.ToString();

                            if (textBox1.Text.StartsWith("B") && getYrCode(char.Parse(textBox1.Text.Substring(1, 1))) < 26 && getYrCode(char.Parse(textBox1.Text.Substring(1, 1))) > 21)
                            {
                                Serial = textBox1.Text.Substring(7, 5);
                                SerialFull = textBox1.Text;

                                if (AppResol == 1)
                                {
                                    BotStart1();
                                }
                                else if (AppResol == 2)
                                {
                                    BotStart2();
                                }
                                else
                                {
                                    throw new Exception("Unsupported Resolution");
                                }
                            }
                            else
                            {
                                textBox1.Clear();
                                Serial = "";
                                SerialFull = "";
                            }
                        }
                        else if (textBox1.TextLength == 14 && textBox1.Text.StartsWith("BW"))
                        {
                            Serial = textBox1.Text.Substring(6, 8);
                            SerialFull = textBox1.Text;
                            combinedValue = Serial + " | " + dec_serial;

                            if (AppResol == 1)
                            {
                                BotStart3(SerialFull, cbm_Cus.Text);
                            }
                            else if (AppResol == 2)
                            {
                                MessageBox.Show("Resolution not Supported");
                                BotStart3(SerialFull, cbm_Cus.Text);
                            }
                            else
                            {
                                throw new Exception("Unsupported Resolution");
                            }
                        }
                        else if ((textBox1.TextLength == 8 && textBox1.Text.StartsWith("00")) && cbm_Cus.Text == "ESSENCORE")
                        {
                            Serial = textBox1.Text.Substring(3, 5);
                            SerialFull = textBox1.Text;
                            dec_serial = Convert.ToInt32(Serial, 16).ToString();
                            combinedValue = Serial + " | " + dec_serial;
                            if (AppResol == 1)
                            {
                                BotStart4(SerialFull, cbm_Cus.Text);
                            }
                            else
                            {
                                throw new Exception("Unsupported Resolution");
                            }
                        }
                        else if ((textBox1.TextLength == 23 && textBox1.Text.StartsWith("DS")) && cbm_Cus.Text == "HITACHI")
                        {
                            Serial = textBox1.Text.Substring(18, 5);
                            SerialFull = textBox1.Text;
                            dec_serial = Convert.ToInt32(Serial, 16).ToString();
                            combinedValue = Serial + " | " + dec_serial;
                            if (AppResol == 1)
                            {
                                BotStart5(SerialFull, cbm_Cus.Text);
                            }
                            else
                            {
                                throw new Exception("Unsupported Resolution");
                            }
                        }
                        else
                        {
                            textBox1.Clear();
                            Serial = "";
                            SerialFull = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select valid options from both Combo Boxes.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message; // Save error message
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear(); // Clear textBox after error
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbl_pos_x_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_pos_y_Click(object sender, EventArgs e)
        {

        }

            private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cbm_Cus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbm_Cus.SelectedIndex > 0) 
            {
                SqlCommand cmd = new SqlCommand("select FG_Name from SPD_PROG_MASTER_DATA where Customer_Name = '" + cbm_Cus.Text + "'", cnn);

                cnn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                cbm_FG.Items.Clear(); 
                cbm_FG.Items.Add("--Select--"); 

                while (sdr.Read())
                {
                    cbm_FG.Items.Add(sdr["FG_Name"].ToString()); 
                }

                cbm_FG.SelectedIndex = 0; 
                cnn.Close();
            }
        }

        private void cbm_FG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbm_FG.SelectedIndex > 0)
            {
                SqlCommand cmd = new SqlCommand("select Model_Name,Capacity,Frequency,SOFTWARE_VER,SPD_Release_date,SPD_Part_No,File_Path from SPD_PROG_MASTER_DATA where Customer_Name = '" + cbm_Cus.Text + "'and FG_Name = '" + cbm_FG.Text + "' ", cnn);

                cnn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    // Display the retrieved values in labels
                    lbl_Model.Text = sdr["Model_Name"].ToString();
                    lbl_capacity.Text = sdr["Capacity"].ToString();
                    Frequence.Text = sdr["Frequency"].ToString();
                    lbl_Ver.Text = sdr["SOFTWARE_VER"].ToString();
                    lbl_Release.Text = sdr["SPD_Release_date"].ToString();
                    lbl_Part.Text = sdr["SPD_Part_No"].ToString();
                    lbl_Filepath.Text = sdr["File_Path"].ToString();
                }

                cnn.Close();
                cbm_Cus.Enabled = false;
                cbm_FG.Enabled = false;
                label1.Visible = true;
                textBox1.Text = "";
                textBox1.Visible = true;
                textBox1.Select();
                Clipboard.Clear();



            }

        }

        private void Date_Click(object sender, EventArgs e)
        {

        }

        private void part_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }

       }

    namespace DisableDevice
    {

        [Flags()]
        internal enum SetupDiGetClassDevsFlags
        {
            Default = 1,
            Present = 2,
            AllClasses = 4,
            Profile = 8,
            DeviceInterface = (int)0x10
        }

        internal enum DiFunction
        {
            SelectDevice = 1,
            InstallDevice = 2,
            AssignResources = 3,
            Properties = 4,
            Remove = 5,
            FirstTimeSetup = 6,
            FoundDevice = 7,
            SelectClassDrivers = 8,
            ValidateClassDrivers = 9,
            InstallClassDrivers = (int)0xa,
            CalcDiskSpace = (int)0xb,
            DestroyPrivateData = (int)0xc,
            ValidateDriver = (int)0xd,
            Detect = (int)0xf,
            InstallWizard = (int)0x10,
            DestroyWizardData = (int)0x11,
            PropertyChange = (int)0x12,
            EnableClass = (int)0x13,
            DetectVerify = (int)0x14,
            InstallDeviceFiles = (int)0x15,
            UnRemove = (int)0x16,
            SelectBestCompatDrv = (int)0x17,
            AllowInstall = (int)0x18,
            RegisterDevice = (int)0x19,
            NewDeviceWizardPreSelect = (int)0x1a,
            NewDeviceWizardSelect = (int)0x1b,
            NewDeviceWizardPreAnalyze = (int)0x1c,
            NewDeviceWizardPostAnalyze = (int)0x1d,
            NewDeviceWizardFinishInstall = (int)0x1e,
            Unused1 = (int)0x1f,
            InstallInterfaces = (int)0x20,
            DetectCancel = (int)0x21,
            RegisterCoInstallers = (int)0x22,
            AddPropertyPageAdvanced = (int)0x23,
            AddPropertyPageBasic = (int)0x24,
            Reserved1 = (int)0x25,
            Troubleshooter = (int)0x26,
            PowerMessageWake = (int)0x27,
            AddRemotePropertyPageAdvanced = (int)0x28,
            UpdateDriverUI = (int)0x29,
            Reserved2 = (int)0x30
        }

        internal enum StateChangeAction
        {
            Enable = 1,
            Disable = 2,
            PropChange = 3,
            Start = 4,
            Stop = 5
        }

        [Flags()]
        internal enum Scopes
        {
            Global = 1,
            ConfigSpecific = 2,
            ConfigGeneral = 4
        }

        internal enum SetupApiError
        {
            NoAssociatedClass = unchecked((int)0xe0000200),
            ClassMismatch = unchecked((int)0xe0000201),
            DuplicateFound = unchecked((int)0xe0000202),
            NoDriverSelected = unchecked((int)0xe0000203),
            KeyDoesNotExist = unchecked((int)0xe0000204),
            InvalidDevinstName = unchecked((int)0xe0000205),
            InvalidClass = unchecked((int)0xe0000206),
            DevinstAlreadyExists = unchecked((int)0xe0000207),
            DevinfoNotRegistered = unchecked((int)0xe0000208),
            InvalidRegProperty = unchecked((int)0xe0000209),
            NoInf = unchecked((int)0xe000020a),
            NoSuchHDevinst = unchecked((int)0xe000020b),
            CantLoadClassIcon = unchecked((int)0xe000020c),
            InvalidClassInstaller = unchecked((int)0xe000020d),
            DiDoDefault = unchecked((int)0xe000020e),
            DiNoFileCopy = unchecked((int)0xe000020f),
            InvalidHwProfile = unchecked((int)0xe0000210),
            NoDeviceSelected = unchecked((int)0xe0000211),
            DevinfolistLocked = unchecked((int)0xe0000212),
            DevinfodataLocked = unchecked((int)0xe0000213),
            DiBadPath = unchecked((int)0xe0000214),
            NoClassInstallParams = unchecked((int)0xe0000215),
            FileQueueLocked = unchecked((int)0xe0000216),
            BadServiceInstallSect = unchecked((int)0xe0000217),
            NoClassDriverList = unchecked((int)0xe0000218),
            NoAssociatedService = unchecked((int)0xe0000219),
            NoDefaultDeviceInterface = unchecked((int)0xe000021a),
            DeviceInterfaceActive = unchecked((int)0xe000021b),
            DeviceInterfaceRemoved = unchecked((int)0xe000021c),
            BadInterfaceInstallSect = unchecked((int)0xe000021d),
            NoSuchInterfaceClass = unchecked((int)0xe000021e),
            InvalidReferenceString = unchecked((int)0xe000021f),
            InvalidMachineName = unchecked((int)0xe0000220),
            RemoteCommFailure = unchecked((int)0xe0000221),
            MachineUnavailable = unchecked((int)0xe0000222),
            NoConfigMgrServices = unchecked((int)0xe0000223),
            InvalidPropPageProvider = unchecked((int)0xe0000224),
            NoSuchDeviceInterface = unchecked((int)0xe0000225),
            DiPostProcessingRequired = unchecked((int)0xe0000226),
            InvalidCOInstaller = unchecked((int)0xe0000227),
            NoCompatDrivers = unchecked((int)0xe0000228),
            NoDeviceIcon = unchecked((int)0xe0000229),
            InvalidInfLogConfig = unchecked((int)0xe000022a),
            DiDontInstall = unchecked((int)0xe000022b),
            InvalidFilterDriver = unchecked((int)0xe000022c),
            NonWindowsNTDriver = unchecked((int)0xe000022d),
            NonWindowsDriver = unchecked((int)0xe000022e),
            NoCatalogForOemInf = unchecked((int)0xe000022f),
            DevInstallQueueNonNative = unchecked((int)0xe0000230),
            NotDisableable = unchecked((int)0xe0000231),
            CantRemoveDevinst = unchecked((int)0xe0000232),
            InvalidTarget = unchecked((int)0xe0000233),
            DriverNonNative = unchecked((int)0xe0000234),
            InWow64 = unchecked((int)0xe0000235),
            SetSystemRestorePoint = unchecked((int)0xe0000236),
            IncorrectlyCopiedInf = unchecked((int)0xe0000237),
            SceDisabled = unchecked((int)0xe0000238),
            UnknownException = unchecked((int)0xe0000239),
            PnpRegistryError = unchecked((int)0xe000023a),
            RemoteRequestUnsupported = unchecked((int)0xe000023b),
            NotAnInstalledOemInf = unchecked((int)0xe000023c),
            InfInUseByDevices = unchecked((int)0xe000023d),
            DiFunctionObsolete = unchecked((int)0xe000023e),
            NoAuthenticodeCatalog = unchecked((int)0xe000023f),
            AuthenticodeDisallowed = unchecked((int)0xe0000240),
            AuthenticodeTrustedPublisher = unchecked((int)0xe0000241),
            AuthenticodeTrustNotEstablished = unchecked((int)0xe0000242),
            AuthenticodePublisherNotTrusted = unchecked((int)0xe0000243),
            SignatureOSAttributeMismatch = unchecked((int)0xe0000244),
            OnlyValidateViaAuthenticode = unchecked((int)0xe0000245)
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct DeviceInfoData
        {
            public int Size;
            public Guid ClassGuid;
            public int DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PropertyChangeParameters
        {
            public int Size;
            // part of header. It's flattened out into 1 structure.
            public DiFunction DiFunction;
            public StateChangeAction StateChange;
            public Scopes Scope;
            public int HwProfile;
        }

        internal class NativeMethods
        {

            private const string setupapi = "setupapi.dll";

            private NativeMethods()
            {
            }

            [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiCallClassInstaller(DiFunction installFunction, SafeDeviceInfoSetHandle deviceInfoSet, [In()]
ref DeviceInfoData deviceInfoData);

            [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiEnumDeviceInfo(SafeDeviceInfoSetHandle deviceInfoSet, int memberIndex, ref DeviceInfoData deviceInfoData);

            [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern SafeDeviceInfoSetHandle SetupDiGetClassDevs([In()]
ref Guid classGuid, [MarshalAs(UnmanagedType.LPWStr)]
string enumerator, IntPtr hwndParent, SetupDiGetClassDevsFlags flags);

            /*
            [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiGetDeviceInstanceId(SafeDeviceInfoSetHandle deviceInfoSet, [In()]
    ref DeviceInfoData did, [MarshalAs(UnmanagedType.LPTStr)]
    StringBuilder deviceInstanceId, int deviceInstanceIdSize, [Out()]
    ref int requiredSize);
            */
            [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiGetDeviceInstanceId(
               IntPtr DeviceInfoSet,
               ref DeviceInfoData did,
               [MarshalAs(UnmanagedType.LPTStr)] StringBuilder DeviceInstanceId,
               int DeviceInstanceIdSize,
               out int RequiredSize
            );

            [SuppressUnmanagedCodeSecurity()]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

            [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiSetClassInstallParams(SafeDeviceInfoSetHandle deviceInfoSet, [In()]
ref DeviceInfoData deviceInfoData, [In()]
ref PropertyChangeParameters classInstallParams, int classInstallParamsSize);

        }

        internal class SafeDeviceInfoSetHandle : SafeHandleZeroOrMinusOneIsInvalid
        {

            public SafeDeviceInfoSetHandle()
                : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return NativeMethods.SetupDiDestroyDeviceInfoList(this.handle);
            }

        }

        public sealed class DeviceHelper
        {

            private DeviceHelper()
            {
            }

            /// <summary>
            /// Enable or disable a device.
            /// </summary>
            /// <param name="classGuid">The class guid of the device. Available in the device manager.</param>
            /// <param name="instanceId">The device instance id of the device. Available in the device manager.</param>
            /// <param name="enable">True to enable, False to disable.</param>
            /// <remarks>Will throw an exception if the device is not Disableable.</remarks>

            public static void SetDeviceEnabled(Guid classGuid, string instanceId, bool enable)
            {
                SafeDeviceInfoSetHandle diSetHandle = null;
                try
                {
                    // Get the handle to a device information set for all devices matching classGuid that are present on the 
                    // system.
                    diSetHandle = NativeMethods.SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, SetupDiGetClassDevsFlags.Present);
                    // Get the device information data for each matching device.
                    DeviceInfoData[] diData = GetDeviceInfoData(diSetHandle);
                    // Find the index of our instance. i.e. the touchpad mouse - I have 3 mice attached...
                    int index = GetIndexOfInstance(diSetHandle, diData, instanceId);
                    // Disable...
                    EnableDevice(diSetHandle, diData[index], enable);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (diSetHandle != null)
                    {
                        if (diSetHandle.IsClosed == false)
                        {
                            diSetHandle.Close();
                        }
                        diSetHandle.Dispose();
                    }
                }
            }

            private static DeviceInfoData[] GetDeviceInfoData(SafeDeviceInfoSetHandle handle)
            {
                List<DeviceInfoData> data = new List<DeviceInfoData>();
                DeviceInfoData did = new DeviceInfoData();
                int didSize = Marshal.SizeOf(did);
                did.Size = didSize;
                int index = 0;
                while (NativeMethods.SetupDiEnumDeviceInfo(handle, index, ref did))
                {
                    data.Add(did);
                    index += 1;
                    did = new DeviceInfoData();
                    did.Size = didSize;
                }
                return data.ToArray();
            }

            // Find the index of the particular DeviceInfoData for the instanceId.
            private static int GetIndexOfInstance(SafeDeviceInfoSetHandle handle, DeviceInfoData[] diData, string instanceId)
            {
                const int ERROR_INSUFFICIENT_BUFFER = 122;
                for (int index = 0; index <= diData.Length - 1; index++)
                {
                    StringBuilder sb = new StringBuilder(1);
                    int requiredSize = 0;
                    bool result = NativeMethods.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb, sb.Capacity, out requiredSize);
                    if (result == false && Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                    {
                        sb.Capacity = requiredSize;
                        result = NativeMethods.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb, sb.Capacity, out requiredSize);
                    }
                    if (result == false)
                        throw new Win32Exception();
                    if (instanceId.Equals(sb.ToString()))
                    {
                        return index;
                    }
                }
                // not found
                return -1;
            }

            // enable/disable...
            private static void EnableDevice(SafeDeviceInfoSetHandle handle, DeviceInfoData diData, bool enable)
            {
                PropertyChangeParameters @params = new PropertyChangeParameters();
                // The size is just the size of the header, but we've flattened the structure.
                // The header comprises the first two fields, both integer.
                @params.Size = 8;
                @params.DiFunction = DiFunction.PropertyChange;
                @params.Scope = Scopes.Global;
                if (enable)
                {
                    @params.StateChange = StateChangeAction.Enable;
                }
                else
                {
                    @params.StateChange = StateChangeAction.Disable;
                }

                bool result = NativeMethods.SetupDiSetClassInstallParams(handle, ref diData, ref @params, Marshal.SizeOf(@params));
                if (result == false) throw new Win32Exception();
                result = NativeMethods.SetupDiCallClassInstaller(DiFunction.PropertyChange, handle, ref diData);
                if (result == false)
                {
                    int err = Marshal.GetLastWin32Error();
                    if (err == (int)SetupApiError.NotDisableable)
                        throw new ArgumentException("Device can't be disabled (programmatically or in Device Manager).");
                    else if (err >= (int)SetupApiError.NoAssociatedClass && err <= (int)SetupApiError.OnlyValidateViaAuthenticode)
                        throw new Win32Exception("SetupAPI error: " + ((SetupApiError)err).ToString());
                    else
                        throw new Win32Exception();
                }
            }
        }


    }
}












