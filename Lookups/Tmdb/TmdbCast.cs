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

using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class the describes the cast of a movie.
    /// </summary>
    [DataContract]
    public class TmdbCast
    {
        /// <summary>
        /// Get or set the cast members.
        /// </summary>
        [DataMember(Name = "cast")]
        public Collection<TmdbCastMember> Cast { get; set; }

        /// <summary>
        /// Get or set the crew members.
        /// </summary>
        [DataMember(Name = "crew")]
        public Collection<TmdbCrewMember> Crew { get; set; }

        /// <summary>
        /// Get or set the guest stars.
        /// </summary>
        [DataMember(Name = "guest_stars")]
        public Collection<TmdbCastMember> GuestStars { get; set; }

        /// <summary>
        /// Get the collection of cast names.
        /// </summary>
        public Collection<string> CastNames
        {
            get
            {
                if (Cast == null)
                    return null;

                Collection<string> castNames = new Collection<string>();

                foreach (TmdbCastMember castMember in Cast)
                    castNames.Add(castMember.Name);

                return castNames;
            }
        }

        /// <summary>
        /// Get the collection of director names.
        /// </summary>
        public Collection<string> DirectorNames
        {
            get
            {
                if (Crew == null)
                    return null;

                Collection<string> directorNames = new Collection<string>();

                foreach (TmdbCrewMember crewMember in Crew)
                {
                    if (crewMember.Job != null && crewMember.Job.ToLowerInvariant() == "director")
                        directorNames.Add(crewMember.Name);
                }

                return directorNames;
            }
        }

        /// <summary>
        /// Get the collection of producer names.
        /// </summary>
        public Collection<string> ProducerNames
        {
            get
            {
                if (Crew == null)
                    return null;

                Collection<string> producerNames = new Collection<string>();

                foreach (TmdbCrewMember crewMember in Crew)
                {
                    if (crewMember.Job != null && crewMember.Job.ToLowerInvariant().Contains("producer"))
                        producerNames.Add(crewMember.Name);
                }

                return producerNames;
            }
        }

        /// <summary>
        /// Get the collection of writer names.
        /// </summary>
        public Collection<string> WriterNames
        {
            get
            {
                if (Crew == null)
                    return null;

                Collection<string> writerNames = new Collection<string>();

                foreach (TmdbCrewMember crewMember in Crew)
                {
                    if (crewMember.Job != null &&
                        (crewMember.Job.ToLowerInvariant() == "author" ||
                        crewMember.Job.ToLowerInvariant() == "screenplay" ||
                        crewMember.Job.ToLowerInvariant() == "writer" ||
                        crewMember.Job.ToLowerInvariant() == "novel")
                        )
                    {
                        if (!writerNames.Contains(crewMember.Name))
                            writerNames.Add(crewMember.Name);
                    }
                }

                return writerNames;
            }
        }

            /// <summary>
        /// Get the collection of other members of the crew.
        /// </summary>
        public NameValueCollection Others
        {
            get
            {
                if (Crew == null)
                    return null;

                NameValueCollection others = new NameValueCollection();

                foreach (TmdbCrewMember crewMember in Crew)
                {
                    if (crewMember.Job != null &&
                        (crewMember.Job.ToLowerInvariant() != "producer" &&
                        crewMember.Job.ToLowerInvariant() != "director" &&
                        crewMember.Job.ToLowerInvariant() != "author" &&
                        crewMember.Job.ToLowerInvariant() != "screenplay" &&
                        crewMember.Job.ToLowerInvariant() != "writer" &&
                        crewMember.Job.ToLowerInvariant() != "novel")
                        )
                        others.Add(crewMember.Name, crewMember.Job);
                }

                return others;
            }
        }

        /// <summary>
        /// Get the collection of guest star names.
        /// </summary>
        public Collection<string> GuestStarNames
        {
            get
            {
                if (GuestStars == null)
                    return null;

                Collection<string> guestStarNames = new Collection<string>();

                foreach (TmdbCastMember guestStar in GuestStars)
                    guestStarNames.Add(guestStar.Name);

                return guestStarNames;
            }
        }

        /// <summary>
        /// Initialize a new instance of the TmdbCast class.
        /// </summary>
        public TmdbCast() { }
    }
}
