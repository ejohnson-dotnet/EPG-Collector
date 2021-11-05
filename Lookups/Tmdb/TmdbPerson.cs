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
using System.Text;
using System.Runtime.Serialization;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class that describes a person.
    /// </summary>
    [DataContract]
    public class TmdbPerson
    {
        /// <summary>
        /// Get or set the adult flag.
        /// </summary>
        [DataMember(Name = "adult")]
        public bool Adult { get; set; }
        
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }
        
        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Get or set the profile path.
        /// </summary>
        [DataMember(Name = "profile_path")]
        public string ProfilePath { get; set; }

        /// <summary>
        /// Get or set the alternative names.
        /// </summary>
        [DataMember(Name = "also_known_as")]
        public string[] AlsoKnownAs { get; set; }
        
        /// <summary>
        /// Get or set the biography.
        /// </summary>
        [DataMember(Name = "biography")]
        public string Biography { get; set; }
        
        /// <summary>
        /// Get or set the birthday.
        /// </summary>
        [DataMember(Name = "birthday")]
        public string BirthdayString
        {
            get { return birthdayString; }
            set { birthdayString = value; }
        }
        
        /// <summary>
        /// Get or set the date of death.
        /// </summary>
        [DataMember(Name = "deathday")]
        public string DeathdayString
        {
            get { return deathdayString; }
            set { deathdayString = value; }
        }
        
        /// <summary>
        /// Get or set the home page.
        /// </summary>
        [DataMember(Name = "homepage")]
        public string HomePage { get; set; }
        
        /// <summary>
        /// Get or set the place of birth.
        /// </summary>
        [DataMember(Name = "place_of_birth")]
        public string PlaceOfBirth { get; set; }        

        /// <summary>
        /// Get or set the credits.
        /// </summary>
        public TmdbPersonCredits Credits { get; set; }
        
        /// <summary>
        /// Get or set the list of images.
        /// </summary>
        public TmdbPersonImages Images { get; set; }

        /// <summary>
        /// Get the alias names as a single comma separated string.
        /// </summary>
        public string AlsoKnownAsString
        {
            get
            {
                if (AlsoKnownAs == null)
                    return string.Empty;

                StringBuilder reply = new StringBuilder();

                foreach (string alias in AlsoKnownAs)
                {
                    if (reply.Length != 0)
                        reply.Append(", ");

                    reply.Append(alias);
                }

                return reply.ToString();                
            }
        }

        /// <summary>
        /// Get the persons birthday.
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                if (birthdayString == null || birthdayString == string.Empty)
                    return new DateTime();
                else
                {
                    try
                    {
                        return DateTime.Parse(birthdayString);
                    }
                    catch (FormatException)
                    {
                        return new DateTime();
                    }
                }
            }
        }

        /// <summary>
        /// Get the persons deathday.
        /// </summary>
        public DateTime Deathday
        {
            get
            {
                if (deathdayString == null || deathdayString == string.Empty)
                    return new DateTime();
                else
                {
                    try
                    {
                        return DateTime.Parse(deathdayString);
                    }
                    catch (FormatException)
                    {
                        return new DateTime();
                    }
                }
            }
        }

        private string birthdayString;
        private string deathdayString;

        /// <summary>
        /// Initialize a new instance of the TmdbPerson class.
        /// </summary>
        public TmdbPerson() { }

        /// <summary>
        /// Load this instance with all available data.
        /// </summary>
        public void LoadAllData(TmdbAPI instance)
        {
            LoadAdditionalData(instance);
            LoadLists(instance);
        }

        /// <summary>
        /// Load this instance with the extended information.
        /// </summary>
        public void LoadAdditionalData(TmdbAPI instance)
        {
            TmdbPerson person = instance.GetPersonInfo(Identity);

            AlsoKnownAs = person.AlsoKnownAs;
            Biography = person.Biography;
            BirthdayString = person.BirthdayString;
            DeathdayString = person.DeathdayString;
            HomePage = person.HomePage;
            PlaceOfBirth = person.PlaceOfBirth;            
        }

        /// <summary>
        /// Load this instance with the optional data.
        /// </summary>
        public void LoadLists(TmdbAPI instance)
        {
            Credits = instance.GetPersonCredits(Identity);
            Images = instance.GetPersonImages(Identity);            
        }

        /// <summary>
        /// Get the profile image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetProfileImage(TmdbAPI instance, string fileName)
        {
            if (ProfilePath == null)
                return false;

            return instance.GetImage(ImageType.Profile, ProfilePath, -1, fileName);
        }

        /// <summary>
        /// Get the profile image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="index">The index of the image.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetProfileImage(TmdbAPI instance, int index, string fileName)
        {
            if (Images == null || index < 0 || index >= Images.Profiles.Length)
                throw new IndexOutOfRangeException("The image index is incorrect");

            return instance.GetImage(ImageType.Profile, Images.Profiles[index].FilePath, -1, fileName);
        }

        /// <summary>
        /// Search for a person.
        /// </summary>
        /// <param name="instance">The aAPI instance.</param>
        /// <param name="name">The persons name or part of it.</param>
        /// <returns>The results object.</returns>
        public static TmdbPersonSearchResults Search(TmdbAPI instance, string name)
        {
            return instance.GetPeople(name);
        }
    }
}
