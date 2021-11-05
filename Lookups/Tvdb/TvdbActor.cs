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
    /// The class that describes an actor.
    /// </summary>
    [DataContract]
    public class TvdbActor
    {
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the series identity.
        /// </summary>
        [DataMember(Name = "seriesId")]
        public int SeriesId { get; set; }        

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the role.
        /// </summary>
        [DataMember(Name = "role")]
        public string Character { get; set; }

        /// <summary>
        /// Get or set the sort order.
        /// </summary>
        [DataMember(Name = "sortOrder")]
        public int SortOrder { get; set; }

        /// <summary>
        /// Get or set the image.
        /// </summary>
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Get or set the image author.
        /// </summary>
        [DataMember(Name = "imageAuthor")]
        public string ImageAuthor { get; set; }

        /// <summary>
        /// Get or set the image added date.
        /// </summary>
        [DataMember(Name = "imageAdded")]
        public string ImageAdded { get; set; }

        /// <summary>
        /// Get or set the last updated date.
        /// </summary>
        [DataMember(Name = "lastUpdated")]
        public string LastUpdated { get; set; }

        /// <summary>
        /// Get the image added date. Returns null if not present or invalid.
        /// </summary>
        public DateTime? ImageAddedDate { get { return TvdbUtils.StringToDate(ImageAdded); } }

        /// <summary>
        /// Get the last updated date date. Returns null if not present or invalid.
        /// </summary>
        public DateTime? LastUpdatedDate { get { return TvdbUtils.StringToDate(LastUpdated); } }

        /// <summary>
        /// Initialize a new instance of the TvdbActor class.
        /// </summary>
        public TvdbActor() { }

        /// <summary>
        /// Get the image for the actor.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The name of the file to hold te image.</param>
        /// <returns>True if the image has been downloaded; false otherwise.</returns>
        public bool GetImage(TvdbAPI instance, string fileName)
        {
            if (Image == null || Image.Length == 0)
                return false;

            instance.GetImage(TvdbAPI.ImageType.Actor, Image, 0, fileName);

            return true;
        }

        /// <summary>
        /// Get the small image for the actor.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The name of the file to hold te image.</param>
        /// <returns>True if the image has been downloaded; false otherwise.</returns>
        public bool GetSmallImage(TvdbAPI instance, string fileName)
        {
            if (Image == null || Image.Length == 0)
                return false;

            instance.GetImage(TvdbAPI.ImageType.SmallActor, Image, 0, fileName);

            return true;
        }
    }
}
