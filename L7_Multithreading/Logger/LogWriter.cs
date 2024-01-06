namespace Logger;

public class LogWriter
{
    private string _filepath;
    private readonly object _logLock = new();
    
    public LogWriter(string filepath)
    {
        _filepath = filepath;
        File.WriteAllText(filepath, string.Empty);
    }

    public void Log(string logMessage)
    {
        lock (_logLock)
        {
            logMessage = DateTime.Now.ToString("HH:mm:ss - ") + logMessage;
            File.AppendAllText(_filepath, logMessage + Environment.NewLine);
            Console.WriteLine(logMessage);
        }
    }
}