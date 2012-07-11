/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2009.04.30
 * Time: 15:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using AODL.Document;
using System;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using NUnit.Framework;

namespace AODLTest.Document.Styles
{
	[TestFixture]
	public class TestStyleCollection
	{
		[Test]
		public void GetStyleByName()
		{
			TextDocument document = new TextDocument();
			document.New();
			StyleCollection collection = new StyleCollection();
			collection.Add(new TableStyle(document, "a"));
			collection.Add(new TableStyle(document, "b"));
			IStyle nullStyle = new TableStyle(document, string.Empty);
			collection.Add(nullStyle);
			
			Assert.AreEqual("a", collection.GetStyleByName("a").StyleName);
			Assert.AreEqual("b", collection.GetStyleByName("b").StyleName);
			Assert.AreEqual(nullStyle, collection.GetStyleByName(string.Empty));
		}
		
		[Test]
		public void GetStyleByName_Null()
		{
			TextDocument document = new TextDocument();
			document.New();
			StyleCollection collection = new StyleCollection();
			IStyle nullStyle = new TableStyle(document, string.Empty);
			collection.Add(nullStyle);
			
			Assert.AreEqual(nullStyle, collection.GetStyleByName(null));
		}
	}
}
