#ifndef PARTI_H
#include "SolverBase.h"
#include <vector>
#include <map>
using namespace std;

class PartI :
	public SolverBase
{
	vector<vector<int>> graph;
	vector<vector<int>> reverseGraph;
	vector<map<int, int>> coarseGraph;
	vector<vector<int>> scc;

	int getSz() { return graph.size(); }

	vector<int> time;
	int parent[1005] = { 0 };
	int deg[1005] = { 0 };
	int groupid[1005] = { 0 };
	bool isAyclic = true;
	bool vis[1005] = { 0 };
	vector<int> cycle;
	vector<int> node;

	void DFS(int);
	void ccDFS(int);
	void torpuDFS(int);

public:
	void read(std::string);
	void solve();
	void write(std::string);
};

#define PARTI_H
#endif