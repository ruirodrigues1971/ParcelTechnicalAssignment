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
        public const string NO_SOLUTION_FOUND = "No solution found";
        public const string NO_PATH_FOUND = "No path found.";

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
        /// Some extra information when the maze is not solved.
        /// </summary>
        public string ReasonsForNotSolving { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for when the maze is solved.
        /// Initializes a new instance of the <see cref="MazeSolution"/> class.
        /// </summary>
        /// <param name="path">The path from the start to the goal.</param>
        /// <exception cref="ArgumentNullException">Thrown when the path is null.</exception>"
        public MazeSolution(List<Cell> path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Steps = Path.Count;
            IsSolved = true;
        }

        /// <summary>
        /// Constructor for when the maze is not solved.
        /// </summary>
        /// <param name="failureReason"></param>
        /// <param name="isSolved"></param>
        public MazeSolution(string failureReason)
        {
            this.IsSolved = false;
            this.ReasonsForNotSolving = failureReason;
        }

        /// <summary>
        /// Returns a string representation of the solution path.
        /// </summary>
        /// <returns>A string representing the solution path.</returns>
        public override string ToString()
        {
            if (!IsSolved)
            {
                return NO_SOLUTION_FOUND;
            }
            if (Path == null)
            {
                return NO_PATH_FOUND;
            }
            StringBuilder sb = new();
            foreach (var cell in Path)
            {
                sb.Append($"({cell.X}, {cell.Y}) -> ");
            }
            sb.Remove(sb.Length - 4, 4); // Remove the last arrow
            return sb.ToString();
        }
    }
}
