using System;
using System.Collections.Generic;

namespace test
{
    public class Het
	{
		public List<int> Napok { get; set; }
		public int MaxFok { get; set; }
		public int MinFok { get; set; }

		public Het(string sor)
		{
			Napok = new List<int>();
			var napok = sor.Split(' ');
			foreach(var n in napok)
			{
				Napok.Add(int.Parse(n));
			}

			MaxEsMinKeres();
		}

		public Het()
		{
			HetGenerator();

			MaxEsMinKeres();
		}

		public void MaxEsMinKeres()
		{
			foreach(var n in Napok)
			{
				if(n > MaxFok)
				{
					MaxFok = n;
				}
				if(n < MinFok)
				{
					MinFok = n;
				}
			}
		}

		public void HetGenerator()
		{
			Napok = new List<int>();
			var rand = new Random();
			for(var i = 0; i < 7; i++)
			{
				Napok.Add(rand.Next(-8, 20));
			}
		}

		public void Print()
		{
			foreach(var n in Napok)
			{
				Console.Write($"{n} ");
			}
			Console.WriteLine($"Max: {MaxFok}, Min: {MinFok}");
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Write("Hetek szama: ");
			var hetekSzama = int.Parse(Console.ReadLine());
			var hetek = new List<Het>();
			for(int i = 0; i < hetekSzama; i++)
			{
				hetek.Add(new Het());
			}

			var megvan = false;
			for(var i = 0; i < hetekSzama && !megvan; i++)
			{
				var hetI = hetek[i];
				for (var j = 0; j < hetekSzama && !megvan; j++)
				{
					var hetJ = hetek[j];
					if (hetI.MaxFok < hetJ.MinFok)
					{
						Console.WriteLine($"A {j + 1} het megfelel a megoldasnak");
						hetI.Print();
						hetJ.Print();
						megvan = true;
					}
				}
			}

		}
	}
}
