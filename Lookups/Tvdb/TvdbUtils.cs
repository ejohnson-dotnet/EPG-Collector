////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2016 nzsjb                                             //
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
using System.Text;

namespace Lookups.Tvdb
{
    /// <summary>
    /// Utility functions for the TVDB library.
    /// </summary>
    internal sealed class TvdbUtils
    {
        internal static DateTime? StringToDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            try
            {
                return new DateTime?(DateTime.Parse(dateString));
            }
            catch (FormatException)
            {
                return null;
            }
        }

        internal static int StringToInt(string numberString)
        {
            if (numberString == null)
                return -1;

            try
            {
                return int.Parse(numberString);
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

        internal static long StringToLong(string numberString)
        {
            if (numberString == null)
                return -1;

            try
            {
                return long.Parse(numberString);
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

        internal static decimal StringToDecimal(string numberString)
        {
            if (numberString == null)
                return -1;

            try
            {
                return decimal.Parse(numberString);
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

        internal static string CollectionToString(Collection<string> collection)
        {
            if (collection == null || collection.Count == 0)
                return string.Empty;

            StringBuilder stringList = new StringBuilder();

            foreach (string entry in collection)
            {
                if (stringList.Length != 0)
                    stringList.Append(", ");
                stringList.Append(entry);
            }

            return stringList.ToString();
        }
    }
}
