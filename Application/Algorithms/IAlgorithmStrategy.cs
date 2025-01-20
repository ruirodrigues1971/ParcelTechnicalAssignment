using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Algorithms
{
    public interface IAlgorithmStrategy
    {
        /// <summary>
        /// Solve the maze
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        /// <exception cref="AlgorithmStrategyException"></exception>"
        MazeSolution Solve(Maze? maze);
    }
}
