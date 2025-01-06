using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Classes
{
    public class Row
    {
        public List<Stack> Stacks { get; }

        public Row(int stackCount)
        {
            Stacks = new List<Stack>();
            for (int i = 0; i < stackCount; i++)
                Stacks.Add(new Stack());
        }

        public int TotalWeight => Stacks.Sum(s => s.TotalWeight);
    }
}
