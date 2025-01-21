using Application.Algorithms;
using Application.Services;
using Domain;
using MazeSolverAPI.HelperServices;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MazeSolverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MazeSolverController : ControllerBase
    {
        private readonly CustomCacheManager _memoryCache;    
        private readonly ILogger<MazeSolverController> _logger;
        private readonly IAlgorithmStrategyFactory _algorithmStrategyFactory;

        public MazeSolverController(
            IAlgorithmStrategyFactory algorithmStrategyFactory,
            ILogger<MazeSolverController> logger,
            CustomCacheManager memoryCache)
        {
            _logger = logger;
            _algorithmStrategyFactory = algorithmStrategyFactory;
            _memoryCache = memoryCache;
        }


        // POST api/<MazeSolverController>
        /// <summary>
        /// Get a maze string and solve it    
        /// </summary>
        /// <param name="mazeString"></param>
        /// <param name="algorithmStrategyEnum"></param>
        /// <returns></returns>
        /// <remarks>
        /// **Sample request body:**    
        /// Example 1: (no path found)   
        ///     "S_________\n_XXXXXXXX_\n_X______X_\nX_XXXX_X_\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"   
        /// Example 2: (has path)    
        ///     "S_________\n_XXXXXXXX_\n_X______X_\nX_XXXX_X__\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"   
        /// </remarks>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MazeSolution))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult<MazeSolution> Post([FromBody][Required] string mazeString, AlgorithmStrategy algorithmStrategyEnum = AlgorithmStrategy.BFSPathFinding)
        {
            Maze? maze;
            // Validations
            if (!_algorithmStrategyFactory.IsValidAlgorithm(algorithmStrategyEnum))
            {
                return BadRequest("Invalid Algorithm");
            }
            try
            {
                maze = new(mazeString);
            }
            catch (MazeException ex)
            {
                _logger.LogDebug(ex, "Bad Request");
                return BadRequest(ex.Message);
            }
            //memory cache
            if (_memoryCache.TryGetValue(mazeString, out MazeSolution? cacheValue))
            {
                if (cacheValue != null)
                {
                    return Ok(cacheValue);
                }
            }
            //solving the problem
            try
            {
                MazeSolution mazeSolution = new MazePathFinding(_algorithmStrategyFactory, maze).Solve(algorithmStrategyEnum);
                _memoryCache.Set(mazeString, mazeSolution);// Save data in cache.
                return Ok(mazeSolution);
            }
            catch (AlgorithmStrategyException ex)
            {
                _logger.LogError(ex, "Error while solving maze");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while solving maze");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get previous solutions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IntegratedSolution>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [Route("GetPreviousSolutions")]
        public ActionResult<IEnumerable<IntegratedSolution>> Get()
        {
            try
            {
                var solutions = _memoryCache.GetSolutions();
                return Ok(solutions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while solving maze");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
