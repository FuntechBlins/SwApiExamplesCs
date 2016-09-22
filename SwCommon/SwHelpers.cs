using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SwCommon
{
    public static class SwHelpers
    {
        /// <summary>
        /// Attempts to open the argument file in SOLIDWORKS.
        /// </summary>
        /// <param name="filePath">The full file path</param>
        /// <returns>The <see cref="ModelDoc2"/> or null if open fails for any reason.</returns>
        public static ModelDoc2 OpenDoc(SldWorks swApp, string filePath)
        {
            // init return value
            ModelDoc2 swModel = null;

            Logger.Log("Opening: " + filePath);

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

                // TODO: Process any errors that occurred
                if (swDocSpec.Error != 0)
                {
                    foreach (swFileLoadError_e error in EnumHelpers.GetFlags((swFileLoadError_e)swDocSpec.Error))
                    {
                        switch (error)
                        {
                            case swFileLoadError_e.swGenericError:
                                // TODO: Handle each possible error (may be one or multiple)
                                break;
                            case swFileLoadError_e.swFileNotFoundError:
                                break;
                            case swFileLoadError_e.swIdMatchError:
                                break;
                            case swFileLoadError_e.swReadOnlyWarn:
                                break;
                            case swFileLoadError_e.swSharingViolationWarn:
                                break;
                            case swFileLoadError_e.swDrawingANSIUpdateWarn:
                                break;
                            case swFileLoadError_e.swSheetScaleUpdateWarn:
                                break;
                            case swFileLoadError_e.swNeedsRegenWarn:
                                break;
                            case swFileLoadError_e.swBasePartNotLoadedWarn:
                                break;
                            case swFileLoadError_e.swFileAlreadyOpenWarn:
                                break;
                            case swFileLoadError_e.swInvalidFileTypeError:
                                break;
                            case swFileLoadError_e.swDrawingsOnlyRapidDraftWarn:
                                break;
                            case swFileLoadError_e.swViewOnlyRestrictions:
                                break;
                            case swFileLoadError_e.swFutureVersion:
                                break;
                            case swFileLoadError_e.swViewMissingReferencedConfig:
                                break;
                            case swFileLoadError_e.swDrawingSFSymbolConvertWarn:
                                break;
                            case swFileLoadError_e.swFileWithSameTitleAlreadyOpen:
                                break;
                            case swFileLoadError_e.swLiquidMachineDoc:
                                break;
                            case swFileLoadError_e.swLowResourcesError:
                                break;
                            case swFileLoadError_e.swNoDisplayData:
                                break;
                            default:
                                break;
                        }
                    }
                }

                // TODO: Process any warnings that occurred
                if (swDocSpec.Warning != 0)
                {
                    foreach (swFileLoadWarning_e warn in EnumHelpers.GetFlags((swFileLoadWarning_e)swDocSpec.Warning))
                    {
                        switch (warn)
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
                }
            }
            return swModel;
        }
    }
}
