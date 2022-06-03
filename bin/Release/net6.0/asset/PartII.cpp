#include "PartII.h"
#include <iostream>
#include <fstream>
using namespace std;

void PartII::read(std::string file)
{
	ifstream fin;
	fin.open(file, ios::in);
	int n, m;
	fin >> n >> m;
	e.resize(n);
	for (int i = 0; i < m; ++i)
	{
		int a, b, c;
		fin >> a >> b >> c;
		e[a].emplace_back(b, c);
	}
	fin.close();
	const int INF = 1e9;
	for (int i = 1; i < n; ++i)
		disB[i] = disD[i] = INF;
	minHeap.push_back(-1);
	for (int i = 0; i < n; ++i)
	{
		minHeap.push_back(i);
		id[i] = i + 1;
	}
}

int PartII::extract()
{
	int e = minHeap[1];
	int back = minHeap.back();
	minHeap[1] = back;
	id[back] = 1;
	minHeap.pop_back();
	heapify();
	return e;
}

void PartII::heapify()
{
	int sz = minHeap.size();
	for (int idx = 1; (idx << 1) < sz;)
	{
		int left = (idx << 1);
		int right = left + 1;
		if (disD[minHeap[left]] < disD[minHeap[idx]]) {
			if (right < sz && disD[minHeap[right]] < disD[minHeap[left]])
			{
				id[minHeap[idx]] ^= id[minHeap[right]]
					^= id[minHeap[idx]] ^= id[minHeap[right]];
				minHeap[idx] ^= minHeap[right]
					^= minHeap[idx] ^= minHeap[right];
				idx = right;
			}
			else
			{
				id[minHeap[idx]] ^= id[minHeap[left]]
					^= id[minHeap[idx]] ^= id[minHeap[left]];
				minHeap[idx] ^= minHeap[left]
					^= minHeap[idx] ^= minHeap[left];
				idx = left;
			}
		}
		else if (right < sz && disD[minHeap[right]] < disD[minHeap[idx]])
		{
			id[minHeap[idx]] ^= id[minHeap[right]]
				^= id[minHeap[idx]] ^= id[minHeap[right]];
			minHeap[idx] ^= minHeap[right]
				^= minHeap[idx] ^= minHeap[right];
			idx = right;
		}
		else
			return;
	}
}

void PartII::Dijkstra()
{
	while (minHeap.size() > 1)
	{
		int cur = extract();
		for (pair<int, int> to : e[cur])
		{
			if (disD[cur] + abs(to.second) < disD[to.first])
			{
				disD[to.first] = disD[cur] + abs(to.second);
				while (id[to.first] != 1)
				{
					int idx = id[to.first];
					int parentid = idx >> 1;
					if (disD[to.first] < disD[minHeap[parentid]])
					{
						id[to.first] ^= id[minHeap[parentid]]
							^= id[to.first] ^= id[minHeap[parentid]];
						minHeap[idx] ^= minHeap[parentid]
							^= minHeap[idx] ^= minHeap[parentid];
						idx = parentid;
					}
					else
						break;
				}
			}
		}
	}
}

void PartII::BellmanFord()
{
	int sz = getSz();
	for (int t = 0; t < sz - 1; ++t)
		for (int i = 0; i < sz; ++i)
			for (pair<int, int> to : e[i])
				if (disB[to.first] > disB[i] + to.second)
					disB[to.first] = disB[i] + to.second;
}

void PartII::DetectNegLoop()
{
	int sz = getSz();
	for (int i = 0; i < sz; ++i)
		for (pair<int, int> to : e[i])
			if (disB[to.first] > disB[i] + to.second)
				hasNegLoop = true;
}

void PartII::solve()
{
	BellmanFord();
	DetectNegLoop();
	if (!hasNegLoop)
		Dijkstra();
}

void PartII::write(std::string file)
{
	ofstream fout;
	fout.open(file, ios::out);
	if (hasNegLoop)
	{
		fout << "Negative loop detected!\n";
		fout << "Negative loop detected!\n";
	}
	else
	{
		fout << disD[getSz() - 1] << '\n';
		fout << disB[getSz() - 1] << '\n';
	}
	fout.close();
}