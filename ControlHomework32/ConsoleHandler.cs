// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace ControlHomework32
{
    /// <summary>
    /// The class that is responsible for interacting with the console.
    /// </summary>
    internal class ConsoleHandler
    {
        /// <summary>
        /// Clears the console/
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
        }

        /// <summary>
        /// Says goodbye to user and exits application.
        /// </summary>
        public static void Exit()
        {
            WriteHeading();
            Console.WriteLine("Thank you for your time, goodbye!");
            Environment.Exit(0);
        }

        /// <summary>
        /// Gets not empty string from console input.
        /// </summary>
        /// <param name="message">Message to ask user.</param>
        public static string GetStringFromUser(string message)
        {
            while (true)
            {
                WriteHeading();
                Console.Write(message);
                string input = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("You have to input atleast something!");
                    Thread.Sleep(1000);
                    continue;
                }
                return input;
            }
        }

        /// <summary>
        /// Gets not integer from console input.
        /// </summary>
        /// <param name="message">Message to ask user.</param>
        public static int GetIntFromUser(string message)
        {
            while (true)
            {
                WriteHeading();
                Console.Write(message);
                string input = Console.ReadLine()!;
                int inputInt;
                if (!int.TryParse(input, out inputInt))
                {
                    Console.WriteLine("There is something wrong! Enter an integer");
                    Thread.Sleep(1000);
                    continue;
                }
                return inputInt;
            }
        }

        /// <summary>
        /// Gets file path from user.
        /// </summary>
        public static string GetFilePathFromUser()
        {
            string nPath = GetStringFromUser("Input absolute JSON file path: ");

            // Check if user input path with quotes.
            if (nPath.Length >= 2 && nPath[0] == '"' && nPath[^1] == '"')
            {
                nPath = nPath[1..^1];
            }

            return nPath;
        }

        /// <summary>
        /// Writes heading.
        /// </summary>
        public static void WriteHeading()
        {
            ClearConsole();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string(' ', 40) + "by Anohin Anton BPI2311");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints message to user.
        /// </summary>
        /// <param name="message">Message to print.</param>
        /// <param name="milesecondsToRead">Wait time for user to read(default 2.5s).</param>
        public static void PrintMessage(string message, int milesecondsToRead = 2500)
        {
            WriteHeading();
            Console.WriteLine(message);
            Thread.Sleep(milesecondsToRead);
            ClearConsole();
        }

        /// <summary>
        /// Shows generating animation.
        /// </summary>
        /// <param name="millisecondsToWait">Time to show(default 1.7s)</param>
        public static void ShowGenerating(int millisecondsToWait = 1700)
        {
            DateTime curTime = DateTime.Now;
            int counter = 0;

            while ((DateTime.Now - curTime).TotalMilliseconds < millisecondsToWait)
            {
                WriteHeading();
                Console.Write("Generating...");

                switch (counter % 4)
                {
                    case 0:
                        Console.WriteLine("/");
                        break;
                    case 1:
                        Console.WriteLine("-");
                        break;
                    case 2:
                        Console.WriteLine("\\");
                        break;
                    case 3:
                        Console.WriteLine("|");
                        break;
                }

                counter++;
                Thread.Sleep(90);
            }
        }
    }
}
