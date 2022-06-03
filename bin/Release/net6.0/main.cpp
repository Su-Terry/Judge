#include <iostream>
#include <string>
#include <cassert>
#include "./proc/PartI.h"
#include "./proc/PartII.h"
using namespace std;

int main(int argc, char* argv[])
{
    assert(argc == 5);
    string fin1 = argv[1];
    string fin2 = argv[2];
    string fout1 = argv[3];
    string fout2 = argv[4];

    SolverBase* solver_1 = new PartI();
    solver_1->read(fin1);
    solver_1->solve();
    solver_1->write(fout1);
    delete solver_1;

    SolverBase* solver_2 = new PartII();
    solver_2->read(fin2);
    solver_2->solve();
    solver_2->write(fout2);
    delete solver_2;
    return 0;
}

