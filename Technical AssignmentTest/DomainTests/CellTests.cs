using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_AssignmentTest.DomainTests
{
    public class CellTests
    {
        [Fact]
        public void EmptyCell()
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
                XXXXXXXXG_
                """;

            // Act
            Maze maze = new(mazeString);
            Cell emptyCell = new(maze, 0, 1);
            Cell startingCell = new(maze, 0, 0);
            Cell wallCell = new(maze, 1, 1);
            Cell goalCell = new(maze, 9, 8);
            // Assert
            Assert.True(emptyCell.IsEmptySpace);
            Assert.False(startingCell.IsEmptySpace);
            Assert.False(wallCell.IsEmptySpace);
            Assert.False(goalCell.IsEmptySpace);
        }

        [Fact]
        public void NeighborsCell()
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
                XXXXXXXXG_
                """;

            // Act
            Maze maze = new(mazeString);
            
            var neighborsEmptyCell = Cell.GetNeighbors(maze, 0, 1);
            var neighborsStartingCell = Cell.GetNeighbors(maze, 0, 0);
            var neighborsEmptyCell2 =  Cell.GetNeighbors(maze, 8, 9);
            var neighborsEmptyCell3 = Cell.GetNeighbors(maze, 9, 9);

            // Assert
            var expectedNeighborsEmptyCell = new List<Cell>() { new(maze, 0, 2) };
            var expectedNeighborsStartingCell = new List<Cell>() { new(maze, 1, 0), new(maze, 0, 1) };
            var expectedNeighborsStartingCell2 = new List<Cell>() { new(maze, 7, 9), new(maze, 9, 9), new(maze, 8, 8) };
            var expectedNeighborsStartingCell3 = new List<Cell>() { new(maze, 8, 9), new(maze, 9, 8)};

            Assert.Equal(expectedNeighborsEmptyCell, neighborsEmptyCell);
            Assert.Equal(expectedNeighborsStartingCell, neighborsStartingCell);
            Assert.Equal(expectedNeighborsStartingCell2, neighborsEmptyCell2);
            Assert.Equal(expectedNeighborsStartingCell3, neighborsEmptyCell3);
        }
    }
}
