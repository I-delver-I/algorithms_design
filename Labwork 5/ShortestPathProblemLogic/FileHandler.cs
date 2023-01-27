namespace ShortestPathProblemLogic
{
    public static class FileHandler
    {
        public static string OutputFileName { get; } = "iterations_data.txt";

        public static void ClearFile()
        {
            using (var output = new StreamWriter(OutputFileName, false))
            {
                output.Write(string.Empty);
            }
        }

        public static void WriteLineToFile(string text)
        {
            using (var output = new StreamWriter(OutputFileName, true))
            {
                output.WriteLine(text);
            }
        }
    }
}