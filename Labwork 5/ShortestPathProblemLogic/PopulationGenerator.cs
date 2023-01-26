using System.Collections;

namespace ShortestPathProblemLogic
{
    public class PopulationGenerator : IEnumerable<Chromosome>
    {
        private GraphOfSites _graph;
        private List<Chromosome> _chromosomes = new List<Chromosome>();

        public int StartVertexNumber { get; }
        
        public int EndVertexNumber { get; }

        public PopulationGenerator
            (GraphOfSites graph, int startVertexNumber, int endVertexNumber)
        {
            _graph = graph;
            StartVertexNumber = startVertexNumber;
            EndVertexNumber = endVertexNumber;
        } 

        public bool ContainsChromosome(int[] verticesNumbers)
        {
            return _chromosomes
                .Exists(c => Enumerable.SequenceEqual(c.GetVerticesNumbers(), verticesNumbers));
        }
        
        public void GenerateChromosomes(int chromosomesCount)
        {
            for (var i = 0; i < chromosomesCount; i++)
            {
                AddChromosome();
            }
        }

        public void AddChromosome()
        {
            var chromosomeToAdd = new Chromosome();
            chromosomeToAdd.AddVertexNumber(StartVertexNumber);

            do
            {
                var allowedEdges = _graph
                    .GetEdgesWithSpecifiedVertexExcept(chromosomeToAdd.LastVertexNumber(), 
                    chromosomeToAdd.GetVerticesNumbers().ToArray());

                if (allowedEdges.Count == 0)
                {
                    chromosomeToAdd.RemoveVerticesNumbersExceptStart();
                    continue;
                }
                
                var random = new Random();
                var randomEdgeIndex = random.Next(allowedEdges.Count);
                var vertexNumberToAdd = (allowedEdges[randomEdgeIndex].FirstVertex.Number 
                    != chromosomeToAdd.LastVertexNumber()) 
                    ? allowedEdges[randomEdgeIndex].FirstVertex.Number
                    : allowedEdges[randomEdgeIndex].SecondVertex.Number;

                chromosomeToAdd.AddVertexNumber(vertexNumberToAdd);

                if (ContainsChromosome(chromosomeToAdd.GetVerticesNumbers().ToArray()))
                {
                    chromosomeToAdd.RemoveVerticesNumbersExceptStart();
                }
            } while (chromosomeToAdd.LastVertexNumber() != EndVertexNumber);

            _chromosomes.Add(chromosomeToAdd);
    }

        public IEnumerator<Chromosome> GetEnumerator()
        {
            foreach (Chromosome chromosome in _chromosomes)
            {
                yield return chromosome;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}