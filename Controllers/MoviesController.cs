using MoviePlatform.Contracts.Movie;
using MoviePlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MoviePlatform.Controllers;

[ApiController]
[Route("api/me/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<MovieResponse>> CreateMovieForUser(
        [FromBody] MovieCreateRequest movieCreateRequest
    )
    {
        return await _movieService.CreateMovieForUser(movieCreateRequest);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<MovieResponse>> UpdateMovieForUser(
        int id,
        [FromBody] MovieUpdateRequest movieUpdateRequest
    )
    {
        return await _movieService.UpdateMovieForUser(id, movieUpdateRequest);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMoviesForUser()
    {
        return Ok(await _movieService.GetAllMoviesForUser());
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<MovieResponse>> GetMovieByIdForUser(int id)
    {
        return await _movieService.GetMovieByIdForUser(id);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMovieByIdForUser(int id)
    {
        bool isDeleted = await _movieService.DeleteMovieByIdForUser(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}