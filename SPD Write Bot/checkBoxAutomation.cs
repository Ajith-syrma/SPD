using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace SPD_Write_Bot
{
    public class checkBoxAutomation
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        private const int BM_GETCHECK = 0x00F0;
        private const int BST_UNCHECKED = 0x0000;
        private const int BST_CHECKED = 0x0001;

        public List<string> checkBoxValidation()
        {
            List<string> result = new List<string>();
            var chk ="false";
            var chkone="false";
            try
            {
                // Step 1: Find the main window of the application
                IntPtr mainWindowHandle = FindWindow(null, "Write SPD cfg");
                if (mainWindowHandle == IntPtr.Zero)
                {
                    // Console.WriteLine("Application window not found.");
                    
                    return new List<string>();
                }

                // Step 2: Find the checkbox control within the window
                IntPtr checkboxHandle = FindWindowEx(mainWindowHandle, IntPtr.Zero, null, "Write Production Serial Number");
                IntPtr checkboxHandleOne = FindWindowEx(mainWindowHandle, IntPtr.Zero, null, "Write Production Date");

                if (checkboxHandle == IntPtr.Zero || checkboxHandleOne == IntPtr.Zero)
                {
                   // Console.WriteLine("Checkbox not found.");
                    return new List<string>();
                }

                // Try to get AutomationElement for checkbox handles
                AutomationElement checkboxElement = AutomationElement.FromHandle(checkboxHandle);
                AutomationElement checkboxElementOne = AutomationElement.FromHandle(checkboxHandleOne);

                if (checkboxElement == null || checkboxElementOne == null)
                {
                   // Console.WriteLine("Could not retrieve AutomationElement for the checkbox.");
                    return new List<string>();
                }

                // Get the TogglePattern and check the state
                object togglePatternObj = checkboxElement.GetCurrentPattern(TogglePattern.Pattern);
                object togglePatternObjOne = checkboxElementOne.GetCurrentPattern(TogglePattern.Pattern);

                if (togglePatternObj == null || togglePatternObjOne == null)
                {
                    //Console.WriteLine("The checkbox does not support the TogglePattern.");
                    return new List<string>();
                }

                // Get the ToggleState
                TogglePattern togglePattern = (TogglePattern)togglePatternObj;
                ToggleState toggleState = togglePattern.Current.ToggleState;

                TogglePattern togglePatternOne = (TogglePattern)togglePatternObjOne;
                ToggleState toggleStateOne = togglePatternOne.Current.ToggleState;

                // Output state information
                switch (toggleState)
                {
                    case ToggleState.On:
                        //Console.WriteLine("The checkbox is checked.");
                        chk = "true";
                        break;
                    case ToggleState.Off:
                        //Console.WriteLine("The checkbox is unchecked.");
                        chk = "false";
                        break;
                    case ToggleState.Indeterminate:
                       // Console.WriteLine("The checkbox is in an indeterminate state.");
                        chk = "error";
                        break;
                }

                switch (toggleStateOne)
                {
                    case ToggleState.On:
                       // Console.WriteLine("The checkbox 2 is checked.");
                        chkone = "true";
                        break;
                    case ToggleState.Off:
                        //Console.WriteLine("The checkbox 2 is unchecked.");
                        chkone = "false";
                        break;
                    case ToggleState.Indeterminate:
                        //Console.WriteLine("The checkbox 2 is in an indeterminate state.");
                        chkone = "error";
                        break;
                }
                result.Add(chk);
                result.Add(chkone);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return result;
            }
            return result;

           
        }
    }
}
