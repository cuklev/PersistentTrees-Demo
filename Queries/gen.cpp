#include<iostream>
#include<random>
#include<vector>

int main()
{
	const int n = 8;
	const int q = 5;

	std::mt19937_64 rng { std::random_device{}() };
	std::uniform_int_distribution<int> dist_number(0, 99);
	std::uniform_int_distribution<int> dist_index(0, n - 1);

	for(int i = 0; i < n; ++i)
	{
		if(i > 0) std::cout << ' ';
		std::cout << dist_number(rng);
	}
	std::cout << '\n';

	for(int i = 0; i < q; ++i)
	{
		int a = dist_index(rng), b;
		do b = dist_index(rng);
		while(a == b);
		if(a > b) std::swap(a, b);
		std::cout << a << ' ' << b << ' ' << dist_number(rng) << '\n';
	}
}
