using System;
using System.IO;
using System.Diagnostics;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SwConsole
{
    class Program
    {
        const string EXAMPLESDIR = @"C:\SolidWorks Training Files\API Fundamentals\";
        const string LESSON5DIR = EXAMPLESDIR + @"Lesson05 - Assembly Automation\";
        const string LESSON5FILEDIR = LESSON5DIR + @"Case Study\Guitar Effect Pedal\";

        static SldWorks swApp;

        /// <summary>
        /// Logs a message to Debug and Console output.
        /// </summary>
        /// <param name="message"></param>
        static void Log(string message)
        {
            Debug.WriteLine(message);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Attempts to open the argument file in SOLIDWORKS.
        /// </summary>
        /// <param name="filePath">The full file path</param>
        /// <returns>The <see cref="ModelDoc2"/> or null if open fails for any reason.</returns>
        static ModelDoc2 OpenDoc(string filePath)
        {
            // init return value
            ModelDoc2 swModel = null;

            Log("Opening: " + filePath);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist...");
            }
            else
            {
                // TODO: Really open...
                var swDocSpec = (DocumentSpecification)swApp.GetOpenDocSpec(filePath);

                swDocSpec.Silent = true;

                swModel = swApp.OpenDoc7(swDocSpec);

                // TODO: Handle open doc errors and warnings.

                if (swDocSpec.Error != 0)
                {
                    // TODO: Handle errors
                }

                //if (swDocSpec.Warning != 0)
                //{
                //    // TODO: Handle warnings
                //}

                // Maybe a more preferable way than above (depends on the situation probably)...

                switch ((swFileLoadWarning_e)swDocSpec.Warning)
                {
                    case swFileLoadWarning_e.swFileLoadWarning_IdMismatch:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_ReadOnly:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_SharingViolation:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_DrawingANSIUpdate:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_SheetScaleUpdate:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_NeedsRegen:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_BasePartNotLoaded:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_AlreadyOpen:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_DrawingsOnlyRapidDraft:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_ViewOnlyRestrictions:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_ViewMissingReferencedConfig:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_DrawingSFSymbolConvert:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_RevolveDimTolerance:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_ModelOutOfDate:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_DimensionsReferencedIncorrectlyToModels:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_ComponentMissingReferencedConfig:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_InvisibleDoc_LinkedDesignTableUpdateFail:
                        break;
                    case swFileLoadWarning_e.swFileLoadWarning_MissingDesignTable:
                        break;
                    default:
                        break;
                }
            }
            return swModel;
        }

        /// <summary>
        /// Simple document feature tree traversal example.
        /// </summary>
        /// <param name="swModel">The document with the feature tree that should be traversed.</param>
        static void TraverseFeatures(ModelDoc2 swModel)
        {
            Log("Traversing features of: " + swModel.GetTitle());

            //FeatureManager swFeatMgr = swModel.FeatureManager;

            //// Basic feature traversal (verbose way that clearly illustrates how GetFeatures returns a boxed Array from COM)
            //object oFeats = swFeatMgr.GetFeatures(false);
            //if (oFeats != null)
            //{
            //    Array aFeats = (Array)oFeats;
            //    foreach (Feature swFeat in aFeats)
            //    {
            //        Log($"Feature name: {swFeat.Name}; Type: {swFeat.GetTypeName2()}");
            //    }
            //}

            //// We can do a bit better than the above (exact same but fewer lines)
            //Array aFeats = swFeatMgr.GetFeatures(false) as Array;
            //if (aFeats != null)
            //{
            //    foreach (Feature swFeat in aFeats)
            //    {
            //        Log($"Feature name: {swFeat.Name}; Type: {swFeat.GetTypeName2()}");
            //    }
            //}

            // Furthermore, can (optional) eliminate the braces since we only had one operation inside the condition and the foreach loop.
            // If/when you prefer this over the above is obviously subjective and opinion based.
            // Also subjective/opinion but var could be just used in this case as well when declaring the Array.
            var aFeats = swModel.FeatureManager.GetFeatures(false) as Array;
            if (aFeats != null)
                foreach (Feature swFeat in aFeats)
                    Log($"Feature name: {swFeat.Name}; Type: {swFeat.GetTypeName2()}");
        }

        static void Main()
        {
            try
            {
                Log("Starting SOLIDWORKS...");

                swApp = new SldWorks();

                swApp.Visible = true;

                // Log some application info
                Log($"SOLIDWORKS version: {swApp.RevisionNumber()}\nSOLIDWORKS current language: {swApp.GetCurrentLanguage()}");

                ModelDoc2 swModel = OpenDoc(LESSON5FILEDIR + @"Guitar Effect Pedal.SLDASM");

                if (swModel != null)
                {
                    // Open succeeded...

                    // Do stuff with the active document (pointer returned from OpenDoc above)
                    TraverseFeatures(swModel);
                }

                Log("Exiting SOLIDWORKS");
                swApp.ExitApp();
            }
            catch (Exception ex)
            {
                // TODO: Do something better but this is better than nothing for now...
                Log("EXCEPTION THROWN! " + ex.ToString());
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
