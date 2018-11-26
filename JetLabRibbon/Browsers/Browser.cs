#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Windows;
#endregion

namespace JetLabRibbon.Browsers

{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	class Browser : IExternalCommand
	{
        
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
            try
            {
                // Quit if active document is null
                if (null == commandData.Application.ActiveUIDocument.Document)
                {
                    message = Properties.Resources.ResourceManager.GetString("NullActiveDocument");
                    return Autodesk.Revit.UI.Result.Failed;
                }

                // Show operation dialog
                BrowserOperationData optionData = new BrowserOperationData(commandData);

                /*
                using (BrowserForm mainForm = new BrowserForm(optionData))
                {
                    if (mainForm.ShowDialog() == DialogResult.Cancel)
                    {
                        return Autodesk.Revit.UI.Result.Cancelled;
                    }
                }
                */

                BrowserForm mainForm = new BrowserForm(optionData);
                mainForm.ShowDialog();


                // Perform the operation
                optionData.Operate();
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return Autodesk.Revit.UI.Result.Failed;
            }

            return Result.Succeeded;
        }
        
	}
}