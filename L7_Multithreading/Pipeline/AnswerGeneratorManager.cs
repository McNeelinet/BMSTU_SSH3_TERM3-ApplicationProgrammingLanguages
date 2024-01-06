using System.Diagnostics;
using Logger;
using Structures;

namespace Pipeline;

public class AnswerGeneratorManager : BaseManager
{
    private readonly LogWriter _logWriter;
    private readonly BaseManager _analyzerManager;

    public AnswerGeneratorManager(LogWriter logWriter, BaseManager analyzerManager)
    {
        _analyzerManager = analyzerManager;
        _logWriter = logWriter;
    }
    
    private protected override void EndlessPromptGenerator(int id, int timeout)
    {
        var generatorTimer = new Stopwatch();
        
        while (true)
        {
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _logWriter.Log($"Генератор {id} закончил свою работу");
                return;
            }
            
            var nullablePrompt= _analyzerManager.DequeuePrompt();
            if (nullablePrompt == null) continue;
            
            generatorTimer.Start();
            Thread.Sleep(timeout);
            
            lock (_promptsLock)
            {
                var prompt = (Prompt)nullablePrompt;
                prompt.GeneratorTimer = generatorTimer.ElapsedMilliseconds;
                _prompts.Enqueue(prompt);
                
                _logWriter.Log($"Генератор {id} сгенерировал ответ на запрос {prompt.Id}");
            }
            generatorTimer.Reset();
        }
    }
}