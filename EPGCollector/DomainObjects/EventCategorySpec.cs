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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects
{
    /// <summary>
    /// The class the encapsulates the different sources of an event category.
    /// </summary>
    public class EventCategorySpec
    {
        /// <summary>
        /// Get the event description.
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Get the event category interface.
        /// </summary>
        public IEventCategory EventCategory { get; private set; }

        private EventCategorySpec() { }

        /// <summary>
        /// Creates a new instance of the EventCategorySpec class from a description.
        /// </summary>
        /// <param name="description">The description.</param>
        public EventCategorySpec(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Creates a new instance of the EventCategorySpec class from the category interface.
        /// </summary>
        /// <param name="eventCategory">The category interface.</param>
        public EventCategorySpec(IEventCategory eventCategory)
        {
            EventCategory = eventCategory;
        }

        /// <summary>
        /// Get the category description based on the target output.
        /// </summary>
        /// <param name="mode">The output target.</param>
        /// <returns></returns>
        public string GetDescription(EventCategoryMode mode)
        {
            switch (mode)
            {
                case EventCategoryMode.Xmltv:
                    if (!string.IsNullOrWhiteSpace(Description))
                        return Description;
                    else
                    {
                        if (EventCategory != null)
                            return EventCategory.XmltvDescription;
                        else
                            return null;
                    }
                case EventCategoryMode.Wmc:
                    if (!string.IsNullOrWhiteSpace(Description))
                        return Description;
                    else
                    {
                        if (EventCategory != null)
                            return EventCategory.WMCDescription;
                        else
                            return null;
                    }
                case EventCategoryMode.DvbLogic:
                    if (EventCategory != null)
                        return EventCategory.DVBLogicDescription;
                    else
                        return null;
                case EventCategoryMode.DvbViewer:
                    if (EventCategory != null)
                        return EventCategory.DVBViewerDescription;
                    else
                        return null;
                default:
                    return null;
            }
        }
    }
}

