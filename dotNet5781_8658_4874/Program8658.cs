

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_5781_00_8658_4874
{
	partial class Program
	{
		static void Main(string[] args)
		{
			Welcome8658();
			Welcome4874();
			Console.ReadKey();
		}

		static partial void Welcome4874();
		private static void Welcome8658()
		{
			Console.WriteLine("Enter your name: ");
			string name = Console.ReadLine();
			Console.WriteLine("{0}, welcome to my first console application", name);
		}
	}
}
