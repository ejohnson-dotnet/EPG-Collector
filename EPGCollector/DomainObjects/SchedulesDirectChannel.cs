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
using System.Runtime.Serialization;

namespace DomainObjects
{
    /// <summary>
    /// The class that describes a Schedules Direct channel.
    /// </summary>
    [DataContract]
    public class SchedulesDirectChannel
    {
        /// <summary>
        /// Get or set the internal identity.
        /// </summary>
        [DataMember(Name = "channel")]
        public string Identity { get; set; }
        
        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Get or set the call sign.
        /// </summary>
        [DataMember(Name = "callsign")]
        public string CallSign { get; set; }
        
        /// <summary>
        /// Get or set the Affiliate.
        /// </summary>
        [DataMember(Name = "affiliate")]
        public string Affiliate { get; set; }
        
        /// <summary>
        /// Get or set the lineup identity that contains the channel.
        /// </summary>
        public string LineupIdentity { get; set; }

        /// <summary>
        /// Get or set the name of the lineup that contains the channel.
        /// </summary>
        public string LineupName { get; set; }
        
        /// <summary>
        /// Get or set the user defined channel name.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Get or set the user defined call sign.
        /// </summary>
        public string UserCallSign { get; set; }
        
        /// <summary>
        /// True if the channel is excluded; false otherwise.
        /// </summary>
        public bool Excluded { get; set; }

        /// <summary>
        /// Get or setthe major channel number.
        /// </summary>
        public int MajorChannelNumber { get; set; }

        /// <summary>
        /// Get or set the minor channel number.
        /// </summary>
        public int MinorChannelNumber { get; set; }

        /// <summary>
        /// Get or set the user defined channel number.
        /// </summary>
        public int UserChannelNumber { get; set; }

        /// <summary>
        /// Get or set the original network ID.
        /// </summary>
        public int OriginalNetworkId { get; set; }

        /// <summary>
        /// Get or set the transport stream ID.
        /// </summary>
        public int TransportStreamId { get; set; }

        /// <summary>
        /// Get or set the service ID.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Get or set the URL of the channel logo.
        /// </summary>
        public Collection<string> Logos { get; set; }

        /// <summary>
        /// Get the full name of the channel.
        /// </summary>
        public string FullName 
        { 
            get 
            {
                string reply = Name;

                if (!string.IsNullOrWhiteSpace(CallSign))
                    reply += " - " + CallSign;

                if (!string.IsNullOrWhiteSpace(Affiliate))
                    reply += " - " + Affiliate;

                return reply;
            }
        }

        /// <summary>
        /// Get the channel identification string using DVB or ATSC identification numbers. 
        /// </summary>
        public string ChannelIdentification
        {
            get
            {
                if (OriginalNetworkId != 0)
                    return OriginalNetworkId + ":" + TransportStreamId + ":" + ServiceId;
                else
                {
                    if (MinorChannelNumber != 0)
                        return MajorChannelNumber + ":" + MinorChannelNumber;
                    else
                        return MajorChannelNumber.ToString();
                }
            }
        }

        /// <summary>
        /// The sort keys for the SchedulesDirectChannelClass.
        /// </summary>
        public enum SortKeys
        {
            /// <summary>
            /// Sort on name.
            /// </summary>
            Name,
            /// <summary>
            /// Sort on call sign.
            /// </summary>
            CallSign,
            /// <summary>
            /// Sort on affiliate.
            /// </summary>
            Affiliate,
            /// <summary>
            /// Sort on lineup name.
            /// </summary>
            LineupName,
            /// <summary>
            /// Sort on excluded.
            /// </summary>
            Excluded,
            /// <summary>
            /// Sort on users name for channel.
            /// </summary>
            UserName,
            /// <summary>
            /// Sort on the users channel number.
            /// </summary>
            UserChannelNumber,
            /// <summary>
            /// Sort on the users call sign for the channel.
            /// </summary>
            UserCallSign,
            /// <summary>
            /// Sort on the major channel number.
            /// </summary>
            MajorChannelNumber,
            /// <summary>
            /// Sort on the minor channel number.
            /// </summary>
            MinorChannelNumber,
            /// <summary>
            /// Sort on the channel identification string.
            /// </summary>
            ChannelIdentification
        }

        /// <summary>
        /// Initialize a new instance of the SdChannel class.
        /// </summary>
        public SchedulesDirectChannel() { }

