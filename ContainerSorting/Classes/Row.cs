using ContainerSorting.CustomExceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Classes
{
    public class Row
    {
            public List<Stack> Stacks { get; }

            public Row(int width)
            {
                if (width <= 0)
                    throw new ArgumentException("Invalid row width.");

                Stacks = new List<Stack>();

                for (int i = 0; i < width; i++)
                {
                    Stacks.Add(new Stack());
                }
            }

            public int TotalWeight => Stacks.Sum(s => s.TotalWeight);

        public bool AddContainer(Container container)
        {
            foreach (var stack in Stacks)
            {
                if (stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    return true;
                }             
            }
            return false; 
        }
    } 
}
