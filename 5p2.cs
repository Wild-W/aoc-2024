using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

static void TopologicalSortStep(
    int num, List<int> sequence,
    Dictionary<int, HashSet<int>> dependenciesMap,
    Dictionary<int, bool> visited, Stack<int> stack)
{
    visited[num] = true;

    if (!dependenciesMap.TryGetValue(num, out var set))
    {
        set = new HashSet<int>();
        dependenciesMap[num] = set;
    }

    HashSet<int> dependencies = new(sequence);
    dependencies.IntersectWith(set);

    foreach (int v in dependencies)
    {
        if (!(visited.TryGetValue(v, out bool didVisit) ? didVisit : false))
            TopologicalSortStep(v, sequence, dependenciesMap, visited, stack);
    }

    stack.Push(num);
}

static List<int> TopologicalSort(List<int> sequence, Dictionary<int, HashSet<int>> dependenciesMap)
{
    Stack<int> stack = new();
    Dictionary<int, bool> visited = new();

    foreach (int v in sequence)
    {
        if (!(visited.TryGetValue(v, out bool didVisit) ? didVisit : false))
            TopologicalSortStep(v, sequence, dependenciesMap, visited, stack);
    }

    List<int> sorted = new();
    while (stack.Count != 0)
    {
        sorted.Add(stack.Pop());
    }

    return sorted;
}

StreamReader reader = new("5.txt");
Dictionary<int, HashSet<int>> dependenciesMap = new();
long score = 0;

string line;
while ((line = reader.ReadLine()) != "")   // {page number}|{page number}
{
    MatchCollection matchedPageNumbers = Regex.Matches(line, @"^(\d+)|(\d+)$");
    int p1 = int.Parse(matchedPageNumbers[0].Value);
    int p2 = int.Parse(matchedPageNumbers[1].Value);

    if (!dependenciesMap.TryGetValue(p2, out var set))
    {
        set = new HashSet<int>();
        dependenciesMap[p2] = set;
    }
    set.Add(p1);
}
while ((line = reader.ReadLine()) != null) // {page number},{page number},...
{
    List<int> previousPageNumbers = new();
    bool needsSorting = false;
    List<int> pageNumbers = Regex.Matches(line, @"\d+").Select(m => {
        int p = int.Parse(m.Value);
        
        if (previousPageNumbers.Any(prior => dependenciesMap.TryGetValue(prior, out var deps) && deps.Contains(p)))
        {
            needsSorting = true;
        }

        previousPageNumbers.Add(p);
        return p;
        }).ToList();
    if (!needsSorting) continue;

    List<int> sortedNumbers = TopologicalSort(pageNumbers, dependenciesMap);
    score += sortedNumbers[sortedNumbers.Count / 2];
}

Console.WriteLine(score);
reader.Close();
