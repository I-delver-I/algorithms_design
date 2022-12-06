using System.Collections;

namespace Labwork_1
{
    public class OutputHandler
    {
        public static void PrintEnumerable(IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                Console.Write($"{item} ");
            }
        }
    }
}