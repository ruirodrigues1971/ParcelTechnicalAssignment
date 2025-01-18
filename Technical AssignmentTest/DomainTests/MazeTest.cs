using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_AssignmentTest.DomainTests
{
    public class MazeTest
    {
        [Fact]
        public void CorrectMaze()
        {
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _X______X_
                X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXG_
                """;

            // Act
            Maze maze = new Maze(mazeString);

            // Assert
            Assert.NotNull(maze);
            // Add more assertions based on the properties and methods of the Maze class
        }

        [Fact]
        public void CorrectWindowsMaze()
        {
            const string NEW_LINE = Maze.WindowsNewLine;
            // Arrange
            string mazeString =
                "S_________" + NEW_LINE +
                "_XXXXXXXX_" + NEW_LINE +
                "_X______X_" + NEW_LINE +
                "X_XXXX_X_" + NEW_LINE +
                "_X_X__X_X_" + NEW_LINE +
                "_X_X__X_X_" + NEW_LINE +
                "_X_X____X_" + NEW_LINE +
                "_X_XXXXXX_" + NEW_LINE +
                "_X________" + NEW_LINE +
                "XXXXXXXXG_";

            // Act
            Maze maze = new Maze(mazeString);

            // Assert
            Assert.NotNull(maze);
            // Add more assertions based on the properties and methods of the Maze class
        }
        [Fact]
        public void CorrectLinuxMaze()
        {
            const string NEW_LINE = Maze.LinuxNewLine;
            // Arrange
            string mazeString =
                "S_________" + NEW_LINE +
                "_XXXXXXXX_" + NEW_LINE +
                "_X______X_" + NEW_LINE +
                "X_XXXX_X_" + NEW_LINE +
                "_X_X__X_X_" + NEW_LINE +
                "_X_X__X_X_" + NEW_LINE +
                "_X_X____X_" + NEW_LINE +
                "_X_XXXXXX_" + NEW_LINE +
                "_X________" + NEW_LINE +
                "XXXXXXXXG_";

            // Act
            Maze maze = new Maze(mazeString);

            // Assert
            Assert.NotNull(maze);
            // Add more assertions based on the properties and methods of the Maze class
        }
        [Fact]
        public void IncorrectCharMaze()
        {
            char c = 'T';
            // Arrange
            string mazeString =
                $"""
                {c}_________
                _XXXXXXXX_
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXX_
                """;
            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.Invalid_CHAR(c), exception.Message);
        }

        [Fact]
        public void NoStartingPointMaze()
        {
            // Arrange
            string mazeString =
                """
                X_________
                _XXXXXXXX_
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXG_
                """;
            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.NO_START_POINT_FOUND, exception.Message);
        }

        [Fact]
        public void ToMuchStartingPointsMaze()
        {
            // Arrange
            string mazeString =
                """
                SS________
                _XXXXXXXX_
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXX_
                """;
            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.MULTIPLE_STARTING_POINTS, exception.Message);
        }

        [Fact]
        public void ToManyEndPointsMaze()
        {
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXGG_
                """;
            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.MULTIPLE_ENDING_POINTS, exception.Message);
        }

        [Fact]
        public void NoEndPointMaze()
        {
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXX_
                """;
            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.NO_END_POINT_FOUND, exception.Message);
        }

        [Fact]
        public void EmptyMaze()
        {
            // Arrange
            string mazeString =
                "";
            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.EMPTY_STRING, exception.Message);
        }

        [Fact]
        public void ToBigMazeInRows()
        {
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX_
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXX_
                X_________
                _XXXXXXXX_
                _X______X_
                X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXX_                
                XXXXXXXXG_
                """;

            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.MAZE_TOO_LARGE(), exception.Message);
        }
        [Fact]
        public void ToBigMazeInColumns()
        {
            // Arrange
            string mazeString =
                """
                S_________
                _XXXXXXXX__XXXXXXXX_X
                _X______X_
                _X_XXXX_X_
                _X_X__X_X_
                _X_X__X_X_
                _X_X____X_
                _X_XXXXXX_
                _X________
                XXXXXXXXG_
                """;

            // Act and Assert
            var exception = Assert.Throws<MazeException>(() => new Maze(mazeString));
            Assert.Equal(Maze.MAZE_TOO_LARGE(), exception.Message);
        }
    }
}
