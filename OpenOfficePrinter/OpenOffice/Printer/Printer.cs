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
using unoidl.com.sun.star.beans;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;

namespace OpenOfficeLib.Printer
{
	/// <summary>
	/// Simple OpenOffice Printer implementation
	/// </summary>
	/// <example>Example usage:<code escaped="true">
	/// //Get the Component Context
	/// XComponentContext xComponentContext			= Connector.GetComponentContext();
	///	//Get a MultiServiceFactory
	///	XMultiServiceFactory xMultiServiceFactory	= Connector.GetMultiServiceFactory(xComponentContext);
	///	//Get a Dektop instance		
	///	XDesktop xDesktop							= Connector.GetDesktop(xMultiServiceFactory);
	///  //Convert a windows path to an OpenOffice one
	///  string myFileToPrint						= Component.PathConverter(@"D:\myFileToPrint.odt");
	///	//Load the document you want to print
	///	XComponent xComponent						= Component.LoadDocument(
	///				(XComponentLoader)xDesktop, myFileToPrint, "_blank");
	///  //Print the XComponent
	///  Printer.Print(xComponent);
	///  </code>
	/// </example>
	public class Printer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Printer"/> class.
		/// </summary>		
		public Printer()
		{
		}

		/// <summary>
		/// Prints the specified XComponent that could be any loaded
		/// OpenOffice document e.g text document, spreadsheet document, ..
		/// </summary>
		/// <param name="xComponent">The x component.</param>
		public static void Print(XComponent xComponent)
		{
			if (xComponent is unoidl.com.sun.star.view.XPrintable)
				((unoidl.com.sun.star.view.XPrintable)xComponent).print(
					new PropertyValue[] {}); 
			else
				throw new NotSupportedException("The given XComponent doesn't implement the XPrintable interface.");
		}
	}
}

/*
 * $Log: Printer.cs,v $
 * Revision 1.2  2008/04/29 15:40:04  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 09:08:40  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.3  2006/02/06 21:29:20  larsbm
 * - documentation for OpenOfficeLib
 *
 * Revision 1.2  2006/02/06 20:17:07  larsbm
 * *** empty log message ***
 *
 * Revision 1.1  2006/02/06 19:27:23  larsbm
 * - fixed bug in spreadsheet document
 * - added smal OpenOfficeLib for document printing
 *
 */