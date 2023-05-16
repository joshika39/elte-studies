using LibraryCore.Book;
using LibraryCore.Book.Types;
using LibraryCore.Book.Types.Online;
using LibraryCore.Book.Types.Physical;
using LibraryCore.Lib;
using LibraryCore.People;
using Moq;

namespace LibraryCoreTests.Library;

public class LibraryTests
{
    #region Tests
        
    [Theory]
    [MemberData(nameof(LT_0001_MemberData))]
    public void LT_0001_Given_NullParameter_When_LendBookCalled_Then_ThrowsException(ILibraryBook book, DateTime date, ILibrary lib)
    {
        var ex = Record.Exception(() => lib.LendBook(book, date, new Guid()));
        
        Assert.IsType<ArgumentNullException>(ex);
    }
    #endregion
        
    #region MemeberData
    public static IEnumerable<object[]> LT_0001_MemberData()
    {
        var lib = LibGen.GetTestLibrary();
        yield return new object[]
        {
            null!,  
            DateTime.Today,
            lib
        };
        yield return new object[]
        {
            new ScienceBook(new PublishedBook("asd", "asd", 2, DateTime.Today,Mock.Of<IAuthor>()), Guid.NewGuid(), 100), 
            default(DateTime),
            lib
        };
    }
    #endregion
}