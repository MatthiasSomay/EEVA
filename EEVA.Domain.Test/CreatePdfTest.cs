using Aspose.Words;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace EEVA.Domain.Test
{
    [TestClass]
    public class CreatePdfTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Tuple<int, string, string, string, string>> testData = new List<Tuple<int, string, string, string, string>>()
            {
                new Tuple<int, string, string, string, string>(1, "Question", "Answer 1", "Answer 2", "Answer 3"),
                new Tuple<int, string, string, string, string>(2, "Question", "Answer 1", "Answer 2", "Answer 3")
            };

            string outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                Document doc = new Document();
                DocumentBuilder docBuilder = new DocumentBuilder(doc);
                Style style = doc.Styles.Add(StyleType.Paragraph, "MyStyle");
                style.Font.Size = 11;
                style.Font.Name = "Calibri";
                style.ParagraphFormat.SpaceAfter = 10;
                style.ListFormat.List = doc.Lists.Add(Aspose.Words.Lists.ListTemplate.BulletCircle);
                style.ListFormat.ListLevelNumber = 0;

                Style style2 = doc.Styles.Add(StyleType.Paragraph, "MyStyle2");
                style2.Font.Size = 12;
                style2.Font.Name = "Calibri";
                style2.ParagraphFormat.SpaceAfter = 12;
                style2.ListFormat.List = doc.Lists.Add(Aspose.Words.Lists.ListTemplate.NumberArabicDot);
                style2.ListFormat.ListLevelNumber = 0;




                Paragraph p = docBuilder.InsertParagraph();
                p.Runs.Add(new Run(doc, "Examen 2019"));
                foreach (var item in testData)
                {
                    docBuilder.InsertBreak(BreakType.LineBreak);
                    docBuilder.InsertParagraph();
                    docBuilder.ParagraphFormat.Style = style2;
                    docBuilder.Writeln(item.Item2);
                    docBuilder.ParagraphFormat.Style = style;
                    docBuilder.Writeln(item.Item3);
                    docBuilder.Writeln(item.Item4);
                    docBuilder.Write(item.Item5);

                    //docBuilder.InsertShape(Aspose.Words.Drawing.ShapeType.Arrow, 2, 2);
                    //docBuilder.Write("  " + item.Item3);
                    //docBuilder.InsertBreak(BreakType.LineBreak);
                    //docBuilder.InsertShape(Aspose.Words.Drawing.ShapeType.Arrow, 2, 2);
                    //docBuilder.Write("  " + item.Item4);
                    //docBuilder.InsertBreak(BreakType.LineBreak);
                    //docBuilder.InsertShape(Aspose.Words.Drawing.ShapeType.Arrow, 2, 2);
                    //docBuilder.Write("  " + item.Item5);
                    //docBuilder.InsertBreak(BreakType.LineBreak);

                }
                doc.Save(outputPath, SaveFormat.Pdf);

                Assert.IsTrue(File.Exists(outputPath));
            }
            finally
            {
                
            }
            
        }
    }
}
