using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

StreamReader reader = new("5.txt");
Dictionary<int, HashSet<int>> rules = new();
int score = 0;

string line;
while ((line = reader.ReadLine()) != "")   // {page number}|{page number}
{
    MatchCollection matchedPageNumbers = Regex.Matches(line, @"^(\d+)|(\d+)$");
    int p1 = int.Parse(matchedPageNumbers[0].Value);
    int p2 = int.Parse(matchedPageNumbers[1].Value);

    if (!rules.TryGetValue(p1, out var set))
    {
        set = new HashSet<int>();
        rules[p1] = set;
    }
    set.Add(p2);
}
while ((line = reader.ReadLine()) != null) // {page number},{page number},...
{
    List<int> previousPageNumbers = new();
    bool rulesUpheld = true;

    foreach (Match matchedPageNumber in Regex.Matches(line, @"\d+"))
    {
        int p = int.Parse(matchedPageNumber.Value);
        if (rules.TryGetValue(p, out var set))
        {
            HashSet<int> violations = new HashSet<int>(previousPageNumbers);
            violations.IntersectWith(set);
            if (violations.Count != 0)
            {
                rulesUpheld = false;
                break;
            }
        }
        previousPageNumbers.Add(p);
    }
    
    if (rulesUpheld) score += previousPageNumbers[previousPageNumbers.Count / 2];
}

Console.WriteLine(score);
reader.Close();
