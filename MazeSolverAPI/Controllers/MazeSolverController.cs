using Application.Algorithms;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MazeSolverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MazeSolverController : ControllerBase
    {
        private readonly ILogger<MazeSolverController> _logger;
        private readonly IAlgorithmStrategyFactory _algorithmStrategyFactory;

        public MazeSolverController(IAlgorithmStrategyFactory algorithmStrategyFactory, ILogger<MazeSolverController> logger)
        {
            _logger = logger;
            _algorithmStrategyFactory = algorithmStrategyFactory;
        }


        // POST api/<MazeSolverController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MazeSolution))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MazeSolution> Post([FromBody][Required] string mazeString, AlgorithmStrategy algorithmStrategyEnum = AlgorithmStrategy.BFSPathFinding)
        {
            try
            {
                Maze maze = new(mazeString);
                MazeSolution mazeSolution = new MazePathFinding(_algorithmStrategyFactory, maze).Solve(algorithmStrategyEnum);
                return Ok(mazeSolution);
            }
            catch (MazeException ex)
            {
                _logger.LogDebug(ex, "Bad Request");
                return BadRequest(ex.Message);
            }
            catch (AlgorithmStrategyException ex)
            {
                _logger.LogError(ex, "Error while solving maze");
                return Problem(detail:ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while solving maze");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }

        }
    }
}
