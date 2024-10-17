### C++ реализация укороченная

```cpp
#include <iostream>
#include <vector>
#include <string>

using namespace std;

struct Item {
    string Name;
    int Weight;
    int Value;
};

int TotalWeight(const vector<Item>& candidate) {
    int totalWeight = 0;
    for (const auto& item : candidate) {
        totalWeight += item.Weight;
    }
    return totalWeight;
}

int TotalValue(const vector<Item>& candidate) {
    int totalValue = 0;
    for (const auto& item : candidate) {
        totalValue += item.Value;
    }
    return totalValue;
}

template <typename T>
vector<vector<T>> PowerSet(const vector<T>& items) {
    vector<vector<T>> powerSet = {{}};
    for (const auto& item : items) {
        vector<vector<T>> newSubsets;
        for (const auto& subset : powerSet) {
            auto newSubset = subset;
            newSubset.push_back(item);
            newSubsets.push_back(newSubset);
        }
        powerSet.insert(powerSet.end(), newSubsets.begin(), newSubsets.end());
    }
    return powerSet;
}

vector<Item> Knapsack(const vector<Item>& items, int maxWeight) {
    vector<Item> bestCandidate;
    int bestValue = 0;
    for (const auto& candidate : PowerSet(items)) {
        int totalWeight = TotalWeight(candidate);
        int totalValue = TotalValue(candidate);
        if (totalWeight <= maxWeight && totalValue > bestValue) {
            bestValue = totalValue;
            bestCandidate = candidate;
        }
    }
    return bestCandidate;
}

int main() {
    vector<Item> items = {
        {"Laptop", 3, 3500},
        {"Camera", 1, 1000},
        {"Headphones", 1, 500},
        {"Charger", 2, 400},
        {"Pod", 1, 150},
        {"Mouse", 1, 300},
        {"Phone", 2, 2000}
    };

    int maxWeight = 6;
    vector<Item> bestSolution = Knapsack(items, maxWeight);

    cout << "Best items to pack:" << endl;
    for (const auto& item : bestSolution) {
        cout << item.Name << " (Weight: " << item.Weight << ", Value: " << item.Value << ")" << endl;
    }

    return 0;
}
```