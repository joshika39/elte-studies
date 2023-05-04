// See https://aka.ms/new-console-template for more information

using Library;
using LibraryCore;
using LibraryCore.Book.Factory;
using LibraryCore.Lib;
using LibraryCore.People;
Console.WriteLine("Hello, World!");

ILibrary lib = new LibraryClass(new BookFactory());
IMember member = new Member("asd", ":asd", new DateTime(), lib);

IAuthor ks = new Author("Kovács Sándor", new DateTime(1652, 3, 24));
var diffEqDraft = ks.WriteBook("Differenciaegyenletek", 456);

IPublisher typoTex = new Publisher();
var diffEq = typoTex.Publish("978-963-4930-99-0", diffEqDraft, new DateTime(2020, 02, 12));
ks.Books.Add(diffEq);

lib.GetNewBook(diffEq, Constants.BookTypes.SCIENCE_BOOK);
var joshua = new Member("Joshua Hegedus", "joshika39", new DateTime(2003, 01, 17), lib);
lib.Register(joshua);

lib.LendBook(joshua ,diffEq, new DateTime(2023, 04, 05));
lib.ReturnBook(joshua, diffEq, new DateTime(2023, 05, 25));



 