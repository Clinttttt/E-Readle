namespace Readle.Client.Services
{
    public class PageState
    {
        public enum PageView
        {
            bookshelve,
            ListMostPopular,
            ListAdventure,
            ListScience,
            ListClassics,
            ListRomance,
            ListMystery,
            ListYoungReaders,
            ListPoetry,
            ListHistory,
            ListShortStories
        }
        public PageView CurrentView { get; set; } = PageView.bookshelve;
        public event Action? Onchange;

        public void SetView(PageView view)
        {
            CurrentView = view;
            NotifyChanges();
        }
        public void NotifyChanges() => Onchange?.Invoke();
    }
}


/*<!--How they map to your original list:

Popular Picks → Everyone’s Talking About

Adventure & Exploration → Adventure Brave New Worlds

Classics & Timeless Reads → Classics Timeless Masterpieces

Romance & Relationships → Romance Passion & Everlasting Love

Science Fiction & Fantasy → Sci-Fi Future Visions + Children’s Magical Journeys (fantasy overlap)

Mystery, Thriller & Horror → Mystery, Horror

For Young Readers → Children’s Books Magical Journeys

Poetry & Drama → Poetry, Plays

History, Philosophy & Culture → History, Philosophy, Religion

Short Stories & Anthologies → Short Stories Small Tales Big Impact-->
*/