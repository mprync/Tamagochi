namespace Tamagotchi.Data.Extentions;

public static class MathExtensions
{
    /// <summary>
    /// Normalizes a value between a min and max value
    /// </summary>
    /// <param name="val"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static decimal Normalize(this decimal val, decimal min, decimal max)
        => (val - min) / (max - min);
}