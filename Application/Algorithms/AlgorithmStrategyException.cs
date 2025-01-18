using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Algorithms
{
    public class AlgorithmStrategyException : Exception
    {
        public const string InvalidAlgorithmStrategyMessage = "Invalid algorithm strategy";

        public AlgorithmStrategyException(string message) : base(message)
        {
        }
    }
}
