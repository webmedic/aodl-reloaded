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
using NUnit.Framework;
using System.Xml;
using AODL.Document.Content.Tables;
using AODL.Document.TextDocuments;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using OpenOfficeLib.Connection;
using OpenOfficeLib.Document;
using OpenOfficeLib.Printer;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;

namespace AODLTest
{
	/// <summary>
	/// OpenOfficeLibTests
	/// </summary>
	[TestFixture]
	public class OpenOfficeLibTests
	{
		/// <summary>
		/// Creates the new document and print it out.
		/// </summary>
		/// <remarks>This test is flagged with the explicit
		/// attribute, it won't run automaticaly.
		/// !!! This Test is marked to run explicit, because you first to have 
		/// make sure that OpenOffice.org is installed and you have a printer 
		/// installed.
		/// </remarks>
		[Test (Description="Simple print test. OpenOffice installation and printer must exist."), Explicit]
		public void CreateNewDocumentAndDoAPrintOut()
		{
			string fileToPrint						= AARunMeFirstAndOnce.outPutFolder+"fileToPrint.odt";

			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a standard paragraph using the ParagraphBuilder
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some simple text!"));
			//Add the paragraph to the document
			document.Content.Add(paragraph);
			//Save empty
			document.SaveTo(fileToPrint);

			//Now print the new document via the OpenOfficeLib
			//Get the Component Context
			XComponentContext xComponentContext			= Connector.GetComponentContext();
			//Get a MultiServiceFactory
			XMultiServiceFactory xMultiServiceFactory	= Connector.GetMultiServiceFactory(xComponentContext);
			//Get a Dektop instance		
			XDesktop xDesktop							= Connector.GetDesktop(xMultiServiceFactory);
			//Convert a windows path to an OpenOffice one
			fileToPrint						= Component.PathConverter(fileToPrint);
			//Load the document you want to print
			XComponent xComponent						= Component.LoadDocument(
				(XComponentLoader)xDesktop, fileToPrint, "_blank");
			//Print the XComponent
			Printer.Print(xComponent);
		}
	}
}

/*
 * $Log: OpenOfficeLibTests.cs,v $
 * Revision 1.4  2008/05/07 17:19:28  larsbehr
 * - Optimized Exporter Save procedure
 * - Optimized Tests behaviour
 * - Added ODF Package Layer
 * - SharpZipLib updated to current version
 *
 * Revision 1.3  2008/04/29 15:39:59  mt
 * new copyright header
 *
 * Revision 1.2  2008/02/08 07:12:18  larsbehr
 * - added initial chart support
 * - several bug fixes
 *
 * Revision 1.1  2007/02/25 09:01:28  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/02/06 19:27:22  larsbm
 * - fixed bug in spreadsheet document
 * - added smal OpenOfficeLib for document printing
 *
 */