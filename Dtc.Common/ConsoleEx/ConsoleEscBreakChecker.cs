using System;

namespace Dtc.Common.ConsoleEx
{
    /// <summary>
    /// Pro moznost preruseni command line aplikace pres ESC
    /// Po stisknuti ESC jeste dotaz na exit a podle vysledku vraci bool.
    /// </summary>
    public class ConsoleEscBreakChecker
    {
        private bool _escBreakSignalized;

        /// <summary>
        /// Detekce preruseni pres ESC
        /// </summary>
        public bool BreakWithEsc()
        {
            if (!_escBreakSignalized)
            {
                if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape))
                {
                    // vycistim buffer
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                    // TODO mozna by bylo pekne to tisknout cervene?
                    Console.Write("Exit (y/n)?");
                    _escBreakSignalized = (Console.ReadKey(true).Key == ConsoleKey.Y);
                }
            }
            return _escBreakSignalized;
        }
    }
}
