using Logger;
using Structures;

namespace Pipeline;

public class ChatManager : BaseManager
{
    private readonly LogWriter _logWriter;
    
    private int TotalPrompts { get; set; }

    public ChatManager(LogWriter logWriter)
    {
        _logWriter = logWriter;
    }

    public override void ResetState()
    {
        base.ResetState();
        TotalPrompts = 0;
    }

    private protected override void EndlessPromptGenerator(int id, int timeout)
    {
        while (true)
        {
            if (_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _logWriter.Log($"Чат {id} завершил свою работу");
                return;
            }
            
            lock (_promptsLock)
            {
                var prompt = new Prompt(TotalPrompts);
                _prompts.Enqueue(prompt);
                ++TotalPrompts;
                _logWriter.Log($"В чат {id} подается запрос {prompt.Id}");
            }
            
            Thread.Sleep(timeout);
        }
    }
}