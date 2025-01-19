using Application.Algorithms;
using Domain;

namespace Application.Services
{
    public class MazePathFinding : IPathFinding
    {
        /// <summary>
        /// maze to be solved
        /// </summary>
        private readonly Maze _Maze;
        private readonly IAlgorithmStrategyFactory _algorithmStrategyFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze"></param>        
        /// <param name="validateMaze">If want to validate maze. If previous validate is not needed to validate again</param>
        public MazePathFinding(IAlgorithmStrategyFactory algorithmStrategyFactory, Maze maze) {
            ArgumentNullException.ThrowIfNull(maze);
            ArgumentNullException.ThrowIfNull(algorithmStrategyFactory);
            _algorithmStrategyFactory = algorithmStrategyFactory;
            _Maze = maze;
        }

        public MazeSolution Solve(AlgorithmStrategy algorithm = AlgorithmStrategy.BFSPathFinding)
        {
            var strategy = _algorithmStrategyFactory.CreateAlgorithm(algorithm);
            return strategy.Solve(_Maze);
        }
    }
}
