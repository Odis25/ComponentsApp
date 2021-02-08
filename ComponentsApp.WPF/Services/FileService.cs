using ComponentsApp.WPF.Interfaces;
using ComponentsApp.WPF.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ComponentsApp.WPF.Services
{
    public class FileService : IFileService
    {
        private Document document;

        private ResultData _data;

        public void SaveToPdf(ResultData data, string filePath)
        {
            _data = data;

            document = CreateDocument();

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filePath);
        }

        public Document CreateDocument()
        {
            // Create a new MigraDoc document
            document = new Document();
            document.Info.Title = "Calculation result";
            document.Info.Subject = "";
            document.Info.Author = "ComponentsApp";

            DefineStyles();

            CreatePage();

            //FillContent(data);

            return document;
        }

        void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Arial";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("5cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Arial";
            style.Font.Name = "Arial";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = document.AddSection();

            // Создаем заголовок
            Paragraph paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddText("Результаты расчета средневзвешенного содержания компонентов");
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Создаем футтер
            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText($"Дата создания протокола: {DateTime.Now}");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Добавляем заголовок таблицы
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "2cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Таблица №3");

            // Создаем таблицу #3
            var table3 = section.AddTable();

            CreateTableAvg(table3);

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            paragraph.Style = "Reference";
            paragraph.AddText("Таблица №4");

            // Создаем таблицу #4
            var table4 = section.AddTable();

            CreateTableQuantity(table4);
        }

        private void CreateTableQuantity(Table table)
        {
            table.Style = "Table";
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Определяем столбцы таблицы
            Column column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Создаем заголовок таблицы
            Row row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Colors.Beige;

            row.Cells[0].AddParagraph("Компонент");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("Проценты весовые");
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("Средняя плотность х 10, г/м3");
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("Содержание С3+в, г/м3");
            row.Cells[3].Format.Font.Bold = true;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            table.SetEdge(0, 0, 4, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

            // Заполняем таблицу
            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("C3");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.C3:f3}   x   {_data.ComponentsQuantityTable.Density:f4} x 10   =   {_data.ComponentsQuantityTable.QuantityC3:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("iC4");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.IC4:f3}   x   {_data.ComponentsQuantityTable.Density:f4} x 10   =   {_data.ComponentsQuantityTable.QuantityIC4:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("nC4");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.NC4:f3}   x   {_data.ComponentsQuantityTable.Density:f4} x 10   =   {_data.ComponentsQuantityTable.QuantityNC4:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("iC5");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.IC5:f3}   x   {_data.ComponentsQuantityTable.Density:f4} x 10   =   {_data.ComponentsQuantityTable.QuantityIC5:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("nC5");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.NC5:f3}   x   {_data.ComponentsQuantityTable.Density:f4} x 10  =   {_data.ComponentsQuantityTable.QuantityNC5:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("C6+");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.C6:f3}   x   {_data.ComponentsQuantityTable.Density:f4} x 10   =   {_data.ComponentsQuantityTable.C6:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("Плотность, кг/м3");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.Density:f4}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;

            row.Cells[0].AddParagraph("Сумма");
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph($"{_data.ComponentsQuantityTable.Summ:f2}");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            table.SetEdge(0, table.Rows.Count - 8, 4, 8, Edge.Box, BorderStyle.Single, 0.75);
        }

        private void CreateTableAvg(Table table)
        {
            table.Style = "Table";
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Определяем столбцы таблицы
            Column column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Создаем заголовок таблицы
            Row row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Colors.Beige;

            row.Cells[0].AddParagraph("Наименование показателя");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("Попутный нефтяной газ");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = 2;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Colors.Beige;

            row.Cells[0].AddParagraph("Точка отбора пробы");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("Рославльское +Западно-Варьеганское");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].AddParagraph("Тагринское +Варьеганское +Новоаганское");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].AddParagraph("Общий ПНГ");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Colors.Beige;

            row.Cells[0].AddParagraph("Компонент");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("Средний % вес.");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].AddParagraph("Средний % вес.");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].AddParagraph("Средний % вес.");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            table.SetEdge(0, 0, 4, 3, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

            // Заполняем таблицу
            row = table.AddRow();
            row.TopPadding = 2.5;
            row.BottomPadding = 2.5;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("C1");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.CH4:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.CH4:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.CH4:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("C2");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.C2H6:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.C2H6:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.C2H6:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("C3");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.C3H8:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.C3H8:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.C3H8:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("iC4");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.IC4H10:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.IC4H10:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.IC4H10:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("nC4");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.NC4H10:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.NC4H10:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.NC4H10:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("iC5");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.IC5H12:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.IC5H12:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.IC5H12:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("nC5");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.NC5H12:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.NC5H12:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.NC5H12:f4}");

            //row = table.AddRow();
            //row.TopPadding = 2.5;
            //row.BottomPadding = 2.5;
            //row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            //row.Cells[0].AddParagraph("C6+");
            //row.Cells[1].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint1.C6H14:f4}");
            //row.Cells[2].AddParagraph($"{_data.AvgSamplesTable.AverageSampleFromPoint2.C6H14:f4}");
            //row.Cells[3].AddParagraph($"{_data.AvgSamplesTable.AverageSampleTotal.C6H14:f4}");

            // Set the borders of the specified cell range
            table.SetEdge(0, table.Rows.Count - 9, 4, 9, Edge.Box, BorderStyle.Single, 0.75);
        }

        public bool SaveSamples(SamplePoint[] samplePoints)
        {
            var serializer = new XmlSerializer(typeof(SamplePoint));
            try
            {
                for (int i = 0; i < samplePoints.Length; i++)
                {
                    using (var file = new FileStream($"samplepoint_{i + 1}.xml", FileMode.Create))
                    {
                        serializer.Serialize(file, samplePoints[i]);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SamplePoint[] LoadSamples()
        {
            var serializer = new XmlSerializer(typeof(SamplePoint));

            var result = new SamplePoint[2];
            try
            {
                for (int i = 0; i < result.Length; i++)
                {
                    using (var file = new FileStream($"samplepoint_{i + 1}.xml", FileMode.Open, FileAccess.Read))
                    {
                        result[i] = (SamplePoint)serializer.Deserialize(file);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                return null;
            }
            return result;
        }
    }
}
