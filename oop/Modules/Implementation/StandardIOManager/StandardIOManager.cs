using System;
using System.IO;
using Infrastructure;
using Infrastructure.IOManager;
using Infrastructure.Logger;

namespace Implementation.StandardIOManager
{
    internal class StandardIOManager : IIOManager
    {
        private readonly ILogger _logger;

        public StandardIOManager(ILogger logger)
        {
            Console.ForegroundColor = ConsoleColor.White;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void PrintSystemDetails(string githubUsername, string creatorName, string creatorId)
        {
            var startedTime = DateTime.Now.ToString("h:mm:ss");
            var host = Environment.MachineName;
            var user = Environment.UserName;
            var platform = Environment.OSVersion.Platform;
            var message =
                $" -------- HOST PC -------- \n" +
                $"[START AT]: {startedTime}\n" +
                $"\t[    HOST]: {host}\n" +
                $"[    USER]: {user}\n" +
                $"[PLATFORM]: {platform}\n\n\t" +
                $" -------- CREATOR -------- \n" +
                $"[ MADE BY]: {creatorId} ({creatorName})\n" +
                $"[  GITHUB]: https://github.com/{githubUsername}/\n\n" +
                $" ------ EXECUTION ------ \n\n";

            Write(Colorize(Constants.EscapeColors.YELLOW, message));
        }

        public void Write(MessageSeverity severity, string msg)
        {
            ConstructMsg(severity);
            Write(msg);
        }

        public void Write(string msg)
        {
            _logger.LogWrite(msg);
            Console.Write(msg);
        }

        public void WriteLine(MessageSeverity severity, string msg)
        {
            var prefix = ConstructMsg(severity);
            WriteLine($"{prefix}{msg}");
        }

        public void WriteLine(string msg)
        {
            Console.Write(_logger.LogWriteLine(msg));
        }

        public T ReadLine<T>(IIOManager.TryParseHandler<T> handler)
        {
            var rawInput = Console.ReadLine()!;
            _logger.LogWrite($"{rawInput}\n");

            if (handler(rawInput, out var result))
            {
                return result;
            }
            throw new InvalidOperationException();
        }

        public T ReadLine<T>(IIOManager.TryParseHandler<T> handler, out bool isConverted)
        {
            var rawInput = Console.ReadLine()!;
            _logger.LogWrite($"{rawInput}\n");
            isConverted = handler(rawInput, out var result);

            return isConverted ? result : default;
        }

        public T ReadLine<T>(IIOManager.TryParseHandler<T> handler, string prompt)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            Write(Colorize(Constants.EscapeColors.CYAN, $"[  INPUT: {time}] {prompt}"));
            var ans = ReadLine(handler);
            return ans;
        }

        public T ReadLine<T>(IIOManager.TryParseHandler<T> handler, string prompt, string errorMsg)
        {
            while (true)
            {
                var time = DateTime.Now.ToString("HH:mm:ss");
                Write(Colorize(Constants.EscapeColors.CYAN, $"[  INPUT: {time}] {prompt}"));
                var ans = ReadLine(handler, out var isCorrect);

                if (isCorrect)
                {
                    return ans;
                }

                WriteLine(MessageSeverity.Error, $"{errorMsg} | (Invalid type: {typeof(T)})!");
            }
        }

        private string ConstructMsg(MessageSeverity severity)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            switch (severity)
            {
                case MessageSeverity.Success:
                    return Colorize(Constants.EscapeColors.GREEN, $"[SUCCESS: {time}] ");
                case MessageSeverity.Info:
                    return Colorize(Constants.EscapeColors.CYAN, $"[   INFO: {time}] ");
                case MessageSeverity.Warning:
                    return Colorize(Constants.EscapeColors.YELLOW, $"[WARNING: {time}] ");
                case MessageSeverity.Error:
                    return Colorize(Constants.EscapeColors.RED, $"[  ERROR: {time}] ");
                default:
                    return Colorize(Constants.EscapeColors.RED, $"[  ERROR: {time}] Unknown Severity!\n");
            }
        }

        public void RestoreTerminalState()
        {
            var content = _logger.GetLoggedContent();
            Console.Write(content);
        }

        #region Private Methods
        private string Colorize(string colorEscape, string msg)
        {
            return $"{Constants.EscapeColors.RESET}{colorEscape}{msg}{Constants.EscapeColors.RESET}";
        }

        private string ColorizeLine(string colorEscape, string msg)
        {
            return $"{Constants.EscapeColors.RESET}{colorEscape}{msg}{Constants.EscapeColors.RESET}\n";
        }
        #endregion

    }
}
