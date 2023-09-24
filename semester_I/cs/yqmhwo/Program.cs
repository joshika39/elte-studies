// #define BIRO

using System;
using System.Collections.Generic;
using System.IO;
using Backend;

/*
  Készítette: Joshua Hegedus
  Neptun: YQMHWO
  E-mail: jhegedus9@gmail.com
  Feladat: Legváltozóbb települések
*/

// ReSharper disable All

namespace Backend
{
    public enum MessageSeverity
    {
        Success,
        Info,
        Warning,
        Error
    }
    #region Classes
    internal class StandardIOManager : IIOManager
    {
        
        public StandardIOManager()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public void Write(MessageSeverity severity, string msg)
        {
            #if BIRO
            Console.Write(msg);
            #else
            ConstructMsg(severity);
            Console.Write(msg);
            #endif
        }
        
        public void WriteLine(MessageSeverity severity, string msg)
        {
            #if BIRO
            Console.WriteLine(msg);
            #else
            ConstructMsg(severity);
            Console.WriteLine(msg);
            #endif
        }
        
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        
        public string ReadLine(string prompt)
        {
            #if BIRO
            return Console.ReadLine();
            #else
            var time = DateTime.Now.ToString("HH:mm:ss");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[  INPUT: {time}] {prompt}");
            var ans = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return ans;
            #endif
        }