        /// <summary>
        /// Create a copy of the current instance.
        /// </summary>
        /// <returns>A copy of the current instance.</returns>
        public SchedulesDirectChannel Clone()
        {
            SchedulesDirectChannel newChannel = new SchedulesDirectChannel();

            newChannel.Identity = Identity;
            newChannel.Name = Name;
            newChannel.CallSign = CallSign;
            newChannel.Affiliate = Affiliate;
            newChannel.LineupIdentity = LineupIdentity;
            newChannel.LineupName = LineupName;
            newChannel.UserName = UserName;
            newChannel.UserCallSign = CallSign;
            newChannel.UserChannelNumber = UserChannelNumber;
            newChannel.Excluded = Excluded;
            newChannel.MajorChannelNumber = MajorChannelNumber;
            newChannel.MinorChannelNumber = MinorChannelNumber;
            newChannel.OriginalNetworkId = OriginalNetworkId;
            newChannel.TransportStreamId = TransportStreamId;
            newChannel.ServiceId = ServiceId;

            if (Logos != null)
            {
                newChannel.Logos = new Collection<string>();
                foreach (string logo in Logos)
                    newChannel.Logos.Add(logo);
            }

            return newChannel;
        }

        /// <summary>
        /// Compare this instance with another.
        /// </summary>
        /// <param name="otherChannel">The other lineup.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public bool CompareTo(SchedulesDirectChannel otherChannel)
        {
            if (Identity != otherChannel.Identity)
                return false;
            if (Name != otherChannel.Name)
                return false;
            if (CallSign != otherChannel.CallSign)
                return false;
            if (Affiliate != otherChannel.Affiliate)
                return false;
            if (LineupIdentity != otherChannel.LineupIdentity)
                return false;
            if (LineupName != otherChannel.LineupName)
                return false;
            if (UserName != otherChannel.UserName)
                return false;
            if (UserCallSign != otherChannel.UserCallSign)
                return false;
            if (UserChannelNumber != otherChannel.UserChannelNumber)
                return false;
            if (Excluded != otherChannel.Excluded)
                return false;
            if (MajorChannelNumber != otherChannel.MajorChannelNumber)
                return false;
            if (MinorChannelNumber != otherChannel.MinorChannelNumber)
                return false;
            if (OriginalNetworkId != otherChannel.OriginalNetworkId)
                return false;
            if (TransportStreamId != otherChannel.TransportStreamId)
                return false;
            if (ServiceId != otherChannel.ServiceId)
                return false;

            if (Logos != null)
            {
                if (otherChannel.Logos != null)
                {
                    if (Logos.Count != otherChannel.Logos.Count)
                        return false;
                    else
                    {
                        Collection<string> notMatched = new Collection<string>();

                        foreach (string logo in Logos)
                        {
                            if (!otherChannel.Logos.Contains(logo))
                                notMatched.Add(logo);
                        }

                        foreach (string logo in otherChannel.Logos)
                        {
                            if (!Logos.Contains(logo))
                                notMatched.Add(logo);
                        }

                        if (notMatched.Count != 0)
                            return false;
                    }
                }
            }
            else
            {
                if (otherChannel.Logos != null)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Compare fields for sorting purposes.
        /// </summary>
        /// <param name="otherChannel">The other instance</param>
        /// <param name="sortKey">The field to use for the comparison.</param>
        /// <param name="ascending">True for ascending comparisons; false otherwise.</param>
        /// <returns></returns>
        public int Compare(SchedulesDirectChannel otherChannel, SortKeys sortKey, bool ascending)
        {
            int reply = 0;

            switch (sortKey)
            {
                case SortKeys.Name:
                    if (ascending)
                        return Name.CompareTo(otherChannel.Name);
                    else
                        return otherChannel.Name.CompareTo(Name);
                case SortKeys.CallSign:
                    if (ascending)
                        return CallSign.CompareTo(otherChannel.CallSign);
                    else
                        return otherChannel.CallSign.CompareTo(CallSign);
                case SortKeys.Affiliate:
                    if (ascending)
                    {
                        reply = Affiliate.CompareTo(otherChannel.Affiliate);
                        if (reply == 0)
                            reply = Name.CompareTo(otherChannel.Name);                        
                    }
                    else
                    {
                        reply = otherChannel.Affiliate.CompareTo(Affiliate);
                        if (reply == 0)
                            reply = otherChannel.Name.CompareTo(Name);                        
                    }
                    return reply;
                case SortKeys.LineupName:
                    if (ascending)
                    {
                        reply = LineupName.CompareTo(otherChannel.LineupName);
                        if (reply == 0)
                            reply = Name.CompareTo(otherChannel.LineupName);
                    }
                    else
                    {
                        reply = otherChannel.LineupName.CompareTo(LineupName);
                        if (reply == 0)
                            reply = otherChannel.LineupName.CompareTo(LineupName);                        
                    }
                    return reply;
                case SortKeys.UserName:
                    if (ascending)
                        return UserName.CompareTo(otherChannel.UserName);
                    else
                        return otherChannel.UserName.CompareTo(UserName);
                case SortKeys.UserCallSign:
                    if (ascending)
                        return UserCallSign.CompareTo(otherChannel.UserCallSign);
                    else
                        return otherChannel.UserCallSign.CompareTo(UserCallSign);
                case SortKeys.UserChannelNumber:
                    if (ascending)
                    {
                        reply = UserChannelNumber.CompareTo(otherChannel.UserChannelNumber);
                        if (reply == 0)
                            reply = Name.CompareTo(otherChannel.Name);
                    }
                    else
                    {
                        reply = otherChannel.UserChannelNumber.CompareTo(UserChannelNumber);
                        if (reply == 0)
                            reply = otherChannel.Name.CompareTo(Name);                        
                    }
                    return reply;
                case SortKeys.Excluded:
                    if (ascending)
                    {
                        reply = Excluded.CompareTo(otherChannel.Excluded);
                        if (reply == 0)
                            reply = Name.CompareTo(otherChannel.Name);                        
                    }
                    else
                    {
                        reply = otherChannel.Excluded.CompareTo(Excluded);
                        if (reply == 0)
                            reply = otherChannel.Name.CompareTo(Name);                                              
                    }
                    return reply;
                case SortKeys.MajorChannelNumber:
                    if (ascending)
                    {
                        reply = MajorChannelNumber.CompareTo(otherChannel.MajorChannelNumber);
                        if (reply == 0)
                            reply = MinorChannelNumber.CompareTo(otherChannel.MinorChannelNumber);                        
                    }
                    else
                    {
                        reply = otherChannel.MajorChannelNumber.CompareTo(MajorChannelNumber);
                        if (reply == 0)
                            reply = otherChannel.MinorChannelNumber.CompareTo(MinorChannelNumber);                        
                    }
                    return reply;
                case SortKeys.MinorChannelNumber:
                    if (ascending)
                    {
                        reply = MinorChannelNumber.CompareTo(otherChannel.MinorChannelNumber);
                        if (reply == 0)
                            reply = Name.CompareTo(otherChannel.Name);
                    }
                    else
                    {
                        reply = otherChannel.MinorChannelNumber.CompareTo(MinorChannelNumber);
                        if (reply == 0)
                            reply = otherChannel.Name.CompareTo(Name);
                    }

                    return reply;
                case SortKeys.ChannelIdentification:
                    if (!Char.IsDigit(ChannelIdentification[0]))
                    {
                        if (ascending)
                        {
                            reply = ChannelIdentification.CompareTo(otherChannel.ChannelIdentification);
                            if (reply == 0)
                                reply = Name.CompareTo(otherChannel.Name);
                        }
                        else
                        {
                            reply = otherChannel.ChannelIdentification.CompareTo(ChannelIdentification);
                            if (reply == 0)
                                reply = otherChannel.Name.CompareTo(Name);
                        }
                    }
                    else
                    {
                        string[] thisParts = ChannelIdentification.Split(new char[] { ':' });
                        int thisMajorNumber = Int32.Parse(thisParts[0]);
                        int thisMinorNumber = 0;
                        int thisSuffixNumber = 0;
                        if (thisParts.Length == 2)
                            thisMinorNumber = Int32.Parse(thisParts[1]);
                        if (thisParts.Length == 3)
                            thisSuffixNumber = Int32.Parse(thisParts[2]);

                        string[] otherParts = otherChannel.ChannelIdentification.Split(new char[] { ':' });
                        int otherMajorNumber = Int32.Parse(otherParts[0]);
                        int otherMinorNumber = 0;
                        int otherSuffixNumber = 0;
                        if (otherParts.Length == 2)
                            otherMinorNumber = Int32.Parse(otherParts[1]);
                        if (thisParts.Length == 3)
                            otherSuffixNumber = Int32.Parse(otherParts[2]);

                        if (ascending)
                        {
                            reply = thisMajorNumber.CompareTo(otherMajorNumber);
                            if (reply == 0)
                            {
                                reply = thisMinorNumber.CompareTo(otherMinorNumber);
                                if (reply == 0)
                                {
                                    reply = thisSuffixNumber.CompareTo(otherSuffixNumber);
                                    if (reply == 0)
                                        reply = Name.CompareTo(otherChannel.Name);
                                }
                            }
                        }
                        else
                        {
                            reply = otherMajorNumber.CompareTo(thisMajorNumber);
                            if (reply == 0)
                            {
                                reply = otherMinorNumber.CompareTo(thisMinorNumber);
                                if (reply == 0)
                                {
                                    reply = otherSuffixNumber.CompareTo(thisSuffixNumber);
                                    if (reply == 0)
                                        reply = otherChannel.Name.CompareTo(Name);
                                }
                            }
                        }
                    }

                    return reply;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Get a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            string reply = Name;

            if (!string.IsNullOrWhiteSpace(CallSign))
                Name += "-" + CallSign;

            if (!string.IsNullOrWhiteSpace(Affiliate))
                Name += "-" + Affiliate;
            
            return reply;
        }
    }
}

