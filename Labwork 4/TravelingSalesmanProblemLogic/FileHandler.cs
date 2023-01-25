namespace TravelingSalesmanProblemLogic
{
    public static class FileHandler
    {
        public static string FileNameToWorkWith { get; set; } = "iterations_data.txt";

        public static void WriteLine(string text)
        {
            using (var output = new StreamWriter(FileNameToWorkWith, true))
            {
                output.WriteLine(text);
            }
        }
    }
}