        private void ConstructMsg(MessageSeverity severity)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            switch (severity)
            {
                case MessageSeverity.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[SUCCESS: {time}] ");
                    break;
                
                case MessageSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[   INFO: {time}] ");
                    break;
                
                case MessageSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[WARNING: {time}] ");
                    break;
                
                case MessageSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.Write($"[  ERROR: {time}] ");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.Write($"[  ERROR: {time}] Unknown Severity!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    throw new ArgumentException("Unknown Severity!");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    
    internal class GeneralDataReader : IDataReader
    {
        public void Details(IIOManager iioManager)
        {
            var startedTime = DateTime.Now.ToString("h:mm:ss");
            var host = Environment.MachineName;
            var user = Environment.UserName;
            var platform = Environment.OSVersion.Platform;
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" -------- HOST PC -------- ");
            Console.WriteLine($"[START AT]: {startedTime}");
            Console.WriteLine($"[    HOST]: {host}");
            Console.WriteLine($"[    USER]: {user}");
            Console.WriteLine($"[PLATFORM]: {platform}\n");
            Console.WriteLine(" -------- CREATOR -------- ");
            Console.WriteLine("[ MADE BY]: YQMHWO (Joshua Hegedus)");
            Console.WriteLine("[  GITHUB]: https://github.com/joshika39/\n");
            Console.WriteLine(" ------ EXECUTION ------ \n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public int GetSingleInput(int lowest, int highest, string prompt, IIOManager iioManager)
        {
            var num = 0;
            var isCorrect = false;
            while (!isCorrect)
            {
                var numStr = iioManager.ReadLine(prompt);
                
                isCorrect = int.TryParse(numStr, out num);
                if (!isCorrect)
                {
                    iioManager.WriteLine(MessageSeverity.Error, $"Incorrect format of values! ({numStr})");
                }
                else if (!(num >= lowest && num <= highest))
                {
                    iioManager.WriteLine(MessageSeverity.Error, $"Value is not in the range! ({num}, range: {lowest} -> {highest})");
                    isCorrect = false;
                }
            }

            return num;
        }

        public List<int> GetFromInput(string line)
        {
            var details = new List<int>();
            foreach (var text in line.Split(' ')) details.Add(int.Parse(text));
            return details;
        }

        public List<int> GetFromInput(int limit, int lowest, int highest, string inputPrompt, string counterPrompt, IIOManager iioManager)
        {
            var count = 0;
            var details = new List<int>();
            while (count != limit)
            {
                details.Add(GetSingleInput(lowest, highest, $"{inputPrompt}: {counterPrompt} {count + 1} = ", iioManager));
                count++;
            }
            Console.WriteLine("--------------\n");
            return details;
        }
    }

    internal class City : ICity
    {
        public City(int limit, int cityNum, IDataReader reader, IIOManager iioManager)
        {
            Num = cityNum;
            var measurements = reader.GetFromInput(limit, -50, 50, $"City {cityNum}", "Temperature", iioManager);
            MaxDifference = GetMaxNum(measurements);
        }

        public City(int cityNum, string line, IDataReader reader)
        {
            Num = cityNum;
            var measurements = reader.GetFromInput(line);
            MaxDifference = GetMaxNum(measurements);
        }

        public int MaxDifference { get; set; }
        public int Num { get; set; }

        private int GetMaxNum(IReadOnlyList<int> list)
        {
            var maxDiff = 0;
            for (var i = 0; i < list.Count; i++)
                if (i + 1 < list.Count)
                {
                    var diff = Math.Abs(list[i] - list[i + 1]);
                    if (diff > maxDiff) maxDiff = diff;
                }

            return maxDiff;
        }
    }

    internal class Cities : ICities
    {
        public Cities(int cityCount, int measureCount, IDataReader reader, IIOManager iioManager)
        {
            CityList = new List<ICity>();
            for (var i = 0; i < cityCount; i++)
            {
                #if BIRO
                var city = new City(i + 1, Console.ReadLine(), reader);
                #else
                var city = new City(measureCount, i + 1, reader, iioManager);
                #endif
                CityList.Add(city);
            }
        }

        public Cities(IDataReader reader, IReadOnlyList<string> lines)
        {
            CityList = new List<ICity>();
            for (var i = 1; i < lines.Count; i++)
            {
                var city = new City(i, lines[i], reader);
                CityList.Add(city);
            }
        }

        public List<ICity> CityList { get; }
        public int HighestDifference => GetHighestDifference();
        public int HighestCityCount => CountTheHighestCities();
        public string Result => GetResult();

        private int GetHighestDifference()
        {
            if (CityList == null) return -1;

            var highestDiff = 0;
            for (var i = 0; i < CityList.Count; i++)
                if (CityList[i].MaxDifference > highestDiff)
                    highestDiff = CityList[i].MaxDifference;
            return highestDiff;
        }

        private int CountTheHighestCities()
        {
            if (CityList == null) return -1;
            var highestCities = 0;
            foreach (var city in CityList)
                if (city.MaxDifference == HighestDifference)
                    highestCities++;
            return highestCities;
        }

        private string GetResult()
        {
            var res = $"{HighestCityCount}";
            for (var i = 0; i < CityList.Count; i++)
                if (i < CityList.Count && CityList[i].MaxDifference == HighestDifference)
                    res += $" {CityList[i].Num}";

            return res;
        }
    }

    #endregion

    #region Interfaces
    public interface IDataReader
    {
        void Details(IIOManager iioManager);
        int GetSingleInput(int lowest, int highest, string prompt, IIOManager iioManager);
        List<int> GetFromInput(string line);
        List<int> GetFromInput(int limit, int lowest, int highest, string inputPrompt, string counterPrompt, IIOManager iioManager);
    }

    public interface ICity
    {
        int MaxDifference { get; }
        int Num { get; }
    }

    public interface ICities
    {
        List<ICity> CityList { get; }
        int HighestDifference { get; }
        int HighestCityCount { get; }
        string Result { get; }
    }

    public interface IIOManager
    {
        void Write(MessageSeverity severity, string msg);
        void WriteLine(MessageSeverity severity, string msg);
        
        string ReadLine();
        string ReadLine(string prompt);
    }

    #endregion
}

namespace yqmhwo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var reader = new GeneralDataReader();
            var outputManager = new StandardIOManager();

            if (args.Length > 0)
            {
                reader.Details(outputManager);
                outputManager.WriteLine(MessageSeverity.Info, $"Reading from file: {args[0]}");
                var lines = File.ReadAllLines(args[0]);
                ICities cities = new Cities(reader, lines);
                outputManager.WriteLine(MessageSeverity.Success, cities.Result);
            }
            else
            {
                # if BIRO
                var details = reader.GetFromInput(Console.ReadLine());
                #else
                reader.Details(outputManager);
                var details = new List<int>
                {
                    reader.GetSingleInput(1, 1000, "Number of cities = ", outputManager),
                    reader.GetSingleInput(1, 1000, "Number of the temperatures in a city = ", outputManager)
                };
                Console.WriteLine("--------------\n");
                #endif
                
                ICities cities = new Cities(details[0], details[1], reader, outputManager);
                outputManager.WriteLine(MessageSeverity.Success, cities.Result);

            }

            # if !BIRO
            outputManager.WriteLine(MessageSeverity.Info, "Press any key to continue...");
            Console.ReadKey();
            #endif
        }
    }
}