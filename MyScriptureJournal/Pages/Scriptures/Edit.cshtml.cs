﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class EditModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public EditModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Scripture Scripture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scripture = await _context.Scripture.FirstOrDefaultAsync(m => m.Id == id);
            if (scripture == null)
            {
                return NotFound();
            }
            Scripture = scripture;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Scripture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScriptureExists(Scripture.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScriptureExists(int id)
        {
            return _context.Scripture.Any(e => e.Id == id);
        }
    }
}
