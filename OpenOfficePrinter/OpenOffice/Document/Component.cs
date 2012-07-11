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

namespace OpenOfficeLib.Document
{
	/// <summary>
	/// Methods for Component handling.
	/// </summary>
	public class Component
	{
		/// <summary>
		/// string for new writer instance
		/// </summary>
		public static string Writer		= "private:factory/swriter";
		/// <summary>
		/// string for new calc instance
		/// </summary>
		public static string Calc		= "private:factory/scalc";
		/// <summary>
		/// string for new Draw instance
		/// </summary>
		public static string Draw		= "private:factory/sdraw";
		/// <summary>
		/// string for new Impress instance
		/// </summary>
		public static string Impress	= "private:factory/simpress";

		/// <summary>
		/// Initializes a new instance of the <see cref="Component"/> class.
		/// </summary>
		public Component()
		{
		}

		/// <summary>
		/// Load a given file or create a new blank file
		/// </summary>
		/// <param name="aLoader">A ComponentLoader</param>
		/// <param name="file">The file</param>
		/// <param name="target">The target frame name</param>
		/// <returns>The Component object</returns>
		public static unoidl.com.sun.star.lang.XComponent LoadDocument(
			unoidl.com.sun.star.frame.XComponentLoader aLoader, string file, string target
			)
		{
			try
			{
				XComponent xComponent = aLoader.loadComponentFromURL(
					file, target, 0,
					new unoidl.com.sun.star.beans.PropertyValue[0] );

				return xComponent;
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Store the document to a given url
		/// </summary>
		/// <param name="storablecomponent">The storable component</param>
		/// <param name="url">The url</param>
		public static void StoreToUrl(
			unoidl.com.sun.star.frame.XStorable storablecomponent, string url)
		{
			try
			{
				storablecomponent.storeToURL(
					PathConverter(url),
					new unoidl.com.sun.star.beans.PropertyValue[] {});
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Store the document to a given url
		/// </summary>
		/// <param name="storablecomponent">The storable component</param>
		/// <param name="url">The url</param>
		public static void StoreAsUrl(
			unoidl.com.sun.star.frame.XStorable storablecomponent, string url)
		{
			try
			{
				storablecomponent.storeAsURL(
					PathConverter(url),
					new unoidl.com.sun.star.beans.PropertyValue[] {});
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Store the document
		/// </summary>
		/// <param name="storablecomponent">The storable component</param>
		/// <param name="url">The url</param>
		public static void Store(
			unoidl.com.sun.star.frame.XStorable storablecomponent, string url)
		{
			try
			{
				storablecomponent.store();
			}
			catch(System.Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Convert a windows path in a OpenOffice url
		/// </summary>
		/// <param name="file">The windows path</param>
		/// <returns>The converted url</returns>
		public static string PathConverter(string file)
		{
			try
			{
				file = file.Replace(@"\", "/");

				return "file:///"+file;
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
		}
	}
}

/*
 * $Log: Component.cs,v $
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