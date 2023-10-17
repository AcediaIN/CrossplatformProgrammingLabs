using Common;
using Common.Models;

namespace Lab3;

public class Lab : LabBase
{
    public override int Code => Constants.ThirdLabCode;

    private Dictionary<string, int> maxDepth = new Dictionary<string, int>();
    private Dictionary<string, HashSet<string>> procedures = new Dictionary<string, HashSet<string>>();

    private const string blockSeparator = "*****";
    private const string procedureSeparator = "\r\n";

    public override async Task<LabResult> Execute(LabInput input)
    {
        var blocks = input.Input.Split(blockSeparator);

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

        List<string> result = new List<string>();
        foreach (string procedureName in procedures.Keys)
        {
            if (IsPotentiallyRecursive(procedureName, new HashSet<string>(), maxDepth[procedureName]))
                result.Add("YES");
            else
                result.Add("NO");
        }

        return new LabResult(string.Join("", result));
    }

    private bool IsPotentiallyRecursive(string procedureName, HashSet<string> currentCallStack, int depth)
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

    public static LabBase Instance => new Lab();
}
