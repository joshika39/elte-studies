// See https://aka.ms/new-console-template for more information


using HF3;
var root = new Dir("/");
var file1 = new OwnFile("file1.txt", 3);
var file2 = new OwnFile("file2.txt", 1);
var file3 = new OwnFile("file3.txt", 6);
var gameDir = new Dir("Games");

var bin = new Dir("bin");
var data = new Dir("data");

bin.Content.Add(new OwnFile("game.exe", 12));
data.Content.Add(new OwnFile("game.data", 67));
gameDir.Content.Add(bin);
gameDir.Content.Add(data);

root.Content.Add(new OwnFile("123.asd", 4));
root.Content.Add(gameDir);
root.Content.Add(file1);
root.Content.Add(file2);
root.Content.Add(file3);

Console.WriteLine(root.GetSize());

root.PrintDir();