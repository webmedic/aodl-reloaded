/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2008-12-18
 * Time: 15:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.TextDocuments;
using AODL.Utils;
using NUnit.Framework;

namespace AODLTest.Performance
{
	[TestFixture]
	public class TestCloneTable
	{
		[Test]
		public void CloneTable()
		{
			TextDocument docuemnt = new TextDocument();
			docuemnt.New();
			Table table = new Table(docuemnt, "table name", "table style");
			
			int numberOfColumns = 10;
			int numberOfRows = 10;
			
			// prepare data
			// add columns
			for (int i = 0; i < numberOfColumns; i++)
			{
				table.ColumnCollection.Add(new Column(table, "style name " + i));
			}
			
			Row row = null;
			// Add rows
			for (int i = 0; i < 2; i++)
			{
				row = new Row(table);
				for (int i1 = 0; i1 < numberOfColumns; i1++)
				{
					Paragraph par = ParagraphBuilder.CreateStandardTextParagraph(docuemnt);
					par.TextContent.AddRange(TextBuilder.BuildTextCollection(docuemnt, (i * numberOfColumns).ToString() + i1));
					row.Cells.Add(new Cell(table.Document, "cell style " + i));
					//row.Cells.Add(new Cell(table.Document));
					row.Cells[i1].Content.Add(par);
					
				}
				table.Rows.Insert(0, row);
			}
			
			
			// clone many rows
			row = table.Rows[0];
			using (IPerformanceCounter counter = new PerformanceCounter())
			{
				for (int i = 0; i < numberOfRows; i++)
				{
					Row newRow = new Row(table, row.StyleName);
					foreach (Cell rowCell in row.Cells)
					{
						Cell cell = new ContentMocker().CloneAny(rowCell) as Cell;
						
						newRow.Cells.Add(cell);
					}
				}
				
				Console.WriteLine(string.Format(
					"Test executed in {0} seconds", counter.GetSeconds()));
			}
		}
	}
	
	[TestFixture]
	public class TestTablePerformance
	{
		[Test, Explicit]
		public void CreateEmptyTable()
		{
			// as of 2008-12-28 this tests runs:
			// 14 seconds with AMD athlon 64 X2 machine,
			
			TextDocument docuemnt = new TextDocument();
			docuemnt.New();
			Table table = new Table(docuemnt, "table name", "table style");
			
			int numberOfColumns = 100;
			int numberOfRows = 10000;
			using (IPerformanceCounter counter = new PerformanceCounter())
			{
				// add columns
				for (int i = 0; i < numberOfColumns; i++)
				{
					table.ColumnCollection.Add(new Column(table, "style name"));
				}
				
				// Add rows
				for (int i = 0; i < numberOfRows; i++)
				{
					Row row = new Row(table);
					for (int i1 = 0; i1 < numberOfColumns; i1++)
					{
						row.Cells.Add(new Cell(table.Document));
					}
					table.Rows.Insert(0, row);
				}
				
				Console.WriteLine(string.Format(
					"Test executed in {0} seconds", counter.GetSeconds()));
			}
		}
		
		[Test, Explicit]
		public void CloneTable()
		{
			// with 100 columns and 5000 rows should execute in less than 29 sec
			// with 100 columns and 1000 rows should execute in less than 6.5 sec
			TextDocument docuemnt = new TextDocument();
			docuemnt.New();
			Table table = new Table(docuemnt, "table name", "table style");
			
			int numberOfColumns = 100;
			int numberOfRows = 1000;
			
			// prepare data
			// add columns
			for (int i = 0; i < numberOfColumns; i++)
			{
				table.ColumnCollection.Add(new Column(table, "style name " + i));
			}
			
			Row row = null;
			// Add rows
			for (int i = 0; i < 2; i++)
			{
				row = new Row(table);
				for (int i1 = 0; i1 < numberOfColumns; i1++)
				{
					Paragraph par = ParagraphBuilder.CreateStandardTextParagraph(docuemnt);
					par.TextContent.AddRange(TextBuilder.BuildTextCollection(docuemnt, (i * numberOfColumns).ToString() + i1));
					row.Cells.Add(new Cell(table.Document, "cell style " + i));
					//row.Cells.Add(new Cell(table.Document));
					row.Cells[i1].Content.Add(par);
					
				}
				table.Rows.Insert(0, row);
			}
			
			
			// clone many rows
			row = table.Rows[0];
			using (IPerformanceCounter counter = new PerformanceCounter())
			{
				for (int i = 0; i < numberOfRows; i++)
				{
					Row newRow = new Row(table, row.StyleName);
					foreach (Cell rowCell in row.Cells)
					{
						Cell cell = new ContentMocker().CloneAny(rowCell) as Cell;
						
						newRow.Cells.Add(cell);
					}
				}
				
				Console.WriteLine(string.Format(
					"Test executed in {0} seconds", counter.GetSeconds()));
			}
		}
	}
}
