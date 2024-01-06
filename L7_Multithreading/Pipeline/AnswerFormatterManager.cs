using System.Diagnostics;
using Logger;
using Structures;

namespace Pipeline;

public class AnswerFormatterManager : BaseManager
{
    private readonly LogWriter _logWriter;
    private readonly BaseManager _generatorManager;

    public AnswerFormatterManager(LogWriter logWriter, BaseManager generatorManager)
    {
        _generatorManager = generatorManager;
        _logWriter = logWriter;
    }
    
    private protected override void EndlessPromptGenerator(int id, int timeout)
    {
        var formatterTimer = new Stopwatch();
        
        while (true)
        {
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _logWriter.Log($"Форматтер {id} закончил свою работу");
                return;
            }
            
            var nullablePrompt= _generatorManager.DequeuePrompt();
            if (nullablePrompt == null) continue;
            
            formatterTimer.Start();
            Thread.Sleep(timeout);
    
            lock (_promptsLock)
            {
                var prompt = (Prompt)nullablePrompt;
                prompt.FormatterTimer = formatterTimer.ElapsedMilliseconds;
                _prompts.Enqueue(prompt);
                
                _logWriter.Log($"Форматтер {id} отформатировал ответ на запрос {prompt.Id}");
            }
            formatterTimer.Reset();
        }
    }
}