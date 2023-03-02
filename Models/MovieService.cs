using MvcMovie.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public class MovieService
{
    //osäker på asyncen nu när jag flyttat runt koden
    private readonly MvcMovieContext _context;

    public MovieService(MvcMovieContext context)
    {
        _context = context;
    }

    public async Task<MovieGenreViewModel> GetMovies(string movieGenre, string searchString)
    {
        IQueryable<string> genreQuery = from m in _context.Movie orderby m.Genre select m.Genre;
        var movies = from m in _context.Movie select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title!.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(genreQuery.Distinct().ToList()),
            Movies = movies.ToList()
        };
        return movieGenreVM;
    }

    public async Task<Movie> GetMovieById(int? id)
    {
        if (id == null || _context.Movie == null)
        {
            return null;
        }
        var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return null;
        }
        return movie;
    }

    public async Task<Movie> CreateMovie(Movie movie)
    {
        _context.Movie.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateMovie(Movie movie)
    {
        _context.Movie.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteMovie(int? id)
    {
        try
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    internal bool MovieExists(int id)
    {
        var movie = _context.Movie.Find(id);
        return movie == null ? false : true;
    }
}
