using NUnit.Framework;
using System;
using PersistentIndexedTree;

namespace PersistentIndexedTrees.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void EmptyTree()
		{
			var tree = new PersistentIndexedTree<int> (10, ((a, b) => a + b), 0);
			int result = tree.Query (0, 1024);
			Assert.AreEqual (result, 0);
		}

		[TestCase(0, 19, 19)]
		[TestCase(1, 20, 19)]
		[TestCase(2, 21, 19)]
		[TestCase(3, 22, 19)]
		[TestCase(4, 23, 19)]
		[TestCase(5, 24, 19)]
		[TestCase(6, 25, 19)]
		[TestCase(7, 26, 19)]
		[TestCase(8, 27, 19)]
		[TestCase(9, 28, 19)]
		public void TreeWithOnes(int l, int r, int expected)
		{
			var tree = new PersistentIndexedTree<int> (10, ((a, b) => a + b), 1);
			int result = tree.Query (l, r);
			Assert.AreEqual (result, expected);
		}

		[TestCase(23, 83, (1L << 59), 1236)]
		[TestCase(47, 2312, (1L << 59) - 11, 9999)]
		[TestCase(43478, -341, (1L << 42), -9999)]
		[TestCase(23478, 239, (1L << 59) + 42, -1)]
		[TestCase(2132, -28800000, 2333, 2)]
		public void BigIntervalQuery(long ind1, int val1, long ind2, int val2)
		{
			var tree = new PersistentIndexedTree<int> (60, ((a, b) => a + b), 0);
			tree = tree
				.Update (ind1, val1)
				.Update (ind2, val2);
			int result = tree.Query (ind1, ind2 + 1);

			Assert.AreEqual (result, val1 + val2);
		}

		// TODO: write more unit tests
	}
}
