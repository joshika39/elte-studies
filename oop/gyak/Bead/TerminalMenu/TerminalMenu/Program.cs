using Infrastructure.Impl;
using Infrastructure.Impl.StandardIOManager;
using Infrastrucutre;
using Infrastrucutre.IOManager;

namespace TerminalMenu
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            var id = Guid.NewGuid();
            var iOManager = new StandardIOManagerFactory().CreateIOManager(new Logger(id, "./log.txt"));

            iOManager.PrintSystemDetails("joshika39", "Joshua Hegedus", "YQMHWO");

            iOManager.WriteLine(MessageSeverity.Success, "Hello World");

            //string[] menuItems = { "Option 1", "Option 2", "Option 3" };
            //int selectedItem = 0;

            //ConsoleKeyInfo keyInfo;
            //int savedX = Console.CursorLeft;
            //int savedY = Console.CursorTop;


            //do
            //{
            //    Console.Clear();
            //    Console.WriteLine("Select an option:");
            //    for (int i = 0; i < menuItems.Length; i++)
            //    {
            //        if (i == selectedItem)
            //        {
            //            Console.Write(">> ");
            //        }
            //        else
            //        {
            //            Console.Write("   ");
            //        }

            //        Console.WriteLine(menuItems[i]);
            //    }

            //    keyInfo = Console.ReadKey();

            //    if (keyInfo.Key == ConsoleKey.UpArrow && selectedItem > 0)
            //    {
            //        selectedItem--;
            //    }
            //    else if (keyInfo.Key == ConsoleKey.DownArrow && selectedItem < menuItems.Length - 1)
            //    {
            //        selectedItem++;
            //    }

            //} while (keyInfo.Key != ConsoleKey.Enter);


            //Console.Clear();
            //Console.WriteLine("You selected: " + menuItems[selectedItem]);

            //Console.SetCursorPosition(savedX, savedY);

        }
    }
}