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

namespace NetworkProtocols.Sdp
{
    /// <summary>
    /// The class that describes a control attribute.
    /// </summary>
    public class ControlAttribute : ISdpControl
    {
        /// <summary>
        /// Get the address.
        /// </summary>
        public string Address { get; private set; }
        
        private static ICreateControl controlCreator;

        /// <summary>
        /// Initialize a new instance of the ControlAttribute class.
        /// </summary>
        public ControlAttribute() { }
        
        /// <summary>
        /// Parse the control attribute.
        /// </summary>
        /// <param name="parameters">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string parameters)
        {
            Address = parameters;
            return (null);
        }

        /// <summary>
        /// Register a control attribute creator.
        /// </summary>
        /// <param name="creator">The creator to be registered.</param>
        public static void RegisterControlCreator(ICreateControl creator)
        {
            controlCreator = creator;
        }

        /// <summary>
        /// Get an instance of the control creator.
        /// </summary>
        /// <returns>The instance.</returns>
        public static ISdpControl GetInstance()
        {
            if (controlCreator == null)
                return (new ControlAttribute());
            else
                return (controlCreator.CreateControl());
        }
    }
}

