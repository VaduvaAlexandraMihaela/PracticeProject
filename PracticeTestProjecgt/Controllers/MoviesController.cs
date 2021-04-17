using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticeTestProjecgt.Data;
using PracticeTestProjecgt.Models;

namespace PracticeTestProjecgt.Controllers
{
    public class MoviesController : Controller
    {
        private readonly McMovieContext _context;

        public MoviesController(McMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
          


            if(searchString != null)
            {
                pageNumber = 1; 
            }else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var movies = from mov in _context.Movie select mov; 

            if(!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(mov => mov.Title.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(mov => mov.Title);
                    break;
                case "Date":
                    movies = movies.OrderBy(mov => mov.ReleaseDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(mov => mov.ReleaseDate);
                    break;
                default:
                    movies = movies.OrderBy(mov => mov.Id);
                    break;
            }

            int pageSize = 3; 
            return View(await PaginatedList<Movie>.CreateAsync(movies.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Movies/Details/5
       
        
        public async Task<IActionResult> Details(int? id,string AuthorName, string ReviewDesc)
        {

           
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);

            var reviews = _context.Reviews.Where(p => p.movie == movie).ToList();

            if (AuthorName != null && ReviewDesc != null)
            {
                _context.Add(new Review
                {
                    UserName = AuthorName,
                    Description = ReviewDesc,
                    date = new DateTime(),
                    movie = movie
                });


                await _context.SaveChangesAsync();
            }

            if (movie == null)
            {
                return NotFound();
            }
            if (reviews == null)
            {
                return NotFound();
            }

            ViewModel mymodel = new ViewModel();
            mymodel.movie = movie;
            mymodel.reviews = (ICollection<Review>)reviews;

            return View(mymodel);
        }

        
       

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
