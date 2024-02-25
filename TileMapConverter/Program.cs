using System.Text.Json;
using TileMapConverter.Models;

using var converter = new TileMapConverter.Converter();
converter.Main();

namespace TileMapConverter
{
    public class Converter : IDisposable
    {
        public Converter()
        {
            Console.WriteLine("\nConverting CSV to JSON, stand by...\n");
        }
        
        public void Main()
        {
            // Read csv data
            Console.WriteLine("Reading CSV...");
            List<TileRow> rows = ReadRowsFromCsv();
            Console.WriteLine("Done reading from CSV \n");
            
            // Write to json file
            Console.WriteLine("Writing to JSON...");
            WriteToJson(rows);
            Console.WriteLine("Done writing to JSON\n");
        }

        private List<TileRow> ReadRowsFromCsv()
        {
            List<TileRow> rows = new List<TileRow>();
            
            try
            {
                string path = Path.Combine(
                    Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, 
                    @"CsvFiles\HomeTileMap.csv"
                );
                
                Console.WriteLine($"Reading from CSV project path: {path}");
                
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read and display lines from the file until the end of the file is reached.
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<TileColumn> cols = new List<TileColumn>();
                        
                        // Split the csv line by comma
                        string[] values = line.Split(',');
                        foreach (string str in values)
                        {
                            // Split the column data by colon
                            string[] columnData = str.Split(':');
                            cols.Add(new TileColumn
                            {
                                HasCollider = columnData[0] == "c",
                                ColliderType = ColliderTypeMapper.Get(columnData[1]),
                                TextureName = columnData[2]
                            });
                        }
                        
                        TileRow tileRow = new TileRow
                        {
                            TileColumns = cols
                        };
                        rows.Add(tileRow);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return rows;
        }

        private void WriteToJson(List<TileRow> rows)
        {
            string path = Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, 
                @"JsonFiles\HomeTileMap.json"
            );
            Console.WriteLine($"Writing to JSON project path: {path}");
            string jsonText = JsonSerializer.Serialize(rows);
            File.WriteAllText(path, jsonText);
        }

        public void Dispose()
        {
            Console.WriteLine("Done converting files, disposing...");
        }
    }
}