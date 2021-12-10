using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVGenerator
{
    class Program
    {
        private static string _dataFileName;
        private static int _dataCount = 100;
        private static readonly string _dataFileDirectory = AppDomain.CurrentDomain.BaseDirectory;

        static void Main(string[] args)
        {
            if (!TryValidateAndParseArgs(args))
                return;
            Console.WriteLine("Generating csv data...");
            try
            {
                Generate(_dataFileName, _dataCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+ "\r\nPress Enter to exit.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"Generated csv data in {_dataFileName}...");
            Console.ReadLine();
        }

        private static void Generate(string dataFileName, int dataCount)
        {
            Random random = new Random();
            string text = "id;fullname;email;phone\r\n";
            using (TextWriter writer = new StreamWriter(dataFileName, false, System.Text.Encoding.UTF8))
            {
                writer.Write(text);
            }
            for (int i = 1; i <= dataCount; i++)
            {
                Console.WriteLine(i);
                text = $"{i};{random.Next()};{random.Next()};{random.Next()}";
                using (StreamWriter sw = File.AppendText(dataFileName))
                {
                    sw.WriteLine(text);
                }
            }
            
        }

        private static bool TryValidateAndParseArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                _dataFileName = Path.Combine(_dataFileDirectory, $"{args[0]}.csv");
            }
            else
            {
                Console.WriteLine("Data file name without extension is required");
                return false;
            }

            if (args.Length > 1)
            {
                if (!int.TryParse(args[1], out _dataCount))
                {
                    Console.WriteLine("Data must be integer");
                    return false;
                }
            }

            return true;
        }
    }
}
