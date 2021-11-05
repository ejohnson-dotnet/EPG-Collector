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
    internal partial class ChangeProgramCategoryControl : BaseChangeCategoryControl, IUpdateControl
    {
        /// <summary>
        /// Get the general window heading for the data.
        /// </summary>
        public string Heading { get { return ("EPG Centre - Change OpenTV Program Categories - "); } }
        /// <summary>
        /// Get the default directory.
        /// </summary>
        public string DefaultDirectory { get { return (RunParameters.ConfigDirectory); } }
        /// <summary>
        /// Get the default output file name.
        /// </summary>
        public string DefaultFileName { get { return ("Program Categories XXX"); } }
        /// <summary>
        /// Get the save file filter.
        /// </summary>
        public string SaveFileFilter { get { return ("OpenTV Program Category Files (Program Categories*.cfg)|Program Categories*.cfg"); } }
        /// <summary>
        /// Get the save file title.
        /// </summary>
        public string SaveFileTitle { get { return ("Save EPG Collection OpenTV Program Category File"); } }
        /// <summary>
        /// Get the save file suffix.
        /// </summary>
        public string SaveFileSuffix { get { return ("cfg"); } }

        internal ChangeProgramCategoryControl()
        {
            InitializeComponent();
        }

        internal bool Process(string fileName)
        {
            if (!OpenTVProgramCategory.Load(fileName))
                return (false);

            CurrentFileName = fileName;

            DoNotValidate = true;

            BindingList = new BindingList<ProgramCategory>();
            foreach (ProgramCategory category in OpenTVProgramCategory.Categories)
                BindingList.Add(category.Clone());
            
            categoryBindingSource.DataSource = BindingList;
            dgCategories.FirstDisplayedCell = dgCategories.Rows[0].Cells[0];
            dgCategories.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

            DoNotValidate = false;

            return (true);
        }

        private void dgCategories_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgCategories.CurrentCell.ColumnIndex == dgCategories.Columns[CategoryTagColumnName].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(TagTextEdit_KeyPressNumeric);
                textEdit.KeyPress += new KeyPressEventHandler(TagTextEdit_KeyPressNumeric);
            }
            else
                CategoriesViewEditingControlShowing(sender, e);
        }

        private void dgCategoriesRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            HasErrors = !IsCategoriesRowValid(sender, e, false);
        }

        protected override bool IsCategoryTagValid(string categoryTag)
        {
            try
            {
                int categoryID = Int32.Parse(categoryTag);
            }
            catch (FormatException)
            {
                MessageBox.Show("Category tag " + categoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void dgCategoriesDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            base.CategoriesViewDefaultValuesNeeded(sender, e);   
        }

        private void dgCategoriesColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            base.SortCategoriesView(sender, e);
        }

        protected override DataState HasDataChanged()
        {
            dgCategories.EndEdit();

            if (HasErrors)
                return (DataState.HasErrors);

            if (BindingList.Count != OpenTVProgramCategory.Categories.Count)
                return (DataState.Changed);

            foreach (ProgramCategory category in BindingList)
            {
                ProgramCategory existingCategory = OpenTVProgramCategory.FindCategory(category.CategoryTag);
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
            bool reply = base.PrepareToSave(dgCategories);
            if (!reply)
                return false;

            OpenTVProgramCategory.Categories.Clear();

            foreach (ProgramCategory category in BindingList)
            {
                if (!category.IsEmpty)
                    OpenTVProgramCategory.Categories.Add(category);
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
        /// <param name="fileName">The name of the file to be saved.</param>
        /// <returns>True if the data has been saved; false otherwise.</returns>
        public bool Save(string fileName)
        {
            string[] parts = fileName.Split(new char[] { Path.DirectorySeparatorChar });
            if (parts.Length < 2 || fileName != Path.Combine(RunParameters.DataDirectory, parts[parts.Length - 1]))
            {
                DialogResult result = MessageBox.Show("The data must be saved to the data directory.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            string message = OpenTVProgramCategory.Save(fileName);

            if (message == null)
            {
                MessageBox.Show("The OpenTV program categories have been saved to '" + fileName + "'", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CurrentFileName = fileName;
            }
            else
                MessageBox.Show("An error has occurred while writing the OpenTV program categories." + Environment.NewLine + Environment.NewLine + message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return (message == null);
        }
    }
}

