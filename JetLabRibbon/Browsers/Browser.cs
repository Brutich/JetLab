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
 
            FormBrowser fbrowser = new FormBrowser(commandData, ref message, elements);

            IWin32Window revit_window = new JtWindowHandle(ComponentManager.ApplicationWindow);
            fbrowser.Show(revit_window);

			return Result.Succeeded;
		}
	}
}