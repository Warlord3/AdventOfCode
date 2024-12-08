
var input = File.ReadAllText(Environment.CurrentDirectory + "/input.txt");
var split = input.Split("\n\n");
var rulesText = split[0];
var updateText = split[1];

List<(string, string)> rules = [];
foreach(var rule in rulesText.Split("\n"))
{
    split = rule.Split("|");
    rules.Add((split[0], split[1]));
}

var result1 = 0;
var result2 = 0;
foreach(var update in updateText.Split("\n"))
{
    if(CheckUpdate(update))
    {
        var updateSplit = update.Split(",");
        result1 += int.Parse(updateSplit[updateSplit.Length / 2]);
    }
    else
    {
        var ordererUpdate = OrderUpdate(update);
        result2 += int.Parse(ordererUpdate[ordererUpdate.Count / 2]);
    }
}
Console.WriteLine(result1);
Console.WriteLine(result2);
bool CheckUpdate(string update)
{
    var updateList = update.Split(",").ToList();
    for(int i = 0; i < updateList.Count; i++)
    {
        var leftRules = GetLeftRules(updateList[i]);
        foreach(var leftRule in leftRules)
        {
            var pos = updateList.IndexOf(leftRule);
            if(pos != -1 && pos < i)
            {
                return false;
            }
        }
        var rightRules = GetRightRules(updateList[i]);
        foreach(var rightRule in rightRules)
        {
            var pos = updateList.IndexOf(rightRule);
            if(pos != -1 && pos > i)
            {
                return false;
            }
        }
    }
    return true;
}


List<string> OrderUpdate(string update)
{
    var result = new List<string>();
    var updateList = update.Split(",").ToList();
    result.Add(updateList[0]);
    foreach(var NextPage in updateList[1..])
    {
        var found = false;
        for(int i = 0; i < result.Count; i++)
        {
            if(RuleMatch(NextPage, result[i]))
            {
                result.Insert(i, NextPage);
                found = true;
                break;
            }
        }
        if(!found)
        {
            result.Add(NextPage);
        }

    }
    return result;
}

List<string> GetLeftRules(string update)
{
    return rules.Where(x => update.Contains(x.Item1)).Select(x => x.Item2).ToList();
}
List<string> GetRightRules(string update)
{
    return rules.Where(x => update.Contains(x.Item2)).Select(x => x.Item1).ToList();
}

bool RuleMatch(string left, string right)
{
    return rules.Any(x => x.Item1 == left && x.Item2 == right);
}
