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
using AODL.Document.Import;
using AODL.Document.TextDocuments;
using AODL.Document.Exceptions;
using AODL.Document.Content;
using AODL.Document.Styles;

namespace AODLTest
{
	[TestFixture]
	public class MetaData
	{
		[Test]
		public void MetaDataDisplay()
		{
			TextDocument document		= null;
			
			document				= new TextDocument();
			document.Load(AARunMeFirstAndOnce.inPutFolder+"ProgrammaticControlOfMenuAndToolbarItems.odt");

			Console.WriteLine(document.DocumentMetadata.InitialCreator);
			Console.WriteLine(document.DocumentMetadata.LastModified);
			Console.WriteLine(document.DocumentMetadata.CreationDate);
			Console.WriteLine(document.DocumentMetadata.CharacterCount);
			Console.WriteLine(document.DocumentMetadata.ImageCount);
			Console.WriteLine(document.DocumentMetadata.Keywords);
			Console.WriteLine(document.DocumentMetadata.Language);
			Console.WriteLine(document.DocumentMetadata.ObjectCount);
			Console.WriteLine(document.DocumentMetadata.PageCount);
			Console.WriteLine(document.DocumentMetadata.ParagraphCount);
			Console.WriteLine(document.DocumentMetadata.Subject);
			Console.WriteLine(document.DocumentMetadata.TableCount);
			Console.WriteLine(document.DocumentMetadata.Title);
			Console.WriteLine(document.DocumentMetadata.WordCount);

			document.DocumentMetadata.SetUserDefinedInfo(UserDefinedInfo.Info1, "Nothing");
			Console.WriteLine(document.DocumentMetadata.GetUserDefinedInfo(UserDefinedInfo.Info1));
		}

		public void DisplyMetaData(TextDocument document)
		{
			Console.WriteLine(document.DocumentMetadata.InitialCreator);
			Console.WriteLine(document.DocumentMetadata.LastModified);
			Console.WriteLine(document.DocumentMetadata.CreationDate);
			Console.WriteLine(document.DocumentMetadata.CharacterCount);
			Console.WriteLine(document.DocumentMetadata.ImageCount);
			Console.WriteLine(document.DocumentMetadata.Keywords);
			Console.WriteLine(document.DocumentMetadata.Language);
			Console.WriteLine(document.DocumentMetadata.ObjectCount);
			Console.WriteLine(document.DocumentMetadata.PageCount);
			Console.WriteLine(document.DocumentMetadata.ParagraphCount);
			Console.WriteLine(document.DocumentMetadata.Subject);
			Console.WriteLine(document.DocumentMetadata.TableCount);
			Console.WriteLine(document.DocumentMetadata.Title);
			Console.WriteLine(document.DocumentMetadata.WordCount);
		}
	}
}

/*
 * $Log: MetaData.cs,v $
 * Revision 1.2  2008/04/29 15:39:59  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 09:01:27  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.3  2006/01/29 19:30:24  larsbm
 * - Added app config support for NUnit tests
 *
 * Revision 1.2  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */