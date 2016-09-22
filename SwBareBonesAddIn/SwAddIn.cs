using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorksTools;

namespace SwBareBonesAddIn
{
    /// <summary>
    /// An example of a very basic add-in implementation.
    /// This implementation provides just enough code in order to get the add-in to show up in SolidWorks.
    /// </summary>
    [Guid("D479985B-C711-4CB5-9321-FFDD306A88E0"), ComVisible(true),
    SolidWorksTools.SwAddin(
        Description = "Barebones add-in description",
        Title = "SwBareBonesAddIn",
        LoadAtStartup = true
    )]
    public class SwAddIn : SolidWorks.Interop.swpublished.ISwAddin
    {
        private SldWorks _swApp;

        // The register and unregister functions below are only ever called by regasm, normally only 
        // on the development machine (see post-build script in project properties).
        #region SolidWorks Registration

        /// <summary>
        /// Creates the registry keys/values required in order to register the add-in in the Solidworks Add-Ins list.
        /// This is invoked by regasm when it's executed on this assembly.
        /// </summary>
        /// <param name="t"></param>
        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            try
            { 
                #region Get Custom Attribute: SwAddinAttribute
                SwAddinAttribute SWattr = null;

                // The type we are after here is THIS class and NOT the type of the same name in the swpublished namespace
                Type type = typeof(SwAddIn);

                foreach (object attr in type.GetCustomAttributes(false))
                {
                    if (attr is SwAddinAttribute)
                    {
                        SWattr = attr as SwAddinAttribute;
                        break;
                    }
                }

                #endregion

                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
                addinkey.SetValue(null, 0);

                addinkey.SetValue("Description", SWattr.Description);
                addinkey.SetValue("Title", SWattr.Title);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                addinkey = hkcu.CreateSubKey(keyname);
                addinkey.SetValue(null, Convert.ToInt32(SWattr.LoadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                System.Windows.Forms.MessageBox.Show("There was a problem registering the function: \n\"" + e.Message + "\"");
            }
        }

        /// <summary>
        /// Deletes the required registry keys/values so that the add-in is removed Solidworks Add-Ins list.
        /// This is invoked by regasm when it's executed on this assembly with the /u switch.
        /// </summary>
        /// <param name="t"></param>
        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                hklm.DeleteSubKey(keyname);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                hkcu.DeleteSubKey(keyname);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem unregistering this dll: " + nl.Message);
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + nl.Message + "\"");
            }
            catch (System.Exception e)
            {
                Console.WriteLine("There was a problem unregistering this dll: " + e.Message);
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + e.Message + "\"");
            }
        }
        #endregion

        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            // TODO: Implement connection time logic
            _swApp = ThisSW as SldWorks;

            return true;
        }

        public bool DisconnectFromSW()
        {
            // TODO: Implement teardown logic

            // Mark COM pointers for release
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_swApp);

            _swApp = null;

            //The addin _must_ call GC.Collect() here in order to release all managed code pointers 
            GC.Collect();
            GC.WaitForPendingFinalizers();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return true;
        }
    }
}
