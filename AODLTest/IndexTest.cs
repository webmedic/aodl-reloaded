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
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;

namespace AODLTest
{
	[TestFixture]
	public class IndexTest
	{
		[Test]
		public void TableOfContentsTest()
		{
		//Create new Document
		TextDocument textDocument		= new TextDocument();
		textDocument.New();
		//Create a new Table of contents
		TableOfContents tableOfContents	= new TableOfContents(
			textDocument, "Table_Of_Contents", false, false, "Table of Contents");
		//Add the toc
		textDocument.Content.Add(tableOfContents);
		//Create a new heading, there's no need of the chapter number
		string sHeading					= "A first headline";
		//The corresponding text entry, here you need to set the
		//chapter number
		string sTocEntry				= "1. A first headline";
		Header header					= new Header(
			textDocument, Headings.Heading_20_1);
		header.OutLineLevel				= "1";
		header.TextContent.Add(new SimpleText(textDocument,  sHeading));
		//add the header to the content
		textDocument.Content.Add(header);
		//add the toc entry text as entry to the Table of contents
		tableOfContents.InsertEntry(sTocEntry, 1);
		//Add some text to this chapter
		Paragraph paragraph				= ParagraphBuilder.CreateStandardTextParagraph(textDocument);
		paragraph.TextContent.Add(new SimpleText(textDocument, "I'm the text for the first chapter!"));
		textDocument.Content.Add(paragraph);
		//Add a sub header to the first chapter
		//Create a new heading, there's no need of the chapter number
		sHeading						= "A first sub headline";
		//The corresponding text entry, here you need to set the
		//chapter number
		sTocEntry						= "1.1. A first sub headline";
		header							= new Header(
			textDocument, Headings.Heading_20_2);
		header.OutLineLevel				= "2";
		header.TextContent.Add(new SimpleText(textDocument,  sHeading));
		//add the header to the content
		textDocument.Content.Add(header);
		//add the toc entry text as entry to the Table of contents
		tableOfContents.InsertEntry(sTocEntry, 2);
		//Add some text to this sub chapter
			paragraph				= ParagraphBuilder.CreateStandardTextParagraph(textDocument);
			paragraph.TextContent.Add(new SimpleText(textDocument, "I'm the text for the subchapter chapter!"));
		textDocument.Content.Add(paragraph);
//		ListStyle listStyle				= new ListStyle(textDocument, "TOC_LIST");
//		listStyle.AutomaticAddListLevelStyles(ListStyles.Number);
//		textDocument.Styles.Add(listStyle);
		//Save it
		textDocument.SaveTo(AARunMeFirstAndOnce.outPutFolder+"toc.odt");
		}
	}
}

/*
 * $Log: IndexTest.cs,v $
 * Revision 1.2  2008/04/29 15:39:59  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 09:01:27  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.3  2006/02/21 19:34:54  larsbm
 * - Fixed Bug text that contains a xml tag will be imported  as UnknowText and not correct displayed if document is exported  as HTML.
 * - Fixed Bug [ 1436080 ] Common styles
 *
 * Revision 1.2  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 */