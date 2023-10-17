using Common;
using Common.Models;

namespace Lab1;

public class Lab : LabBase
{
    public override int Code => Constants.FirstLabCode;

    public override async Task<LabResult> Execute(LabInput input)
    {
        var values = input.Input.Trim().Split(' ');

        if (values.Length != 3)
            throw new Exception("Invalid data format.");

        var n = Convert.ToInt32(int.Parse(values[0]));
        var a = Convert.ToInt32(int.Parse(values[1]));
        var b = Convert.ToInt32(int.Parse(values[2]));

        if (n < 1 || n > 20)
            throw new ArgumentOutOfRangeException(nameof(n), n, "Argument N is out of range.");
        if (a < 0 || a > 20)
            throw new ArgumentOutOfRangeException(nameof(a), a, "Argument A is out of range.");
        if (b < 0 || b > 20)
            throw new ArgumentOutOfRangeException(nameof(b), b, "Argument B is out of range.");

        int result = Ways(n, a, b);

        return new LabResult(result.ToString());
    }

    static int Ways(int n, int a, int b)
    {
        if (n == 0)
            return 1;

        int total = 0;

        total += Ways(n - 1, a, b);

        if (a > 0)
            total += Ways(n - 1, a - 1, b);
        if (b > 0)
            total += Ways(n - 1, a, b - 1);
        if (a > 0 && b > 0)
            total += Ways(n - 1, a - 1, b - 1);

        return total;
    }

    public static LabBase Instance => new Lab();
}
