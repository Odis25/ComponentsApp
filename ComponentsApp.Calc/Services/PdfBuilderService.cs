using ComponentsApp.Data.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ComponentsApp.Services.Services
{
    public class PdfBuilderService
    {
        private static Document _document;
        private static Result _data;

        public static Document BuildDocument(Result data)
        {
            _data = data;

            // Create a new MigraDoc document
            _document = new Document();
            _document.Info.Title = "Результаты расчета";
            _document.Info.Subject = "";
            _document.Info.Author = "ComponentsApp";

            DefineStyles();


            try
            {
            CreatePage();

            }
            catch (Exception e)
            {
                throw e;
            }

            //FillContent(data);

            return _document;
        }
        private static void DefineStyles()
        {
            // Общий стиль документа 
            Style style = _document.Styles[StyleNames.Normal];
            style.Font.Name = "Arial";
            style.Font.Size = 12;
            //style.ParagraphFormat.Font.Bold = true;

            // Стиль заголовка
            style = _document.Styles[StyleNames.Header];
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            style.ParagraphFormat.Font.Bold = true;
            style.ParagraphFormat.Font.Size = 14;
            style.ParagraphFormat.AddTabStop("10cm", TabAlignment.Center);

            // Стиль подзаголовка
            style = _document.Styles[StyleNames.Heading1];
            style.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            style.ParagraphFormat.Font.Bold = true;
            style.ParagraphFormat.Font.Size = 11;
            style.ParagraphFormat.SpaceAfter = 10;
            style.ParagraphFormat.SpaceBefore = 10;

            // Стиль футтера
            style = _document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Стиль для табличных данных
            style = _document.Styles.AddStyle("Table", StyleNames.Normal);
        }

        private static void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            var section = _document.AddSection();

            // Создаем футтер
            var paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText($"Дата создания протокола: {DateTime.Now}");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Создаем заголовок страницы
            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddText("Результат расчета средневзвешенной массовой концентрации фракций углеводородов С3+высшие в ПНГ");
            paragraph.Style = StyleNames.Header;

            // Создаем основной контент

            // Создаем 1-й подзаголовок
            paragraph = section.AddParagraph();
            paragraph.AddText("1. Массовые доли компонентов ПНГ");
            paragraph.Style = StyleNames.Heading1;

            // Создаем таблицу "Массовые доли компонентов ПНГ"
            var table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;           
            table.Rows.VerticalAlignment = VerticalAlignment.Center;
            table.Rows.Height = 18;
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Создаем столбцы таблицы
            var column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("6cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn("4cm");
            table.AddColumn("4cm");

            // Создаем строки
            var row = table.AddRow();
            row.Cells[0].AddParagraph("№ п/п");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("Компонент");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].AddParagraph("Массовая доля, %");
            row.Cells[0].MergeDown = 1;
            row.Cells[1].MergeDown = 1;
            row.Cells[2].MergeRight = 1;

            row = table.AddRow();
            row.Cells[2].AddParagraph("1-ая группа МР");
            row.Cells[3].AddParagraph("2-ая группа МР");

            row = table.AddRow();
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].AddParagraph("1");
            row.Cells[1].AddParagraph("2");
            row.Cells[2].AddParagraph("3");
            row.Cells[3].AddParagraph("4");

            var properties = typeof(Sample).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name.Equals("Density")) break;

                var name = properties[i]
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .Cast<DisplayNameAttribute>()
                    .First()
                    .DisplayName;

                var value1 = (double)properties[i].GetValue(_data.SamplesCollection[0]);
                var value2 = (double)properties[i].GetValue(_data.SamplesCollection[1]);

                row = table.AddRow();
                row.Cells[0].AddParagraph((i + 1).ToString());
                row.Cells[1].AddParagraph(name);
                row.Cells[2].AddParagraph(value1.ToString("f3"));
                row.Cells[3].AddParagraph(value2.ToString("f3"));
            }

            table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Box, BorderStyle.Single, 0.75);

            // Создаем 2-й подзаголовок
            paragraph = section.AddParagraph();
            paragraph.AddText("2. Молярная масса ПНГ");
            paragraph.Style = StyleNames.Heading1;

            // Создаем таблицу "Молярная масса ПНГ"
            table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Rows.VerticalAlignment = VerticalAlignment.Center;
            table.Rows.Height = 18;
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Создаем столбцы таблицы
            table.AddColumn("8cm");
            table.AddColumn("8cm");

            // Создаем строки
            row = table.AddRow();
            row.Cells[0].AddParagraph("Молярная масса, г/моль");
            row.Cells[0].MergeRight = 1;

            row = table.AddRow();
            row.Cells[0].AddParagraph("1-я группа МР");
            row.Cells[1].AddParagraph("2-я группа МР");

            row = table.AddRow();
            row.Cells[0].AddParagraph(_data.MassCollection[0].ToString("f3"));
            row.Cells[1].AddParagraph(_data.MassCollection[1].ToString("f3"));

            table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Box, BorderStyle.Single, 0.75);

            // Создаем 3-й подзаголовок
            paragraph = section.AddParagraph();
            paragraph.AddText("3. Плотность ПНГ при стандарных условиях");
            paragraph.Style = StyleNames.Heading1;

            // Создаем таблицу "Плотность при стандартных условиях"
            table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Rows.VerticalAlignment = VerticalAlignment.Center;
            table.Rows.Height = 18;
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Создаем столбцы таблицы
            table.AddColumn("8cm");
            table.AddColumn("8cm");

            // Создаем строки
            row = table.AddRow();
            row.Cells[0].AddParagraph("Плотность при стандартных условиях, кг/м3");
            row.Cells[0].MergeRight = 1;

            row = table.AddRow();
            row.Cells[0].AddParagraph("1-я группа МР");
            row.Cells[1].AddParagraph("2-я группа МР");

            row = table.AddRow();
            row.Cells[0].AddParagraph(_data.DensityCollection[0].ToString("f5"));
            row.Cells[1].AddParagraph(_data.DensityCollection[1].ToString("f5"));

            table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Box, BorderStyle.Single, 0.75);

            // Создаем 4-й подзаголовок
            paragraph = section.AddParagraph();
            paragraph.AddText("4. Средневзвешенная массовая концентрация фракций углеводородов C3+высшие");
            paragraph.Style = StyleNames.Heading1;

            // Заполняем раздел
            paragraph = section.AddParagraph();
            paragraph.AddText($"Сумма массовых долей углеводородов C3+высшие первого МР равна {_data.ComponentsSummCollection[0]:f3}%");
            paragraph.Style = StyleNames.Normal;

            paragraph = section.AddParagraph();
            paragraph.AddText($"Сумма массовых долей углеводородов C3+высшие второго МР равна {_data.ComponentsSummCollection[1]:f3}%");
            paragraph.Style = StyleNames.Normal;

            paragraph = section.AddParagraph();
            paragraph.AddText($"Средневзвешенная массовая концентрация фракций углеводородов C3+высшие равна {_data.WeightedAvgConc:f2} г/моль");
            paragraph.Style = StyleNames.Normal;
        }
    }
}
