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
            collector.OfCategory(BuiltInCategory.OST_TitleBlocks);

            FamilySymbol titleblock = collector.Cast<FamilySymbol>().FirstOrDefault();
            if (titleblock != null)
                titleblockId = titleblock.Id;

            View view = document.ActiveView;
            ElementId viewId = view.Id;

            // Get the BoundingBox of view.
            BoundingBoxXYZ bbox = view.get_BoundingBox(view);
            if (null == bbox)
            {
                throw new Exception("Selected view doesn't contain a bounding box.");
            }


            #region Point Location Calculation (WIP)
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
