using System.Globalization;
using BL;
using BL.Book;
using BL.Lib;
using BL.People;
using Implementation.Navigator;
using Implementation.Navigator.Factories;
using Infrastructure;
using Infrastructure.IO;
using Infrastructure.Navigator;
using Constants = Implementation.Constants;

namespace Client
{
    public class Manager
    {
        private readonly IMember? _loggedInUser;
        private readonly IWriter _writer;
        private readonly IReader _reader;
        private readonly ILibrary _lib;

        public Manager(IWriter writer, IReader reader, ILibrary lib)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _lib = lib ?? throw new ArgumentNullException(nameof(lib));

            _lib.LoadBooks(@"Resources\books.txt");
            _lib.LoadMembers(@"Resources\members.txt");

            writer.PrintSystemDetails("joshika39",
                "Joshua Hegedus",
                "YQMHWO",
                "Library",
                "This is a library management system created for a University assignment.");

            var uname = _reader.ReadLine<string>(Dummy, "Enter your username: ", "");
            _loggedInUser = _lib.Login(uname.Replace("\r\n", ""));
            writer.CompleteClear();
            if (_loggedInUser != null)
            {
                var mainNavElements = new List<INavigatorElement<string>>()
                {
                    new NavigatorElement<string>("Borrow Book", "", SelectBook),
                    new NavigatorElement<string>("Return Book", "", ReturnBook),
                    new NavigatorElement<string>($"{Constants.EscapeColors.GREEN}Add Balance{Constants.EscapeColors.RESET}", "", AddBalance),
                    new NavigatorElement<string>($"Pay Bill", "", PayBill),
                    new NavigatorElement<string>($"Show {Constants.EscapeColors.YELLOW}{_loggedInUser.UserName}{Constants.EscapeColors.RESET}'s Details", "", Details),
                    new NavigatorElement<string>($"{Constants.EscapeColors.RED}Log Out{Constants.EscapeColors.RESET}", "", Program.DisposeApp)
                };

                while (true)
                {
                    new NavigatorFactory().CreateNavigator(writer, mainNavElements).Show();
                    writer.HelperMessage(MessageSeverity.Success, "Press any key to continue...");
                    Console.ReadLine();
                    writer.CompleteClear();
                }
            }
        }

        private void SelectBook()
        {


            var books = new List<INavigatorElement<ILibraryBook>>();

            foreach (var book in _lib.AvailableBooks)
            {
                books.Add(new NavigatorElement<ILibraryBook>(book.Title, book));
            }

            var nav = new NavigatorFactory().CreateNavigator(_writer, books);

            var selectedBook = nav.Show();
            _loggedInUser!.Borrow(selectedBook);
        }

        private void ReturnBook()
        {
            ValidateUser();
            var books = new List<INavigatorElement<ILibraryBook>>();

            foreach (var book in _loggedInUser!.BorrowedBooks)
            {
                books.Add(new NavigatorElement<ILibraryBook>(book.Title, book));
            }

            var nav = new NavigatorFactory().CreateNavigator(_writer, books);

            var selectedBook = nav.Show();

            var date = _reader.ReadLine<DateTime>(DateTime.TryParse, "Enter the return date (yyyy-mm-dd): ",
                "Wrong date format");
            _loggedInUser.Return(selectedBook, date);
        }

        private void AddBalance()
        {
            ValidateUser();
            var amount = _reader.ReadLine<double>(double.TryParse, "Enter the amount that you want to upload: ",
                "Wrong double format!");
            _loggedInUser!.Balance += amount;
        }

        private void PayBill()
        {
            ValidateUser();

            var bills = new List<INavigatorElement<IBill>>();

            foreach (var bill in _loggedInUser?.PendingBills!)
            {
                bills.Add(new NavigatorElement<IBill>($"{bill.Amount.ToString(CultureInfo.CurrentCulture)}HUF", bill));
            }

            var nav = new NavigatorFactory().CreateNavigator(_writer, bills);

            var selectedBill = nav.Show();

            var amount = _reader.ReadLine<double>(double.TryParse,
                "Enter the amount that you want to pay (enter 0 for auto): ", "Wrong double format!");
            _loggedInUser.Pay(selectedBill, amount == 0 ? _loggedInUser.Balance : amount);
        }

        private void Details()
        {
            ValidateUser();
            _loggedInUser!.PrintDetails(_writer);
        }

        private bool Dummy(string line, out string res)
        {
            res = line;
            return true;
        }

        private void ValidateUser()
        {
            if (_loggedInUser == null)
            {
                _writer.WriteLine(MessageSeverity.Error, "No user is logged in");
                Program.DisposeApp();
            }
        }

        public void Sample()
        {
            IAuthor ks = new Author("Kovács Sándor", new DateTime(1652, 3, 24));
            IAuthor hj = new Author("Hegedus Joshua", new DateTime(2003, 1, 17));
            var diffEqDraft = ks.WriteBook("Differenciaegyenletek", 456);
            hj.Contribute(diffEqDraft);
            
            IPublisher typoTex = new Publisher();
            var diffEq = typoTex.Publish("978-963-4930-99-0", diffEqDraft, new DateTime(2020, 02, 12));
            _lib.GetNewBook(diffEq, BL.Constants.BookTypes.SCIENCE_BOOK);
        }

    }
}
