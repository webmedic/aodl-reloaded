/*************************************************************************
 *
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER
 * 
 * Copyright 2008 Sun Microsystems, Inc. All rights reserved.
 * 
 * Use is subject to license terms.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0. You can also
 * obtain a copy of the License at http://odftoolkit.org/docs/license.txt
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * 
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 ************************************************************************/

using System;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;

namespace OpenOfficeLib.Connection
{
	/// <summary>
	/// All connection relevant methods
	/// </summary>
	public class Connector
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Connector"/> class.
		/// </summary>
		public Connector()
		{
		}

		/// <summary>
		/// Get a the Component Context using default bootstrap
		/// </summary>
		/// <returns>ComponentContext object</returns>
		public static unoidl.com.sun.star.uno.XComponentContext GetComponentContext()
		{
			try
			{
				return uno.util.Bootstrap.bootstrap();
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Get the MultiServiceFactory
		/// </summary>
		/// <param name="componentcontext">A component context</param>
		/// <returns>MultiServiceFactory object</returns>
		public static unoidl.com.sun.star.lang.XMultiServiceFactory GetMultiServiceFactory(
			unoidl.com.sun.star.uno.XComponentContext componentcontext)
		{
			try
			{
				return (unoidl.com.sun.star.lang.XMultiServiceFactory)componentcontext.getServiceManager();
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Get the Desktop
		/// </summary>
		/// <param name="multiservicefactory">A multi service factory</param>
		/// <returns>Desktop object</returns>
		public static unoidl.com.sun.star.frame.XDesktop GetDesktop(
			unoidl.com.sun.star.lang.XMultiServiceFactory multiservicefactory)
		{
			try
			{
				return (unoidl.com.sun.star.frame.XDesktop)multiservicefactory.createInstance("com.sun.star.frame.Desktop");
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}
	}
}

/*
 * $Log: Connector.cs,v $
 * Revision 1.2  2008/04/29 15:40:04  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 09:08:39  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/02/06 19:27:23  larsbm
 * - fixed bug in spreadsheet document
 * - added smal OpenOfficeLib for document printing
 *
 */