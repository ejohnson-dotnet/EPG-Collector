////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                             //
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
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a page link.
    /// </summary>
    [DataContract]
    public class TvdbPageLink
    {
        /// <summary>
        /// Get or set the first page number.
        /// </summary>
        [DataMember(Name = "first")]
        public int FirstPage { get; set; }

        /// <summary>
        /// Get or set the last page number.
        /// </summary>
        [DataMember(Name = "last")]
        public int LastPage { get; set; }

        /// <summary>
        /// Get or set the next page number.
        /// </summary>
        [DataMember(Name = "next")]
        public string NextPage { get; set; }        

        /// <summary>
        /// Get or set the previous page number.
        /// </summary>
        [DataMember(Name = "prev")]
        public string PreviousPage { get; set; }        

        /// <summary>
        /// Get the next page number. Returns -1 if no next page.
        /// </summary>
        public int NextPageNumber
        {
            get
            {
                if (NextPage == null)
                    return -1;

                try
                {
                    return Int32.Parse(NextPage);
                }
                catch (FormatException)
                {
                    return -1;
                }
                catch (OverflowException)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Get the previous page number. Returns -1 if no previous page.
        /// </summary>
        public int PreviousPageNumber
        {
            get
            {
                if (PreviousPage == null)
                    return -1;

                try
                {
                    return Int32.Parse(PreviousPage);
                }
                catch (FormatException)
                {
                    return -1;
                }
                catch (OverflowException)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Initialize a new instance of the TvdbPageLink class.
        /// </summary>
        public TvdbPageLink() { }
    }
}
