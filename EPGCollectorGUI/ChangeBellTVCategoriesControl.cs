////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                           //
//                                                                              //
//  This Program is free software; you can redistribute it and/or modify        //
//  it under the terms of the GNU General Public License as published by        //
//  the Free Software Foundation; either version 2, or (at your option)         //
//  any later version.                                                          //
//                                                                              //
//  This Program is distributed in the hope that it will be useful,             //
//  but WITHOUT ANY WARRANTY; without even the implied warranty of              //
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                //
//  GNU General Public License for more details.                                //
//                                                                              //
//  You should have received a copy of the GNU General Public License           //
//  along with GNU Make; see the file COPYING.  If not, write to                //
//  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.       //
//  http://www.gnu.org/copyleft/gpl.html                                        //
//                                                                              //  
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

using DomainObjects;
using DVBServices;

namespace EPGCentre
{
    internal partial class ChangeBellTVCategoriesControl : BaseChangeCategoryControl, IUpdateControl
    {
        /// <summary>
        /// Get the heading.
        /// </summary>
        public string Heading { get { return ("EPG Centre - Change Bell TV Program Categories - "); } }
        /// <summary>
        /// Get the default directory.
        /// </summary>
        public string DefaultDirectory { get { return (RunParameters.ConfigDirectory); } }
        /// <summary>
        /// Get the default output file name.
        /// </summary>
        public string DefaultFileName { get { return ("Bell TV Categories"); } }
        /// <summary>
        /// Get the save file filter.
        /// </summary>
        public string SaveFileFilter { get { return ("Bell TV Program Category Files (Bell TV Categories*.cfg)|Bell TV Categories*.cfg"); } }
        /// <summary>
        /// Get the save file title.
        /// </summary>
        public string SaveFileTitle { get { return ("Save EPG Collection Bell TV Program Category File"); } }
        /// <summary>
        /// Get the save file suffix.
        /// </summary>
        public string SaveFileSuffix { get { return ("cfg"); } }

        internal ChangeBellTVCategoriesControl()
        {
            InitializeComponent();
        }

        internal bool Process(string fileName)
        {
            if (!BellTVProgramCategory.Load(fileName))
                return (false);

            CurrentFileName = fileName;

            DoNotValidate = true;

            BindingList = new BindingList<ProgramCategory>();
            foreach (ProgramCategory category in BellTVProgramCategory.Categories)
                BindingList.Add(category.Clone());

            contentBindingSource.DataSource = BindingList;
            dgContents.FirstDisplayedCell = dgContents.Rows[0].Cells[0];
            dgContents.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

            DoNotValidate = false;

            return (true);
        }

        private void dgContents_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgContents.CurrentCell.ColumnIndex == dgContents.Columns[CategoryTagColumnName].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(TagTextEdit_KeyPressNumericComma);
                textEdit.KeyPress += new KeyPressEventHandler(TagTextEdit_KeyPressNumericComma);
            }
            else
                CategoriesViewEditingControlShowing(sender, e);
        }

        private void dgContentsRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            HasErrors = !IsCategoriesRowValid(sender, e, false);
        }

        protected override bool IsCategoryTagValid(string categoryTag)
        {  
            if (!categoryTag.Contains(","))
            {
                MessageBox.Show("Category tag '" + categoryTag + "' is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string[] idParts = categoryTag.Split(new char[] { ',' });

            if (idParts.Length != 2)
            {
                MessageBox.Show("Category tag '" + categoryTag + "' is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Int32.Parse(idParts[0]);
            }
            catch (FormatException)
            {
                MessageBox.Show("Category tag '" + categoryTag + "' is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Int32.Parse(idParts[1]);
            }
            catch (FormatException)
            {
                MessageBox.Show("Category tag '" + categoryTag + "' is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void dgContentsDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            base.CategoriesViewDefaultValuesNeeded(sender, e);          
        }

        private void categoriesViewColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            base.SortCategoriesView(sender, e);
        }

        protected override DataState HasDataChanged()
        {
            dgContents.EndEdit();

            if (HasErrors)
                return (DataState.HasErrors);

            if (BindingList.Count != BellTVProgramCategory.Categories.Count)
                return (DataState.Changed);

            foreach (ProgramCategory category in BindingList)
            {
                ProgramCategory existingCategory = BellTVProgramCategory.FindCategory(category.CategoryTag);
                if (existingCategory == null)
                    return (DataState.Changed);

                if (existingCategory.IsDifferent(category))
                    return (DataState.Changed);
            }

            return (DataState.NotChanged);
        }

        /// <summary>
        /// Validate the data and set up to save it.
        /// </summary>
        /// <returns>True if the data can be saved; false otherwise.</returns>
        public bool PrepareToSave()
        {
            bool reply = base.PrepareToSave(dgContents);
            if (!reply)
                return false;

            BellTVProgramCategory.Categories.Clear();

            foreach (ProgramCategory category in BindingList)
            {
                if (!category.IsEmpty)
                    BellTVProgramCategory.Categories.Add(category);
            }

            return (true);
        }

        /// <summary>
        /// Save the current data set to the original file.
        /// </summary>
        /// <returns>True if the data has been saved; false otherwise.</returns>
        public bool Save()
        {
            FileInfo fileInfo = new FileInfo(CurrentFileName);
            return (Save(Path.Combine(RunParameters.DataDirectory, fileInfo.Name)));
        }

        /// <summary>
        /// Save the current data set to a specified file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>True if the data has been saved; false otherwise.</returns>
        public bool Save(string fileName)
        {
            string[] parts = fileName.Split(new char[] { Path.DirectorySeparatorChar });
            if (parts.Length < 2 || fileName != Path.Combine(RunParameters.DataDirectory, parts[parts.Length - 1]))
            {
                DialogResult result = MessageBox.Show("The data must be saved to the data directory.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            string message = BellTVProgramCategory.Save(fileName);

            if (message == null)
            {
                MessageBox.Show("The Bell TV program categories have been saved to '" + fileName + "'", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CurrentFileName = fileName;
            }
            else
                MessageBox.Show("An error has occurred while writing the Bell TV program categories." + Environment.NewLine + Environment.NewLine + message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return (message == null);
        }
    }
}

