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
using System.IO;
using AODL.Document.Content;
using AODL.Document.Content.Charts;
using AODL.Document.Content.EmbedObjects;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.SpreadsheetDocuments;
using NUnit.Framework;

namespace AODLTest
{
	/// <summary>
	/// Summary description for SpreadsheetChartTest.
	/// </summary>
	
	// This test fixture should run in explicit since there are still some
	// buggy parts.
	[TestFixture]
	public class SpreadsheetChartTest
	{
		[Test (Description="Simple test of create a chart in a spreadsheet document")]
		public  void CreateNewChart()
			
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.New ();
			Table table = new Table (doc,"tab1","tab1");
			
			for(int i=1; i<=1; i++)
			{
				for(int j=1; j<=6;j++)
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
			
			Chart chart=ChartBuilder.CreateChartByAxisName
				(table,ChartTypes.bar ,"A1:E4","years","dollars");
			Assert.AreEqual(7, table.Rows[1].Cells.Count);
			Assert.AreEqual(6, table.Rows[2].Cells.Count);
			Assert.AreEqual(6, table.Rows[3].Cells.Count);
			Assert.AreEqual(6, table.Rows[4].Cells.Count);
			/*Chart chart = new Chart (table,"ch1");
			chart.ChartType=ChartTypes.bar .ToString () ;
			chart.XAxisName ="yeer";
			chart.YAxisName ="dollar";
			chart.CreateFromCellRange ("A1:E4");
			chart.EndCellAddress ="tab1.K17";*/
			table.InsertChartAt ("G2",chart);
			
			doc.Content .Add (table);
			doc.SaveTo(Path.Combine(AARunMeFirstAndOnce.outPutFolder, @"NewChartOne.ods"));
		}

		[Test]
		public void NewChartWithTitle()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"testsheet.ods"));
			Table table = doc.TableCollection[0];
			Chart chart = ChartBuilder.CreateChartByTitle (table,"A3:E7",ChartTypes.stock,"北京红旗中文两千公司九月工资报表","0.5cm","0.5cm",null,null);
			chart.ChartTitle .TitleStyle.TextProperties .FontSize="3pt";
			chart.EndCellAddress =table.TableName +".P17";
			table.InsertChartAt ("I2",chart);
			doc.Content.Add (table);
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"NewChartWithTitle.ods"));

		}
		
		[Test]
		public void NewChartWithAxises()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"testsheet.ods"));
			Table table = doc.TableCollection[0];
			Chart chart = ChartBuilder.CreateChartByAxises (table,"A1:B2",ChartTypes.line ,2);
			table.InsertChartAt ("I2",chart);
			doc.Content.Add (table);
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"NewChartWithAxis.ods"));
		}

		[Test]
		public void NewChartWithLegend()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"testsheet.ods"));
			Table table = doc.TableCollection[0];
			Chart chart = ChartBuilder.CreateChartByLegend (table,"A3:F8",ChartTypes.surface ,"left","0.5","5","year","dollars");
			table.InsertChartAt ("M2",chart);
			doc.Content.Add (table);
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"NewChartWithLegend.ods"));
		}

		[Test]
		public void NewChartWithCellRange()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"testsheet.ods"));
			Table table = doc.TableCollection[0];
			Chart chart = ChartBuilder.CreateChartByCellRange (table,"A4:F8",ChartTypes.bar ,null,null,"刘玉花的测试",3,"bottom","P14");
			table.InsertChartAt ("H2",chart);
			doc.Content.Add (table);
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"NewChartWithCellRange.ods"));

		}

		[Test]
		[Ignore("Ignore still buggy!")]
		public void LoadChart()
		{
			SpreadsheetDocument doc= new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"TestChartOne.ods"));
			IContent iContent = (EmbedObject)doc.EmbedObjects [0];
			((Chart)iContent).ChartType =ChartTypes.bar.ToString ();
			((Chart)iContent).XAxisName ="XAxis";
			((Chart)iContent).YAxisName ="YAxis";
			((Chart)iContent).SvgWidth ="20cm";
			((Chart)iContent).SvgHeight ="20cm";
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"LoadChart.ods"));
		}

		[Test]
		[Ignore("Ignore still buggy!")]
		public void LoadChartModifyTitle()
		{
			SpreadsheetDocument doc= new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"TestChartOne.ods"));
			IContent iContent = (EmbedObject)doc.EmbedObjects [0];
			((Chart)iContent).ChartTitle.SetTitle ("A New Title");
			((Chart)iContent).ChartTitle.SvgX ="2cm";
			((Chart)iContent).ChartTitle.SvgY ="0.5cm";
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"TestTitle.ods"));

		}
        [Test]
        public void NewBasicChartThenSetTitle()
        {
            string expected="Basic Chart";
            SpreadsheetDocument doc = new SpreadsheetDocument();
            doc.New();
            Table table = new Table(doc, "tab1", "tab1");

            for (int i = 1; i <= 1; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    Cell cell = table.CreateCell();
                    cell.OfficeValueType = "float";
                    Paragraph paragraph = new Paragraph(doc);
                    string text = (j + i - 1).ToString();
                    paragraph.TextContent.Add(new SimpleText(doc, text));
                    cell.Content.Add(paragraph);
                    cell.OfficeValueType = "string";
                    cell.OfficeValue = text;
                    table.InsertCellAt(i, j, cell);
                }
            }
            Chart basicChart = ChartBuilder.CreateChart(table, ChartTypes.line, "A4:F8");
            ChartTitle ct = new ChartTitle(basicChart);
            //ct.InitTitle();
            ct.SetTitle(expected);
            Assert.AreEqual(expected, ((Paragraph)ct.Content[0]).TextContent[0].Text);
            basicChart.ChartTitle = ct;
            IContent chartTitleContent = null;
            chartTitleContent=basicChart.Content.Find(o => o is ChartTitle);
            if (chartTitleContent == null)
            {
                foreach (IContent iContent in basicChart.Content)
                {
                    if (iContent is ChartTitle) chartTitleContent = iContent;
                }
            }
            Assert.AreEqual(expected, ((Paragraph)((ChartTitle)chartTitleContent).Content[0]).TextContent[0].Text);
            table.InsertChartAt("H2", basicChart);
            doc.TableCollection.Add(table);
            doc.SaveTo(Path.Combine(AARunMeFirstAndOnce.outPutFolder,"BasicChartWithTitlesetafterwards.ods"));
        }
        
		[Test]
		[Ignore("Ignore still buggy!")]
		public void TestLengend()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"TestChartOne.ods"));
			Chart chart = (Chart)doc.EmbedObjects [0];
			chart.ChartLegend .LegendPosition ="left";
			chart.ChartLegend .SvgX ="5cm";
			chart.ChartLegend .SvgY ="2cm";
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"TestLegend.ods"));

		}

		[Test]
		[Ignore("Ignore still buggy!")]
		public void TestPlotArea()
		{
			SpreadsheetDocument doc = new SpreadsheetDocument ();
			doc.Load(Path.Combine(AARunMeFirstAndOnce.inPutFolder,@"TestChartOne.ods"));
			Chart chart =(Chart)doc.EmbedObjects [0];
			chart.ChartPlotArea .SvgX ="1.2cm";
			chart.ChartPlotArea .SvgY ="2.5cm";
			chart.ChartPlotArea .Width ="5cm";
			chart.ChartPlotArea .Height ="5cm";
			doc.SaveTo (Path.Combine(AARunMeFirstAndOnce.outPutFolder,"TestPlotArea.ods"));
		}
	}

	
}
