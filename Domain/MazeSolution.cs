using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Represents the solution to a maze.
    /// </summary>
    public class MazeSolution
    {
        /// <summary>
        /// The path from the start to the goal.
        /// </summary>
        public List<Cell>? Path { get; init; }

        /// <summary>
        /// Indicates whether a solution was found.
        /// </summary>
        public bool IsSolved { get; init; }

        /// <summary>
        /// The number of steps in the solution path.
        /// </summary>
        public int Steps { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSolution"/> class.
        /// </summary>
        /// <param name="path">The path from the start to the goal.</param>
        public MazeSolution(List<Cell> path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Steps = Path.Count;
            IsSolved = true;
        }
        public MazeSolution(bool isSolved = false)
        {
            IsSolved = isSolved;
        }

        /// <summary>
        /// Returns a string representation of the solution path.
        /// </summary>
        /// <returns>A string representing the solution path.</returns>
        public override string ToString()
        {
            if (!IsSolved)
            {
                return "No solution found.";
            }
            if(Path == null)
            {
                return "No path found.";
            }
            StringBuilder sb = new StringBuilder();
            foreach (var cell in Path)
            {
                sb.Append($"({cell.X}, {cell.Y}) -> ");
            }
            sb.Remove(sb.Length - 4, 4); // Remove the last arrow
            return sb.ToString();
        }
    }
}
