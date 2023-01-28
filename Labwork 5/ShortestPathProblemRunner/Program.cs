using ShortestPathProblemLogic;
using ShortestPathProblemLogic.Crossover;
using ShortestPathProblemLogic.Mutation;
using ShortestPathProblemLogic.LocalImprovement;

namespace ShortestPathProblemRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileHandler.ClearFile();

            System.Console.WriteLine("Operators of the first solving case:\n"
                + " - single point crossover\n"
                + " - insertion mutation\n"
                + " - vertex replacement local improvement\n");

            var firstCaseGraph = DataCatcher.CatchGraph();
            var firstCasePopulationGenerator = DataCatcher.CatchPopulationGenerator(firstCaseGraph);
            ICrossoverable firstCaseCrossover = 
                new SinglePointCrossover(firstCasePopulationGenerator, firstCaseGraph);
            IMutationMakable mutationMaker = new InsertionMutationMaker(firstCaseGraph);

            SolvingCases.UseSolvingCase(firstCaseGraph, firstCasePopulationGenerator, firstCaseCrossover,
                mutationMaker);

            PrintHyphenLine();
            FileHandler.WriteLineToFile($"{new string('-', 80)}\n");

            System.Console.WriteLine("Operators of the second solving case:\n"
                + " - uniform crossover\n"
                + " - insertion mutation\n"
                + " - vertex replacement local improvement\n");

            var secondCaseGraph = DataCatcher.CatchGraph();
            var secondCasePopulationGenerator = DataCatcher.CatchPopulationGenerator(secondCaseGraph);
            ICrossoverable secondCaseCrossover = new UniformCrossover(secondCasePopulationGenerator, secondCaseGraph);
            IMutationMakable secondCaseMutationMaker = new InsertionMutationMaker(secondCaseGraph);

            SolvingCases.UseSolvingCase(secondCaseGraph, secondCasePopulationGenerator, secondCaseCrossover, 
                secondCaseMutationMaker);
        }

        public static void PrintHyphenLine()
        {
            System.Console.WriteLine(new string('-', 70));
        }
    }
}