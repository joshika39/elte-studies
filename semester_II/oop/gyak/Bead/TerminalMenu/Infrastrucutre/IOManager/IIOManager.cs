using Infrastrucutre;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucutre.IOManager
{
    public interface IIOManager
    {
        /// <summary>
        /// This method prints out the details of the client machine and the creator of the project.
        /// </summary>
        /// <param name="githubUsername">The github username of the creator.</param>
        /// <param name="creatorName">The full name of the creator.</param>
        /// <param name="creatorID">The ID or neptun code of the creator.</param>
        void PrintSystemDetails(string githubUsername, string creatorName, string creatorID);


        void Write(MessageSeverity severity, string msg);
        void Write(string colorEscape, string msg);

        void WriteLine(string colorEscape, string msg);
        void WriteLine(string msg);

        delegate bool TryParseHandler<T>(string value, out T result);
        T ReadLine<T>(TryParseHandler<T> handler);
        T ReadLine<T>(string prompt, TryParseHandler<T> handler);

        void RestoreTerminalState();
    }
}
