using Domain;
using System.Diagnostics;
using System.Text;
using Application.Algorithms;

namespace Technical_AssignmentTest.AlgorithmsTests
{
    /// <summary>
    /// Testing if time constraints are meet for the BFS strategy
    /// </summary>
#pragma warning disable S101 // Types should be named in PascalCase
    public class BFSStrategyTimeConstraints
#pragma warning restore S101 // Types should be named in PascalCase
    {
        /// <summary>
        /// The objective of this test is to test the time constraints of the BFS strategy in the worst-case scenario
        /// </summary>
        [Fact]
        [Trait("Functional", "TimeConstraint")]
        public void BFSStrategyTimeConstraintsWorstCase1()
        {
            // Arrange
            string mazeString = GenerateWorstCaseMaze(Maze.MAX_DIMENSION, Maze.MAX_DIMENSION);
            Maze maze = new(mazeString);            

            // Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            MazeSolution solution = (new BFS_Strategy()).Solve(maze);
            stopwatch.Stop();

            // Assert
            Assert.True(stopwatch.ElapsedMilliseconds < 1000, "BFS strategy took too long to solve the maze.");
            Assert.True(solution.IsSolved, "BFS strategy failed to solve the maze.");
        }

        private static string GenerateWorstCaseMaze(uint width, uint height)
        {
            StringBuilder sb = new();
            sb.Append('S');
            for (int i = 1; i < width; i++)
            {
                sb.Append('_');
            }
            sb.Append('\n');

            for (int i = 1; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    sb.Append('_');
                }
                sb.Append('\n');
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("G\n");

            return sb.ToString();
        }
    }
}
