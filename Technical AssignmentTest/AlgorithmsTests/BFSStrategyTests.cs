using Application.Algorithms;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_AssignmentTest.AlgorithmsTests
{
    public class BFSStrategyTests
    {
        [Fact]
        public void MazeNullTest()
        {
            // Arrange
            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act and Assert
            var exception = Assert.Throws<AlgorithmStrategyException>(() => bfsStrategy.Solve(null));
            Assert.Equal(AlgorithmStrategyException.MazeNullError, exception.Message);
        }

        
        [Fact]
        public void SimplePuzzleTest()
        {
            // Arrange
            // Arrange
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _XXXXXXXX_
                XXXXXXXXG_
                """;

            // Act
            Maze maze = new Maze(mazeString);

            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act
            var result = bfsStrategy.Solve(maze);
            // Assert
            string pathExpected = "(0, 0) -> (0, 1) -> (0, 2) -> (0, 3) -> (0, 4) -> (0, 5) -> (0, 6) -> (0, 7) -> (0, 8) -> (0, 9) -> (1, 9) -> (2, 9) -> (3, 9) -> (3, 8)";
            Assert.NotNull(result);
            Assert.Equal(pathExpected, result.ToString());
        }

        [Fact]
        public void NoSolutionPuzzleTest()
        {
            // Arrange
            // Arrange
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _XXXXXXXX_
                XXXXXXXXGX
                """;

            // Act
            Maze maze = new Maze(mazeString);

            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act
            var result = bfsStrategy.Solve(maze);
            // Assert
            string expected = MazeSolution.NO_SOLUTION_FOUND;
            Assert.NotNull(result);
            Assert.Equal(expected, result.ToString());
        }

        [Fact]
        public void NoSolutionNoStartPointPuzzleTest()
        {
            // Arrange
            // Arrange
            // Arrange
            string mazeString =
                """
                X_________
                _XXXXXXXX_
                _XXXXXXXX_
                XXXXXXXXGX
                """;

            // Act
            Maze maze = new Maze(mazeString);

            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act
            var result = bfsStrategy.Solve(maze);
            // Assert
            Assert.NotNull(result);
            string expected = MazeSolution.NO_SOLUTION_FOUND;
            Assert.Equal(expected, result.ToString());
            expected = Maze.NO_START_POINT_FOUND_ERROR;
            Assert.Equal(expected, result.ReasonsForNotSolving);
        }

        [Fact]
        public void NoSolutionNoEndPointPuzzleTest()
        {
            // Arrange
            // Arrange
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _XXXXXXXX_
                XXXXXXXXXX
                """;

            // Act
            Maze maze = new Maze(mazeString);

            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act
            var result = bfsStrategy.Solve(maze);
            // Assert
            Assert.NotNull(result);
            string expected = MazeSolution.NO_SOLUTION_FOUND;
            Assert.Equal(expected, result.ToString());
            expected = Maze.NO_END_POINT_FOUND_ERROR;
            Assert.NotNull(result);
            Assert.Equal(expected, result.ReasonsForNotSolving);
        }

        [Fact]
        public void NoSolutionNoStartOrEndPointPuzzleTest()
        {
            // Arrange
            // Arrange
            // Arrange
            string mazeString =
                """
                X_________
                _XXXXXXXX_
                _XXXXXXXX_
                XXXXXXXXXX
                """;

            // Act
            Maze maze = new Maze(mazeString);

            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act
            var result = bfsStrategy.Solve(maze);
            // Assert
            Assert.NotNull(result);
            string expected = MazeSolution.NO_SOLUTION_FOUND;
            Assert.Equal(expected, result.ToString());
            expected = Maze.NO_START_POINT_FOUND_ERROR + ";" + Maze.NO_END_POINT_FOUND_ERROR;
            Assert.NotNull(result);
            Assert.Equal(expected, result.ReasonsForNotSolving);
        }

        [Fact]
        public void NotSoSimplePuzzleTest()
        {
            // Arrange
            // Arrange
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _X______X_
                X_XXXX_XX_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXG_
                """;

            // Act
            Maze maze = new Maze(mazeString);

            IAlgorithmStrategy bfsStrategy = new BFS_Strategy();
            // Act
            var result = bfsStrategy.Solve(maze);
            // Assert
            string pathExpected = "(0, 0) -> (0, 1) -> (0, 2) -> (0, 3) -> (0, 4) -> (0, 5) -> (0, 6) -> (0, 7) -> (0, 8) -> (0, 9) -> (1, 9) -> (2, 9) -> (3, 9) -> (4, 9) -> (5, 9) -> (6, 9) -> (7, 9) -> (8, 9) -> (9, 9) -> (9, 8)";
            Assert.NotNull(result);
            Assert.Equal(pathExpected, result.ToString());

        }
    }
}
