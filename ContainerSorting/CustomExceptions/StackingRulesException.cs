using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSorting.CustomExceptions
{
    public class StackingRulesException : Exception
    {
        public StackingRulesException(string message) : base(message)
        { 
        }
    }
}
