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
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes the Schedules Direct movie rating.
    /// </summary>
    [DataContract]
    public class SchedulesDirectQualityRating
    {
        /// <summary>
        /// Get or set the ratings body.
        /// </summary>
        [DataMember(Name = "ratingsBody")]
        public string RatingsBody { get; set; }

        /// <summary>
        /// Get or set the rating.
        /// </summary>
        [DataMember(Name = "rating")]
        public string Rating { get; set; }

        /// <summary>
        /// Get or set the minimum rating from the rating body.
        /// </summary>
        [DataMember(Name = "minRating")]
        public string MinumumRating { get; set; }

        /// <summary>
        /// Get or set the maximum rating from the rating body.
        /// </summary>
        [DataMember(Name = "maxRating")]
        public string MaximumRating { get; set; }

        /// <summary>
        /// Get or set the rating increment.
        /// </summary>
        [DataMember(Name = "increment")]
        public string Increment { get; set; }

        /// <summary>
        /// Get the rating as a star rating string.
        /// </summary>
        public string StarRating
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(RatingsBody) || RatingsBody != "Gracenote" || string.IsNullOrWhiteSpace(Increment))
                    return null;

                try
                {
                    decimal ratingNumber = decimal.Parse(Rating);
                    decimal ratingIncrement = decimal.Parse(Increment);

                    string[] ratings = new string[] { "+", "*", "*+", "**", "**+", "***", "***+", "****" };

                    int ratingIndex = (int)(ratingNumber / ratingIncrement);
                    if (ratingIndex < 1 || ratingIndex > 8)
                        return null;

                    return ratings[ratingIndex - 1];
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectQualityRating class.
        /// </summary>
        public SchedulesDirectQualityRating() { }
    }
}

