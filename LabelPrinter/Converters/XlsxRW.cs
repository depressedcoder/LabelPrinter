using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;

namespace LabelPrinter
{
    internal static class XlsxRW
    {
        public static void DeleteDirectoryContents(string directory)
        {
            var info = new DirectoryInfo(directory);

            foreach (var file in info.GetFiles())
                file.Delete();

            foreach (var dir in info.GetDirectories())
                dir.Delete(true);
        }

        public static void UnzipFile(string zipFileName, string targetDirectory)
        {
            new FastZip().ExtractZip(zipFileName, targetDirectory, null);
        }

        public static void ZipDirectory(string sourceDirectory, string zipFileName)
        {
            new FastZip().CreateZip(zipFileName, sourceDirectory, true, null);
        }

        public static IList<string> ReadStringTable(Stream input)
        {
            var stringTable = new List<string>();

            using (var reader = XmlReader.Create(input))
                for (reader.MoveToContent(); reader.Read(); )
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "t")
                        stringTable.Add(reader.ReadElementString());

            return stringTable;
        }

        public static void ReadWorksheet(Stream input, IList<string> stringTable, DataTable data)
        {
            using (var reader = XmlReader.Create(input))
            {
                DataRow row = null;
                int columnIndex = 0;
                string type;
                int value;

                for (reader.MoveToContent(); reader.Read(); )
                    if (reader.NodeType == XmlNodeType.Element)
                        switch (reader.Name)
                        {
                            case "row":
                                row = data.NewRow();
                                data.Rows.Add(row);

                                columnIndex = 0;

                                break;

                            case "c":
                                type = reader.GetAttribute("t");
                                reader.Read();
                                value = int.Parse(reader.ReadElementString(), CultureInfo.InvariantCulture);

                                if (type == "s")
                                    row[columnIndex] = stringTable[value];
                                else
                                    row[columnIndex] = value;

                                columnIndex++;

                                break;
                        }
            }
        }

        public static IList<string> CreateStringTables(DataTable data, out IDictionary<string, int> lookupTable)
        {
            var stringTable = new List<string>();
            lookupTable = new Dictionary<string, int>();

            foreach (DataRow row in data.Rows)
                foreach (DataColumn column in data.Columns)
                    if (column.DataType == typeof(string))
                    {
                        var value = (string)row[column];

                        if (!lookupTable.ContainsKey(value))
                        {
                            lookupTable.Add(value, stringTable.Count);
                            stringTable.Add(value);
                        }
                    }

            return stringTable;
        }

        public static void WriteStringTable(Stream output, IList<string> stringTable)
        {
            using (var writer = XmlWriter.Create(output))
            {
                writer.WriteStartDocument(true);

                writer.WriteStartElement("sst", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
                writer.WriteAttributeString("count", stringTable.Count.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("uniqueCount", stringTable.Count.ToString(CultureInfo.InvariantCulture));

                foreach (var str in stringTable)
                {
                    writer.WriteStartElement("si");
                    writer.WriteElementString("t", str);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        public static string RowColumnToPosition(int row, int column)
        {
            return ColumnIndexToName(column) + RowIndexToName(row);
        }

        public static string ColumnIndexToName(int columnIndex)
        {
            var second = (char)(((int)'A') + columnIndex % 26);

            columnIndex /= 26;

            if (columnIndex == 0)
                return second.ToString();
            else
                return ((char)(((int)'A') - 1 + columnIndex)).ToString() + second.ToString();
        }

        public static string RowIndexToName(int rowIndex)
        {
            return (rowIndex + 1).ToString(CultureInfo.InvariantCulture);
        }

        public static void WriteWorksheet(Stream output, DataTable data, IDictionary<string, int> lookupTable)
        {
            using (XmlTextWriter writer = new XmlTextWriter(output, Encoding.UTF8))
            {
                writer.WriteStartDocument(true);

                writer.WriteStartElement("worksheet");
                writer.WriteAttributeString("xmlns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
                writer.WriteAttributeString("xmlns:r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

                writer.WriteStartElement("dimension");
                var lastCell = RowColumnToPosition(data.Rows.Count - 1, data.Columns.Count - 1);
                writer.WriteAttributeString("ref", "A1:" + lastCell);
                writer.WriteEndElement();

                writer.WriteStartElement("sheetViews");
                writer.WriteStartElement("sheetView");
                writer.WriteAttributeString("tabSelected", "1");
                writer.WriteAttributeString("workbookViewId", "0");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("sheetFormatPr");
                writer.WriteAttributeString("defaultRowHeight", "15");
                writer.WriteEndElement();

                writer.WriteStartElement("sheetData");
                WriteWorksheetData(writer, data, lookupTable);
                writer.WriteEndElement();

                writer.WriteStartElement("pageMargins");
                writer.WriteAttributeString("left", "0.7");
                writer.WriteAttributeString("right", "0.7");
                writer.WriteAttributeString("top", "0.75");
                writer.WriteAttributeString("bottom", "0.75");
                writer.WriteAttributeString("header", "0.3");
                writer.WriteAttributeString("footer", "0.3");
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public static void WriteWorksheetData(XmlTextWriter writer, DataTable data, IDictionary<string, int> lookupTable)
        {
            var rowsCount = data.Rows.Count;
            var columnsCount = data.Columns.Count;
            string relPos;

            for (int row = 0; row < rowsCount; row++)
            {
                writer.WriteStartElement("row");
                relPos = RowIndexToName(row);
                writer.WriteAttributeString("r", relPos);
                writer.WriteAttributeString("spans", "1:" + columnsCount.ToString(CultureInfo.InvariantCulture));

                for (int column = 0; column < columnsCount; column++)
                {
                    object value = data.Rows[row][column];

                    writer.WriteStartElement("c");
                    relPos = RowColumnToPosition(row, column);
                    writer.WriteAttributeString("r", relPos);

                    var str = value as string;
                    if (str != null)
                    {
                        writer.WriteAttributeString("t", "s");
                        value = lookupTable[str];
                    }

                    writer.WriteElementString("v", value.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
    }
}
