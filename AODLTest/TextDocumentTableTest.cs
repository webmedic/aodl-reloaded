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
using AODL.Document.Helper;

namespace AODLTest
{
	[TestFixture]
	public class TextDocumentTableTest
	{
		[Test]
		public void SimpleTable()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
														document,
														"table1",
														"table1",
														3,
														3,
														16.99,
														false,
														false);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some cell text"));
			//Insert paragraph into the first cell
			table.Rows[0].Cells[0].Content.Add(paragraph);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTable.odt");
		}

		[Test]
		public void SimpleTableWithRowHeaderAndBorder()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
														document,
														"table1",
														"table1",
														3,
														3,
														16.99,
														true,
														true);
			//Create a standard paragraph for the row header
			Paragraph paragraph1					= ParagraphBuilder.CreateStandardTextParagraph(document);
			paragraph1.TextContent.Add(new SimpleText(document, "Table row header"));
			table.RowHeader.RowCollection[0].Cells[0].Content.Add(paragraph1);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some cell text"));
			//Insert paragraph into the first cell
			table.Rows[0].Cells[0].Content.Add(paragraph);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTableWithBorderRowheader.odt");
		}

		[Test]
		public void SimpleTableWithList()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
				document,
				"table1",
				"table1",
				3,
				3,
				16.99,
				false,
				false);
			//Create a bullet list
			List list								= new List(document, "L1", ListStyles.Bullet, "L1P1");
			//Create a list item
			ListItem lit							= new ListItem(list);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "List item text"));
			//Add paragraph to the list item
			lit.Content.Add(paragraph);
			//Add item to the list
			list.Content.Add(lit);
			//Insert paragraph into the first cell
			table.Rows[0].Cells[0].Content.Add(list);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTableWithList.odt");
		}

		[Test]
		public void SimpleTableWithMergedCells()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
				document,
				"table1",
				"table1",
				3,
				3,
				16.99,
				false,
				false);

			//Fill first all tables
			foreach(Row row in table.Rows)
				foreach(Cell cell in row.Cells)
				{
					//Create a standard paragraph
					Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
					//Add some simple text
					paragraph.TextContent.Add(new SimpleText(document, "Cell text"));
					cell.Content.Add(paragraph);
				}
			//Merge some cells. Notice this is only available in text documents!
			table.Rows[1].MergeCells(document, 1, 2, true);
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"simpleTableWithMergedCells.odt");
		}

		[Test]
		public void NestedTable()
		{
			//Create a new text document
			TextDocument document					= new TextDocument();
			document.New();
			//Create a table for a text document using the TableBuilder
			Table table								= TableBuilder.CreateTextDocumentTable(
				document,
				"table1",
				"table1",
				3,
				3,
				16.99,
				false,
				false);
			//Create a standard paragraph
			Paragraph paragraph						= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some cell text"));
			Assert.IsNotNull(table.Rows, "Must exist.");
			Assert.IsTrue(table.Rows.Count == 3, "There must be 3 rows.");
			//Insert paragraph into the second cell
			table.Rows[0].Cells[1].Content.Add(paragraph);
			//Get width of the nested table
			double nestedTableWidth					= SizeConverter.GetDoubleFromAnOfficeSizeValue(
				table.ColumnCollection[0].ColumnStyle.ColumnProperties.Width);
			//Create another table using the TableBuilder
			Table nestedTable						= TableBuilder.CreateTextDocumentTable(
				document,
				"table1",
				"table1",
				2,
				2,
				nestedTableWidth,
				false,
				false);
			//Create a new standard paragraph
			paragraph								= ParagraphBuilder.CreateStandardTextParagraph(document);
			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Some cell text inside the nested table"));
			Assert.IsNotNull(nestedTable.Rows, "Must exist.");
			Assert.IsTrue(nestedTable.Rows.Count == 2, "There must be 3 rows.");
			//Insert paragraph into the first cell
			nestedTable.Rows[0].Cells[0].Content.Add(paragraph);
			//Insert the nested table into the first row and first cell
			table.Rows[0].Cells[0].Content.Add(nestedTable);
			Assert.IsTrue(table.Rows[0].Cells[0].Content[0] is Table, "Must be the nested table.");
			//Add table to the document
			document.Content.Add(table);
			//Save the document
			document.SaveTo(AARunMeFirstAndOnce.outPutFolder+"nestedTable.odt");
		}
	}
}
