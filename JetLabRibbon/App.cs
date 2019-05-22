using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.IO;

namespace JetLabRibbon
{
	class App : IExternalApplication
	{

        /*
        // define a method that will add Failure Messege
        static void AddFailureMessege(UIControlledApplication application)
        {
            Document document = application.ControlledApplication.

            // Create youw own new failure definition Ids
            Guid guid1 = new Guid(
              "d1cf2412-08fe-476b-a6e6-e9a00583c52d");

            FailureDefinitionId m_idWarning
              = new FailureDefinitionId(guid1);

            // Create failure definitions and add resolutions
            FailureDefinition m_fdWarning
              = FailureDefinition.CreateFailureDefinition(
                m_idWarning, FailureSeverity.Warning,
                "Textvalue is changed for all instances "
                + "in textchain");

            m_fdWarning.SetDefaultResolutionType(
              FailureResolutionType.SetValue);


            FailureMessage fm = new FailureMessage(
                ExternalApplication.m_idWarning);

            document.PostFailure(fm);
        }
        */



		// define a method that will create our tab and button
		static void AddRibbonPanel(UIControlledApplication application)
		{
			// Create a custom ribbon tab
			String tabName = "JetLab";
			application.CreateRibbonTab(tabName);

			// Add a new ribbon panel
			RibbonPanel ribbonPanel1 = application.CreateRibbonPanel(tabName, "Tools");

			// Add a another one ribbon panel
			RibbonPanel ribbonPanel2 = application.CreateRibbonPanel(tabName, "Browsers");

			// Get dll assembly path
			string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

			// create push button for PlaceGroupe
			PushButtonData b1Data = new PushButtonData(
				"cmdPlaceGroup",
				"Copy" + System.Environment.NewLine + "  Group  ",
				thisAssemblyPath,
				"JetLab.PlaceGroup");

            PushButton pb1 = ribbonPanel1.AddItem(b1Data) as PushButton;
			pb1.ToolTip = "Copy Selected Group";
			BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/JetLabRibbon;component/Resources/JetLab.png"));
			pb1.LargeImage = pb1Image;

            
            // create push button for CreateLevel
            PushButtonData b3Data = new PushButtonData(
                "cmdCreateSheets",
                "Create" + System.Environment.NewLine + "  Sheets  ",
                thisAssemblyPath,
                "JetLabRibbon.JetLab.CreateSheets");

            PushButton pb3 = ribbonPanel1.AddItem(b3Data) as PushButton;
            pb3.ToolTip = "Create Levels";
            BitmapImage pb3Image = new BitmapImage(new Uri("pack://application:,,,/JetLabRibbon;component/Resources/JetLab.png"));
            pb3.LargeImage = pb3Image;


            // create push button for Browser
            string browserFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Autodesk\Revit\Addins\2019\BIMBrowser.dll");
            PushButtonData b2Data = new PushButtonData(
				"cmdOpenBrowser",
				"Families" + System.Environment.NewLine + "  Browser  ",
                browserFileName,
                "BIMBrowser.BIMBrowserCommand");

			PushButton pb2 = ribbonPanel2.AddItem(b2Data) as PushButton;
			pb2.ToolTip = "Open Families Browser";
			BitmapImage pb2Image = new BitmapImage(new Uri("pack://application:,,,/JetLabRibbon;component/Resources/JetLab.png"));
			pb2.LargeImage = pb2Image;

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