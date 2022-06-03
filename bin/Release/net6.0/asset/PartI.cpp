#include"PartI.h"
#include <iostream>
#include <fstream>
using namespace std;

void PartI::read(string file)
{
	ifstream fin;
	fin.open(file, ios::in);
	int n, m;
	fin >> n >> m;
	graph.resize(n);
	reverseGraph.resize(n);
	coarseGraph.resize(n);
	scc.resize(n);
	for (int i = 0; i < n; ++i)
		parent[i] = i;
	for (int i = 0; i < m; ++i)
	{
		int s, t, w; fin >> s >> t >> w;
		// w == 1
		graph[s].push_back(t);
		reverseGraph[t].push_back(s);
	}
	fin.close();
}

// O(n + m)
void PartI::solve()
{
	int sz = getSz();
	for (int i = 0; i < sz; ++i)
		if (!vis[i]) DFS(i);

	reverse(time.begin(), time.end());

	memset(vis, 0, sizeof(vis));
	// O(n + m)
	for (int i : time)
	{
		if (!vis[i])
		{
			cycle.clear();
			ccDFS(i);
			if (cycle.size() > 1)
				isAyclic = false;
			int mn = cycle[0];
			for (int mem : cycle)
				mn = min(mn, mem);
			for (int mem : cycle)
			{
				parent[mem] = mn;
				scc[mn].push_back(mem);
			}
		}
	}

	// O(n + m)
	for (int i = 0; i < sz; ++i)
		for (int to : graph[i])
			if (parent[i] != parent[to])
				++coarseGraph[parent[i]][parent[to]];

	if (isAyclic)
	{
		// find deg0 point
		for (int i = 0; i < sz; ++i)
			for (int e : graph[i])
				++deg[e];

		// O(n + m)
		memset(vis, 0, sizeof(vis));
		for (int i = 0; i < sz; ++i)
			if (deg[i] == 0)
				if (!vis[i])
					torpuDFS(i);
	}
	else
	{
		int id = 0;
		for (int i = 1; i < sz; ++i)
			groupid[i] = (i == parent[i]) ? ++id : groupid[parent[i]];
	}
}

void PartI::write(string file)
{
	ofstream fout;
	fout.open(file, ios::out);

	if (isAyclic)
	{
		for (int e : node)
			fout << e << ' '; fout << '\n';
	}
	else
	{
		vector<tuple<int, int, int>> ans;
		int node = 0, edge = 0;
		int sz = getSz();
		for (int i = 0; i < sz; ++i)
		{
			if (i == parent[i])
			{
				++node;
				for (pair<int, int> to : coarseGraph[i])
				{
					++edge;
					ans.emplace_back(groupid[i], groupid[to.first], to.second);
				}
			}
		}
		fout << node << ' ' << edge << '\n';
		for (tuple<int, int, int> e : ans)
		{
			int s, t, w;
			tie(s, t, w) = e;
			fout << s << ' ' << t << ' ' << w << '\n';
		}
	}

	fout.close();
}

void PartI::DFS(int cur)
{
	vis[cur] = true;
	for (int to : graph[cur])
		if (!vis[to])
			DFS(to);
	time.push_back(cur);
}

void PartI::ccDFS(int cur)
{
	cycle.push_back(cur);
	vis[cur] = true;
	for (int to : reverseGraph[cur])
		if (!vis[to])
			ccDFS(to);
}

void PartI::torpuDFS(int cur)
{
	vis[cur] = true;
	node.push_back(cur);
	for (int to : graph[cur])
	{
		--deg[to];
		if (!vis[to] && deg[to] == 0)
			torpuDFS(to);
	}
}
