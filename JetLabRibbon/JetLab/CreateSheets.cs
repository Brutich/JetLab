using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace JetLabRibbon.JetLab

{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateSheets : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)

        {
            Document document = commandData.Application.ActiveUIDocument.Document;        

            // Find a titleblock in the project, or use InvalidElementId to create a sheet with no titleblock
            ElementId titleblockId = ElementId.InvalidElementId;

            FilteredElementCollector tbCollector = new FilteredElementCollector(document);
            tbCollector.OfClass(typeof(FamilySymbol));
            tbCollector.OfCategory(BuiltInCategory.OST_TitleBlocks).Where(q => q.Name == "0_TBL_NewForm3").First();

            FamilySymbol titleblock = tbCollector.Cast<FamilySymbol>().FirstOrDefault();

            if (titleblock != null)
                titleblockId = titleblock.Id;

            /*
            // Get sourse sheet
            FilteredElementCollector vsCollector = new FilteredElementCollector(document);
            vsCollector.OfClass(typeof(ViewSheet));
            vsCollector.Where(q => q.Name == "План 1 этажа. М 1ː100").First();
            ViewSheet sourseSheet = vsCollector.Cast<ViewSheet>().FirstOrDefault();
            */

            // Get plan view
            View view = document.ActiveView;
            ElementId viewId = view.Id;


            // Get room schedule
            FilteredElementCollector rsCollector = new FilteredElementCollector(document);
            rsCollector.OfClass(typeof(ScheduleSheetInstance));
            rsCollector.Where(q => q.Name == "50_ROM_L01_AS").First();
            ScheduleSheetInstance existRoomScheduleInst = rsCollector.Cast<ScheduleSheetInstance>().FirstOrDefault();           
            

            #region Predefined Points
            // Predefined point for plan vieport placement
            XYZ pointViewPort = new XYZ(
                -2.4925,
                 0.5036,
                 1.0819);

            // Predefined point for legend 01 placement
            XYZ pointLeged_01 = new XYZ(
                -0.9366,
                 0.5089,
                 0.0000);

            // Predefined point for legend 02 placement
            XYZ pointLeged_02 = new XYZ(
                -0.9504,
                 0.3157,
                 0.0000);
            #endregion

            // Begin to place views on sheet
            Transaction trans = new Transaction(document);
            trans.Start("Create Sheet and Place View");

            // Create the new sheet
            ViewSheet newSheet = ViewSheet.Create(document, titleblockId);
            ElementId viewSheetId = newSheet.Id;
            if (null == viewSheetId)
            {
                throw new Exception("Create new sheet failed.");
            }

            // Create the new viewport
            Viewport viewport = Viewport.Create(document, viewSheetId, viewId, pointViewPort);
            if (null == viewport)
            {
                throw new Exception("Place view failed.");
            }

            // Create the new room schedule
            ScheduleSheetInstance newNoomScheduleInst = ScheduleSheetInstance.Create(
                document,
                viewSheetId,
                existRoomScheduleInst.ScheduleId,
                existRoomScheduleInst.Point);
            if (null == newNoomScheduleInst)
            {
                throw new Exception("Place room schedule failed.");
            }

            /*
            ICollection<ElementId> ids = sourseSheet.GetAllPlacedViews();
            CopyPasteOptions copyPasteOptions = new CopyPasteOptions();
            ElementTransformUtils.CopyElements(sourseSheet, ids, newSheet, null, copyPasteOptions);
            */

            trans.Commit();


            return Result.Succeeded;

        }

    }

}
