using System.Windows.Media;

namespace NekoBot.WPFProgram.Loggers
{
    public class LogEntry
    {
        public string Time { get; set; }
        public string Message { get; set; }
        public Brush MessageColor { get; set; }
        public LogEntry()
        {

        }
        public LogEntry(string message, Brush messageColor = null)
        {
            Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Message = message;
            MessageColor = messageColor ?? Brushes.White;
        }
    }
}
