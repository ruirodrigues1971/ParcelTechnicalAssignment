using Domain;

namespace MazeSolverAPI.HelperServices
{
    /// <summary>
    /// Integrated solution class to be used in the API
    /// </summary>
    public class IntegratedSolution
    {
        /// <summary>
        /// The maze puzzle
        /// </summary>
        public string? Puzzle { get; set; }
        /// <summary>
        /// The maze solution
        /// </summary>
        public MazeSolution? Solution { get; set; }
    }
}
