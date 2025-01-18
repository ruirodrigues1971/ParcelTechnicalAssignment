using Application.Algorithms;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_AssignmentTest.AlgorithmsTests
{
    public class DijkstraStrategyTests
    {
        [Fact]
        public void SimpleTest()
        {
            // Arrange
            // Arrange
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

            IAlgorithmStrategy dijkstraStrategy = new DijkstraStrategy();
            // Act
            var result = dijkstraStrategy.Solve(maze);
            // Assert
            Assert.NotNull(result);

        }
    }
}
