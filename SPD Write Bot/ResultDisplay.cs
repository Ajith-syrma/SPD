using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPD_Write_Bot
{
    public partial class ResultDisplay : Form
    {
        public genclass rowdetails { get; set; }
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
            try
            {
                if (rowdetails != null)
                {
                    if (rowdetails.entries != null)
                    {
                        foreach (var item in rowdetails.entries)
                        {
                            entryDetails = new EntryDetails();
                            entryDetails.bytsEntry = item.bytsEntry;
                            entryDetails.ExpectedValue = item.ExpectedValue;
                            entryDetails.OriginalValue = item.OriginalValue;
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

                if (rowdetails.hexDetails != null)
                {

                    foreach (var item in rowdetails.hexDetails)
                    {
                        hexDetails = new HexDetails();
                        hexDetails.bytshexvalue = item.bytshexvalue;
                        hexDetails.ExpectedValuehex = item.ExpectedValuehex;
                        hexDetails.hexoriginalvalues = item.hexoriginalvalues;
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
            }
        }
    }
}
