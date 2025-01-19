using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Algorithms
{
    /// <summary>
    /// Abstract class that implements the IAlgorithmStrategy interface and provides some common methods to the strategies
    /// </summary>
    public abstract class Strategy : IAlgorithmStrategy
    {
        public abstract MazeSolution Solve(Maze maze);
        /// <summary>
        /// If the maze is null, throw an exception
        /// </summary>
        /// <param name="maze"></param>
        /// <exception cref="AlgorithmStrategyException"></exception>
        protected static void ValidateMaze(Maze maze)
        {
            if (maze == null)
            {
                throw new AlgorithmStrategyException(AlgorithmStrategyException.MazeNullError);
            }
        }

        /// <summary>
        /// If the maze is not possible to solve, return false
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        protected static bool NotPossibleToSolve(Maze maze)
        {
            return maze.StartingCell == null || maze.EndCell == null;
        }
    }
}
