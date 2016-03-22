using System.Linq;

public static class MathEx
{
    public static float Min(params float[] values)
    {
        return values.Min();
    }

    public static float Max(params float[] values)
    {
        return values.Max();
    }
}