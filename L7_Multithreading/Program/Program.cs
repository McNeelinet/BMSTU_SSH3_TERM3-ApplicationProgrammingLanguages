using System.Diagnostics;
using Configurator;
using Logger;
using Pipeline;
using Structures;

namespace Program;

static class Program
{
    private static List<int> _chatsTimeouts = new();
    private static List<int> _analyzersTimeouts = new();
    private static List<int> _generatorsTimeouts = new();
    private static List<int> _formattersTimeouts = new();
    


    private static void Main(string[] args)
    {
        var logWriter = new LogWriter("log.txt");
        var timer = new Stopwatch();
        ReadConfig("../../../config.xml");
        
        BaseManager chatManager = new ChatManager(logWriter);
        BaseManager analyzerManager = new AnalyzerManager(logWriter, chatManager);
        BaseManager answerGeneratorManager = new AnswerGeneratorManager(logWriter, analyzerManager);
        BaseManager answerFormatterManager = new AnswerFormatterManager(logWriter, answerGeneratorManager);
        
        timer.Start();
        StartAll(chatManager, analyzerManager, answerGeneratorManager, answerFormatterManager);
        Console.ReadKey();
        StopAll(chatManager, analyzerManager, answerGeneratorManager, answerFormatterManager);
        timer.Stop();
        
        LogStats(chatManager, analyzerManager, answerGeneratorManager, answerFormatterManager, logWriter, timer);
    }

    private static void ReadConfig(string filename)
    {
        try
        {
            var config = new ConfigReader(filename);
            _chatsTimeouts = config.GetTimers("chats", "chat");
            _analyzersTimeouts = config.GetTimers("analyzers", "analyzer");
            _generatorsTimeouts = config.GetTimers("generators", "generator");
            _formattersTimeouts = config.GetTimers("formatters", "formatter");
        }
        catch (FormatException)
        {
            Console.WriteLine("В конфигурационном файле числа записаны в неверном формате.");
            ResetConfig();
        }
        catch
        {
            Console.WriteLine("Произошла ошибка при чтении конфигурационного файла.");
            ResetConfig();
        }
    }

    private static void ResetConfig()
    {
        _chatsTimeouts.Clear();
        _analyzersTimeouts.Clear();
        _generatorsTimeouts.Clear();
        _formattersTimeouts.Clear();
    }

    private static void StartAll(
        BaseManager chatManager,
        BaseManager analyzerManager,
        BaseManager answerGeneratorManager,
        BaseManager answerFormatterManager)
    {
        chatManager.CreateManagedTasks(_chatsTimeouts);
        analyzerManager.CreateManagedTasks(_analyzersTimeouts);
        answerGeneratorManager.CreateManagedTasks(_generatorsTimeouts);
        answerFormatterManager.CreateManagedTasks(_formattersTimeouts);
    }

    private static void StopAll(
        BaseManager chatManager,
        BaseManager analyzerManager,
        BaseManager answerGeneratorManager,
        BaseManager answerFormatterManager)
    {
        chatManager.StopTasks();
        analyzerManager.StopTasks();
        answerGeneratorManager.StopTasks();
        answerFormatterManager.StopTasks();
    }

    private static void LogStats(
        BaseManager chatManager,
        BaseManager analyzerManager,
        BaseManager answerGeneratorManager,
        BaseManager answerFormatterManager,
        LogWriter logWriter,
        Stopwatch timer)
    {
        var prompts = new List<Prompt>();
        prompts.AddRange(chatManager.GetPromptsList());
        prompts.AddRange(analyzerManager.GetPromptsList());
        prompts.AddRange(answerGeneratorManager.GetPromptsList());
        prompts.AddRange(answerFormatterManager.GetPromptsList());
        
        logWriter.Log("==================================================");
        logWriter.Log($"Прошло времени: {timer.ElapsedMilliseconds} милисекунд");
        logWriter.Log($"Всего создано {prompts.Count} запросов");
        logWriter.Log($"Среднее время создания запросов: {timer.ElapsedMilliseconds / prompts.Count} милисекунд");
        logWriter.Log($"Осталось {chatManager.GetPromptsList().Count} запросов в очереди на анализатор");
        logWriter.Log($"Осталось {analyzerManager.GetPromptsList().Count} запросов в очереди на генератор");
        logWriter.Log($"Осталось {answerGeneratorManager.GetPromptsList().Count} запросов в очереди на форматтер");
        logWriter.Log($"{answerFormatterManager.GetPromptsList().Count} запросов прошли полный цикл");
        logWriter.Log($"Среднее время анализа: {StatsCalculator.CalcAverageAnalyzerTime(prompts)} милисекунд");
        logWriter.Log($"Среднее время генерации: {StatsCalculator.CalcAverageGeneratorTime(prompts)} милисекунд");
        logWriter.Log($"Среднее время форматирования: {StatsCalculator.CalcAverageFormatterTime(prompts)} милисекунд");
    }
}