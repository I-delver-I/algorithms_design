using System.Diagnostics;

namespace Labwork_1
{
    public class Program
    {
        public static bool Debug = false;

        public static void Main(string[] args)
        {   
            var fileHandler = new FileHandler();
            const int sectionSize = 256;

            if (Debug)
            {
                var initialElementsCount = 128;

                if (!File.Exists(FileHandler.InitialFilePath) 
                    || ((FileHandler.InitialFileInfo.Length / sizeof(long)) != initialElementsCount))
                {
                    fileHandler
                        .CreateFileWithRandomNumbers(FileHandler.InitialFilePath, initialElementsCount);
                }

                Console.WriteLine("Initial file:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.InitialFilePath));
                Console.WriteLine();
            }
            else
            {
                var megabytesCount = 1024;

                if (!File.Exists(FileHandler.InitialFilePath) 
                    || (FileHandler.InitialFileInfo.Length != (megabytesCount * FileHandler.Mb)))
                {
                    fileHandler.CreateFileWithRandomNumbers(megabytesCount, FileHandler.InitialFilePath);
                }
            }

            var timer = new Stopwatch();
            timer.Start();
            var sortingHandler = new SortingHandler(fileHandler);

            sortingHandler.DistributeFileOnSeries(sectionSize);

            Console.WriteLine($"Distribution of the initial file took: {timer.Elapsed}");

            if (Debug)
            {
                Console.WriteLine("A:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.AfilePath));
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("B:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.BfilePath));
                Console.WriteLine(Environment.NewLine);
            }

            sortingHandler.PolyPhaseMerge(sectionSize);

            timer.Stop();
            Console.WriteLine($"Sorting took: {timer.Elapsed}");

            if (Debug)
            {
                Console.WriteLine("C:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.CfilePath));
                Console.WriteLine(Environment.NewLine);
    
                Console.WriteLine("D:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.DfilePath));
                Console.WriteLine(Environment.NewLine);
    
                Console.WriteLine("A:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.AfilePath));
                Console.WriteLine(Environment.NewLine);
    
                Console.WriteLine("B:");
                OutputHandler.PrintEnumerable(fileHandler.GetFileContent(FileHandler.BfilePath));
                Console.WriteLine();
            }
        }
    }
}