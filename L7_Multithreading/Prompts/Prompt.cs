using System.Diagnostics;

namespace Structures;

public struct Prompt
{
    public int Id { get; set; }
    public long AnalyzerTimer { get; set; }
    public long GeneratorTimer { get; set; }
    public long FormatterTimer { get; set; }

    public Prompt(int id)
    {
        Id = id;
    }
}