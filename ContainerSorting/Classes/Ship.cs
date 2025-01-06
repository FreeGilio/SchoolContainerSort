using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Classes
{
    public class Ship
    {
        private const double BalanceThreshold = 0.2; // 20%
        public List<Row> rows;

        public int Length { get; }
        public int Width { get; }

        public Ship(int length, int width)
        {
            if (length <= 0 || width <= 0)
                throw new ArgumentException("Invalid ship dimensions.");

            Length = length;
            Width = width;
            rows = new List<Row>();

            for (int i = 0; i < Length; i++)
                rows.Add(new Row(Width));
        }

        public bool LoadContainer(Container container)
        {
            // If container is refrigerated, prioritize front row (first row)
            Row targetRow = container.IsRefrigerated ? rows.First() : GetOptimalRowForContainer();

            foreach (var stack in targetRow.Stacks)
            {
                if (stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    return true;
                }
            }

            return false; 
        }

        private Row GetOptimalRowForContainer()
        {
            // Find the row with the lightest weight to balance load
            return rows.OrderBy(r => r.TotalWeight).First();
        }

        public void PrintShipLayout()
        {
            for (int i = 0; i < rows.Count; i++)
            {
                Console.WriteLine($"\nRow {i + 1}:");
                for (int j = 0; j < rows[i].Stacks.Count; j++)
                {
                    Console.Write($"  Stack {j + 1}: ");
                    var containers = rows[i].Stacks[j].Containers;
                    Console.WriteLine(string.Join(", ", containers.Select(c => c.ToString())));
                }
            }
        }
    }

}
