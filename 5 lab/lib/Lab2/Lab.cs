using Common;
using Common.Models;
using Lab2.Extensions;

namespace Lab2;

public class Lab : LabBase
{
    public override int Code => Constants.SecondLabCode;

    public override async Task<LabResult> Execute(LabInput input)
    {
        var values = input.Input.Trim().Split(' ');

        if (values.Length != 1)
            throw new Exception("Invalid data format.");

        var n = Convert.ToInt32(int.Parse(values[0]));

        if (n <= 3 || n >= 1000)
            throw new ArgumentOutOfRangeException(nameof(n), n, "Argument N is out of range.");

        int primaryCount = 0;

        int from = (int)Math.Pow(10, n - 1);
        int to = Enumerable.Repeat(9, n).Aggregate((x, y) => x * 10 + y);

        bool found = false;

        for (int num = from; num <= to; num++)
        {
            for (int j = 0; (j <= n - 3) && !found; j++)
            {
                var part = (num / (int)Math.Pow(10, (n - 3) - j)) % 1000;

                if (part.IsPrime()) { primaryCount++; found = true; }
            }

            found = false;
        }

        var formated = primaryCount % (Math.Pow(10, 9) + 9);

        return new LabResult(formated.ToString());
    }

    public static LabBase Instance => new Lab();
}