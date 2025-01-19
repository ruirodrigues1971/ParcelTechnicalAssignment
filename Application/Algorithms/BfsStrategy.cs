// Ignore Spelling: Bfs

using Domain;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Algorithms
{
    /// <summary>
    /// Breadth-first search algorithm to solve the maze
    /// </summary>
    public class BfsStrategy: Strategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        /// <exception cref="AlgorithmStrategyException"></exception>"
        public override MazeSolution Solve(Maze maze)
        {
            ValidateMaze(maze);

            // this situation is already handled in the Maze class
            //if (NotPossibleToSolve(maze))
            //{
            //    return new MazeSolution(isSolved: false);
            //}

            var queue = new Queue<Cell>();
            var visited = new HashSet<Cell>();
            var previousCells = new Dictionary<Cell, Cell?>();

            queue.Enqueue(maze.StartingCell);
            visited.Add(maze.StartingCell);
            previousCells[maze.StartingCell] = null;

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();

                if (currentCell == maze.EndCell)
                {
                    return ConstructSolution(maze, previousCells);
                }

                foreach (var neighbor in Cell.GetNeighbors(maze, currentCell.X, currentCell.Y))
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        previousCells[neighbor] = currentCell;
                    }
                }
            }

            return new MazeSolution(isSolved: false);
        }

        /// <summary>
        /// Construct the solution to the maze
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="previousCells"></param>
        /// <returns></returns>
        private static MazeSolution ConstructSolution(Maze maze, Dictionary<Cell, Cell?> previousCells)
        {
            var path = new List<Cell>();
            for (var cell = maze.EndCell; cell != null; cell = previousCells[cell])
            {
                path.Add(cell);
            }
            path.Reverse();
            return new MazeSolution(path);
        }
    }
}
