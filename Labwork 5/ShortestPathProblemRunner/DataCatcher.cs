using ShortestPathProblemLogic;

namespace ShortestPathProblemRunner
{
    public static class DataCatcher
    {
        public static int CatchChromosomesCount(GraphOfSites graph)
        {
            int result = 0;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                System.Console.Write("Enter the chromosomes count: ");

                try
                {
                    result = Convert.ToInt32(Console.ReadLine());

                    if (result < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(result));
                    }
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    exceptionIsCaught = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    exceptionIsCaught = true;
                }
            } while (exceptionIsCaught);          

            return result;
        }

        public static PopulationGenerator CatchPopulationGenerator(GraphOfSites graph)
        {
            PopulationGenerator populationGenerator = null;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                int startVertexNumber = DataCatcher.CatchStartVertexNumber();
                int endVertexNumber = DataCatcher.CatchEndVertexNumber();

                try
                {
                    populationGenerator = new PopulationGenerator(graph, startVertexNumber, endVertexNumber);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    exceptionIsCaught = true;
                }
            } while (exceptionIsCaught);          

            return populationGenerator;
        }

        public static GraphOfSites CatchGraph()
        {
            GraphOfSites graph = null;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                int initialVerticesCount = DataCatcher.CatchInitialVerticesCount();

                try
                {
                    graph = new GraphOfSites(initialVerticesCount);
                }
                catch (InvalidOperationException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    exceptionIsCaught = true;
                }
            } while (exceptionIsCaught);

            return graph;
        }

        public static int CatchEndVertexNumber()
        {
            int result = 0;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                System.Console.Write("Enter end vertex number: ");

                try
                {
                    result = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    exceptionIsCaught = true;
                }
                
            } while (exceptionIsCaught);

            return result;
        }

        public static int CatchStartVertexNumber()
        {
            int result = 0;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                System.Console.Write("Enter start vertex number: ");

                try
                {
                    result = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    exceptionIsCaught = true;
                }
                
            } while (exceptionIsCaught);

            return result;
        }

        public static int CatchInitialVerticesCount()
        {
            int result = 0;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                System.Console.Write("Enter initial vertices' count (required 300): ");

                try
                {
                    result = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    exceptionIsCaught = true;
                }
                
            } while (exceptionIsCaught);

            return result;
        }

        public static int CatchIterationsCount()
        {
            int iterationsCount = 0;
            var exceptionIsCaught = false;

            do
            {
                exceptionIsCaught = false;
                System.Console.Write("Enter iterations count: ");

                try
                {
                    iterationsCount = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    exceptionIsCaught = true;
                }
                
            } while (exceptionIsCaught);

            return iterationsCount;
        }
    }
}