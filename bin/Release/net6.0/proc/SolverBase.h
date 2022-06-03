#pragma once
#include <string>

class SolverBase
{
public:
    virtual void read(std::string) = 0;
    virtual void solve() = 0;
    virtual void write(std::string) = 0;
};