using Infrastrucutre;
using Infrastrucutre.IOManager;
using joshika39.Infrastructure.Impl;
using System.ComponentModel;

namespace Infrastructure.Impl.StandardIOManager
{
    internal class StandardIOManager : IIOManager
    {
        private readonly ILogger _logger;

        public StandardIOManager(ILogger logger)
        {
            Console.ForegroundColor = ConsoleColor.White;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void PrintSystemDetails(string githubUsername, string creatorName, string creatorID)
        {
            var startedTime = DateTime.Now.ToString("h:mm:ss");
            var host = Environment.MachineName;
            var user = Environment.UserName;
            var platform = Environment.OSVersion.Platform;

            WriteLine(ColorConstants.EscapeCodes.YELLOW, $" -------- HOST PC -------- ");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $"[START AT]: {startedTime}");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $"[    HOST]: {host}");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $"[    USER]: {user}");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $"[PLATFORM]: {platform}\n");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $" -------- CREATOR -------- ");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $"[ MADE BY]: {creatorID} ({creatorName})");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $"[  GITHUB]: https://github.com/{githubUsername}/\n");
            WriteLine(ColorConstants.EscapeCodes.YELLOW, $" ------ EXECUTION ------ \n");
            Console.ResetColor();
        }

        public void Write(MessageSeverity severity, string msg)
        {
            ConstructMsg(severity);
            Write(msg);
        }

        public void WriteLine(MessageSeverity severity, string msg)
        {
            ConstructMsg(severity);
            WriteLine(msg);
        }

        public T ReadLine<T>(IIOManager.TryParseHandler<T> handler)
        {
            var value = Console.ReadLine()!;

            if (handler(value, out var result))
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public T ReadLine<T>(string prompt, IIOManager.TryParseHandler<T> handler)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            Write(ColorConstants.EscapeCodes.CYAN, $"[  INPUT: {time}] {prompt}");
            var ans = ReadLine(handler);
            return ans;
        }

        private void ConstructMsg(MessageSeverity severity)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            switch (severity)
            {
                case MessageSeverity.Success:
                    Write(ColorConstants.EscapeCodes.GREEN, $"[SUCCESS: {time}] ");
                    break;

                case MessageSeverity.Info:
                    Write($"[   INFO: {time}] ");
                    break;

                case MessageSeverity.Warning:
                    Write(ColorConstants.EscapeCodes.YELLOW, $"[WARNING: {time}] ");
                    break;

                case MessageSeverity.Error:
                    Write(ColorConstants.EscapeCodes.RED, $"[  ERROR: {time}] ");
                    break;
                default:
                    Write(ColorConstants.EscapeCodes.RED, $"[  ERROR: {time}] Unknown Severity!\n");
                    throw new ArgumentException("Unknown Severity!");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RestoreTerminalState()
        {

        }

        public void Write(string colorEscape, string msg)
        {
            //_logger.LogWrite(msg);
            Console.Write($"{colorEscape}{msg}");
            Console.ResetColor();
        }

        public void Write(string msg)
        {
            //_logger.LogWrite(msg);
            Console.Write(msg);
        }

        public void WriteLine(string colorEscape, string msg)
        {
            //_logger.LogWrite(msg);
            Console.WriteLine($"{colorEscape}{msg}");
            Console.ResetColor();
        }

        public void WriteLine(string msg)
        {
            //_logger.LogWrite(msg);
            Console.WriteLine();
        }
    }
}