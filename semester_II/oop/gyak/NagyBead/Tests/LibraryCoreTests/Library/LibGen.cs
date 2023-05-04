using Library;
using LibraryCore;
using LibraryCore.Book.Factory;
using LibraryCore.Lib;
using LibraryCore.People;

namespace LibraryCoreTests.Library;

public static class LibGen
{
    public static ILibrary GetTestLibrary()
    {
        return new LibraryClass(new BookFactory());
    }

    public static ILibrary PopulateLibraryMembers(this ILibrary library)
    {
        IAuthor ks = new Author("Kovács Sándor", new DateTime(1652, 3, 24));
        var diffEqDraft = ks.WriteBook("Differenciaegyenletek", 456);

        IPublisher typoTex = new Publisher();
        var diffEq = typoTex.Publish("978-963-4930-99-0", diffEqDraft, new DateTime(2020, 02, 12));
        ks.Books.Add(diffEq);

        library.GetNewBook(diffEq, Constants.BookTypes.SCIENCE_BOOK);

        library.Register(new Member("Joshua Hegedus", "joshika39", new DateTime(2003, 01, 17), library));
        return library;
    }
    
    public static ILibrary PopulateLibraryBooks(this ILibrary library)
    {
        IAuthor ks = new Author("Kovács Sándor", new DateTime(1652, 3, 24));
        
        IPublisher typoTex = new Publisher();
        var diffEq = typoTex.Publish(
            "978-963-4930-99-0",
            ks.WriteBook("Differenciaegyenletek", 456),
            new DateTime(2020, 02, 12));

        var book1 = typoTex.Publish(ks.WriteBook("TestBook1", 123));
        var book2 = typoTex.Publish(ks.WriteBook("TestBook2", 234));
        var book3 = typoTex.Publish(ks.WriteBook("TestBook3", 345));
        library.GetNewBook(diffEq, Constants.BookTypes.SCIENCE_BOOK);
        library.GetNewBook(diffEq, Constants.BookTypes.SCIENCE_BOOK);
        library.GetNewBook(diffEq, Constants.BookTypes.SCIENCE_BOOK);
        library.GetNewBook(book1, Constants.BookTypes.SCIENCE_BOOK);
        library.GetNewBook(book2, Constants.BookTypes.SCIENCE_BOOK);
        library.GetNewBook(book3, Constants.BookTypes.SCIENCE_BOOK);

        return library;
    }
}