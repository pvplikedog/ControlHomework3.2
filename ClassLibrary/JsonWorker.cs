// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using System.Text.Json;

namespace PlayerJSONClassLibrary
{
    /// <summary>
    /// Static class for JSON serialization and deserialization.
    /// </summary>
    public static class JsonWorker
    {
        /// <summary>
        /// Writes player data to a JSON file.
        /// </summary>
        /// <param name="players">The list of players to write.</param>
        /// <param name="path">The file path where the JSON data will be written.</param>
        public static void WriteJson(List<Player> players, string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(players, new JsonSerializerOptions
            {
                // Make the JSON output as in given file.
                WriteIndented = true
            }));
        }

        /// <summary>
        /// Reads player data from a JSON file.
        /// </summary>
        /// <param name="path">The file path from which to read the JSON data.</param>
        /// <returns>The list of players read from the JSON file.</returns>
        public static List<Player> ReadJson(string path)
        {
            string jsonText = File.ReadAllText(path);
            List<Player> players = JsonSerializer.Deserialize<List<Player>>(jsonText)!;

            CheckIfDataValid(players);

            return players;
        }

        /// <summary>
        /// Checks if the deserialized player data is valid.
        /// </summary>
        /// <param name="players">The list of players to validate.</param>
        /// <exception cref="FieldAccessException">Thrown when the player data is invalid.</exception>
        private static void CheckIfDataValid(List<Player> players)
        {
            foreach (Player player in players)
            {
                // Check if the player object or its properties are null or empty.
                // I'm ok if there was no integer properties, I replace it by 0.
                if (player == null || string.IsNullOrWhiteSpace(player.Id) ||
                    string.IsNullOrEmpty(player.Username) || string.IsNullOrEmpty(player.Guild) ||
                    player.Achievements == null)
                {
                    // I feel like this exception fits situation well.
                    throw new FieldAccessException();
                }

                foreach (Achievement achievement in player.Achievements)
                {
                    // Check if the achievement object or its properties are null or empty.
                    // I'm ok if there was no integer properties, I replace it by 0.
                    if (achievement == null || string.IsNullOrWhiteSpace(achievement.Id) ||
                        string.IsNullOrWhiteSpace(achievement.Description) ||
                        string.IsNullOrEmpty(achievement.Name))
                    {
                        // I feel like this exception fits situation well.
                        throw new FieldAccessException();
                    }
                }
            }
        }
    }
}
