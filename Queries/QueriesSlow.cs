// An array of numbers is given. You have to process queries. Each query must answer how many numbers in a interval [ L, R ) are < X.

using System;
using System.Linq;

namespace Queries
{
	class StartUp
	{
		static void Main()
		{
			var array = Console.ReadLine()
				.Split(' ')
				.Select(int.Parse)
				.ToArray();

			string line = Console.ReadLine();
			while(line != null)
			{
				var sp = line.Split(' ');
				if(sp.Length != 3) continue;

				int l = int.Parse(sp[0]);
				int r = int.Parse(sp[1]);
				int x = int.Parse(sp[2]);

				int result = 0;		
				for(int i = l; i < r; ++i)
				{
					if(array[i] < x)
					{
						++result;
					}
				}

				Console.WriteLine(result);

				line = Console.ReadLine();
			}
		}
	}
}
