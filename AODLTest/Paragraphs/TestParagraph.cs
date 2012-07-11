/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2008-12-19
 * Time: 14:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using NUnit.Framework;
using System.IO;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using AODL.Document.Content.Text;

namespace AODLTest.Paragraphs
{
	[TestFixture]
	public class TestParagraph
	{
		[Test]
		public void LoadDocument_With_PageBreak()
		{
			string file = AARunMeFirstAndOnce.inPutFolder + @"paragraph_with_page_break.odt";
			TextDocument textDocument = new TextDocument();
			textDocument.Load(file);

			Assert.AreEqual(3, textDocument.Content.Count);
			Paragraph paragraph = textDocument.Content[2] as Paragraph;
			Assert.IsNotNull(paragraph);
			Assert.AreEqual("page", paragraph.ParagraphStyle.ParagraphProperties.BreakBefore );
			
		}
		
		[TestFixtureSetUp]
		public void Init()
		{
		}
	}
}
