using ContainerSorting.Classes;
using ContainerSorting.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Test.Classes
{
    public class ContainerTests
    {
        [Fact]
        public void CreateStandardContainer_ValidWeight_ShouldSucceed()
        {
            // Arrange & Act
            var container = new Container(25000, ContainerType.Standard);

            // Assert
            Assert.Equal(25000, container.Weight);
            Assert.Equal(ContainerType.Standard, container.Type);
            Assert.False(container.IsValuable);
            Assert.False(container.IsCoolable);
        }

        [Fact]
        public void CreateRefrigeratedContainer_ValidWeight_ShouldSucceed()
        {
            // Arrange & Act
            var container = new Container(29000, ContainerType.Coolable);

            // Assert
            Assert.Equal(29000, container.Weight);
            Assert.Equal(ContainerType.Coolable, container.Type);
            Assert.True(container.IsCoolable);
            Assert.False(container.IsValuable);
        }

        [Fact]
        public void CreateValuableContainer_ValidWeight_ShouldSucceed()
        {
            // Arrange & Act
            var container = new Container(29000, ContainerType.Valuable);

            // Assert
            Assert.Equal(29000, container.Weight);
            Assert.Equal(ContainerType.Valuable, container.Type);
            Assert.True(container.IsValuable);
            Assert.False(container.IsCoolable);
        }

        [Theory]
        [InlineData(31000, ContainerType.Coolable)]
        [InlineData(35000, ContainerType.Valuable)]
        public void CreateSpecialContainer_ExceedsWeight_ShouldThrowException(int weight, ContainerType type)
        {
            // Act & Assert
            var exception = Assert.Throws<ContainerExceedsWeightLimitException>(() => new Container(weight, type));
            Assert.Equal("Weight cannot exceed 30 tons for valuable/Coolable containers.", exception.Message);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var container = new Container(24000, ContainerType.Standard);

            // Act
            var result = container.ToString();

            // Assert
            Assert.Equal("[Type: Standard, Weight: 24 tons]", result);
        }
    }
}
