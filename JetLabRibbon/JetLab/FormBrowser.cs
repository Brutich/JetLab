﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JetLabRibbon.JetLab
{
	public partial class FormBrowser : Form
	{
		public FormBrowser()
		{
			InitializeComponent();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = openFileDialog1.FileName;

                MessageBox.Show(fileName);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
