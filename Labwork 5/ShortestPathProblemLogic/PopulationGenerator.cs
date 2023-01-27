using System.Collections;

namespace ShortestPathProblemLogic
{
    public class PopulationGenerator : IEnumerable<Chromosome>
    {
        private GraphOfSites _graph;
        private List<Chromosome> _chromosomes = new List<Chromosome>();

        public int StartVertexNumber { get; }
        
        public int EndVertexNumber { get; }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public PopulationGenerator
            (GraphOfSites graph, int startVertexNumber, int endVertexNumber)
        {
            _graph = graph;

            if ((startVertexNumber < 1) || (startVertexNumber >= endVertexNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(startVertexNumber));
            }

            if (endVertexNumber > graph.GetVerticesCount())
            {
                throw new ArgumentOutOfRangeException(nameof(endVertexNumber));
            }

            StartVertexNumber = startVertexNumber;
            EndVertexNumber = endVertexNumber;
        } 

        public void AddChromosome(Chromosome chromosome)
        {
            _chromosomes.Add(chromosome);
        }

        public void RemoveChromosome(Chromosome chromosome)
        {
            if (chromosome is null)
            {
                throw new ArgumentNullException(nameof(chromosome));
            }

            if (ContainsChromosome(chromosome.GetVerticesNumbers()))
            {
                
            }

            _chromosomes.Remove(chromosome);
        }
        
        public bool ContainsChromosome(List<int> verticesNumbers)
        {
            return _chromosomes
                .Exists(c => Enumerable.SequenceEqual(c.GetVerticesNumbers(), verticesNumbers));
        }
        
        public void GenerateChromosomes(int chromosomesCount)
        {
            for (var i = 0; i < chromosomesCount; i++)
            {
                CreateChromosome();
            }
        }

        public void CreateChromosome()
        {
            var chromosomeToAdd = new Chromosome();
            chromosomeToAdd.AddVertexNumber(StartVertexNumber);

            do
            {
                var allowedEdges = _graph
                    .GetEdgesWithSpecifiedVertexExcept(chromosomeToAdd.LastVertexNumber(), 
                    chromosomeToAdd.GetVerticesNumbers());

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

                if (ContainsChromosome(chromosomeToAdd.GetVerticesNumbers()))
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