using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace SPD_Write_Bot
{
    public class TableValue
    {
        public string mainWindowName;
        public string serialNumber, Customer_Name, Serial_hexadecimal;
        public string Serial_1, Serial_2, Serial_3, Serial_4, Serial_5, Serial_6, Serial_7;
        public string Hex_1, Hex_2, Hex_3, Hex_4, Hex_5, Hex_6, Hex_7, Hex_8;
        public AutomationElement desktop;
        public List<string> serialNumbers;
        public List<string> HexaNumbers;
        public string Error_filepath = ConfigurationManager.AppSettings["FilePath1"];
        genclass rowvalue = new genclass();
       // bool spdpage=false;

        // Constructor to initialize instance fields
        public genclass Program(string Cus_Name,string Ser_Number)
        {
            writeErrorMessage("TableValue page entr",Cus_Name, Ser_Number);
            var rowDetails=new genclass();
            try
            {
                // Initialize the main window name and serial number
                serialNumber = string.Empty;
                mainWindowName = "DDR4 EZSPD Programmer V1.9.4";
                //Customer_Name = "ESSENCORE";
                //serialNumber = "000D0E6E";
                //Customer_Name = "BIWIN";
                // serialNumber = "BWB63380103058";
                Customer_Name = Cus_Name;
                serialNumber = Ser_Number;
                Customer_Name = Customer_Name.Trim();
                serialNumber = serialNumber.Trim();
                if (Customer_Name == "ESSENCORE")
                {

                    serialNumber = serialNumber.Trim();
                    Serial_1 = serialNumber.Substring(0, 2); // "00"
                    Serial_2 = serialNumber.Substring(2, 2); // "0D"
                    Serial_3 = serialNumber.Substring(4, 2); // "0E"
                    Serial_4 = serialNumber.Substring(6, 2); // "6D"

                    // Create a list of serial numbers
                    serialNumbers = new List<string>
                {
                    Serial_1,
                    Serial_2,
                    Serial_3,
                    Serial_4
                };
                    writeErrorMessage(serialNumbers.Count.ToString() + " --" + "list count", Cus_Name, Ser_Number);
                    // Convert serial number to hexadecimal
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(serialNumber);
                    string hexString = BitConverter.ToString(byteArray).Replace("-", "");
                   // Console.WriteLine("Hexadecimal representation: " + hexString);

                    // Create a list of hexadecimal values

                    Hex_1 = hexString.Substring(0, 2); // "00"
                    Hex_2 = hexString.Substring(2, 2);// "0D"
                    Hex_3 = hexString.Substring(4, 2);// "0E"
                    Hex_4 = hexString.Substring(6, 2);// "6D"
                    Hex_5 = hexString.Substring(8, 2); // "00"
                    Hex_6 = hexString.Substring(10, 2); // "0D"
                    Hex_7 = hexString.Substring(12, 2); // "0E"
                    Hex_8 = hexString.Substring(14, 2);
                    HexaNumbers = new List<string>
                    {
                      Hex_1, Hex_2, Hex_3, Hex_4, Hex_5, Hex_6, Hex_7,Hex_8
                };
                    // start

                   // writeErrorMessage(hexString + " --" + "Program Excuted Start", Cus_Name, Ser_Number);
                  //  var resultRowDetails= Start();
                   // rowDetails = resultRowDetails;
                   // writeErrorMessage("Program Excuted Start", "Start Enter", "");
                  
                    try
                    {
                        //if (rowDetails != null)
                        //    rowDetails = null;
                        // Initialize AutomationElement for desktop
                        AutomationElement desktop = AutomationElement.RootElement;
                       
                       // writeErrorMessage("Program Excuted Start","Check11", "111");
                        // var desktop1 =  AutomationElement.RootElement;
                        // Create a condition to find the main window by its name
                        Condition mainWindowCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                       // writeErrorMessage("Program Excuted Start", "Check1221", "11122");
                        AutomationElementCollection mainWindow = desktop.FindAll(TreeScope.Children, mainWindowCondition);
                       // writeErrorMessage("Program Excuted Start", "Check1234341", "111333");
                        writeErrorMessage(mainWindow.ToString(), "Main Window Count", mainWindow.Count.ToString());
                        foreach (AutomationElement MAINWindowINSIDE in mainWindow)
                        {
                            if(MAINWindowINSIDE.Current.Name.ToString().Contains("DDR4 EZSPD Programmer V1.9.6"))
                            {
                               // writeErrorMessage("Child Page Name", MAINWindowINSIDE.Current.Name.ToString(), "");
                                AutomationElementCollection CHILDWINDOWS = MAINWindowINSIDE.FindAll(TreeScope.Children, mainWindowCondition);
                                if (CHILDWINDOWS.Count > 0)
                                {
                                   // writeErrorMessage("1", CHILDWINDOWS.Count.ToString(), "");
                                    foreach (AutomationElement CHILDWINDOW in CHILDWINDOWS)
                                    {
                                       // writeErrorMessage("SRY", CHILDWINDOW.Current.Name.ToString(), "");
                                        if (CHILDWINDOW.Current.Name.ToString() == "Show SPD Compare Result - All SPD Bytes are Identical")
                                        {
                                            writeErrorMessage("Child window name ", CHILDWINDOW.Current.Name.ToString(), "");
                                            rowvalue = FindTablesInChildWindow(CHILDWINDOW, Customer_Name);
                                            //if (rowvalue == null)
                                            //{
                                            //    spdpage = false;
                                            //}
                                            //else if (rowvalue != null && rowvalue.hexDetails.Count > 0 && rowvalue.entries.Count > 0)
                                            //{
                                            //    spdpage = true;
                                            //    rowvalue.spdPAGE = spdpage;
                                            //}
                                            //else
                                            //{
                                            //    spdpage = false;
                                            //}

                                            break;
                                        }                                       
                                        //    AutomationElementCollection CHILDWINDOWSinside = CHILDWINDOW.FindAll(TreeScope.Children, mainWindowCondition);
                                        //writeErrorMessage("Child Page Name", CHILDWINDOW.Current.Name.ToString(), "");
                                        //writeErrorMessage("Mugesh1", "Inside Child Window Count", CHILDWINDOWS.Count.ToString());
                                        ////foreach (AutomationElement CHILDWINDOWall in CHILDWINDOWS)
                                        ////{
                                        //    writeErrorMessage("Child Page Name", MAINWindowINSIDE.Current.Name.ToString(), "");
                                        //    writeErrorMessage("Mugesh1", "Inside Child Window Count", CHILDWINDOWS.Count.ToString());
                                        //    //writeErrorMessage("Program Excuted Start", MAINWindowINSIDE.Current.Name.ToString(), "Page Name");
                                        //    //if (MAINWindowINSIDE.Current.Name == "Show SPD Compare Result - All SPD Bytes are Identical")
                                        //    //    rowvalue = FindTablesInChildWindow(MAINWindowINSIDE, Customer_Name);
                                        //    // Print the name and other details of each child window
                                        //    // Console.WriteLine($"  Popup Window Name: {childWindow.Current.Name}");
                                        //    //Console.WriteLine($"  Popup Window AutomationId: {childWindow.Current.AutomationId}");
                                        //    // Console.WriteLine($"  Popup Window ControlType: {childWindow.Current.ControlType.ProgrammaticName}");
                                        //    // Console.WriteLine($"  Popup Window ProcessId: {childWindow.Current.ProcessId}");

                                        //    //  FindDataGridViewInChildWindow(childWindow); testing

                                        //    // Console.WriteLine("-------------------------------");
                                        //}
                                        // Print the name and other details of each child window
                                        // Console.WriteLine($"  Popup Window Name: {childWindow.Current.Name}");
                                        //Console.WriteLine($"  Popup Window AutomationId: {childWindow.Current.AutomationId}");
                                        // Console.WriteLine($"  Popup Window ControlType: {childWindow.Current.ControlType.ProgrammaticName}");
                                        // Console.WriteLine($"  Popup Window ProcessId: {childWindow.Current.ProcessId}");

                                        //  FindDataGridViewInChildWindow(childWindow); testing

                                        // Console.WriteLine("-------------------------------");
                                    }


                                }
                            }
                                                                    
                            //// Print the name and other details of each child window
                            //// Console.WriteLine($"  Popup Window Name: {childWindow.Current.Name}");
                            ////Console.WriteLine($"  Popup Window AutomationId: {childWindow.Current.AutomationId}");
                            //// Console.WriteLine($"  Popup Window ControlType: {childWindow.Current.ControlType.ProgrammaticName}");
                            //// Console.WriteLine($"  Popup Window ProcessId: {childWindow.Current.ProcessId}");
                            //writeErrorMessage("Program Excuted Start", MAINWindowINSIDE.Current.Name.ToString(), "Page Name");
                            //if (MAINWindowINSIDE.Current.Name == "Show SPD Compare Result - All SPD Bytes are Identical")
                            //    rowvalue = FindTablesInChildWindow(MAINWindowINSIDE, Customer_Name);
                            ////  FindDataGridViewInChildWindow(childWindow); testing

                            //// Console.WriteLine("-------------------------------");
                        }



                        ////writeErrorMessage("Program Excuted Start", mainWindow.ToString(), "1");
                        ////if (mainWindow == null)
                        ////{
                        ////    // Console.WriteLine($"Main window '{mainWindowName}' not found.");
                        ////    return rowvalue;
                        ////}

                        //try
                        //{
                        //    MessageBox.Show(mainWindow.ToString());
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message.ToString());
                        //}

                    

                        //writeErrorMessage("Program Excuted Start", "", "2");
                        //// Create a condition to find any child windows (popups, dialogs, etc.)
                        //Condition popupCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                        //writeErrorMessage("Program Excuted Start", "", "3");
                        //// Find all child windows (and descendants) under the main window
                        //AutomationElementCollection childWindows = mainWindow.FindAll(TreeScope.Descendants, popupCondition);
                        //writeErrorMessage("Program Excuted Start", childWindows.Count.ToString(), "4");
                        //if (childWindows.Count == 0)
                        //{
                        //    writeErrorMessage("Program Excuted Start", "Child window count zero", "");
                        //    // Console.WriteLine("No child windows found.");
                        //}
                        //else
                        //{
                        //    // Console.WriteLine("Found child windows:");
                        //    writeErrorMessage("Program Excuted Start", "Start Foreach", "");
                        //    foreach (AutomationElement childWindow in childWindows)
                        //    {
                        //        // Print the name and other details of each child window
                        //        // Console.WriteLine($"  Popup Window Name: {childWindow.Current.Name}");
                        //        //Console.WriteLine($"  Popup Window AutomationId: {childWindow.Current.AutomationId}");
                        //        // Console.WriteLine($"  Popup Window ControlType: {childWindow.Current.ControlType.ProgrammaticName}");
                        //        // Console.WriteLine($"  Popup Window ProcessId: {childWindow.Current.ProcessId}");
                        //        writeErrorMessage("Program Excuted Start", childWindow.Current.Name.ToString(), "Page Name");
                        //        if (childWindow.Current.Name == "Show SPD Compare Result - All SPD Bytes are Identical")
                        //            rowvalue = FindTablesInChildWindow(childWindow, Customer_Name);
                        //        //  FindDataGridViewInChildWindow(childWindow); testing

                        //        // Console.WriteLine("-------------------------------");
                        //    }
                        //}

                        ////Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                      
                        rowvalue = null;
                        writeErrorMessage(ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString(), "Error", serialNumber);
                    }

                    return rowvalue;

                }
                else if (Customer_Name == "HITACHI")
                {

                    serialNumber = serialNumber.Trim();
                    //Serial_1 = serialNumber.Substring(8, 2); // "00"
                    //Serial_2 = serialNumber.Substring(10, 2); // "0D"
                    //Serial_3 = serialNumber.Substring(12, 2); // "0E"
                    //Serial_4 = serialNumber.Substring(14, 2); // "6D"
                    //Serial_5 = serialNumber.Substring(16, 2);
                    //Serial_6 = serialNumber.Substring(18, 2);
                    Serial_1 = serialNumber.Substring(15, 2);
                    Serial_2 = serialNumber.Substring(17, 2);
                    Serial_3 = serialNumber.Substring(19, 2);
                    Serial_4 = serialNumber.Substring(21, 2);
                    Serial_hexadecimal = serialNumber.Substring(15, 8);

                    // Create a list of serial numbers
                    serialNumbers = new List<string>
                {
                    Serial_1,
                    Serial_2,
                    Serial_3,
                    Serial_4,
                   // Serial_5,
                    //Serial_6,
                };

                    // Convert serial number to hexadecimal
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(Serial_hexadecimal);
                    string hexString = BitConverter.ToString(byteArray).Replace("-", "");
                   // Console.WriteLine("Hexadecimal representation: " + hexString);

                    Hex_1 = hexString.Substring(0, 2); // "00"
                    Hex_2 = hexString.Substring(2, 2);// "0D"
                    Hex_3 = hexString.Substring(4, 2);// "0E"
                    Hex_4 = hexString.Substring(6, 2);// "6D"
                    Hex_5 = hexString.Substring(8, 2); // "00"
                    Hex_6 = hexString.Substring(10, 2); // "0D"
                    Hex_7 = hexString.Substring(12, 2); // "0E"
                    Hex_8 = hexString.Substring(14, 2);  // "6D"
                    HexaNumbers = new List<string>
                {
                      Hex_1, Hex_2, Hex_3, Hex_4, Hex_5, Hex_6, Hex_7,Hex_8
                };
                    try
                    {

                        // Initialize AutomationElement for desktop
                        AutomationElement desktop = AutomationElement.RootElement;

                        // writeErrorMessage("Program Excuted Start","Check11", "111");
                        // var desktop1 =  AutomationElement.RootElement;
                        // Create a condition to find the main window by its name
                        Condition mainWindowCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                        // writeErrorMessage("Program Excuted Start", "Check1221", "11122");
                        AutomationElementCollection mainWindow = desktop.FindAll(TreeScope.Children, mainWindowCondition);
                        // writeErrorMessage("Program Excuted Start", "Check1234341", "111333");
                        writeErrorMessage(mainWindow.ToString(), "Main Window Count", mainWindow.Count.ToString());
                        foreach (AutomationElement MAINWindowINSIDE in mainWindow)
                        {
                            if (MAINWindowINSIDE.Current.Name.ToString().Contains("DDR4 EZSPD Programmer V1.9.6"))
                            {
                                // writeErrorMessage("Child Page Name", MAINWindowINSIDE.Current.Name.ToString(), "");
                                AutomationElementCollection CHILDWINDOWS = MAINWindowINSIDE.FindAll(TreeScope.Children, mainWindowCondition);
                                if (CHILDWINDOWS.Count > 0)
                                {
                                    // writeErrorMessage("1", CHILDWINDOWS.Count.ToString(), "");
                                    foreach (AutomationElement CHILDWINDOW in CHILDWINDOWS)
                                    {
                                        // writeErrorMessage("SRY", CHILDWINDOW.Current.Name.ToString(), "");
                                        if (CHILDWINDOW.Current.Name.ToString().Contains("Show SPD Compare Result - All SPD Bytes are Identical"))
                                        {
                                            writeErrorMessage("Child window name ", CHILDWINDOW.Current.Name.ToString(), "");
                                            rowvalue = FindTablesInChildWindow(CHILDWINDOW, Customer_Name);
                                            //if (rowvalue == null)
                                            //    spdpage = false;
                                            //else if (rowvalue != null)
                                            //    spdpage = true;

                                            break;
                                        }

                                    }


                                }
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        // throw ex;
                        rowvalue = null;
                        writeErrorMessage(ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString(), "Error", serialNumber);
                    }

                    return rowvalue;

                }

                if (Customer_Name == "BIWIN")
                {


                    //-----MessageBox.Show("Biwin is not added to the system");//
                    serialNumber = serialNumber.Trim();
                    //Serial_1 = serialNumber.Substring(0, 2); // "00"
                    //Serial_2 = serialNumber.Substring(2, 2); // "0D"
                    //Serial_3 = serialNumber.Substring(4, 2); // "0E"
                    //Serial_4 = serialNumber.Substring(6, 2); // "6D"
                    //Serial_5 = serialNumber.Substring(8, 2); // "00"
                    //Serial_6 = serialNumber.Substring(10, 2); // "0D"
                    //Serial_7 = serialNumber.Substring(12, 2); // "0E"


                    //// Create a list of serial numbers
                    //serialNumbers = new List<string>
                    //{
                    //    Serial_1,
                    //    Serial_2,
                    //    Serial_3,
                    //    Serial_4,
                    //    Serial_5,
                    //    Serial_6,
                    //    Serial_7,
                    //};

                    //// Convert serial number to hexadecimal
                    ///
                    string hexString1 = serialNumber.Substring(6, 8);
                    long decimalNumber = int.Parse(hexString1); // Example decimal number
                    string hexString = decimalNumber.ToString("X8"); // "X" format specifier converts to uppercase hexadecimal
                    Console.WriteLine("Hexadecimal representation: " + hexString);


                    // Create a list of hexadecimal values

                    Hex_1 = hexString.Substring(0, 2); // "00"
                    Hex_2 = hexString.Substring(2, 2);// "0D"
                    Hex_3 = hexString.Substring(4, 2);// "0E"
                    Hex_4 = hexString.Substring(6, 2);// "6D"

                    HexaNumbers = new List<string>
                {
                      Hex_1, Hex_2, Hex_3, Hex_4
                };
                    //  rowDetails = Start();
                    try
                    {
                        rowvalue = new genclass();
                        // Initialize AutomationElement for desktop
                        AutomationElement desktop = AutomationElement.RootElement;

                        // writeErrorMessage("Program Excuted Start","Check11", "111");
                        // var desktop1 =  AutomationElement.RootElement;
                        // Create a condition to find the main window by its name
                        Condition mainWindowCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                        // writeErrorMessage("Program Excuted Start", "Check1221", "11122");
                        AutomationElementCollection mainWindow = desktop.FindAll(TreeScope.Children, mainWindowCondition);
                        // writeErrorMessage("Program Excuted Start", "Check1234341", "111333");
                        writeErrorMessage(mainWindow.ToString(), "Main Window Count", mainWindow.Count.ToString());
                        foreach (AutomationElement MAINWindowINSIDE in mainWindow)
                        {
                            if (MAINWindowINSIDE.Current.Name.ToString().Contains("DDR4 EZSPD Programmer V1.9.6"))
                            {
                                // writeErrorMessage("Child Page Name", MAINWindowINSIDE.Current.Name.ToString(), "");
                                AutomationElementCollection CHILDWINDOWS = MAINWindowINSIDE.FindAll(TreeScope.Children, mainWindowCondition);
                                if (CHILDWINDOWS.Count > 0)
                                {
                                    // writeErrorMessage("1", CHILDWINDOWS.Count.ToString(), "");
                                    foreach (AutomationElement CHILDWINDOW in CHILDWINDOWS)
                                    {
                                        // writeErrorMessage("SRY", CHILDWINDOW.Current.Name.ToString(), "");
                                        if (CHILDWINDOW.Current.Name.ToString().Contains("Show SPD Compare Result - All SPD Bytes are Identical"))
                                        {
                                            writeErrorMessage("Child window name ", CHILDWINDOW.Current.Name.ToString(), "");
                                            rowvalue = FindTablesInChildWindow(CHILDWINDOW, Customer_Name);
                                            //if (rowvalue == null)
                                            //    spdpage = false;
                                            //else if (rowvalue != null)
                                            //    spdpage = true;

                                            break;
                                        }

                                    }


                                }
                            }


                        }


                    }
                    catch (Exception ex)
                    {
                        // throw ex;
                        rowvalue = null;
                        writeErrorMessage(ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString(), "Error", serialNumber);
                    }

                    return rowvalue;

                }
            }

            catch(Exception ex)
            {
                // throw ex;
                rowvalue = null;
                writeErrorMessage(ex.Message.ToString(), "Error", Ser_Number);
            }
            return rowDetails;
        }

        // Instance method to start the process
        public genclass Start()
        {
            writeErrorMessage("Program Excuted Start", "Start Enter", "");
            var rowvalue=new genclass();
            try
            {
                // Initialize AutomationElement for desktop
                desktop = AutomationElement.RootElement;
                // Create a condition to find the main window by its name
                Condition mainWindowCondition = new PropertyCondition(AutomationElement.NameProperty, mainWindowName);
                AutomationElement mainWindow = desktop.FindFirst(TreeScope.Children, mainWindowCondition);

                if (mainWindow == null)
                {
                   // Console.WriteLine($"Main window '{mainWindowName}' not found.");
                    return rowvalue;
                }

                // Create a condition to find any child windows (popups, dialogs, etc.)
                Condition popupCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);

                // Find all child windows (and descendants) under the main window
                AutomationElementCollection childWindows = mainWindow.FindAll(TreeScope.Descendants, popupCondition);

                if (childWindows.Count == 0)
                {
                   // Console.WriteLine("No child windows found.");
                }
                else
                {
                    // Console.WriteLine("Found child windows:");
                    writeErrorMessage("Program Excuted Start", "Start Foreach", "");
                    foreach (AutomationElement childWindow in childWindows)
                    {
                        // Print the name and other details of each child window
                        // Console.WriteLine($"  Popup Window Name: {childWindow.Current.Name}");
                        //Console.WriteLine($"  Popup Window AutomationId: {childWindow.Current.AutomationId}");
                        // Console.WriteLine($"  Popup Window ControlType: {childWindow.Current.ControlType.ProgrammaticName}");
                        // Console.WriteLine($"  Popup Window ProcessId: {childWindow.Current.ProcessId}");
                        writeErrorMessage("Program Excuted Start", childWindow.Current.Name.ToString(), "Page Name");
                        if (childWindow.Current.Name == "Show SPD Compare Result - All SPD Bytes are Identical")
                            rowvalue = FindTablesInChildWindow(childWindow, Customer_Name);
                        //  FindDataGridViewInChildWindow(childWindow); testing

                       // Console.WriteLine("-------------------------------");
                    }
                }

                //Console.ReadLine();
            }
            catch(Exception ex)
            {
                rowvalue = null;
                writeErrorMessage(ex.Message.ToString(), "Error", serialNumber);
                
            }

            return rowvalue;
        }

        // Instance method to find tables or custom controls inside a child window
        public genclass FindTablesInChildWindow(AutomationElement childWindow, string Customer_Name)
        {
            writeErrorMessage("jAMMM", "FindTablesInChildWindow", "");
            var row_Details=new genclass();
            try
            {
                // Look for all descendants of type Table, Custom, or DataGrid
                AutomationElementCollection tableControls = childWindow.FindAll(
                    TreeScope.Descendants,
                    new OrCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Table),
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom),
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataGrid)
                    ));

                if (tableControls.Count == 0)
                {
                    Console.WriteLine("  No table-like controls found in this child window.");
                }
                else
                {
                    Console.WriteLine("  Found table-like controls in this child window:");
                    writeErrorMessage("Program Excuted Start", "FindTablesInChildWindow", "ForEach");
                    foreach (AutomationElement tableControl in tableControls)
                    {
                        Console.WriteLine($"    Table-like Control Name: {tableControl.Current.Name}");
                        Console.WriteLine($"    Control AutomationId: {tableControl.Current.AutomationId}");
                        Console.WriteLine($"    Control Type: {tableControl.Current.ControlType.ProgrammaticName}");

                        // Now check if this control implements any relevant patterns like GridPattern
                        writeErrorMessage("Program Excuted Start", "FindTablesInChildWindow" + Customer_Name.ToString(), "Table Control");
                        row_Details = CheckForGridPattern(tableControl, Customer_Name);
                    }
                }
            }
            catch (Exception ex)
            {
                row_Details = null;
                writeErrorMessage(ex.Message.ToString(), "Error", serialNumber);
                
            }
            
            return row_Details;
        }

        // Instance method to check if the control implements GridPattern or TablePattern
        public genclass CheckForGridPattern(AutomationElement element, String Customer_Name)
        {
            writeErrorMessage("Program Excuted Start", "CheckForGridPattern" + Customer_Name.ToString(), "");
            var rowDetails = new RowDetails();
            genclass objentry = new genclass();
            try
            {
                // Check if the element supports GridPattern (commonly used for DataGridView)
                if (element.TryGetCurrentPattern(GridPattern.Pattern, out object gridPattern))
                {
                    Console.WriteLine($"    Found DataGridView with GridPattern.");
                    var grid = (GridPattern)gridPattern;
                    Console.WriteLine($"    RowCount: {grid.Current.RowCount}, ColumnCount: {grid.Current.ColumnCount}");

                    // Lists to store values for column 2 and column 3
                    List<string> column2Values = new List<string>();
                    List<string> column3Values = new List<string>();

                    // Variables to store values for rows 325 to 360
                    Dictionary<int, (string column2Value, string column3Value)> rowValuesInRange = new Dictionary<int, (string, string)>();
                    Dictionary<int, (string column2Value, string column3Value)> rowValuesInRange_Hex = new Dictionary<int, (string, string)>();
                   
                    var lstvalues = new List<EntryDetails>();
                    var lsthexvalues = new List<HexDetails>();
                    var obj = new EntryDetails();
                    // Loop through all rows and columns to get data from column 2 and column 3
                    // Loop through all rows and columns to get data from column 2 and column 3
                    for (int row = 0; row < grid.Current.RowCount; row++)
                    {
                        // Only process columns 2 and 3
                        for (int col = 2; col <= 3; col++)
                        {
                            if (col == 2 || col == 3) // Only process column 2 and column 3
                            {
                                var cell = grid.GetItem(row, col); // Get cell at (row, col)
                                string cellValue = cell.Current.Name; // Access the Name property of the cell

                                // Store the values in the respective lists
                                if (col == 2)
                                {
                                    column2Values.Add(cellValue);
                                }
                               else if (col == 3)
                                {
                                    column3Values.Add(cellValue);
                                }

                                //ESSENCORE
                                if (Customer_Name == "ESSENCORE")
                                {
                                    if (row >= 323 && row <= 328)
                                    {
                                        // Ensure that both column lists have values at that row index
                                        if (row < column2Values.Count && row < column3Values.Count)
                                        {
                                            // rowValuesInRange[row] = (column2Values[row], column3Values[row]);
                                            lstvalues.Add(new EntryDetails { RowNumber = row, Expected = column2Values[row].ToString(), TableValue = column3Values[row].ToString() });
                                        }
                                    }
                                    if (row >= 353 && row <= 360)
                                    {
                                        // Ensure that both column lists have values at that row index
                                        if (row < column2Values.Count && row < column3Values.Count)
                                        {
                                            // rowValuesInRange_Hex[row] = (column2Values[row].ToString(), column3Values[row].ToString());
                                            lsthexvalues.Add(new HexDetails { RowNumber = row, Expected = column2Values[row].ToString(), TableValue = column3Values[row].ToString() });
                                        }
                                    }
                                }
                                else if (Customer_Name == "HITACHI")
                                {
                                    if (row >= 323 && row <= 328)
                                    {
                                        // Ensure that both column lists have values at that row index
                                        if (row < column2Values.Count && row < column3Values.Count)
                                        {
                                           // rowValuesInRange[row] = (column2Values[row], column3Values[row]);
                                            lstvalues.Add(new EntryDetails { RowNumber = row, Expected = column2Values[row].ToString(), TableValue = column3Values[row].ToString() });

                                        }
                                    }
                                    if (row >= 370 && row <= 377)
                                    {
                                        // Ensure that both column lists have values at that row index
                                        if (row < column2Values.Count && row < column3Values.Count)
                                        {
                                           // rowValuesInRange_Hex[row] = (column2Values[row], column3Values[row]);
                                            lsthexvalues.Add(new HexDetails { RowNumber = row, Expected = column2Values[row].ToString(), TableValue = column3Values[row].ToString() });
                                        }

                                       
                                    }
                                }
                                else if (Customer_Name == "BIWIN")
                                {
                                    if (row >= 323 && row <= 328)
                                    {
                                        // Ensure that both column lists have values at that row index
                                        if (row < column2Values.Count && row < column3Values.Count)
                                        {
                                            //  rowValuesInRange[row] = (column2Values[row], column3Values[row]);
                                            lstvalues.Add(new EntryDetails { RowNumber = row, Expected = column2Values[row].ToString(), TableValue = column3Values[row].ToString() });
                                        }
                                    }
                                    if (row >= 353 && row <= 366)
                                    {
                                        // Ensure that both column lists have values at that row index
                                        if (row < column2Values.Count && row < column3Values.Count)
                                        {
                                            //  rowValuesInRange_Hex[row] = (column2Values[row], column3Values[row]);
                                            lsthexvalues.Add(new HexDetails { RowNumber = row, Expected = column2Values[row].ToString(), TableValue = column3Values[row].ToString() });
                                        }
                                    }
                                }
                               
                            
                                //// Store values for rows 325 to 360
                               // //if (row >= 324 && row <= 359)
                                //{
                                //    // Ensure that both column lists have values at that row index
                                //    if (row < column2Values.Count && row < column3Values.Count)
                                //    {
                                //        rowValuesInRange[row] = (column2Values[row], column3Values[row]);
                                //    }
                                //}
                            }
                        }
                    }
                    //  rowDetails.ramvalue = rowValuesInRange;
                    // rowDetails.hexvalue = rowValuesInRange_Hex;
                    writeErrorMessage("Program Excuted Start", lstvalues.Count.ToString() + lsthexvalues.ToString(), "");
                    objentry.entries=lstvalues;
                    objentry.hexDetails = lsthexvalues;
                    // Example: Perform comparison between values of column 2 and column 3
                    //For Customer Confirmation !
                    //  PerformComparison(column2Values, column3Values, rowValuesInRange, Customer_Name);
                }
                else
                {

                    // Console.WriteLine($"    GridPattern not found for this element.");
                    objentry = null;
                    MessageBox.Show("GridPattern not found for this element.");//
                }
            }
            catch(Exception ex)
            {
                objentry = null;
                writeErrorMessage(ex.Message.ToString(), "Error", serialNumber);
            }   
            return objentry;

        }

        // Instance method to perform comparison between values from column 2 and column 3
        public void PerformComparison(List<string> column2Values, List<string> column3Values,
                                      Dictionary<int, (string, string)> rowValuesInRange, String Customer_Name)
        {
            bool allValuesSame = true;
            bool anyDifferencesInRange = false;  // Flag to check if any differences occur in the range of 325-360

            for (int i = 0; i < column2Values.Count; i++)
            {
                string col2Value = column2Values[i];
                string col3Value = column3Values[i];

                // Check if any value is different between column 2 and column 3
                if (!col2Value.Equals(col3Value))
                {
                    allValuesSame = false;

                    if (Customer_Name == "ESSENCORE")
                    {
                        // Check if the row number falls between 325 and 360 (0-indexed 324 to 359)
                        if ((i >= 323 && i <= 328) && (i >= 353 && i <= 360))  // Row numbers are 1-based, so 324 = 325th row
                        {
                            anyDifferencesInRange = true; // Set the flag if differences are detected in the range
                        }

                    }

                    if (Customer_Name == "HITACHI")
                    {
                        // Check if the row number falls between 325 and 360 (0-indexed 324 to 359)
                        if ((i >= 323 && i <= 328) && (i >= 370 && i <= 377))  // Row numbers are 1-based, so 324 = 325th row
                        {
                            anyDifferencesInRange = true; // Set the flag if differences are detected in the range
                        }

                    }

                    if (Customer_Name == "BIWIN")
                    {
                        // Check if the row number falls between 325 and 360 (0-indexed 324 to 359)
                        if ((i >= 324 && i <= 327) && (i >= 352 && i <= 365))  // Row numbers are 1-based, so 324 = 325th row
                        {
                            anyDifferencesInRange = true; // Set the flag if differences are detected in the range
                        }

                    }


                }
            }

            // If all values are the same, call the empty function
            if (allValuesSame)
            {
               // Console.WriteLine("All values are the same. Calling function...");
                CallFunction(serialNumbers, HexaNumbers, rowValuesInRange, Customer_Name); ;  // Call the function when all values are the same
            }
            else
            {
                // Only call the function once if differences were detected in the range 325–360
                if (anyDifferencesInRange)
                {
                   // Console.WriteLine($"    Values are different in the range between 325 and 360. Calling function...");
                    CallFunction(serialNumbers, HexaNumbers, rowValuesInRange, Customer_Name); ;  // Call the function after checking the range
                }
                else
                {
                    // If differences were detected, but the row was outside the 325–360 range
                   // Console.WriteLine("    Board is not pass. No values found in the range 325 to 360.");
                   // MessageBox.Show("Differences were found in the SerialNUmbers 325 to 360");
                }
            }

        }

        // Instance method to call when certain conditions are met
        public void CallFunction(List<string> serialNumbers, List<string> HexaNumbers, Dictionary<int, (string column2Value, string column3Value)> rowValuesInRange, string Customer_Name)
        {
           // Console.WriteLine("    Function called.");

            // Print out the serial numbers and hexadecimal values
            if (Customer_Name == "ESSENCORE" || Customer_Name == "HITACHI")
            {
               // Console.WriteLine("    Serial Numbers: " + string.Join(", ", serialNumbers));
            }

           // Console.WriteLine("    Hexadecimal Numbers: " + string.Join(", ", HexaNumbers));

            // Define expected values for each row and column2Value (in terms of serials and hexadecimals)
            Dictionary<int, string> expectedValues = new Dictionary<int, string>();

            // For Essencore Customer
            if (Customer_Name == "ESSENCORE")
            {
                expectedValues[325] = Serial_4;
                expectedValues[326] = Serial_3;
                expectedValues[327] = Serial_2;
                expectedValues[328] = Serial_1;
                expectedValues[353] = Hex_1;
                expectedValues[354] = Hex_2;
                expectedValues[355] = Hex_3;
                expectedValues[356] = Hex_4;
                expectedValues[357] = Hex_5;
                expectedValues[358] = Hex_6;
                expectedValues[359] = Hex_7;
                expectedValues[360] = Hex_8;
            }

            // For HITACHI Customer
            else if (Customer_Name == "HITACHI")
            {
                //expectedValues[325] = Serial_6;
                //expectedValues[326] = Serial_5;
                //expectedValues[327] = Serial_4;
                //expectedValues[328] = Serial_3;
                //expectedValues[323] = Serial_1;
                //expectedValues[324] = Serial_2;

                expectedValues[328] = Serial_4;
                expectedValues[327] = Serial_3;
                expectedValues[326] = Serial_2;
                expectedValues[325] = Serial_1;

                expectedValues[370] = Hex_1;
                expectedValues[371] = Hex_2;
                expectedValues[372] = Hex_3;
                expectedValues[373] = Hex_4;
                expectedValues[374] = Hex_5;
                expectedValues[375] = Hex_6;
                expectedValues[376] = Hex_7;
                expectedValues[377] = Hex_8;
            }
            else if (Customer_Name == "BIWIN")
            {
                expectedValues[325] = Hex_4;
                expectedValues[326] = Hex_3;
                expectedValues[327] = Hex_2;
                expectedValues[328] = Hex_1;
            }

            // Flag to check if all values match
            bool allValuesMatch = true;

            // Now let's perform the comparison of values within the 325-360 range
            foreach (var entry in rowValuesInRange)
            {
                int rowNumber = entry.Key;
                string col2Value = entry.Value.column2Value;
                string col3Value = entry.Value.column3Value;

                // Check if the rowNumber has an expected value in the dictionary
                if (expectedValues.ContainsKey(rowNumber))
                {
                    // Compare the actual col3Value with the expected value
                    if (col3Value == expectedValues[rowNumber])
                    {
                       // Console.WriteLine($"Row {rowNumber}: Value matches expected value.");
                    }
                    else
                    {
                       // Console.WriteLine($"Row {rowNumber}: Value mismatch! Expected {expectedValues[rowNumber]}, but found {col3Value}.");
                        allValuesMatch = false;  // Set the flag to false if there's a mismatch
                    }
                }
                else
                {
                   // Console.WriteLine($"Row {rowNumber}: No expected value defined for this row.");
                    //allValuesMatch = false;  // Set the flag to false if there's no expected value for this row
                }
            }

            // After checking all rows, display the board pass/fail result
            if (allValuesMatch)
            {
              //  Console.WriteLine("Board Pass: All conditions checked and passed.");
                MessageBox.Show("Board Pass:All conditions checked and passed");

            }
            else
            {
               // Console.WriteLine("Board Fail: One or more conditions failed.");
                MessageBox.Show("Board Fail: One or more conditions failed.");
            }
        }



        // Function to find DataGridView inside a child window
        public void FindDataGridViewInChildWindow(AutomationElement childWindow)
        {
            // Look for all descendants of type ControlType.Custom (DataGridView might be a custom control)
            AutomationElementCollection customControls = childWindow.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

            if (customControls.Count == 0)
            {
              //  Console.WriteLine("  No custom controls found in this child window.");
            }
            else
            {
               // Console.WriteLine("  Found custom controls in this child window:");


                foreach (AutomationElement customControl in customControls)
                {
                   // Console.WriteLine($"    Custom Control Name: {customControl.Current.Name}");
                }
            }
        }

        public void writeErrorMessage(string errorMessage, string functionName, string Serialnumber)
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
                errLogs.WriteLine($"Serial Number: {Serialnumber}");
                errLogs.Close();
            }
        }























        //old

        //public static void ReadTableValues()
        //{
        //    // Find the window of the application (you can use the window's title, or process)
        //    AutomationElement rootElement = AutomationElement.RootElement;

        //    // Find the table control by its automation properties (you might need to use a more specific way to find it)
        //    AutomationElement tableElement = rootElement.FindFirst(TreeScope.Descendants,
        //        new PropertyCondition(AutomationElement.AutomationIdProperty, "your_table_automation_id"));

        //    if (tableElement != null)
        //    {
        //        // Assuming the table is structured as a list of rows
        //        var rows = tableElement.FindAll(TreeScope.Children,
        //            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem));

        //        foreach (AutomationElement row in rows)
        //        {
        //            // Read the cells (columns) in this row
        //            AutomationElement cell1 = row.FindFirst(TreeScope.Children,
        //                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));

        //            if (cell1 != null)
        //            {
        //                // Extract the text (value of the cell)
        //                string cellValue = cell1.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
        //                Console.WriteLine(cellValue);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Table not found!");
        //    }
        //}
    }
}
