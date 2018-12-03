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
            
            /* temporary
            // All views in document
            ICollection<Element> view = collector.OfClass(typeof(View)).ToElements();
            */
            
            // Find a titleblock in the project, or use InvalidElementId to create a sheet with no titleblock
            ElementId titleblockId = ElementId.InvalidElementId;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            collector.OfClass(typeof(FamilySymbol));
            collector.OfCategory(BuiltInCategory.OST_TitleBlocks);

            FamilySymbol titleblock = collector.Cast<FamilySymbol>().FirstOrDefault();
            if (titleblock != null)
                titleblockId = titleblock.Id;

            ElementId viewId = document.ActiveView.Id;

            XYZ point = new XYZ(0, 0, 0);


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
