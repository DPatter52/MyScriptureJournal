using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;

namespace MyScriptureJournal.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MyScriptureJournalContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MyScriptureJournalContext>>()))
        {
            if (context == null || context.Scripture == null)
            {
                throw new ArgumentNullException("Null MyScriptureJournalContext");
            }


            if (context.Scripture.Any())
            {
                return;
            }

            context.Scripture.AddRange(
                new Scripture
                {
                    StandardWork = "Doctrine & Covenants",
                    Book = "",
                    Chapter = 12,
                    Verse = "5",
                    VerseText = "15 For behold, my brethren, it is given unto you to judge, that ye may know good from evil; and the way to judge is as plain, that ye may know with a perfect knowledge, as the daylight is from the dark night.",
                    Notes = "I love this scripture."

                },
                 new Scripture
                 {
                     StandardWork = "Book of Mormon",
                     Book = "Moroni",
                     Chapter = 2,
                     Verse = "5",
                     VerseText = "16 For behold, the Spirit of Christ is given to every man, that he may know good from evil; wherefore, I show unto you the way to judge; for every thing which inviteth to do good, and to persuade to believe in Christ, is sent forth by the power and gift of Christ; wherefore ye may know with a perfect knowledge it is of God.",
                     Notes = "Great Scripture."

                 }
            );
            context.SaveChanges();
        }
    }
}