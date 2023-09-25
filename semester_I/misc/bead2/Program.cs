using System;
using System.Collections.Generic;

namespace Bead2
{
    internal class Pasdas
    {
        static void Main(string[] args)
        {
            var manuList = new List<string>();
            var developmentList = new List<string>();
            var manuNum = int.Parse(Console.ReadLine());
            for (int i = 0; i < manuNum; i++)
            {
                manuList.Add(Console.ReadLine());
            }
            var developmentNum = int.Parse(Console.ReadLine());
            for (int i = 0; i < developmentNum; i++)
            {
                developmentList.Add(Console.ReadLine());
            }
            Console.ReadLine();

            var correctManu = new List<string>();
            var count = 0;
            var hasSeven = false;
	    foreach (var dev in developmentList)
            {
                var data = dev.Split(' ');
                var intData = new int[] { int.Parse(data[0]), int.Parse(data[0]), int.Parse(data[0]) };
                if(data[1] == "7")
                {
                    correctManu.Add(manuList[intData[0] - 1]);
                    count++;
		    hasSeven = true;
                }
            }
	    if(!hasSeven)
	        Console.WriteLine("0 NINCS");
	    else
	    {

            	Console.Write($"{count} ");
           	for(int i = 0; i < correctManu.Count; i++)
            	{
			if(i == correctManu.Count-1)
                   		Console.Write($"{correctManu[i]}");
			else
		    		Console.Write($"{correctManu[i]} ");
            	}
	    	Console.WriteLine();
	    }

        }
    }
}
