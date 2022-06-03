#ifndef PARTII_H
#include <vector>
#include"SolverBase.h"
using std::pair;
using std::vector;

class PartII : public SolverBase
{
    vector<vector<pair<int, int>>> e;
    int disB[1010] = { 0 };
    int disD[1010] = { 0 };
    int getSz() { return e.size(); }

    bool hasNegLoop = false;
    
    int id[1010] = { 0 };
    vector<int> minHeap;
    int extract();
    void heapify();

    void Dijkstra();
    void BellmanFord();
    void DetectNegLoop();
public:
    void read(std::string);
    void solve();
    void write(std::string);
};

#define PARTII_H
#endif
