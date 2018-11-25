using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace JetLabRibbon.Browsers
{
	public partial class BrowserForm : System.Windows.Forms.Form
    {
        private ExternalCommandData commandData;
        private string message;
        private ElementSet elements;

        public BrowserForm()
		{
			InitializeComponent();
		}

        public BrowserForm(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            this.commandData = commandData;
            this.message = message;
            this.elements = elements;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://google.com");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // opens the folder in explorer
            var fileName = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Autodesk\\Revit\\Addins\\2018");

            System.Diagnostics.Process.Start(fileName);
        }

        private void buttonRunScript_Click(object sender, EventArgs e)
        {
            /*
            JetLab.PlaceGroup placeGroupe = new JetLab.PlaceGroup();
            placeGroupe.Execute(commandData, ref message, elements);
            */
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
