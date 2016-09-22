using System;
using System.IO;
using System.Diagnostics;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using SwCommon;

namespace SwConsole
{
    class Program
    {
        const string EXAMPLESDIR = @"C:\SolidWorks Training Files\API Fundamentals\";
        const string LESSON5DIR = EXAMPLESDIR + @"Lesson05 - Assembly Automation\";
        const string LESSON5FILEDIR = LESSON5DIR + @"Case Study\Guitar Effect Pedal\";

        static SldWorks swApp;

        /// <summary>
        /// Simple document feature tree traversal example.
        /// </summary>
        /// <param name="swModel">The document with the feature tree that should be traversed.</param>
        static void TraverseFeatures(ModelDoc2 swModel)
        {
            Logger.Log("Traversing features of: " + swModel.GetTitle());

            //FeatureManager swFeatMgr = swModel.FeatureManager;

            //// Basic feature traversal (verbose way that clearly illustrates how GetFeatures returns a boxed Array from COM)
            //object oFeats = swFeatMgr.GetFeatures(false);
            //if (oFeats != null)
            //{
            //    Array aFeats = (Array)oFeats;
            //    foreach (Feature swFeat in aFeats)
            //    {
            //        Logger.Log($"Feature name: {swFeat.Name}; Type: {swFeat.GetTypeName2()}");
            //    }
            //}

            //// We can do a bit better than the above (exact same but fewer lines)
            //Array aFeats = swFeatMgr.GetFeatures(false) as Array;
            //if (aFeats != null)
            //{
            //    foreach (Feature swFeat in aFeats)
            //    {
            //        Logger.Log($"Feature name: {swFeat.Name}; Type: {swFeat.GetTypeName2()}");
            //    }
            //}

            // Furthermore, can (optional) eliminate the braces since we only had one operation inside the condition and the foreach loop.
            // If/when you prefer this over the above is obviously subjective and opinion based.
            // Also subjective/opinion but var could be just used in this case as well when declaring the Array.
            var aFeats = swModel.FeatureManager.GetFeatures(false) as Array;
            if (aFeats != null)
                foreach (Feature swFeat in aFeats)
                    Logger.Log($"Feature name: {swFeat.Name}; Type: {swFeat.GetTypeName2()}");
        }

        static void Main()
        {
            try
            {
                Logger.Log("Starting SOLIDWORKS...");

                swApp = new SldWorks();

                swApp.Visible = true;

                // Logger.Log some application info
                Logger.Log($"SOLIDWORKS version: {swApp.RevisionNumber()}\nSOLIDWORKS current language: {swApp.GetCurrentLanguage()}");

                ModelDoc2 swModel = SwHelpers.OpenDoc(swApp, LESSON5FILEDIR + @"Guitar Effect Pedal.SLDASM");

                if (swModel != null)
                {
                    // Open succeeded...

                    // Do stuff with the active document (pointer returned from OpenDoc above)
                    TraverseFeatures(swModel);
                }

                Logger.Log("Exiting SOLIDWORKS");
                swApp.ExitApp();
            }
            catch (Exception ex)
            {
                // TODO: Do something better but this is better than nothing for now...
                Logger.Log("EXCEPTION THROWN! " + ex.ToString());
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
