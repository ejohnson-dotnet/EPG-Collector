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

using DomainObjects;

namespace DVBServices
{
    /// <summary>
    /// The class that describes a MediaHighway program category.
    /// </summary>
    public class MediaHighwayProgramCategory : ProgramCategory
    {
        /// <summary>
        /// Get the collection of program categories.
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
        /// Get the standard name of the configuration file.
        /// </summary>
        public static string FileName { get { return ("MHWn Categories fffff - x"); } }

        private static Collection<ProgramCategory> categories;
        private static Collection<ProgramCategory> undefinedCategories;

        /// <summary>
        /// Initialize a new instance of the OpenTVProgramCategory class.
        /// </summary>
        public MediaHighwayProgramCategory() { }

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
        /// Find a category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>A category instance or null if the category is undefined.</returns>
        public static ProgramCategory FindCategory(int category)
        {
            return ProgramCategory.FindCategory(categories, category);
        }

        /// <summary>
        /// Add a category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="description">The full category description.</param>
        /// <returns>True if category added; false otherwise.</returns>
        public static bool AddCategory(int category, string description)
        {
            return ProgramCategory.AddCategory(categories, category, description);
        }

        /// <summary>
        /// Load the category definitions given the frequency.
        /// </summary>
        /// <param name="protocol">The protocol (1 or 2) to load.</param>
        /// <param name="frequency">The frequency to load.</param>
        /// <returns>True if the file has been loaded; false otherwise.</returns>
        public static bool LoadFromFrequency(string protocol, string frequency)
        {
            string fileName = Path.Combine(RunParameters.DataDirectory, "MHW" + protocol + " Categories " + frequency + ".cfg");
            if (!File.Exists(fileName))
                fileName = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", "MHW" + protocol + " Categories " + frequency + ".cfg"));

            return (Load(fileName));
        }

        /// <summary>
        /// Load the category definitions.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>True if the file has been loaded; false otherwise.</returns>
        public static bool Load(string fileName)
        {
            Categories.Clear();
            UndefinedCategories.Clear();

            categories = new Collection<ProgramCategory>();

            if (fileName != null)
                return ProgramCategory.Load(categories, fileName);
            else
                return true;
        }

        /// <summary>
        /// Save the categories to a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>Null if the operation succeeded; otherwise an error message.</returns>
        public static string Save(string fileName)
        {
            return ProgramCategory.Save(categories, fileName);
        }

        /// <summary>
        /// Add an undefined category to the collection of undefined contents.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="sampleEvent">The description of a sample event.</param>
        public static void AddUndefinedCategory(int category, string sampleEvent)
        {
            ProgramCategory undefinedCategory = findUndefinedCategory(category);
            undefinedCategory.SampleEvent = sampleEvent;
            undefinedCategory.UsedCount++;
        }

        private static ProgramCategory findUndefinedCategory(int category)
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

