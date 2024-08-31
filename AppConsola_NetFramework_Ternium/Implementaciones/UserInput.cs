using AppConsola_NetFramework_Ternium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium.Implementaciones
{
    public class UserInput : IUserInput
    {
        public string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public int GetIntInput(string prompt, ILogManager logManager)
        {
            Console.WriteLine(prompt);
            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                logManager.LogError("La entrada no es un número válido.");
                throw new InvalidOperationException("La entrada no es un número válido.");
            }
            return value;
        }

        public bool GetBoolInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().ToUpper() == "S";
        }
    }
}
