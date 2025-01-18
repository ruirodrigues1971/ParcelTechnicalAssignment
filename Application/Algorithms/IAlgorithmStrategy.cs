using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Algorithms
{
    public interface IAlgorithmStrategy
    {
        MazeSolution Solve(Maze maze);
    }
}
