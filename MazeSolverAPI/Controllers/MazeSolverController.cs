using Application.Algorithms;
using Application.Services;
using Domain;
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
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MazeSolverController> _logger;
        private readonly IAlgorithmStrategyFactory _algorithmStrategyFactory;

        public MazeSolverController(
            IAlgorithmStrategyFactory algorithmStrategyFactory,
            ILogger<MazeSolverController> logger,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _algorithmStrategyFactory = algorithmStrategyFactory;
            _memoryCache = memoryCache;
        }


        // POST api/<MazeSolverController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MazeSolution))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(string))]
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
                return Ok(cacheValue);
            }
            //solving the problem
            try
            {
                MazeSolution mazeSolution = new MazePathFinding(_algorithmStrategyFactory, maze).Solve(algorithmStrategyEnum);
                 _memoryCache.Set(mazeString, cacheValue);// Save data in cache.
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
    }
}
