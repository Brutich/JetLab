using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace JetLabRibbon
{
	class App : IExternalApplication
	{
		// define a method that will create our tab and button
		static void AddRibbonPanel(UIControlledApplication application)
		{
			// Create a custom ribbon tab
			String tabName = "JetLab";
			application.CreateRibbonTab(tabName);

			// Add a new ribbon panel
			RibbonPanel ribbonPanel1 = application.CreateRibbonPanel(tabName, "Tools");

			// Add a another one ribbon panel
			RibbonPanel ribbonPanel2 = application.CreateRibbonPanel(tabName, "Browser");

			// Get dll assembly path
			string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

			// create push button for CurveTotalLength
			PushButtonData b1Data = new PushButtonData(
				"cmdPlaceGroup",
				"Copy" + System.Environment.NewLine + "  Group  ",
				thisAssemblyPath,
				"JetLabPlaceGroup.Class1");

			PushButton pb1 = ribbonPanel1.AddItem(b1Data) as PushButton;
			pb1.ToolTip = "Select Groupe to Copying";
			BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/JetLabRibbon;component/Resources/JetLab.png"));
			pb1.LargeImage = pb1Image;
		}

		public Result OnShutdown(UIControlledApplication application)
		{
			// do nothing
			return Result.Succeeded;
		}

		public Result OnStartup(UIControlledApplication application)
		{
			// call our method that will load up our toolbar
			AddRibbonPanel(application);
			return Result.Succeeded;
		}
	}
}