using Core;
using LibraryCore.Book.Factory;
using LibraryCore.Lib;

Bootstrapper.Initialize(out var reader);
var bookFact = new BookFactory();
ILibrary lib = new LibraryClass(bookFact, reader);

lib.LoadBooks(@"Resources\books.txt");
lib.LoadMembers(@"Resources\members.txt");
var joshua = lib.Login("joshika39")!;

joshua.Borrow(lib.AllBooks[0]);
joshua.Return(lib.AllBooks[0], new DateTime(2023, 06, 28));

joshua.Balance = 5000;

Console.WriteLine(joshua.PendingBills[0]);
joshua.Pay(1500);
Console.WriteLine(joshua.PendingBills[0]);


 