using ContainerSorting.Classes;
using ContainerSorting.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Test.Classes
{
    public class StackTests
    {
        [Fact]
        public void AddContainer_ValidContainer_ShouldSucceed()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(25000, ContainerType.Standard);

            // Act
            stack.AddContainer(container);

            // Assert
            Assert.Single(stack.Containers);
            Assert.Equal(container, stack.Containers.First());
            Assert.Equal(25000, stack.TotalWeight);
        }

        [Fact]
        public void AddContainer_ExceedingWeightLimit_ShouldThrowException()
        {
            // Arrange
            var stack = new Stack();
            var heavyContainer = new Container(60000, ContainerType.Standard); // 60 tons
            var anotherHeavyContainer = new Container(70000, ContainerType.Standard); // 70 tons

            stack.AddContainer(heavyContainer);

            // Act & Assert
            var exception = Assert.Throws<StackingRulesException>(() => stack.AddContainer(anotherHeavyContainer));
            Assert.Equal("Cannot place container due to stacking rules.", exception.Message);
        }

        [Fact]
        public void AddContainer_OnTopOfValuableContainer_ShouldThrowException()
        {
            // Arrange
            var stack = new Stack();
            var valuableContainer = new Container(15000, ContainerType.Valuable);
            var standardContainer = new Container(20000, ContainerType.Standard);

            stack.AddContainer(valuableContainer);

            // Act & Assert
            var exception = Assert.Throws<StackingRulesException>(() => stack.AddContainer(standardContainer));
            Assert.Equal("Cannot place container due to stacking rules.", exception.Message);
        }

        [Fact]
        public void CanAddContainer_ValidScenario_ShouldReturnTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(20000, ContainerType.Standard);

            // Act
            var result = stack.CanAddContainer(container);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanAddContainer_ExceedingWeight_ShouldReturnFalse()
        {
            // Arrange
            var stack = new Stack();
            var heavyContainer = new Container(80000, ContainerType.Standard); // 80 tons
            var lightContainer = new Container(50000, ContainerType.Standard); // 50 tons

            stack.AddContainer(heavyContainer);

            // Act
            var result = stack.CanAddContainer(lightContainer);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanAddContainer_OnTopOfValuableContainer_ShouldReturnFalse()
        {
            // Arrange
            var stack = new Stack();
            var valuableContainer = new Container(15000, ContainerType.Valuable);
            var standardContainer = new Container(20000, ContainerType.Standard);

            stack.AddContainer(valuableContainer);

            // Act
            var result = stack.CanAddContainer(standardContainer);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TotalWeight_WithMultipleContainers_ShouldReturnCorrectWeight()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(20000, ContainerType.Standard); // 20 tons
            var container2 = new Container(25000, ContainerType.Standard); // 25 tons
            var container3 = new Container(30000, ContainerType.Standard); // 30 tons

            stack.AddContainer(container1);
            stack.AddContainer(container2);
            stack.AddContainer(container3);

            // Act
            var totalWeight = stack.TotalWeight;

            // Assert
            Assert.Equal(75000, totalWeight); // 75 tons
        }

        [Fact]
        public void WeightOnTop_WithEmptyStack_ShouldReturnZero()
        {
            // Arrange
            var stack = new Stack();

            // Act
            var weightOnTop = stack.CanAddContainer(new Container(20000, ContainerType.Standard));

            // Assert
            Assert.True(weightOnTop);
        }

        [Fact]
        public void WeightOnTop_WithExistingContainers_ShouldReturnCorrectValue()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(20000, ContainerType.Standard); // 20 tons
            var container2 = new Container(30000, ContainerType.Standard); // 30 tons

            stack.AddContainer(container1);

            // Act
            var canAddContainer = stack.CanAddContainer(container2);

            // Assert
            Assert.True(canAddContainer);
        }
    }
}
