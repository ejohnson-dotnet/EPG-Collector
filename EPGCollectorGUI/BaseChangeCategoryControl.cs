using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using DomainObjects;

namespace EPGCentre
{
    public partial class BaseChangeCategoryControl : UserControl
    {
        /// <summary>
        /// Get or set the flag that inhibits validation.
        /// </summary>
        protected bool DoNotValidate { get; set; }

        /// <summary>
        /// Return the state of the data set.
        /// </summary>
        public DataState DataState { get { return (HasDataChanged()); } }

        /// <summary>
        /// Get or set the binding list.
        /// </summary>
        protected BindingList<ProgramCategory> BindingList { get; set; }
        /// <summary>
        /// Get or set the errors flag.
        /// </summary>
        protected bool HasErrors { get; set; }
        /// <summary>
        /// Get or set the file name.
        /// </summary>
        protected string CurrentFileName { get; set; }

        /// <summary>
        /// Get the category tag column name.
        /// </summary>
        protected const string CategoryTagColumnName = "categoryTagColumn";
        /// <summary>
        /// Get the XMLTV column name.
        /// </summary>
        protected const string XmltvDescriptionColumnName = "xmltvDescriptionColumn";
        /// <summary>
        /// Get the WMC column name.
        /// </summary>
        protected const string WmcDescriptionColumnName = "wmcDescriptionColumn";
        /// <summary>
        /// Get the DVBLogic column name.
        /// </summary>
        protected const string DvblogicDescriptionColumnName = "dvblogicDescriptionColumn";
        /// <summary>
        /// Get the DVBViewer column name.
        /// </summary>
        protected const string DvbviewerDescriptionColumnName = "dvbviewerDescriptionColumn";

        private bool sortedAscending;
        private string sortedColumnName;
        private ProgramCategory.SortKey sortedKeyName;

        /// <summary>
        /// Initialize a new instance of the BaseChangeCategoryControl class.
        /// </summary>
        public BaseChangeCategoryControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get default values for columns.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void CategoriesViewDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            foreach (DataGridViewCell cell in e.Row.Cells)
                cell.Value = string.Empty;
        }

        /// <summary>
        /// Set the data entry constraints.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">    The event arguments.</param>
        protected virtual void CategoriesViewEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView categoriesView = sender as DataGridView;

            if (categoriesView.CurrentCell.ColumnIndex == categoriesView.Columns[XmltvDescriptionColumnName].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(xmltvTextEdit_KeyPress);
                textEdit.KeyPress += new KeyPressEventHandler(xmltvTextEdit_KeyPress);

                return;
            }
            
            if (categoriesView.CurrentCell.ColumnIndex == categoriesView.Columns[WmcDescriptionColumnName].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(wmcTextEdit_KeyPress);
                textEdit.KeyPress += new KeyPressEventHandler(wmcTextEdit_KeyPress);

                return;
            }
                
            if (categoriesView.CurrentCell.ColumnIndex == categoriesView.Columns[DvblogicDescriptionColumnName].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(dvblogicTextEdit_KeyPress);
                textEdit.KeyPress += new KeyPressEventHandler(dvblogicTextEdit_KeyPress);

                return;
            }
                    
