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
            {
                throw new ArgumentException("Invalid ship dimensions.");
            }         

            Length = length;
            Width = width;
            rows = new List<Row>();

            for (int i = 0; i < Length; i++)
            {
                rows.Add(new Row(Width));
            }
        }

        public bool LoadContainer(Container container)
        {
            if (rows == null || rows.Count == 0)
                throw new InvalidOperationException("Ship rows are not properly initialized.");

            if (container.IsCoolable)
            {
                return AddToRow(container, rows[0]);
            }

            if (container.IsValuable)
            {

                if (AddToRow(container, rows[0])) return true;

                if (AddToRow(container, rows[rows.Count - 1])) return true;

                throw new InvalidOperationException("Could not place valuable container in accessible rows.");

                return false;
            }

            Row targetRow = GetOptimalRowForContainer(container);
            if (targetRow == null)
            {
                throw new InvalidOperationException("No valid row to place the container.");
            }

            return AddToRow(container, targetRow);
        }


        private bool AddToRow(Container container, Row targetRow)
        {
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

        private Row GetOptimalRowForContainer(Container container)
        {
            var validRows = rows.Where(row => row.Stacks.Any(stack => stack.CanAddContainer(container)));
            if (!validRows.Any())
                throw new InvalidOperationException("No available rows to place the container.");

            return validRows.OrderBy(row => row.TotalWeight).First();
        }


        public void PrintShipLayout()
        {
            for (int i = 0; i < rows.Count; i++)
            {
                Console.WriteLine($"\nRow {i + 1}:");
                for (int j = 0; j < rows[i].Stacks.Count; j++)
                {
                    Console.Write($"  Stack {j + 1}: ");
                    var containers = rows[i].Stacks[j].Containers
                        .OrderBy(c => c.IsValuable ? 1 : 0) 
                        .ToList();

                    Console.WriteLine(string.Join(", ", containers.Select(c => c.ToString())));
                }
            }
        }

        public string SerializeToUrl()
        {
            var shipData = new StringBuilder();

            // Add ship dimensions
            shipData.Append($"?length={Length}&width={Width}");

            // Serialize container types
            shipData.Append("&stacks=");
            var rowTypeData = rows.Select(row =>
                string.Join(",", row.Stacks.Select(stack =>
                    string.Join("-", stack.Containers
                        .OrderBy(c => c.IsValuable ? 1 : 0) 
                        .Select(c =>
                            c.IsValuable && c.IsCoolable ? "4" :
                            c.IsValuable ? "2" :
                            c.IsCoolable ? "3" : "1"
                        )))));
            shipData.Append(string.Join("/", rowTypeData));

            // Serialize container weights
            shipData.Append("&weights=");
            var rowWeightData = rows.Select(row =>
                string.Join(",", row.Stacks.Select(stack =>
                    string.Join("-", stack.Containers
                        .OrderBy(c => c.IsValuable ? 1 : 0) 
                        .Select(c => c.Weight / 1000)) 
                )));
            shipData.Append(string.Join("/", rowWeightData));

            return shipData.ToString();
        }


    }

}
