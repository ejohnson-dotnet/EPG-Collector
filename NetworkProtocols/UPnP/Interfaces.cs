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

using System.Xml;

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The interface for a custom line loader.
    /// </summary>
    public interface ICreateLoader
    {
        /// <summary>
        /// Create a custom line loader.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        IDescriptionLine CreateLoader(string identity);
        /// <summary>
        /// Determine if the loader processes a given line; false otherwise false.
        /// </summary>
        /// <param name="identity">The identity of the line to process.</param>
        /// <returns>True if the loader processes the line; false otherwise.</returns>
        bool IsLoader(string identity);
    }

    /// <summary>
    /// Load a custom line.
    /// </summary>
    public interface IDescriptionLine
    {        
        /// <summary>
        /// Load the line.
        /// </summary>
        /// <param name="xmlReader">An XmlReader instance containing the data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        ErrorSpec Load(XmlReader reader); 
    }
}

