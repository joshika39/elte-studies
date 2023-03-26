using System;

namespace Infrastructure.IOManager
{
    public interface IIOManager
    {
        /// <summary>
        /// This method prints out the details of the client machine and the creator of the project.
        /// </summary>
        /// <param name="githubUsername">The github username of the creator.</param>
        /// <param name="creatorName">The full name of the creator.</param>
        /// <param name="creatorId">The ID or neptun code of the creator.</param>
        void PrintSystemDetails(string githubUsername, string creatorName, string creatorId);

        /// <summary>
        /// Writes a single line with a certain severity (color), and time, while logging the console output, for later restore.
        /// </summary>
        /// <param name="severity">Tells what is the severity of the message: info, warning, error, success.</param>
        /// <param name="msg">The printed message.</param>
        void Write(MessageSeverity severity, string msg);
        
        /// <summary>
        /// Writes a single line only, while logging the console output, for later restore.
        /// </summary>
        /// <param name="msg">The printed message.</param>
        void Write(string msg);

        /// <summary>
        /// Writes a single line with a <b>new line at the end</b> with a certain severity (color), and time, while logging the console output, for later restore.
        /// </summary>
        /// <param name="severity">Tells what is the severity of the message: info, warning, error, success.</param>
        /// <param name="msg">The printed message.</param>
        void WriteLine(MessageSeverity severity, string msg);
        
        /// <summary>
        /// Writes a single line with a <b>new line at the end</b>, while logging the console output, for later restore.
        /// </summary>
        /// <param name="msg">The printed message.</param>
        void WriteLine(string msg);

        /// <summary>
        /// Used to specify the parser method for the <c>ReadLine()</c> method.
        /// </summary>
        /// <typeparam name="T">Can be any ValueType.</typeparam>
        delegate bool TryParseHandler<T>(string value, out T result);
        
        /// <summary>
        /// This is the core method. It is using <c>Console.ReadLine</c> method, to get the data
        /// </summary>
        /// <param name="handler">A type parser method</param>
        /// <example>
        /// This shows a conversion to <c>int</c>.
        /// <code>var age = ReadLine&lt;int&gt;(int.TryParse);</code>
        /// </example>
        /// <typeparam name="T">The target value converted from <c>Console.Readline()</c></typeparam>
        /// <seealso cref="Console.ReadLine()"/>
        /// <returns>The converted Type passed with the type parameter.</returns>
        /// <exception cref="InvalidOperationException">Exception being thrown when the conversion is unsuccessful.</exception>
        T ReadLine<T>(TryParseHandler<T> handler);
        
        
        T ReadLine<T>(TryParseHandler<T> handler, out bool isConverted);
        
        T ReadLine<T>(TryParseHandler<T> handler, string prompt);
        T ReadLine<T>(TryParseHandler<T> handler, string prompt, string errorMsg);

        void RestoreTerminalState();
    }
}
