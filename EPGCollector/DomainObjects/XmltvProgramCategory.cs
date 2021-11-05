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

using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace DomainObjects
{
    /// <summary>
    /// The class that describes an XMLTV program category.
    /// </summary>
    public class XmltvProgramCategory : ProgramCategory
    {
        /// <summary>
        /// Get the collection of XMLTV categories.
        /// </summary>
        public static Collection<ProgramCategory> Categories
        {
            get
            {
                if (categories == null)
                    categories = new Collection<ProgramCategory>();
                return (categories);
            }
        }

        /// <summary>
        /// Get the collection of undefined categories.
        /// </summary>
        public static Collection<ProgramCategory> UndefinedCategories
        {
            get
            {
                if (undefinedCategories == null)
                    undefinedCategories = new Collection<ProgramCategory>();
                return (undefinedCategories);
            }
        }

        /// <summary>
        /// Get the standard filename.
        /// </summary>
        public static string FileName { get { return ("XMLTV Categories"); } }

        private static Collection<ProgramCategory> categories;
        private static Collection<ProgramCategory> undefinedCategories;

        private XmltvProgramCategory() { }

        /// <summary>
        /// Find a category.
        /// </summary>
        /// <param name="categoryTag">The category tag.</param>
        /// <returns>A category instance or null if the category is undefined.</returns>
        public static ProgramCategory FindCategory(string categoryTag)
        {
            return ProgramCategory.FindCategory(categories, categoryTag);
        }

        /// <summary>
        /// Add a category.
        /// </summary>
        /// <param name="categoryTag">The category tag.</param>
        /// <param name="description">The full category description.</param>
        public static void AddCategory(string categoryTag, string description)
        {
            ProgramCategory.AddCategory(categories, categoryTag, description);
        }

        /// <summary>
        /// Load the categories from the standard file.
        /// </summary>
        /// <returns>True if load was successful; false otherwise.</returns>
        public static bool Load()
        {
            return Load(FileName + ".cfg");
        }

        /// <summary>
        /// Load the categories from a specified file.
        /// </summary>
        /// <param name="fileName">The specied filename.</param>
        /// <returns>True if load was successful; false otherwise.</returns>
        public static bool Load(string fileName)
        {
            Categories.Clear();
            UndefinedCategories.Clear();

            categories = new Collection<ProgramCategory>();
            return ProgramCategory.Load(categories, fileName);
        }

        /// <summary>
        /// Save the categories to a specified file.
        /// </summary>
        /// <param name="fileName">The specied filename.</param>
        /// <returns>Error message if it failed; null otherwise.</returns>
        public static string Save(string fileName)
        {
            return ProgramCategory.Save(categories, fileName);
        }

        /// <summary>
        /// Add an undefined category to the collection of undefined contents.
        /// </summary>
        /// <param name="category">The category identifier.</param>
        /// <param name="sampleEvent">The description of a sample event.</param>
        public static void AddUndefinedCategory(string category, string sampleEvent)
        {
            ProgramCategory undefinedCategory = findUndefinedCategory(category);            
            undefinedCategory.SampleEvent = sampleEvent;            
            undefinedCategory.UsedCount++;
        }

        private static ProgramCategory findUndefinedCategory(string category)
        {
            ProgramCategory undefinedCategory = FindCategory(UndefinedCategories, category);
            if (undefinedCategory != null)
                return undefinedCategory;

            ProgramCategory newCategory = new ProgramCategory(category);
            UndefinedCategories.Add(newCategory);

            return newCategory;
        }
    }
}

