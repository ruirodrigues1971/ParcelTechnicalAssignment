using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Algorithms
{
    public class AlgorithmStrategyFactory : IAlgorithmStrategyFactory
    {

        public IAlgorithmStrategy CreateAlgorithm(AlgorithmStrategy algorithmStrategyEnum)
        {
            switch (algorithmStrategyEnum)
            {
                case AlgorithmStrategy.BFSPathFinding:
                    return new BFS_Strategy();
                case AlgorithmStrategy.None:
                default:
                    throw new AlgorithmStrategyException(algorithmStrategyEnum);
            }
        }
    }
}
