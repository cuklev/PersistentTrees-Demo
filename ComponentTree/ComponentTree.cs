// http://codeforces.com/gym/100513/problem/C

using System;
using System.Collections.Generic;
using DataStructures.PersistentIndexedTree;

namespace ComponentTree
{
	class StartUp
	{
		static void Main()
		{
			int componentCount = int.Parse(Console.ReadLine());

			var compress = new Dictionary<string, int>();

			var adjList = new List<int>[componentCount + 1];
			var properties = new List<KeyValuePair<int, string>>[componentCount + 1];

			for(int i = 0; i <= componentCount; ++i)
			{
				adjList[i] = new List<int>();
				properties[i] = new List<KeyValuePair<int, string>>();
			}

			for(int i = 1; i <= componentCount; ++i)
			{
				var sp = Console.ReadLine().Split(' ');
				int parent = int.Parse(sp[0]);

				adjList[parent].Add(i);

				int propertyCount = int.Parse(sp[1]);
				for(int j = 0; j < propertyCount; ++j)
				{
					sp = Console.ReadLine().Split('=');
					var key = sp[0];
					var value = sp[1];
					if(!compress.ContainsKey(key))
					{
						compress.Add(key, compress.Count);
					}
					properties[i].Add(new KeyValuePair<int, string>(compress[key], value));
				}
			}

			int levels = 1;
			while((1 << levels) < compress.Count)
			{
				++levels;
			}

			var roots = new PersistentIndexedTree<string>[componentCount + 1];
			roots[0] = new PersistentIndexedTree<string>(levels, ((a, b) => null), null);

			dfs(roots, 0, adjList, properties);

			int queryCount = int.Parse(Console.ReadLine());
			for(int i = 0; i < queryCount; ++i)
			{
				var sp = Console.ReadLine().Split(' ');
				int node = int.Parse(sp[0]);
				string property = sp[1];

				if(compress.ContainsKey(property))
				{
					int id = compress[property];
					Console.WriteLine(roots[node].AtIndex(id) ?? "N/A");
				}
				else
				{
					Console.WriteLine("N/A");
				}
			}
		}

		static void dfs(PersistentIndexedTree<string>[] roots, int node, List<int>[] adjList, List<KeyValuePair<int, string>>[] properties)
		{
			foreach(var pair in properties[node])
			{
				roots[node] = roots[node].Update(pair.Key, pair.Value);
			}
			foreach(var to in adjList[node])
			{
				roots[to] = roots[node];
				dfs(roots, to, adjList, properties);
			}
		}
	}
}
