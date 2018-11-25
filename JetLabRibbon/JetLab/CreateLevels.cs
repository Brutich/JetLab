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
    public class CreateLevels : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)

        {
            Document document = commandData.Application.ActiveUIDocument.Document;

            // The elevation to apply to the new level
            double elevation = 20.0;        

            // Begin to create a level
            Transaction trans = new Transaction(document);
            trans.Start("Create Levels");

            Level level = Level.Create(document, elevation);
            if (null == level)
            {
                throw new Exception("Create a new level failed.");
            }

            // Change the level name
            level.Name = "New level";

            trans.Commit();

            return Result.Succeeded;

        }

    }

}
