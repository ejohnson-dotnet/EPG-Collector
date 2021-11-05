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
using System.Reflection;
using System.IO;

namespace WMCUtility
{
    internal sealed class ReflectionServices
    {
        private static Assembly mcstore;
        private static Assembly mcepg;
        
        private ReflectionServices() { }

        internal static string LoadLibraries()
        {
            try
            {
                mcstore = Assembly.LoadFile(Path.Combine(Environment.GetEnvironmentVariable("windir"), Path.Combine("ehome", "mcstore.dll")));
                mcepg = Assembly.LoadFile(Path.Combine(Environment.GetEnvironmentVariable("windir"), Path.Combine("ehome", "mcepg.dll")));
                
                return (null);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("<e> Reflection Services: Failed to load assemblies");
                Logger.Instance.Write("<e> " + e.Message);
                return (e.Message);
            }
        }

        internal static Type GetType(string category, string name)
        {
            Assembly assembly;

            switch (category)
            {
                case "mcstore":
                    assembly = mcstore;
                    break;
                case "mcepg":
                    assembly = mcepg;
                    break;
                default:
                    Logger.Instance.Write("Reflection Services: Unknown category - " + category);
                    return (null);
            }

            Type[] types = assembly.GetTypes();
            if (types.Length == 0)
            {
                Logger.Instance.Write("Reflection Services: No types defines in " + category);
                return (null);
            }
                    
            foreach (Type type in types)
            {
                if (type.Name != null && type.Name == name)
                    return (type);
            }

            Logger.Instance.Write("Reflection Services: Type " + name + " not found in " + category);

            return (null);
        }

        internal static object InvokeConstructor(string category, string name, Type[] types, object[] parameters)
        {
            Type type = GetType(category, name);
            MethodInfo[] methods = type.GetMethods();
            ConstructorInfo constructorInfo = type.GetConstructor(types);
            object instance = constructorInfo.Invoke(parameters);

            return(instance);
        }

        internal static object InvokeMethod(Type type, object instance, string method, object[] parameters)
        {
            Type[] types = new Type[parameters.Length];

            for (int index = 0; index < parameters.Length; index++)
                types[index] = parameters[index].GetType();

            MethodInfo methodInfo;

            if (instance == null)
                methodInfo = type.GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, null, types, null);
            else
                methodInfo = type.GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            
            if (instance == null)
                return (methodInfo.Invoke(null, parameters));
            else
                return (methodInfo.Invoke(instance, parameters));
        }

        internal static object GetStaticValue(Type type, string method, object[] parameters)
        {
            return (type.GetMethod(method).Invoke(null, parameters));
        }

        internal static object GetPropertyValue(Type type, object instance, string propertyName)
        {
            return (type.GetProperty(propertyName).GetValue(instance, null));
        }

        internal static void SetPropertyValue(Type type, object instance, string propertyName, object value)
        {
            type.GetProperty(propertyName).SetValue(instance, value, new object[0]);
        }
    }
}

