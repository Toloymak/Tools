using System;

namespace CertParser
{
    using System.IO;
    using System.Runtime.CompilerServices;

    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new RequestModel(args);

            var lines = File.ReadLines(args[0]);
            var counter = 0;

            Directory.CreateDirectory(parameters.OutPath);

            foreach (var line in lines)
            {
                File.WriteAllText($"{parameters.OutPath}\\cert{counter}.{parameters.FileFormat ?? "cer"}", line);
                counter++;
            }
            
            Console.WriteLine($"Записано {counter} новых файлов");
        }
    }

    public class RequestModel
    {
        public string Path { get; set; }
        public string OutPath { get; set; }
        public string FileFormat { get; set; }

        public RequestModel(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Не указан путь до обрабатываемого файла");
                PrintHelper.PrintHelp();
                throw new ArgumentException();
            }

            Path = args[0];

            if (args.Length > 1)
            {
                OutPath = args[1];
            }
            else
            {
                OutPath = "output";
            }
            
            if (args.Length > 2)
            {
                FileFormat = args[2];
            }
        }
    }

    public static class PrintHelper
    {
        public static void PrintHelp()
        {
            Console.WriteLine($"Пример параметров запуска:");
            Console.WriteLine($"CertParser.exe c:\\work\\test.txt");
            Console.WriteLine($"Можно добавить больше параметров:");
            Console.WriteLine("CertParser.exe {sourcePath} {outputFolder} {fileFormat}");
            Console.WriteLine("============================");
        }
    } 
}