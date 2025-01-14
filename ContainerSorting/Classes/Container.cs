using ContainerSorting.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.Classes
{
        public enum ContainerType
        {
            Standard,
            Valuable,
            Coolable
        }

        public class Container
        {
            public int Weight { get; }
            public ContainerType Type { get; }
            public bool IsValuable => Type == ContainerType.Valuable;
            public bool IsCoolable => Type == ContainerType.Coolable;
            public int MaxWeight = 30000;
            

            public Container(int weight, ContainerType type)
            {
                Weight = weight;
                Type = type;

                if (type != ContainerType.Standard && weight > MaxWeight)
                    throw new ContainerExceedsWeightLimitException("Weight cannot exceed 30 tons for valuable/Coolable containers.");
            }

            public override string ToString()
            {
                return $"[Type: {Type}, Weight: {Weight / 1000} tons]";
            }
        }
}
