namespace TravelingSalesmanProblemRunner
{
    public class DataCapturer
    {
        public static int CaptureInitialVerticesCount(int antsCount)
        {
            int initialVerticesCount = 200;
            bool verticesAreCaptured;

            do
            {
                verticesAreCaptured = true;
                System.Console.Write("Enter the initial count of vertices (default 200): ");

                try
                {
                    initialVerticesCount = Convert.ToInt32(Console.ReadLine());

                    if (initialVerticesCount < antsCount)
                    {
                        throw new ArgumentOutOfRangeException
                            (nameof(initialVerticesCount), "The initial vertices count can't be less than ants count");
                    }
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    verticesAreCaptured = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    verticesAreCaptured = false;
                }
            } while (!verticesAreCaptured);
            
            return initialVerticesCount;
        }

        public static int CaptureIterationsCount()
        {
            int iterationsCount = 1000;
            bool iterationsAreCaptured;

            do
            {
                iterationsAreCaptured = true;
                System.Console.Write("Enter the count of iterations (default 1000): ");

                try
                {
                    iterationsCount = Convert.ToInt32(Console.ReadLine());

                    if (iterationsCount < 20)
                    {
                        throw new ArgumentOutOfRangeException
                            (nameof(iterationsCount), "The iterations' count can't be less than 20");
                    }
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("You entered not a number");
                    iterationsAreCaptured = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    iterationsAreCaptured = false;
                }
            } while (!iterationsAreCaptured);
            
            return iterationsCount;
        }
    }
}