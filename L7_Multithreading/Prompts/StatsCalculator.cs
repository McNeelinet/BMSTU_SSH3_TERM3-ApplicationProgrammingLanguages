namespace Structures;

public class StatsCalculator
{
    public static long CalcAverageAnalyzerTime(List<Prompt> prompts)
    {
        var sum = prompts.Sum(prompt => prompt.AnalyzerTimer);

        if (prompts.Count != 0)
            return sum / prompts.Count;
        return 0;
    }
    
    public static long CalcAverageGeneratorTime(List<Prompt> prompts)
    {
        var sum = prompts.Sum(prompt => prompt.GeneratorTimer);

        if (prompts.Count != 0)
            return sum / prompts.Count;
        return 0;
    }
    
    public static long CalcAverageFormatterTime(List<Prompt> prompts)
    {
        var sum = prompts.Sum(prompt => prompt.FormatterTimer);

        if (prompts.Count != 0)
            return sum / prompts.Count;
        return 0;
    }
}