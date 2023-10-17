Dictionary<string, int> maxDepth = new Dictionary<string, int>();
Dictionary<string, HashSet<string>> procedures = new Dictionary<string, HashSet<string>>();

const string blockSeparator = "*****";
const string procedureSeparator = "\r\n";

var input = File.ReadAllText("input.txt");

var blocks = input.Split(blockSeparator);

var n = int.Parse(blocks.First().Split(procedureSeparator).First());

//remove n 
blocks[0] = string.Join(procedureSeparator, blocks.First().Split(procedureSeparator).Skip(1));

if (string.IsNullOrEmpty(blocks.Last()))
    Array.Resize(ref blocks, blocks.Length - 1);

if (n < 1 || n > 100)
    throw new ArgumentOutOfRangeException(nameof(n), n, "Argument N is out of range.");

if (n != blocks.Length)
    throw new ApplicationException("Argument 'n' is not equal to procedure count.");

foreach (var block in blocks)
{
    var procedureFields = block.Split(procedureSeparator);

    if (string.IsNullOrEmpty(procedureFields.First()))
        procedureFields = procedureFields.Skip(1).ToArray();


    if (string.IsNullOrEmpty(procedureFields.Last()))
        Array.Resize(ref procedureFields, procedureFields.Length - 1);

    string procedureName = procedureFields[0];
    int depth = int.Parse(procedureFields[1]); 

    procedures[procedureName] = new HashSet<string>(procedureFields.Skip(2));
    maxDepth[procedureName] = depth;
}

bool IsPotentiallyRecursive(string procedureName, HashSet<string> currentCallStack, int depth)
{
    if (currentCallStack.Contains(procedureName))
        return true; 

    if (currentCallStack.Any(x => procedures[procedureName].Contains(x)))
        return true;

    if (depth == 0)
        return false; 

    currentCallStack.Add(procedureName);

    foreach (string call in procedures[procedureName])
    {
        if (IsPotentiallyRecursive(call, currentCallStack, depth - 1))
            return true;
    }

    currentCallStack.Remove(procedureName);
    return false;
}

List<string> result = new List<string>();
foreach (string procedureName in procedures.Keys)
{
    if (IsPotentiallyRecursive(procedureName, new HashSet<string>(), maxDepth[procedureName]))
        result.Add("YES");
    else
        result.Add("NO");
}

File.WriteAllText("output.txt", string.Join("", result));