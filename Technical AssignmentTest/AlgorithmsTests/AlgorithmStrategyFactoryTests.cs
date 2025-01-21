using Application.Algorithms;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_AssignmentTest.AlgorithmsTests
{
    public class AlgorithmStrategyFactoryTests
    {
        [Fact]
        public void TestAlgorithmStrategyFactory()
        {
            // Arrange
            AlgorithmStrategyFactory algorithmStrategyFactory = new();
            // Act
            IAlgorithmStrategy algorithmStrategy = algorithmStrategyFactory.CreateAlgorithm(AlgorithmStrategy.BFSPathFinding);
            // Assert
            Assert.NotNull(algorithmStrategy);
        }

        [Fact]
        public void TestAlgorithmStrategyFactoryNotImplemented()
        {
            // Arrange
            AlgorithmStrategyFactory algorithmStrategyFactory = new();
            // Act
            var exception = Assert.Throws<AlgorithmStrategyException>(() => algorithmStrategyFactory.CreateAlgorithm(AlgorithmStrategy.None));
            AlgorithmStrategyException algorithmStrategyException = new(AlgorithmStrategy.None);

            // Assert
            Assert.Equal(algorithmStrategyException.Message, exception.Message);
            
        }
    }
}
