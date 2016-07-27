// An array of numbers is given. You have to process queries. Each query must answer how many numbers in a interval [ L, R ) are < X.

using System;
using System.Linq;
using System.Collections.Generic;
using DataStructures.PersistentIndexedTree;

namespace Queries
{
	struct ValueIndex
	{
		public int Value { get; private set; }
		public int Index { get; private set; }

		public ValueIndex(int v, int i)
		{
			this.Value = v;
			this.Index = i;
		}
	}

	class StartUp
	{
		static void Main()
		{
			var array = Console.ReadLine()
				.Split(' ')
				.Select(int.Parse)
				.Select((x, i) => new ValueIndex(x, i))
				.OrderBy(x => x.Value)
				.ToArray();

			int n = array.Length;
			int levels = 1;
			while((1 << levels) < n)
			{
				++levels;
			}

			var roots = new PersistentIndexedTree<int>[n + 1];
			roots[0] = new PersistentIndexedTree<int>(levels, ((a, b) => a + b), 0);

			for(int i = 0; i < n; ++i)
			{
				roots[i + 1] = roots[i].Update(array[i].Index, 1);
			}

			string line = Console.ReadLine();
			while(line != null)
			{
				var sp = line.Split(' ');
				if(sp.Length != 3) continue;

				int l = int.Parse(sp[0]);
				int r = int.Parse(sp[1]);
				int x = int.Parse(sp[2]);

				Console.WriteLine(roots[FindTree(array, x)].Query(l, r));

				line = Console.ReadLine();
			}
		}

		static int FindTree(ValueIndex[] array, int x)
		{
			int l = 0;
			int r = array.Length;

			while(l < r)
			{
				int m = (l + r) / 2;
				if(array[m].Value < x)
				{
					l = m + 1;
				}
				else
				{
					r = m;
				}
			}

			return l; // avoiding off-by-one errors
		}
	}
}
