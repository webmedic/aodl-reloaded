/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2009.04.30
 * Time: 10:37
 * 
 */

using AODL.Document.Content.Text;
using System;
using AODL.Document.Content.Tables;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.TextDocuments;
using NUnit.Framework;

namespace AODLTest.Rows
{
	[TestFixture]
	public class RowCellCreation
	{
		[Test]
		public  void CreateRowsAndColumns()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.New ();
			Table table = new Table (doc,"tab1","tab1");
			
			for(int i=1; i<=4; i++)
			{
				for(int j=1; j<=5;j++)
				{
					Cell cell = table.CreateCell ();
					cell.OfficeValueType ="float";
					Paragraph paragraph = new Paragraph (doc);
					string text= (j+i-1).ToString();
					paragraph.TextContent .Add (new SimpleText ( doc,text));
					cell.Content.Add(paragraph);
					cell.OfficeValueType = "string";
					cell.OfficeValue = text;
					
					table.InsertCellAt (i, j, cell);
				}
			}
			
			Assert.AreEqual(5, table.Rows.Count);
			for (int i = 1; i < 4; i++)
			{
				Row row = table.Rows[i];
				Assert.AreEqual(6, row.Cells.Count);
			}
			
		}
		
		[Test]
		public  void AutomaticallCreateRows()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.New ();
			Table table = new Table (doc,"tab1","tab1");
			
			for(int i=1; i<=1; i++)
			{
				for(int j=1; j<=1;j++)
				{
					Cell cell = table.CreateCell ();
					cell.OfficeValueType ="float";
					Paragraph paragraph = new Paragraph (doc);
					string text= (j+i-1).ToString();
					paragraph.TextContent .Add (new SimpleText ( doc,text));
					cell.Content.Add(paragraph);
					cell.OfficeValueType = "string";
					cell.OfficeValue = text;
					
					table.InsertCellAt (i, j, cell);
				}
			}
			
			// test that we have this number of rows and cells
			Assert.AreEqual(2, table.Rows.Count);
			for (int i = 1; i < table.Rows.Count; i++)
			{
				Row row = table.Rows[i];
				Assert.AreEqual(2, row.Cells.Count);
			}
			
			
			// force to insert more cells
			table.InsertCellAt(5, 5, table.CreateCell () );
			
			// assert, that the cells were added
			Assert.AreEqual(6, table.Rows.Count);
			for (int i = 0; i < table.Rows.Count; i++)
			{
				Row row = table.Rows[i];
				Assert.AreEqual(6, row.Cells.Count);
			}
			
		}
	}
	
	[TestFixture]
	public class RowStyles
	{
		[Test]
		public void DoesNotDuplicateStyles()
		{
			TextDocument document = new TextDocument().New();
			Table table = new Table(document, "table name", "table style");
			Assert.AreEqual(1, document.Styles.Count);
			table.Rows.Add(new Row(table, "style name"));
			Assert.AreEqual(2, document.Styles.Count);
			table.Rows.Add(new Row(table, "style name"));
			Assert.AreEqual(2, document.Styles.Count);
		}
	}
	
	
	[TestFixture]
	public class TableStyles
	{
		[Test]
		public void DoesNotDuplicateStyles()
		{
			TextDocument document = new TextDocument().New();
			Assert.AreEqual(0, document.Styles.Count);
			Table table = new Table(document, "table name", "table style");
			Assert.AreEqual(1, document.Styles.Count);
			
			table = new Table(document, "table name", "table style");
			Assert.AreEqual(1, document.Styles.Count);
		}
		
		
	}
	
	[TestFixture]
	public class ColumnStyles
	{
		[Test]
		public void DoesNotDuplicateStyles()
		{
			TextDocument document = new TextDocument().New();
			Table table = new Table(document, "table name", "table style");
			Assert.AreEqual(1, document.Styles.Count);
			table.ColumnCollection.Add(new Column(table, "style name"));
			Assert.AreEqual(2, document.Styles.Count);
			table.ColumnCollection.Add(new Column(table, "style name"));
			Assert.AreEqual(2, document.Styles.Count);
		}
	}
}