            if (categoriesView.CurrentCell.ColumnIndex == categoriesView.Columns[DvbviewerDescriptionColumnName].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(dvbviewerTextEdit_KeyPress);
                textEdit.KeyPress += new KeyPressEventHandler(dvbviewerTextEdit_KeyPress);                        
            }
        }

        /// <summary>
        /// Set constraints for numeric tags.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void TagTextEdit_KeyPressNumeric(object sender, KeyPressEventArgs e)
        {
            if ("0123456789\b".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        /// <summary>
        /// Set constraints for 2 part tags.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void TagTextEdit_KeyPressNumericComma(object sender, KeyPressEventArgs e)
        {
            if ("0123456789,\b".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        /// <summary>
        /// Set the constraints for alphanumeric tags.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void TagTextEdit_KeyPressAlphaNumeric(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z0-9,!&*()--+'?<>\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void xmltvTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z0-9,!&*()--+'?<>\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void wmcTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z0-9,!&*()--+'?<>\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void dvblogicTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z,\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void dvbviewerTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("0123456789,\b".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        /// <summary>
        /// Validate a row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        /// <param name="dvbviewerPresent">True if the DVBViewer column is present; false otherwise.</param>
        /// <returns></returns>
        protected bool IsCategoriesRowValid(object sender, DataGridViewCellCancelEventArgs e, bool dvbviewerPresent)
        {
            if (DoNotValidate)
                return true;

            DataGridView categoriesView = sender as DataGridView;
            if (categoriesView == null)
                return true;

            string categoryTag;
            string xmltvDescription;
            string wmcDescription;
            string dvblogicDescription;
            string dvbviewerDescription = null;

            if (categoriesView.Rows[e.RowIndex].Cells[CategoryTagColumnName].Value == null)
                categoryTag = string.Empty;
            else
                categoryTag = categoriesView.Rows[e.RowIndex].Cells[CategoryTagColumnName].Value.ToString().Trim();

            if (categoriesView.Rows[e.RowIndex].Cells[XmltvDescriptionColumnName].Value == null)
                xmltvDescription = string.Empty;
            else
                xmltvDescription = categoriesView.Rows[e.RowIndex].Cells[XmltvDescriptionColumnName].Value.ToString().Trim();

            if (categoriesView.Rows[e.RowIndex].Cells[WmcDescriptionColumnName].Value == null)
                wmcDescription = string.Empty;
            else
                wmcDescription = categoriesView.Rows[e.RowIndex].Cells[WmcDescriptionColumnName].Value.ToString().Trim();

            if (categoriesView.Rows[e.RowIndex].Cells[DvblogicDescriptionColumnName].Value == null)
                dvblogicDescription = string.Empty;
            else
                dvblogicDescription = categoriesView.Rows[e.RowIndex].Cells[DvblogicDescriptionColumnName].Value.ToString().Trim();

            if (dvbviewerPresent)
            {
                if (categoriesView.Rows[e.RowIndex].Cells[DvbviewerDescriptionColumnName].Value == null)
                    dvbviewerDescription = string.Empty;
                else
                    dvbviewerDescription = categoriesView.Rows[e.RowIndex].Cells[DvbviewerDescriptionColumnName].Value.ToString().Trim();
            }

            if (categoryTag == string.Empty &&
                xmltvDescription == string.Empty &&
                wmcDescription == string.Empty &&
                dvblogicDescription == string.Empty &&
                dvbviewerDescription == string.Empty)
            {
                e.Cancel = true;
                return true;
            }

            if (!IsCategoryTagValid(categoryTag))
            {
                e.Cancel = true;
                return false;
            }

            BindingSource bindingSource = categoriesView.DataSource as BindingSource;
            if (bindingSource == null)
                return true;
            BindingList<ProgramCategory> bindingList = bindingSource.DataSource as BindingList<ProgramCategory>; ;
            if (bindingList == null)
                return true;

            Collection<string> tags = new Collection<string>();
            foreach (ProgramCategory category in bindingList)
            {
                if (tags.Contains(category.CategoryTag))
                {
                    MessageBox.Show("Category tag " + categoryTag + " already exists.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return false;
                }
                else
                    tags.Add(category.CategoryTag);
            }

            if (xmltvDescription == string.Empty)
            {
                MessageBox.Show("The XMLTV description for tag " + categoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return false;
            }

            if (dvblogicDescription != null)
            {
                bool validValue = DVBLogicProgramCategory.CheckDescription(dvblogicDescription.ToString());
                if (!validValue)
                {
                    MessageBox.Show("The DVBLogic description for category tag " + categoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return false;
                }
            }

            if (dvbviewerDescription != null)
            {
                bool validValue = DVBViewerProgramCategory.CheckDescription(dvbviewerDescription.ToString());
                if (!validValue)
                {
                    MessageBox.Show("The DVBViewer description for category tag " + categoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if the category tag is valid.
        /// </summary>
        /// <param name="categoryTag">The tag string.</param>
        /// <returns>True if it is valid; false otherwise.</returns>
        protected virtual bool IsCategoryTagValid(string categoryTag)
        {
            if (categoryTag == string.Empty)
            {
                MessageBox.Show("Category tag " + categoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sort the categories view.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void SortCategoriesView(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView categoriesView = sender as DataGridView;
            if (categoriesView == null)
                return;

            BindingSource bindingSource = categoriesView.DataSource as BindingSource;
            if (bindingSource == null)
                return;
            BindingList<ProgramCategory> bindingList = bindingSource.DataSource as BindingList<ProgramCategory>; ;
            if (bindingList == null)
                return;

            if (sortedColumnName == null)
            {
                sortedAscending = false;
                sortedColumnName = categoriesView.Columns[e.ColumnIndex].Name;
            }
            else
            {
                if (sortedColumnName == categoriesView.Columns[e.ColumnIndex].Name)
                    sortedAscending = !sortedAscending;
                else
                    sortedColumnName = categoriesView.Columns[e.ColumnIndex].Name;
            }

            Collection<ProgramCategory> sortedCategories = new Collection<ProgramCategory>();            

            foreach (ProgramCategory category in bindingList)
            {
                switch (sortedColumnName)
                {
                    case CategoryTagColumnName:
                        addInOrder(sortedCategories, category, sortedAscending, ProgramCategory.SortKey.CategoryTag);
                        break;
                    case XmltvDescriptionColumnName:
                        addInOrder(sortedCategories, category, sortedAscending, ProgramCategory.SortKey.XmltvDescription);
                        break;
                    case WmcDescriptionColumnName:
                        addInOrder(sortedCategories, category, sortedAscending, ProgramCategory.SortKey.WmcDescription);
                        break;
                    case DvblogicDescriptionColumnName:
                        addInOrder(sortedCategories, category, sortedAscending, ProgramCategory.SortKey.DvbLogicDescription);
                        break;
                    case DvbviewerDescriptionColumnName:
                        addInOrder(sortedCategories, category, sortedAscending, ProgramCategory.SortKey.DvbViewerDescription);
                        break;
                    default:
                        return;
                }
            }

            DoNotValidate = true;

            bindingList = new BindingList<ProgramCategory>();
            foreach (ProgramCategory category in sortedCategories)
                bindingList.Add(category);

            categoriesView.DataSource = new BindingSource();
            ((BindingSource)categoriesView.DataSource).DataSource = bindingList;

            if (sortedAscending)
                categoriesView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            else
                categoriesView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;

            DoNotValidate = false;
        }

        private void addInOrder(Collection<ProgramCategory> categories, ProgramCategory newCategory, bool sortedAscending, ProgramCategory.SortKey keyName)
        {
            sortedKeyName = keyName;

            foreach (ProgramCategory oldCategory in categories)
            {
                if (sortedAscending)
                {
                    if (oldCategory.CompareForSorting(newCategory, keyName) > 0)
                    {
                        categories.Insert(categories.IndexOf(oldCategory), newCategory);
                        return;
                    }
                }
                else
                {
                    if (oldCategory.CompareForSorting(newCategory, keyName) < 0)
                    {
                        categories.Insert(categories.IndexOf(oldCategory), newCategory);
                        return;
                    }
                }
            }

            categories.Add(newCategory);
        }

        /// <summary>
        /// Validate acategory addition.
        /// </summary>
        /// <param name="addedList">The current additions.</param>
        /// <param name="category">The new category.</param>
        /// <returns></returns>
        protected bool ValidateEntry(Collection<ProgramCategory> addedList, ProgramCategory category)
        {
            foreach (ProgramCategory existingCategory in addedList)
            {
                if (existingCategory.CategoryTag == category.CategoryTag)
                {
                    MessageBox.Show("Category tag " + existingCategory.CategoryTag + " already exists.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false);
                }
            }

            if (string.IsNullOrWhiteSpace(category.XmltvDescription))
            {
                MessageBox.Show("The XMLTV description for category tag " + category.CategoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            bool validValue = DVBLogicProgramCategory.CheckDescription(category.DVBLogicDescription);
            if (!validValue)
            {
                MessageBox.Show("The DVBLogic description for category tag " + category.CategoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            validValue = DVBViewerProgramCategory.CheckDescription(category.DVBViewerDescription);
            if (!validValue)
            {
                MessageBox.Show("The DVBViewer description for category tag " + category.CategoryTag + " is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// Check if any data has changed.
        /// </summary>
        /// <returns></returns>
        protected virtual DataState HasDataChanged()
        {
            return DataState.NotChanged;
        }

        /// <summary>
        /// Prepare to save the categories.
        /// </summary>
        /// <param name="categoriesView">The categories view.</param>
        /// <returns>True if they have been saved; false otherwise.</returns>
        protected bool PrepareToSave(DataGridView categoriesView)
        {
            categoriesView.EndEdit();

            if (BindingList.Count == 0)
            {
                MessageBox.Show("No categories defined.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            Collection<ProgramCategory> addedList = new Collection<ProgramCategory>();

            foreach (ProgramCategory category in BindingList)
            {
                if (!category.IsEmpty)
                {
                    bool valid = ValidateEntry(addedList, category);
                    if (!valid)
                        return (false);

                    addedList.Add(category);
                }
            }

            if (addedList.Count == 0)
            {
                DialogResult result = MessageBox.Show("No categories defined." + Environment.NewLine + Environment.NewLine +
                    "Is this correct?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return (false);
            }

            return true;
        }
    }
}
