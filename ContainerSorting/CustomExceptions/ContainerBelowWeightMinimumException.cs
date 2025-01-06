using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.CustomExceptions
{
    public class ContainerBelowWeightMinimumException : Exception
    {
        public ContainerBelowWeightMinimumException(string message) : base(message)
        {
        }
    }
}
