using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Book { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Note { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SaveDate { get; set; }


        public async Task OnGetAsync(string sortOrder)
        {
            Console.WriteLine($"Sort Order: {sortOrder}");

            if (string.IsNullOrEmpty(sortOrder) || sortOrder == "sort_reset")
            {
                sortOrder = "default";
            }

            ViewData["BookSortParam"] = sortOrder == "book_asc" ? "book_desc" : (sortOrder == "book_desc" ? "sort_reset" : "book_asc");
            ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : (sortOrder == "date_desc" ? "sort_reset" : "date_asc");

            ViewData["CurrentSort"] = sortOrder;

            var scriptures = from m in _context.Scripture select m;


            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s =>
                    s.StandardWork.Contains(SearchString) ||
                    s.Book.Contains(SearchString) ||
                    s.Notes.Contains(SearchString)
                );
            }


            switch (sortOrder)
            {
                case "book_asc":
                    scriptures = scriptures.OrderBy(s => s.Book);
                    break;
                case "book_desc":
                    scriptures = scriptures.OrderByDescending(s => s.Book);
                    break;
                case "date_asc":
                    scriptures = scriptures.OrderBy(s => s.SaveDate);
                    break;
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.SaveDate);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.Book); // Default sorting
                    break;
            }

            Scripture = await scriptures.ToListAsync();
        }

    }
}
