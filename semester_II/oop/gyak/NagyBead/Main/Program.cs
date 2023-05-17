using System.Globalization;
using Implementation.IO.Factories;
using Implementation.Logger.Factories;
using Implementation.Navigator;
using Implementation.Navigator.Factories;
using Infrastructure;
using Infrastructure.Navigator;
using LibraryCore;
using LibraryCore.Book;
using LibraryCore.Book.Factory;
using LibraryCore.Lib;

var id = Guid.NewGuid();

using var logger = new LoggerFactory().CreateLogger(id);
var ioFactory = new IOFactory();
var writer = ioFactory.CreateWriter(logger);
var reader = ioFactory.CreateReader(logger, writer);

var bookFact = new BookFactory();
ILibrary lib = new LibraryClass(bookFact, reader, writer);

lib.LoadBooks(@"Resources\books.txt");
lib.LoadMembers(@"Resources\members.txt");

writer.PrintSystemDetails("joshika39",
    "Joshua Hegedus",
    "YQMHWO",
    "Library",
    "This is a library management system created for a University assignment.");

var uname = reader.ReadLine<string>(Dummy, "Enter your username (after submit finish with the END OF FILE character): ", "");
var loggedInUser = lib.Login(uname.Replace("\r\n", ""));


if (loggedInUser != null)
{
    var mainNavElements = new List<INavigatorElement<string>>()
    {
        new NavigatorElement<string>("Borrow Book", "", SelectBook),
        new NavigatorElement<string>("Return Book", "", ReturnBook),
        new NavigatorElement<string>("Add Balance", "", AddBalance),
        new NavigatorElement<string>("Pay Bill", "", PayBill),
        new NavigatorElement<string>($"Show {loggedInUser.UserName}'s Details", "", Details),
        new NavigatorElement<string>($"Log Out", "", () =>
        {
            logger.Dispose();
            Environment.Exit(0);
        })
    };

    while (true)
    {
        new NavigatorFactory().CreateNavigator(writer, mainNavElements).Show();
        writer.HelperMessage(MessageSeverity.Success, "Press any key to continue...");
        Console.ReadLine();
        writer.CompleteClear();
    }

    void SelectBook()
    {
        var books = new List<INavigatorElement<ILibraryBook>>();

        foreach (var book in lib.AvailableBooks)
        {
            books.Add(new NavigatorElement<ILibraryBook>(book.Title, book));
        }

        var nav = new NavigatorFactory().CreateNavigator(writer, books);

        var selectedBook = nav.Show();
        loggedInUser.Borrow(selectedBook);
    }

    void ReturnBook()
    {
        var books = new List<INavigatorElement<ILibraryBook>>();

        foreach (var book in loggedInUser.BorrowedBooks)
        {
            books.Add(new NavigatorElement<ILibraryBook>(book.Title, book));
        }

        var nav = new NavigatorFactory().CreateNavigator(writer, books);

        var selectedBook = nav.Show();

        var date = reader.ReadLine<DateTime>(DateTime.TryParse, "Enter the return date (yyyy-mm-dd): ",
            "Wrong date format");
        loggedInUser.Return(selectedBook, date);
    }

    void AddBalance()
    {
        var amount = reader.ReadLine<double>(double.TryParse, "Enter the amount that you want to upload: ",
            "Wrong double format!");
        loggedInUser.Balance += amount;
    }

    void PayBill()
    {
        var bills = new List<INavigatorElement<IBill>>();

        foreach (var bill in loggedInUser.PendingBills)
        {
            bills.Add(new NavigatorElement<IBill>($"bill.Amount.ToString(CultureInfo.CurrentCulture)HUF", bill));
        }

        var nav = new NavigatorFactory().CreateNavigator(writer, bills);

        var selectedBill = nav.Show();

        var amount = reader.ReadLine<double>(double.TryParse,
            "Enter the amount that you want to pay (enter 0 for auto): ", "Wrong double format!");
        loggedInUser.Pay(selectedBill, amount == 0 ? loggedInUser.Balance : amount);
    }

    void Details()
    {
        loggedInUser.PrintDetails(writer);
    }
}


bool Dummy(string line, out string res)
{
    res = line;
    return true;
}