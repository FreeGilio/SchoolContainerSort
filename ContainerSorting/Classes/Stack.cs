using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerSorting.CustomExceptions;

namespace ContainerSorting.Classes
{
    public class Stack
    {
        private const int MaxWeightAboveContainer = 120000; // 120 tons
        private readonly List<Container> containers;

        public IReadOnlyList<Container> Containers => containers;

        public Stack()
        {
            containers = new List<Container>();
        }

        public bool CanAddContainer(Container container)
        {
            if (container == null)
                return false; 

            if (container.IsValuable)
            {
                return !containers.Any(c => c.IsValuable);
            }

            if (container.IsCoolable && !IsFirstRowStack)
            {
                return false; 
            }

            if (WeightOnTop() + container.Weight > MaxWeightAboveContainer)
            {
                return false; 
            }

            return true; 
        }


        private bool IsFirstRowStack => containers.Count == 0 || containers.First().IsCoolable;


        private int WeightOnTop()
        {            
            return containers.Skip(1).Sum(c => c.Weight);
        }

        public void AddContainer(Container container)
        {
            if (!CanAddContainer(container))
                throw new StackingRulesException("Cannot place container due to stacking rules.");

            containers.Add(container);
        }

        public int TotalWeight => containers.Sum(c => c.Weight);
    }

}
