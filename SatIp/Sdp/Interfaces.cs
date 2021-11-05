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

using SatIpDomainObjects;

namespace Sdp
{
    /// <summary>
    /// The interface for create a format processor.
    /// </summary>
    public interface ICreateFormat
    {
        /// <summary>
        /// Create the format processor.
        /// </summary>
        /// <param name="identity">The name of the format.</param>
        /// <returns>An instance of the format processor.</returns>
        ISdpFormat CreateFormat(string identity);
        /// <summary>
        /// Check if processor handles a format.
        /// </summary>
        /// <param name="identity">The name of the format.</param>
        /// <returns></returns>
        bool IsFormat(string identity);
    }

    /// <summary>
    /// The interface for a format processor.
    /// </summary>
    public interface ISdpFormat
    {
        /// <summary>
        /// Get or set the name of the format processor.
        /// </summary>
        string Identity { get; set;  }
        
        /// <summary>
        /// Parse a format line.
        /// </summary>
        /// <param name="parameters">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        ErrorSpec Process(string parameters);
    }

    /// <summary>
    /// The interface to create a control processor.
    /// </summary>
    public interface ICreateControl
    {
        /// <summary>
        /// Create a control processor.
        /// </summary>
        /// <returns>An instance of a control processor.</returns>
        ISdpControl CreateControl();        
    }

    /// <summary>
    /// The interface for a control processor.
    /// </summary>
    public interface ISdpControl
    {
        /// <summary>
        /// Parse a control line.
        /// </summary>
        /// <param name="parameters">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        ErrorSpec Process(string parameters);
    }
}

