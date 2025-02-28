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
using System.IO;
using System.Text;

namespace DomainObjects
{
    /// <summary>
    /// The class that describes an XMLTV program category.
    /// </summary>
    public class ProgramCategory : IEventCategory
    { 
        /// <summary>
        /// Get or set the category tag.
        /// </summary>
        public string CategoryTag { get; set; }        

        /// <summary>
        /// Get the XMLTV description.
        /// </summary>
        public string XmltvDescription { get; set; }        

        /// <summary>
        /// Get the WMC description.
        /// </summary>
        public string WMCDescription { get; set; }        

        /// <summary>
        /// Get the DVBLogic description.
        /// </summary>
        public string DVBLogicDescription { get; set; }        

        /// <summary>
        /// Get the DVBViewer description.
        /// </summary>
        public string DVBViewerDescription { get; set; }        

        /// <summary>
        /// Get or set a description of the sample event for the category.
        /// </summary>
        public string SampleEvent { get; set; }

        /// <summary>
        /// Get or set the usage count for the content.
        /// </summary>
        public int UsedCount { get; set; }        

        /// <summary>
        /// Return true if the instance is empty; false otherwise.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(CategoryTag) &&
                    string.IsNullOrWhiteSpace(XmltvDescription) &&
                    string.IsNullOrWhiteSpace(WMCDescription) &&
                    string.IsNullOrWhiteSpace(DVBLogicDescription) &&
                    string.IsNullOrWhiteSpace(DVBViewerDescription);
            }
        }

        /// <summary>
        /// The available sort fields.
        /// </summary>
        public enum SortKey
        {
            /// <summary>
            /// Order on the tag.
            /// </summary>
            CategoryTag,
            /// <summary>
            /// Order on the XMLTV description.
            /// </summary>
            XmltvDescription,
            /// <summary>
            /// Order on the WMC description.
            /// </summary>
            WmcDescription,
            /// <summary>
            /// Order on the DVBLogic description.
            /// </summary>
            DvbLogicDescription,
            /// <summary>
            /// Order on the DVBViewer description.
            /// </summary>
            DvbViewerDescription
        }

        /// <summary>
        /// Initialize a new instance of the ProgramCategory class.
        /// </summary>
        public ProgramCategory() { }

        /// <summary>
        /// Initialize a new instance of the ProgramCategory class.
        /// </summary>
        /// <param name="categoryTag">The category tag.</param>
        public ProgramCategory(string categoryTag)
        {
            CategoryTag = categoryTag;            
        }

        /// <summary>
        /// Initialize a new instance of the ProgramCategory class.
        /// </summary>
        /// <param name="categoryNumber">The category number.</param>
        public ProgramCategory(int categoryNumber) : this(categoryNumber.ToString())
        { 
        }

        /// <summary>
        /// Initialize a new instance of the ProgramCategory class.
        /// </summary>
        /// <param name="categoryNumber">The category number.</param>
        /// /// <param name="categorySubnumber">The category subnumber.</param>
        public ProgramCategory(int categoryNumber, int categorySubnumber) : this(categoryNumber + "," + categorySubnumber)
        {            
        }

        /// <summary>
        /// Find a category.
        /// </summary>
        /// <param name="categories">The collection of categories to search.</param>
        /// <param name="categoryTag">The category tag.</param>
        /// <returns>A category instance or null if the category is undefined.</returns>
        protected static ProgramCategory FindCategory(Collection<ProgramCategory> categories, string categoryTag)
        {
            if (categories == null || string.IsNullOrWhiteSpace(categoryTag))
                return (null);

            foreach (ProgramCategory category in categories)
            {
                if (category.CategoryTag.ToLowerInvariant() == categoryTag.ToLowerInvariant())
                    return (category);
            }

            return (null);
        }

        /// <summary>
        /// Find a category.
        /// </summary>
        /// <param name="categories">The collection of categories to search.</param>
        /// <param name="categoryNumber">The category number.</param>
        /// <returns>A category instance or null if the category is undefined.</returns>
        protected static ProgramCategory FindCategory(Collection<ProgramCategory> categories, int categoryNumber)
        {
            return FindCategory(categories, categoryNumber.ToString());
        }

        /// <summary>
        /// Find a category.
        /// </summary>
        /// <param name="categories">The collection of categories to search.</param>
        /// <param name="categoryNumber">The category number.</param>
        /// <param name="subnumber">The category subnumber.</param>
        /// <returns>A category instance or null if the category is undefined.</returns>
        protected static ProgramCategory FindCategory(Collection<ProgramCategory> categories, int categoryNumber, int subnumber)
        {
            return FindCategory(categories, categoryNumber + "," + subnumber);
        }

        /// <summary>
        /// Add a category to the collection.
        /// </summary>
        /// <param name="categories">The collection of categories to update.</param>
        /// <param name="categoryTag">The category tag.</param>
        /// <param name="description">The category description.</param>
        /// <returns>True if category added; false otherwise.</returns>
        public static bool AddCategory(Collection<ProgramCategory> categories, string categoryTag, string description)
        {
            foreach (ProgramCategory category in categories)
            {
                if (category.CategoryTag == categoryTag)
                    return false;
            }
            
            string[] parts = description.Split(new char[] { '=' });
            if (parts.Length == 0 || parts.Length > 4)
            {
                Logger.Instance.Write("Invalid category description '" + categoryTag + "' - category ignored ");
                return false;
            }

            ProgramCategory newCategory = new ProgramCategory(categoryTag.Trim());
            if (parts.Length > 0)
                newCategory.XmltvDescription = parts[0].Trim();
            if (parts.Length > 1)
                newCategory.WMCDescription = parts[1].Trim();
            if (parts.Length > 2)
                newCategory.DVBLogicDescription = parts[2].Trim();
            if (parts.Length > 3)
                newCategory.DVBViewerDescription = parts[3].Trim();

            categories.Add(newCategory);

            return true;
        }

        /// <summary>
        /// Add a category to the collection.
        /// </summary>
        /// <param name="categories">The collection of categies to update.</param>
        /// <param name="categoryNumber">The category number.</param>
        /// <param name="description">The category description.</param>
        /// <returns>True if category added; false otherrwise.</returns>
        public static bool AddCategory(Collection<ProgramCategory> categories, int categoryNumber, string description)
        {
            return AddCategory(categories, categoryNumber.ToString(), description);
        }

        /// <summary>
        /// Add a category to the collection.
        /// </summary>
        /// <param name="categories">The collection of categories to update..</param>
        /// <param name="categoryNumber">The category number.</param>
        /// <param name="subnumber">The category subnumber.</param>
        /// <param name="description">The category description.</param>
        public static void AddCategory(Collection<ProgramCategory> categories, int categoryNumber, int subnumber, string description)
        {
            AddCategory(categories, categoryNumber + "," + subnumber, description);
        }

        /// <summary>
        /// Load the category definitions.
        /// </summary>
        /// <returns>True if the file has been loaded; false otherwise.</returns>
        public static bool Load(Collection<ProgramCategory> categories, string fileName)
        {
            string actualFileName = Path.Combine(RunParameters.DataDirectory, fileName);
            if (!File.Exists(actualFileName))
            {
                actualFileName = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", fileName));
                if (!File.Exists(actualFileName))
                    return (true);
            }

            Logger.Instance.Write("Loading Program Categories from " + actualFileName);

            FileStream fileStream = null;

            try { fileStream = new FileStream(actualFileName, FileMode.Open, FileAccess.Read); }
            catch (IOException e)
            {
                Logger.Instance.Write("Program category file " + actualFileName + " not available");
                Logger.Instance.Write(e.Message);
                return (false);
            }

            categories.Clear();

            StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (line != string.Empty && line[0] != '#')
                {
                    string[] parts = line.Split(new char[] { '=' });
                    if (parts.Length < 2)
                        Logger.Instance.Write("Program category line '" + line + "' format wrong - line ignored ");
                    else
                        AddCategory(categories, parts[0].Trim(), line.Substring(parts[0].Length + 1));
                }
            }

            streamReader.Close();
            fileStream.Close();

            if (categories != null)
                Logger.Instance.Write("Loaded " + categories.Count + " program categories");
            else
                Logger.Instance.Write("No program categories loaded");

            return (true);
        }

        /// <summary>
        /// Save the categories to a file.
        /// </summary>
        /// <param name="categories">The collection of categories to save.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>Null if the operation succeeded; otherwise an error message.</returns>
        public static string Save(Collection<ProgramCategory> categories, string fileName)
        {
            string actualFileName = Path.Combine(RunParameters.DataDirectory, fileName);

            Logger.Instance.Write("Saving Program Categories to " + actualFileName);

            try
            {
                if (File.Exists(actualFileName))
                {
                    if (File.Exists(actualFileName + ".bak"))
                    {
                        File.SetAttributes(actualFileName + ".bak", FileAttributes.Normal);
                        File.Delete(actualFileName + ".bak");
                    }

                    File.Copy(actualFileName, actualFileName + ".bak");
                    File.SetAttributes(actualFileName + ".bak", FileAttributes.ReadOnly);

                    File.SetAttributes(actualFileName, FileAttributes.Normal);
                }

                FileStream fileStream = new FileStream(actualFileName, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                foreach (ProgramCategory category in categories)
                {
                    if (!string.IsNullOrWhiteSpace(category.DVBViewerDescription))
                    {
                        streamWriter.WriteLine(category.CategoryTag + "=" +
                            (string.IsNullOrWhiteSpace(category.XmltvDescription) ? string.Empty : category.XmltvDescription) + "=" +
                            (string.IsNullOrWhiteSpace(category.WMCDescription) ? string.Empty : category.WMCDescription) + "=" +
                            (string.IsNullOrWhiteSpace(category.DVBLogicDescription) ? string.Empty : category.DVBLogicDescription) + "=" +
                            (string.IsNullOrWhiteSpace(category.DVBViewerDescription) ? string.Empty : category.DVBViewerDescription));
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(category.DVBLogicDescription))
                        {
                            streamWriter.WriteLine(category.CategoryTag + "=" +
                                (string.IsNullOrWhiteSpace(category.XmltvDescription) ? string.Empty : category.XmltvDescription) + "=" +
                                (string.IsNullOrWhiteSpace(category.WMCDescription) ? string.Empty : category.WMCDescription) + "=" +
                                (string.IsNullOrWhiteSpace(category.DVBLogicDescription) ? string.Empty : category.DVBLogicDescription));
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(category.WMCDescription))
                            {
                                streamWriter.WriteLine(category.CategoryTag + "=" +
                                    (string.IsNullOrWhiteSpace(category.XmltvDescription) ? string.Empty : category.XmltvDescription) + "=" +
                                    (string.IsNullOrWhiteSpace(category.WMCDescription) ? string.Empty : category.WMCDescription));
                            }
                            else
                            {
                                streamWriter.WriteLine(category.CategoryTag + "=" +
                                    (string.IsNullOrWhiteSpace(category.XmltvDescription) ? string.Empty : category.XmltvDescription));
                            }
                        }
                    }
                }

                streamWriter.Close();
                fileStream.Close();

                File.SetAttributes(actualFileName, FileAttributes.ReadOnly);

                return (null);
            }
            catch (IOException e)
            {
                return (e.Message);
            }
        }

        /// <summary>
        /// Compare this instance with another for sorting purposes.
        /// </summary>
        /// <param name="category">The other instance.</param>
        /// <param name="sortKey">The name of the key to compare on.</param>
        /// <returns>Zero if the instances are equal, Greater than 0 if this instance is greater; less than zero otherwise.</returns>
        public int CompareForSorting(ProgramCategory category, SortKey sortKey)
        {
            int reply;

            switch (sortKey)
            {
                case SortKey.CategoryTag:                    
                    return compareTags(category.CategoryTag);
                case SortKey.XmltvDescription:
                    reply = compareDescriptions(XmltvDescription, category.XmltvDescription);
                    if (reply == 0)
                        reply = compareTags(category.CategoryTag);
                    return reply;
                case SortKey.WmcDescription:
                    reply = compareDescriptions(WMCDescription, category.WMCDescription);
                    if (reply == 0)
                        reply = compareTags(category.CategoryTag);
                    return reply;
                case SortKey.DvbLogicDescription:
                    reply = compareDescriptions(DVBLogicDescription, category.DVBLogicDescription);
                    if (reply == 0)
                        reply = compareTags(category.CategoryTag);
                    return reply;
                case SortKey.DvbViewerDescription:
                    reply = compareNumbers(DVBViewerDescription, category.DVBViewerDescription);
                    if (reply == 0)
                        reply = compareTags(category.CategoryTag);
                    return reply;
                default:
                    return (0);
            }
        }

        private int compareTags(string otherTag) 
        {
            if (Char.IsDigit(CategoryTag[0]))
                return compareNumbers(CategoryTag, otherTag);
            else
                return CategoryTag.CompareTo(otherTag);
        }

        private int compareDescriptions(string thisDescription, string otherDescription)
        {
            string description1;
            string description2;

            if (thisDescription != null)
                description1 = thisDescription;
            else
                description1 = string.Empty;

            if (otherDescription != null)
                description2 = otherDescription;
            else
                description2 = string.Empty;

            return (description1.CompareTo(description2));
        }

        private int compareNumbers(string thisDescription, string otherDescription)
        {
            if (thisDescription == null && otherDescription == null)
                return 0;

            if (thisDescription == null)
                return -1;

            if (otherDescription == null)
                return 1;

            string[] thisParts = thisDescription.Split(new char[] { ',' });
            string[] otherParts = otherDescription.Split(new char[] { ',' });

            if (thisParts.Length == 1)
            {
                try
                {
                    int thisPartNumber = Int32.Parse(thisParts[0]); 
                    int otherPartNumber = Int32.Parse(otherParts[0]);                    

                    return thisPartNumber.CompareTo(otherPartNumber);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
            {
                try
                {
                    int thisPartNumber = Int32.Parse(thisParts[0]);
                    int thisSubNumber = Int32.Parse(thisParts[1]);

                    int otherPartNumber = Int32.Parse(otherParts[0]);
                    int otherSubNumber = Int32.Parse(otherParts[1]);

                    int reply = thisPartNumber.CompareTo(otherPartNumber);
                    if (reply == 0)
                        reply = thisSubNumber.CompareTo(otherSubNumber);

                    return reply;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Combine another category with the current instance.
        /// </summary>
        /// <param name="otherCategory">The other category.</param>
        /// <returns>An update category instance.</returns>
        public ProgramCategory Combine(ProgramCategory otherCategory)
        {
            ProgramCategory newCategory = Clone();

            string[] otherXmltvParts = otherCategory.XmltvDescription.Split(new char[] { ',' });
            foreach (string otherXmltvPart in otherXmltvParts)
            {
                if (!newCategory.XmltvDescription.Contains(otherXmltvPart))
                    newCategory.XmltvDescription += "," + otherXmltvPart;
            }

            string[] otherWmcParts = otherCategory.WMCDescription.Split(new char[] { ',' });
            foreach (string otherWmcPart in otherWmcParts)
            {
                if (!newCategory.WMCDescription.Contains(otherWmcPart))
                    newCategory.WMCDescription += "," + otherWmcPart;
            }

            return newCategory;
        }

        /// <summary>
        /// Update the descriptions.
        /// </summary>
        /// <param name="newCategories">The list of new categories.</param>
        /// <param name="startIndex">The index to start at.</param>
        /// <returns>An updated XMLTV category.</returns>
        public ProgramCategory AddToDescriptions(Collection<string> newCategories, int startIndex)
        {
            ProgramCategory newCategory = Clone();

            newCategory.XmltvDescription = updateDescription(XmltvDescription, newCategories, startIndex);
            newCategory.WMCDescription = updateDescription(WMCDescription, newCategories, startIndex);

            return newCategory;
        }

        private string updateDescription(string description, Collection<string> newCategories, int startIndex)
        {
            string newDescription = string.Empty;
            string[] parts = null;

            if (!string.IsNullOrWhiteSpace(description))
            {
                parts = description.Split(new char[] { ',' });

                foreach (string part in parts)
                {

                    if (!part.StartsWith("is"))
                    {
                        if (newDescription.Length != 0)
                            newDescription += ",";
                        newDescription += part;
                    }
                }
            }

            for (; startIndex < newCategories.Count; startIndex++)
            {
                if (newDescription.Length != 0)
                    newDescription += ",";
                newDescription += newCategories[startIndex];
            }

            if (parts != null)
            {
                foreach (string part in parts)
                {

                    if (part.StartsWith("is"))
                    {
                        if (newDescription.Length != 0)
                            newDescription += ",";
                        newDescription += part;
                    }
                }
            }

            return newDescription;
        }

        /// <summary>
        /// Copy this instance.
        /// </summary>
        /// <returns>A copy of this instance.</returns>
        public ProgramCategory Clone()
        {
            ProgramCategory newCategory = new ProgramCategory(CategoryTag);
            
            newCategory.XmltvDescription = XmltvDescription;
            newCategory.WMCDescription = WMCDescription;
            newCategory.DVBLogicDescription = DVBLogicDescription;
            newCategory.DVBViewerDescription = DVBViewerDescription;

            return newCategory;
        }

        /// <summary>
        /// Compare this instance with another.
        /// </summary>
        /// <param name="otherCategory">The other instance.</param>
        /// <returns>True if the values are the same; false otherwise.</returns>
        public bool IsDifferent(ProgramCategory otherCategory)
        {
            return CategoryTag != otherCategory.CategoryTag ||
                XmltvDescription != otherCategory.XmltvDescription ||
                WMCDescription != otherCategory.WMCDescription ||
                DVBLogicDescription != otherCategory.DVBLogicDescription ||
                DVBViewerDescription != otherCategory.DVBViewerDescription;
        }

        /// <summary>
        /// Log the category usage.
        /// </summary>
        public static void LogUsage(Collection<ProgramCategory> categories, Collection<ProgramCategory> undefinedCategories, string title)
        {
            if (categories != null)
            {
                bool first = true;
                Collection<ProgramCategory> sortedCategories = sortCategories(categories);

                foreach (ProgramCategory category in sortedCategories)
                {
                    if (category.UsedCount != 0)
                    {
                        if (first)
                        {
                            Logger.Instance.WriteSeparator(title + " Programme Categories Used");
                            first = false;
                        }

                        Logger.Instance.Write(category.CategoryTag + ": " + 
                            " Description: " + (!string.IsNullOrWhiteSpace(category.XmltvDescription) ? category.XmltvDescription : "No XMLTV Description") +
                            " Used: " + category.UsedCount +
                            (category.SampleEvent != null ? " Sample Event: " + category.SampleEvent : string.Empty));
                    }
                }

                if (!first)
                    Logger.Instance.WriteSeparator("End Of " + title + " Programme Categories Used");
                else
                    Logger.Instance.Write("No " + title + " programme categories used");
            }

            if (undefinedCategories != null)
            {
                if (undefinedCategories.Count != 0)
                {
                    Collection<ProgramCategory> sortedCategories = sortCategories(undefinedCategories);

                    Logger.Instance.WriteSeparator(title + " Undefined Programme Categories");

                    foreach (ProgramCategory category in sortedCategories)
                    {
                        Logger.Instance.Write(category.CategoryTag +
                            " Used: " + category.UsedCount +
                            (category.SampleEvent != null ? " Sample Event: " + category.SampleEvent : string.Empty));
                    }

                    Logger.Instance.WriteSeparator("End Of " + title + " Undefined Programme Categories");
                }
                else
                    Logger.Instance.Write("No " + title + " undefined programme categories");
            }
        }

        private static Collection<ProgramCategory> sortCategories(Collection<ProgramCategory> categories)
        {
            Collection<ProgramCategory> sortedCategories = new Collection<ProgramCategory>();

            foreach (ProgramCategory category in categories)
            {
                bool inserted = false;

                foreach (ProgramCategory sortedCategory in sortedCategories)
                {
                    if (sortedCategory.compareTags(category.CategoryTag) > 0)
                    {
                        sortedCategories.Insert(sortedCategories.IndexOf(sortedCategory), category);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                    sortedCategories.Add(category);
            }
            
            return sortedCategories;
        }
    }
}

