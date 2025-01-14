using ContainerSorting.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Test.Classes
{
    public class ShipTests
    {
        [Fact]
        public void CreateShip_ValidDimensions_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var ship = new Ship(3, 2);

            // Assert
            Assert.Equal(3, ship.Length);
            Assert.Equal(2, ship.Width);
            Assert.Equal(3, ship.rows.Count); // 3 rows
            foreach (var row in ship.rows)
            {
                Assert.Equal(2, row.Stacks.Count); // 2 stacks per row
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, 1)]
        public void CreateShip_InvalidDimensions_ShouldThrowException(int length, int width)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Ship(length, width));
        }

        [Fact]
        public void LoadContainer_RefrigeratedContainer_ShouldLoadInFrontRow()
        {
            // Arrange
            var ship = new Ship(3, 2);
            var refrigeratedContainer = new Container(20000, ContainerType.Coolable);

            // Act
            var result = ship.LoadContainer(refrigeratedContainer);

            // Assert
            Assert.True(result);
            var frontRow = ship.rows.First();
            Assert.Contains(refrigeratedContainer, frontRow.Stacks.First().Containers);
        }

        [Fact]
        public void LoadContainer_StandardContainer_ShouldBalancerows()
        {
            // Arrange
            var ship = new Ship(3, 2);
            var container1 = new Container(20000, ContainerType.Standard); // 20 tons
            var container2 = new Container(25000, ContainerType.Standard); // 25 tons

            // Act
            var result1 = ship.LoadContainer(container1);
            var result2 = ship.LoadContainer(container2);

            // Assert
            Assert.True(result1);
            Assert.True(result2);

            // Verify containers are added to the optimal rows
            Assert.NotEqual(ship.rows.First().TotalWeight, ship.rows.Last().TotalWeight);
        }


        [Fact]
        public void LoadContainer_ValuableContainer_ShouldRemainAccessible()
        {
            // Arrange
            var ship = new Ship(3, 2);
            var valuableContainer = new Container(20000, ContainerType.Valuable);

            // Act
            var result = ship.LoadContainer(valuableContainer);

            // Assert
            Assert.True(result);

            var stack = ship.rows.First().Stacks.First();
            Assert.Contains(valuableContainer, stack.Containers);

            // Valuable container should always be accessible
            Assert.Equal(valuableContainer, stack.Containers.Last());
        }

        [Fact]
        public void PrintShipLayout_ShouldProduceExpectedStructure()
        {
            // Arrange
            var ship = new Ship(2, 1); // 2 rows, 1 stack each
            var container1 = new Container(20000, ContainerType.Standard);
            var container2 = new Container(15000, ContainerType.Valuable);

            ship.LoadContainer(container1);
            ship.LoadContainer(container2);

            // Act
            ship.PrintShipLayout();
        }
    }
}
