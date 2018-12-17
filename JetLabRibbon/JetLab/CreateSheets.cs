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

            FilteredElementCollector collector = new FilteredElementCollector(document);
            collector.OfClass(typeof(FamilySymbol));
            collector.OfCategory(BuiltInCategory.OST_TitleBlocks).Where(q => q.Name == "0_TBL_NewForm3").First();

            FamilySymbol titleblock = collector.Cast<FamilySymbol>().FirstOrDefault();

            if (titleblock != null)
                titleblockId = titleblock.Id;


            // Get plan view
            View view = document.ActiveView;
            ElementId viewId = view.Id;


            #region Point Location Calculation (WIP)
           
            // Get the BoundingBox of view.
            BoundingBoxXYZ bbox = view.get_BoundingBox(view);
            if (null == bbox)
            {
                throw new Exception("Selected view doesn't contain a bounding box.");
            }
           

            // get the outline max and min,
            // offset in each direction of max point of 
            // bounding box than the outline box.

            double dMaxOffset = 0.01;
            XYZ ptMaxOutline = new XYZ(
              view.Outline.Max.U,
              view.Outline.Max.V, 0);

            UV ptSourceViewOriginInSheet = new UV(
              bbox.Max.X - dMaxOffset - ptMaxOutline.X,
              bbox.Max.Y - dMaxOffset - ptMaxOutline.Y);

            int scale = view.Scale;

            XYZ point = ptMaxOutline;
            #endregion


            #region Predefined Points
            // Predefined point for plan vieport placement
            XYZ pointViewPort = new XYZ(
                -2.4925,
                 0.5036,
                 1.0819);

            // Predefined point for schedule instance placement
            XYZ pointScheduleInstance = new XYZ(
                -1.5665,
                 1.4124,
                 0.0000);

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

            // Begin to place view on sheet
            Transaction trans = new Transaction(document);
            trans.Start("Create Sheet and Place View");

            // Create the new sheet
            ViewSheet sheet = ViewSheet.Create(document, titleblockId);
            ElementId viewSheetId = sheet.Id;

            // Create the new viewport
            Viewport viewport = Viewport.Create(document, viewSheetId, viewId, point);
            if (null == viewport)
            {
                throw new Exception("Place view failed.");
            }

            trans.Commit();
            
            return Result.Succeeded;

        }

    }

}
