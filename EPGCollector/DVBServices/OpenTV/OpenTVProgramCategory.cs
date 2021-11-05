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
    /// The class that describes an OpenTV program category.
    /// </summary>
    public class OpenTVProgramCategory : ProgramCategory
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
        /// Get the standard file name.
        /// </summary>
        public static string FileName { get { return ("OpenTV Categories xxx"); } }

        private static Collection<ProgramCategory> categories;
        private static Collection<ProgramCategory> undefinedCategories;
        
        /// <summary>
        /// Initialize a new instance of the OpenTVProgramCategory class.
        /// </summary>
        public OpenTVProgramCategory() { }

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
        public static void AddCategory(int category, string description)
        {
            ProgramCategory.AddCategory(categories, category, description);
        }

        /// <summary>
        /// Load the categories from the standard file.        
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>True if load was successful; false otherwise.</returns>
        public static bool Load(string fileName)
        {
            return Load(fileName, null);
        }

        /// <summary>
        /// Load the categories from a specified file.
        /// </summary>
        /// <param name="fileName">The specied filename.</param>
        /// <param name="countryCode">The country code.</param>
        /// <returns>True if load was successful; false otherwise.</returns>
        public static bool Load(string fileName, string countryCode)
        {
            Categories.Clear();
            UndefinedCategories.Clear();

            categories = new Collection<ProgramCategory>();
            return ProgramCategory.Load(categories, (countryCode == null ? fileName : fileName.Replace("xxx", countryCode) + ".cfg"));
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

        /// <summary>
        /// Get the category for an event.
        /// </summary>
        /// <param name="eventEntry">The event entry.</param>
        /// <returns>A category instance or null.</returns>
        public static ProgramCategory GetCategory(EventInformationTableEntry eventEntry)
        {
            if (eventEntry.Descriptors == null)
                return (null);

            ProgramCategory programCategory = null;

            foreach (DescriptorBase descriptorBase in eventEntry.Descriptors)
            {
                GenreDescriptor genreDescriptor = descriptorBase as GenreDescriptor;
                if (genreDescriptor != null)
                {
                    if (genreDescriptor.Attributes != null)
                    {
                        foreach (GenreAttribute genreAttribute in genreDescriptor.Attributes)
                        {
                            ProgramCategory category = AtscPsipProgramCategory.FindCategory(genreAttribute.Attribute);
                            if (category != null)
                            {
                                if (programCategory == null)
                                    programCategory = category.Clone();
                                else
                                    programCategory = programCategory.Combine(category);

                                if (category.SampleEvent == null)
                                    category.SampleEvent = eventEntry.EventName.ToString();
                                category.UsedCount++;
                            }
                            else
                                AtscPsipProgramCategory.AddUndefinedCategory(genreAttribute.Attribute, eventEntry.EventName.ToString());
                        }
                    }
                }
            }

            return (programCategory);
        }
    }
}

