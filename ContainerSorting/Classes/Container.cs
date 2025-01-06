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
            Refrigerated
        }

        public class Container
        {
            public int Weight { get; }
            public ContainerType Type { get; }
            public bool IsValuable => Type == ContainerType.Valuable;
            public bool IsRefrigerated => Type == ContainerType.Refrigerated;

            public Container(int weight, ContainerType type)
            {
                Weight = weight;
                Type = type;

                if (type != ContainerType.Standard && weight > 30000)
                    throw new ContainerExceedsWeightLimitException("Weight cannot exceed 30 tons for valuable/refrigerated containers.");
            }

            public override string ToString()
            {
                return $"[Type: {Type}, Weight: {Weight / 1000} tons]";
            }
        }
}
