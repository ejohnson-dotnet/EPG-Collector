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

using DomainObjects;

namespace Lookups
{
    /// <summary>
    /// The class that describes the category created from a TMDB lookup.
    /// </summary>
    public class LookupCategory : IEventCategory
    {
        /// <summary>
        /// Get the XMLTV part of the description.
        /// </summary>
        public string XmltvDescription { get; internal set; }

        /// <summary>
        /// Get or set the Windows Media Centre part of the description.
        /// </summary>
        public string WMCDescription { get; internal set; }
        
        /// <summary>
        /// Get or set the DVBLogic part of the description.
        /// </summary>
        public string DVBLogicDescription { get; internal set; }
        
        /// <summary>
        /// Get or set the DVBViewer part of the description.
        /// </summary>
        public string DVBViewerDescription { get; internal set; }        

        /// <summary>
        /// Initialize a new instance of the MovieCategory class.
        /// </summary>
        public LookupCategory() { }
    }
}

