namespace csh_bruteforce_hashsets_knapsack;

using System;
using System.Collections.Generic;

class Program
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

    static void Main(string[] args)
    {
        // Пример использования: набор предметов
        List<Item> items = new List<Item>
        {
            new Item { Name = "Laptop", Weight = 3, Value = 2000 },
            new Item { Name = "Camera", Weight = 1, Value = 1000 },
            new Item { Name = "Headphones", Weight = 1, Value = 500 },
            // Расширенные набор предметов
            new Item { Name = "Charger", Weight = 2, Value = 2000 },
            new Item { Name = "Pod", Weight = 1, Value = 1000 },
            new Item { Name = "Mouse", Weight = 1, Value = 500 },
            new Item { Name = "Phone", Weight = 2, Value = 500 }
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
