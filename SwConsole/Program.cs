using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolidWorks.Interop.sldworks;
using System.IO;
using SolidWorks.Interop.swconst;
using System.Diagnostics;

namespace SwConsole
{
    class Program
    {
        const string EXAMPLESDIR = @"C:\SolidWorks Training Files\API Fundamentals\";

        static SldWorks swApp;

        /// <summary>
        /// Attempts to open the argument file in SOLIDWORKS.
        /// </summary>
        /// <param name="filePath">The full file path</param>
        /// <returns>The <see cref="ModelDoc2"/> or null if open fails for any reason.</returns>
        static ModelDoc2 OpenDoc(string filePath)
        {
            // init return value
            ModelDoc2 swModel = null;

            Debug.WriteLine("Opening: " + filePath);

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

        static void Main()
        {
            Console.WriteLine("Starting SOLIDWORKS...");

            swApp = new SldWorks();

            swApp.Visible = true;

            Console.WriteLine("SOLIDWORKS current language: " + swApp.GetCurrentLanguage());

            Console.WriteLine("Open a document...");

            OpenDoc(EXAMPLESDIR + @"Lesson02 - Object Model Basics\Case Study\sheetmetalsample.SLDASM");

            Console.WriteLine("Closing document...");


            Console.WriteLine("\nPress any key to exit");

            Console.ReadKey();
        }
    }
}
