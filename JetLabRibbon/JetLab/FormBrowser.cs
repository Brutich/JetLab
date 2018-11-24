﻿using System;
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

namespace JetLabRibbon.JetLab
{
	public partial class FormBrowser : System.Windows.Forms.Form
    {
        private ExternalCommandData commandData;
        private string message;
        private ElementSet elements;

        public FormBrowser()
		{
			InitializeComponent();
		}

        public FormBrowser(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            this.commandData = commandData;
            this.message = message;
            this.elements = elements;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                MessageBox.Show(fileName);
            }
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
            JetLabPlaceGroup.PlaceGroup placeGroupe = new JetLabPlaceGroup.PlaceGroup();
            placeGroupe.Execute(commandData, ref message, elements);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
