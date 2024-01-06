using Structures;

namespace Pipeline;

public abstract class BaseManager
{
    protected readonly object _promptsLock = new();
    protected readonly Queue<Prompt> _prompts = new();
    protected readonly List<Task> _managedTasks = new();
    protected CancellationTokenSource _cancellationTokenSource = new();
    
    public virtual void CreateManagedTasks(List<int> timeouts)
    {
        ResetState();
        
        for (var i = 0; i < timeouts.Count; ++i)
        {
            var i1 = i;
            var chat = Task.Run(() => EndlessPromptGenerator(i1, timeouts[i1]), _cancellationTokenSource.Token);
            _managedTasks.Add(chat);
        }
    }

    public virtual void ResetState()
    {
        StopTasks();
        _cancellationTokenSource = new CancellationTokenSource();
        
        lock (_promptsLock)
        {
            _prompts.Clear();
        }
    }
    
    public virtual void StopTasks()
    {
        if (_managedTasks.Count <= 0) return;
        
        _cancellationTokenSource.Cancel();
        Task.WhenAll(_managedTasks).Wait();
        _managedTasks.Clear();
    }
    
    public virtual Prompt? DequeuePrompt()
    {
        lock (_promptsLock)
        {
            if (_prompts.Count != 0)
            {
                return _prompts.Dequeue();
            }

            return null;
        }
    }
    
    public virtual List<Prompt> GetPromptsList()
    {
        List<Prompt> prompts;
        
        lock (_promptsLock)
        {
            prompts = _prompts.ToList();
        }

        return prompts;
    }

    private protected abstract void EndlessPromptGenerator(int id, int timeout);
}