using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SPD_Write_Bot
{
    public partial class ResultDisplay : Form
    {
        public genclass rowdetails { get; set; }
        public string Error_filepath = ConfigurationManager.AppSettings["FilePath1"];
       
        public ResultDisplay(genclass rowDetails,String customer_name,string serial_number)
        {
            //
            //this.Close();
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Serial_Number.Text = serial_number;    
            this.Customer_Name.Text = customer_name;
            this.rowdetails = rowDetails;

        }

        private void ResultDisplay_Load(object sender, EventArgs e)
        {

            var lstentry = new List<EntryDetails>();
            var lsthexvalues=new List<HexDetails>();
            EntryDetails entryDetails;
            HexDetails hexDetails;
            //int i = 10;
            dgvEntryValue.DataSource = null;
            dgvHexValue.DataSource = null;

            try
            {
                if (rowdetails != null)
                {
                    if (rowdetails.entries != null)
                    {
                        foreach (var item in rowdetails.entries)
                        {
                            entryDetails = new EntryDetails();
                            entryDetails.RowNumber = item.RowNumber;
                            entryDetails.Expected = item.Expected;
                            entryDetails.TableValue = item.TableValue;
                          
                            lstentry.Add(entryDetails);
                        }
                        dgvEntryValue.DataSource = lstentry;
                    }
                    else
                    {
                        MessageBox.Show("value Empty");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("value Empty");
                    return;
                }

                if (rowdetails.hexDetails != null)
                {

                    foreach (var item in rowdetails.hexDetails)
                    {
                        hexDetails = new HexDetails();
                        hexDetails.RowNumber = item.RowNumber;
                        hexDetails.Expected = item.Expected;
                        hexDetails.TableValue = item.TableValue;
                       // i = i + 10;
                       // hexDetails.TableValue = i.ToString();
                        lsthexvalues.Add(hexDetails);
                    }
                    dgvHexValue.DataSource = lsthexvalues;
                }
                else
                {
                    MessageBox.Show("value Empty");
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorMessage( ex.Message.ToString(),"Error",this.Serial_Number.Text);
            }

            dgvEntryValue.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvEntryValue.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHexValue.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvEntryValue.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEntryValue.DefaultCellStyle.Font = new Font("Arial", 10);
            dgvEntryValue.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHexValue.DefaultCellStyle.Font = new Font("Arial", 10);
            dgvEntryValue.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CompareAndHighlightrows1();
            CompareAndHighlightrows2();

        }

        private void CompareAndHighlightrows1()
        {
            foreach (DataGridViewRow row in dgvEntryValue.Rows)
            {
                if (row.IsNewRow)
                    continue;
                if (row.Cells["Expected"].Value != null && row.Cells["TableValue"].Value != null)
                {
                    if (row.Cells["Expected"].Value.ToString() == row.Cells["TableValue"].Value.ToString())
                    {
                        // If values are equal, change row's background color (e.g., light green)
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        // If values are not equal, reset to default (optional)
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }

        }

        private void CompareAndHighlightrows2()
        {
            foreach (DataGridViewRow row in dgvHexValue.Rows)
            {
                if (row.IsNewRow)
                    continue;
                if (row.Cells["Expected"].Value != null && row.Cells["TableValue"].Value != null)
                {
                    if (row.Cells["Expected"].Value.ToString() == row.Cells["TableValue"].Value.ToString())
                    {
                        // If values are equal, change row's background color (e.g., light green)
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        // If values are not equal, reset to default (optional)
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }

        }
        public void writeErrorMessage(string errorMessage,string functionName,string Serialnumber)
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
                errLogs.WriteLine($"Log Message: {errorMessage}" + Environment.NewLine);
                errLogs.WriteLine($"Product Type: {functionName}" + Environment.NewLine);
                errLogs.WriteLine($"Serial Number: {Serialnumber}" );
                errLogs.Close();
            }
        }

    }
}
