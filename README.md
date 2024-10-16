# csh-bruteforce-hashsets-knapsack

Алгоритм решает классическую задачу **рюкзака** (Knapsack Problem) с использованием метода полного перебора, основанного на генерации **множества всех подмножеств** (power set). В задаче рюкзака нам нужно выбрать подмножество предметов с максимальной ценностью, которое суммарно не превышает заданный вес.

### 1. Псевдокод:

```
function knapsack(items, max_weight)
    best_value = 0
    best_candidate = empty set

    for each candidate in power_set(items) do
        if total_weight(candidate) ≤ max_weight then
            if sales_value(candidate) > best_value then
                best_value = sales_value(candidate)
                best_candidate = candidate
            end if
        end if
    end for

    return best_candidate
end function
```

### 2. C# реализация:

```csharp
using System;
using System.Collections.Generic;

public class KnapsackSolver
{
    // Определяем элемент с весом и ценностью
    public class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
    }

    // Функция для нахождения наилучшего подмножества предметов для рюкзака
    public static List<Item> Knapsack(List<Item> items, int maxWeight)
    {
        List<Item> bestCandidate = null;
        int bestValue = 0;

        // Генерируем множество всех подмножеств (power set)
        List<List<Item>> powerSet = PowerSet(items);

        // Проходим по каждому подмножеству (candidate)
        foreach (var candidate in powerSet)
        {
            int totalWeight = TotalWeight(candidate);
            int totalValue = TotalValue(candidate);

            // Если вес подмножества <= maxWeight и его ценность больше текущей лучшей
            if (totalWeight <= maxWeight && totalValue > bestValue)
            {
                bestValue = totalValue;
                bestCandidate = candidate;
            }
        }

        return bestCandidate;
    }

    // Функция для генерации всех подмножеств
    public static List<List<T>> PowerSet<T>(List<T> items)
    {
        List<List<T>> powerSet = new List<List<T>>();
        powerSet.Add(new List<T>());  // Добавляем пустое множество

        // Проходим по каждому элементу и добавляем его в уже существующие подмножества
        foreach (var item in items)
        {
            List<List<T>> newSubsets = new List<List<T>>();

            foreach (var subset in powerSet)
            {
                List<T> newSubset = new List<T>(subset);
                newSubset.Add(item);
                newSubsets.Add(newSubset);
            }

            powerSet.AddRange(newSubsets);
        }

        return powerSet;
    }

    // Функция для подсчета общего веса подмножества
    public static int TotalWeight(List<Item> candidate)
    {
        int totalWeight = 0;
        foreach (var item in candidate)
        {
            totalWeight += item.Weight;
        }
        return totalWeight;
    }

    // Функция для подсчета общей ценности подмножества
    public static int TotalValue(List<Item> candidate)
    {
        int totalValue = 0;
        foreach (var item in candidate)
        {
            totalValue += item.Value;
        }
        return totalValue;
    }

    public static void Main(string[] args)
    {
        // Пример использования: набор предметов
        List<Item> items = new List<Item>
        {
            new Item { Name = "Laptop", Weight = 3, Value = 2000 },
            new Item { Name = "Camera", Weight = 1, Value = 1000 },
            new Item { Name = "Headphones", Weight = 1, Value = 500 }
        };

        int maxWeight = 4;  // Максимальный вес, который может выдержать рюкзак

        // Решаем задачу рюкзака
        List<Item> bestSolution = Knapsack(items, maxWeight);

        // Выводим результат
        Console.WriteLine("Best items to pack:");
        foreach (var item in bestSolution)
        {
            Console.WriteLine($"{item.Name} (Weight: {item.Weight}, Value: {item.Value})");
        }
    }
}
```

### Пример выполнения:

Для набора предметов:
- **Laptop** (Вес: 3, Ценность: 2000)
- **Camera** (Вес: 1, Ценность: 1000)
- **Headphones** (Вес: 1, Ценность: 500)

И максимального веса рюкзака = 4, алгоритм выберет такие предметы:

```
Laptop (Вес: 3, Ценность: 2000)
Camera (Вес: 1, Ценность: 1000)
```
Так как их суммарный вес (3+1 = 4) не превышает максимальный вес, и их ценность (2000+1000 = 3000) максимальна среди всех возможных подмножеств.
