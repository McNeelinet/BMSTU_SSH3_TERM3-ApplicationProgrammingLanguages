using System.Diagnostics;
using Logger;
using Structures;

namespace Pipeline;

public class AnalyzerManager : BaseManager
{
    private readonly LogWriter _logWriter;
    private readonly BaseManager _chatManager;

    public AnalyzerManager(LogWriter logWriter, BaseManager chatManager)
    {
        _chatManager = chatManager;
        _logWriter = logWriter;
    }
    
    private protected override void EndlessPromptGenerator(int id, int timeout)
    {
        var analyzerTimer = new Stopwatch();
        
        while (true)
        {
            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _logWriter.Log($"Анализатор {id} закончил свою работу");
                return;
            }
            
            var nullablePrompt = _chatManager.DequeuePrompt();
            if (nullablePrompt == null) continue;
            
            analyzerTimer.Start();
            Thread.Sleep(timeout);
            
            lock (_promptsLock)
            {
                var prompt = (Prompt)nullablePrompt;
                prompt.AnalyzerTimer = analyzerTimer.ElapsedMilliseconds;
                _prompts.Enqueue(prompt);
                
                _logWriter.Log($"Анализатор {id} проанализировал запрос {prompt.Id}");
            }
            analyzerTimer.Reset();
        }
    }
}