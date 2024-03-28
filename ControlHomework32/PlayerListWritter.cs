// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using PlayerJSONClassLibrary;
using System.Text;

namespace ControlHomework32
{
    /// <summary>
    /// Provides methods for displaying a list of players in a formatted manner.
    /// </summary>
    public static class PlayerListWritter
    {
        static ConsoleColor _currentColor;

        /// <summary>
        /// Displays the list of players in beautiful table.
        /// </summary>
        /// <param name="players">The list of players to display.</param>
        public static void ShowPlayers(List<Player> players)
        {
            WritePlayersList(players);

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("return");
            Console.ResetColor();

            while (true)
            {
                // Waiting for user to press return button.
                ConsoleKey keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Writes the list of players to the console.
        /// </summary>
        /// <param name="players">The list of players to write.</param>
        private static void WritePlayersList(List<Player> players)
        {
            WriteHeading();
            foreach (Player player in players)
            {
                // Switch color for each line for more readable view.
                _currentColor = (Console.ForegroundColor == ConsoleColor.Gray ?
                    ConsoleColor.DarkGray : ConsoleColor.Gray);
                WritePlayer(player);
            }
        }

        /// <summary>
        /// Writes a player's information to the console.
        /// </summary>
        /// <param name="player">The player whose information to write.</param>
        private static void WritePlayer(Player player)
        {
            WriteInWhite("│");
            WriteWordInSpecificLen(player.Id, "  player_id  ".Length);
            WriteWordInSpecificLen(player.Username, "   username   ".Length);
            WriteWordInSpecificLen(player.Level.ToString(), "level".Length);
            WriteWordInSpecificLen(player.GameScore.ToString(), "game_score".Length);
            WriteWordInSpecificLen(player.Guild, "  guild  ".Length);
            WriteWordInSpecificLen(player.Achievements.Count().ToString(), "achievments".Length);
            WriteWordInSpecificLen(GetAchievmentsNames(player), "     achievments_names     ".Length);
            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Writes a string in white color to the console.
        /// </summary>
        /// <param name="stringToWrite">The string to write.</param>
        private static void WriteInWhite(string stringToWrite)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(stringToWrite);
            Console.ForegroundColor = _currentColor;
        }

        /// <summary>
        /// Writes the heading for the player list to the console.
        /// </summary>
        private static void WriteHeading()
        {
            ConsoleHandler.WriteHeading();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("│   player_id   │    username    │ level │ game_score │   guild   │");
            Console.WriteLine(" achievments │      achievments_names      │");
            Console.Write("┼───────────────┼────────────────┼───────┼────────────┼───────────┼");
            Console.WriteLine("─────────────┼─────────────────────────────┼");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Gets the names of achievements for a player.
        /// </summary>
        /// <param name="player">The player whose achievements to get.</param>
        /// <returns>The names of the player's achievements.</returns>
        private static string GetAchievmentsNames(Player player)
        {
            if (player.Achievements.Count == 0)
                return "";

            StringBuilder adhievementNames = new StringBuilder();
            foreach (Achievement achievement in player.Achievements)
            {
                adhievementNames.Append(achievement.Name);
                adhievementNames.Append(", ");
            }

            // Remove last coma.
            adhievementNames.Remove(adhievementNames.Length - 2, 2);
            return adhievementNames.ToString();
        }

        /// <summary>
        /// Writes a word to the console with a specific length.
        /// </summary>
        /// <param name="word">The word to write.</param>
        /// <param name="len">The desired length of the word.</param>
        private static void WriteWordInSpecificLen(string word, int len)
        {
            Console.Write(" ");

            if (word.Length < len)
            {
                // If the word is shorter, pad it with spaces.
                Console.Write(word);
                for (int i = 0; i < len - word.Length; i++)
                {
                    Console.Write(" ");
                }
            }
            else
            {
                // If the word is longer, truncate it and add ellipsis at the end.
                for (int i = 0; i < len - 3; i++)
                {
                    Console.Write(word[i]);
                }
                Console.Write("...");
            }

            WriteInWhite(" │");
        }
    }
}
