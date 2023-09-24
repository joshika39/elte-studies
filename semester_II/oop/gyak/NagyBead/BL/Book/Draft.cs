using BL.People;

namespace BL.Book
{
    public class Draft : IDraft
    {
        public string DraftTitle { get; }
        public int DraftPageCount { get; }
        public IList<IAuthor> Contributors { get; }

        public Draft(string draftTitle, int draftPageCount, IAuthor author)
        {
            DraftTitle = draftTitle;
            DraftPageCount = draftPageCount;
            Contributors = new List<IAuthor> { author };
        }
    }
}