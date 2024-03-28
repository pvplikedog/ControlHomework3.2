// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using PlayerJSONClassLibrary;
using System.Text;

namespace ControlHomework32
{
    /// <summary>
    /// Class that generates some usefull stuff.
    /// </summary>
    public static class RandomGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// Generates random string given length from given alphabet.
        /// </summary>
        /// <param name="length">Length of string to generate.</param>
        /// <param name="chars">Alphabet from what generate.</param>
        /// <returns></returns>
        private static string RandomString(int length, string chars)
        {
            // Generates string using Enumerable.
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Generates random username using letters, digits and space.
        /// </summary>
        /// <param name="showGeneration">Should show generation animation.</param>
        /// <returns>Generated username.</returns>
        public static string GenerateRandomUsername(bool showGeneration = true)
        {
            if (showGeneration)
                ConsoleHandler.ShowGenerating();
            const string chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Username length from 5 to 14.
            return RandomString(random.Next(5, 15), chars);
        }

        /// <summary>
        /// Generates random sentence from random words.
        /// </summary>
        /// <param name="showGeneration">Should show generation animation.</param>
        /// <returns>Generated sentence.</returns>
        public static string GenerateRandomDescription(bool showGeneration = true)
        {
            if (showGeneration)
                ConsoleHandler.ShowGenerating();
            const string chars = "abcdefghijklmnopqrstuvwxyz";

            StringBuilder description = new StringBuilder();
            int amountOfWords = random.Next(1, 7);
            for (int i = 0; i < amountOfWords; i++)
            {
                // Word lenght from 3 to 7.
                description.Append(RandomString(random.Next(3, 8), chars));
                description.Append(", ");
            }

            // Removing last coma, adding dot and capitalize the first letter.
            description.Remove(description.Length - 2, 2);
            description.Append('.');
            description[0] = char.ToUpper(description[0]);

            return description.ToString();
        }

        /// <summary>
        /// Generates random name from letters.
        /// </summary>
        /// <param name="showGeneration">Should show generation animation.</param>
        /// <returns>Generated name.</returns>
        public static string GenerateRandomName(bool showGeneration = true)
        {
            if (showGeneration)
                ConsoleHandler.ShowGenerating();
            const string chars = "abcdefghijklmnopqrstuvwxyz";

            // Name length from 5 to 9.
            return RandomString(random.Next(5, 10), chars);
        }

        /// <summary>
        /// Generates Achievement with random fields using other RandomGenerator methods.
        /// </summary>
        /// <param name="showGeneration">Should show generation animation.</param>
        /// <returns>Generated achievement.</returns>
        public static Achievement GenerateRandomAchievement(bool showGeneration = true)
        {
            return new Achievement(GenerateRandomId(showGeneration), GenerateRandomName(false),
                GenerateRandomDescription(false), random.Next(0, 100));
        }

        /// <summary>
        /// Generates Player with random fields using other RandomGenerator methods.
        /// </summary>
        /// <returns>Generated player.</returns>
        public static Player GenerateRandomPlayer()
        {
            return new Player(GenerateRandomId(), GenerateRandomUsername(false), random.Next(1, 100), 0,
                GenerateAchievementList(), GenerateRandomName(false));
        }

        /// <summary>
        /// Generates list from 0 to 3 random achievements.
        /// </summary>
        /// <returns>Generated achievement list.</returns>
        private static List<Achievement> GenerateAchievementList()
        {
            List<Achievement> achievements = new List<Achievement>();

            int amountOfAchievements = random.Next(0, 4);
            for (int i = 0; i < amountOfAchievements; i++)
            {
                achievements.Add(GenerateRandomAchievement(false));
            }

            return achievements;
        }

        /// <summary>
        /// Generates random Id as in JSON using letters and digits.
        /// </summary>
        /// <param name="showGeneration">Should show generation animation.</param>
        /// <returns>Generated Id.</returns>
        private static string GenerateRandomId(bool showGeneration = true)
        {
            if (showGeneration)
                ConsoleHandler.ShowGenerating();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            // Making id same format as in give JSON.
            return $"{RandomString(8, chars)}-{RandomString(4, chars)}-{RandomString(4, chars)}" +
                $"-{RandomString(4, chars)}-{RandomString(12, chars)}";
        }
    }
}
