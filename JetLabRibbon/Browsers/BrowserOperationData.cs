using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI.Selection;

namespace JetLabRibbon.Browsers
{
    /// <summary>
    /// Data class which stores the information of browser operation
    /// </summary>
    public class BrowserOperationData
    {
        #region Fields
        /// <summary>
        /// Active UI Revit document
        /// </summary>
        UIDocument m_revitDoc;

        /// <summary>
        /// Operation on elements
        /// </summary>
        private Operation m_operation;
        #endregion

        #region Properties
        /// <summary>
        /// Operation type
        /// </summary>
        public Operation Operation
        {
            get
            {
                return m_operation;
            }

            set
            {
                m_operation = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commandData">Revit's external commandData</param>
        public BrowserOperationData(ExternalCommandData commandData)
        {
            m_revitDoc = commandData.Application.ActiveUIDocument;
        }

        /// <summary>
        /// Dispatch operations
        /// </summary>
        public void Operate()
        {
            Transaction transaction = new Transaction(m_revitDoc.Document, m_operation.ToString());
            transaction.Start();
            switch (m_operation)
            {
                case Operation.CreateLevel:
                    CreateLevel();
                    break;
                default:
                    break;
            }
            transaction.Commit();

        }

        /// <summary>
        /// Create a level
        /// </summary>
        public void CreateLevel()
        {
            // The elevation to apply to the new level
            double elevation = 20.0;

            // Begin to create a level
            Level level = Level.Create(m_revitDoc.Document, elevation);
            if (null == level)
            {
                throw new Exception("Create a new level failed.");
            }

            // Change the level name
            level.Name = "New level";
        }


        /// <summary>
        /// Show message box with specified string
        /// </summary>
        /// <param name="message">specified string to show</param>
        static private void ShowErrorMessage(String message)
        {
            TaskDialog.Show(Properties.Resources.ResourceManager.GetString("OperationFailed"), Properties.Resources.ResourceManager.GetString(message), TaskDialogCommonButtons.Ok);
        }
        #endregion
    }
}
