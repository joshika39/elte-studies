using LibraryCore.Book;
using LibraryCore.Book.Factory;
using Moq;

namespace LibraryCoreTests.Book
{
    public class BookTests
    {
        private readonly IBookFactory _bookFactory;
        public BookTests()
        {
            _bookFactory = new BookFactory();
        }

        #region BookFactoryTests
        [Theory]
        [MemberData(nameof(BT_00001_MemberData))]
        public void BT_00001_Given_NullArgument_When_CreateGeneralBookCalled_Then_ThrowsException(IBook book, string bookId, string category)
        {
            var ex = Record.Exception(() =>
            {
                _bookFactory.CreateGeneralBook(book, Guid.NewGuid(), bookId, category);
            });

            Assert.IsType<ArgumentNullException>(ex);
        }
        
        [Theory]
        [MemberData(nameof(BT_00001_MemberData))]
        public void BT_00011_Given_NullArgument_When_CreatePhysicalBookCalled_Then_ThrowsException(IBook book, string bookId, string category)
        {
            var ex = Record.Exception(() =>
            {
                _bookFactory.CreateBook(book, Guid.NewGuid(), bookId,100, category);
            });

            Assert.IsType<ArgumentNullException>(ex);
        }
        
        [Theory]
        [MemberData(nameof(BT_00021_MemberData))]
        public void BT_00021_Given_NullArgument_When_CreateOnlineBookCalled_Then_ThrowsException(IBook book, string bookId, string format, string category)
        {
            var ex = Record.Exception(() =>
            {
                _bookFactory.CreateOnlineBook(book, Guid.NewGuid(), bookId,100, format, category);
            });

            Assert.IsType<ArgumentNullException>(ex);
        }
        #endregion



        #region MemeberData
        public static IEnumerable<object[]> BT_00001_MemberData()
        {
            yield return new object[] { null!, "asd", "asd" };
            yield return new object[] { Mock.Of<IBook>(), null!, "asd" };
            yield return new object[] { Mock.Of<IBook>(), "asd", null! };
        }
        
        public static IEnumerable<object[]> BT_00021_MemberData()
        {
            yield return new object[] { null!, "asd", "asd", "pdf" };
            yield return new object[] { Mock.Of<IBook>(), null!, "asd", "pdf" };
            yield return new object[] { Mock.Of<IBook>(), "asd", null!, "epub" };
            yield return new object[] { Mock.Of<IBook>(), "asd", "cat", null!};
        }
        #endregion
    }
}
