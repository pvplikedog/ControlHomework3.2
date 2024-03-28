// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using PlayerJSONClassLibrary;
using System.Collections;

namespace ControlHomework32
{
    /// <summary>
    /// Represents a class responsible for managing a list of players.
    /// </summary>
    public class PlayerListWorker
    {
        /// <summary>
        /// Specifies the target property for sorting players.
        /// </summary>
        public enum SortTarget
        {
            player_id,
            username,
            level,
            game_score,
            guild,
            achievements_amount
        }

        /// <summary>
        /// Event raised when the player list is updated.
        /// </summary>
        public event EventHandler<UpdatedEventArgs>? Updated;

        public List<Player> Players { get; set; }

        public AutoSaver autoSaver { get; set; }
        
        public string OriginalFilePath { get; set; }

        /// <summary>
        /// Sorts the player list based on the specified target and optional reverse order.
        /// </summary>
        /// <param name="sortTarget">The target property for sorting.</param>
        /// <param name="reverse">True to sort in reverse order; otherwise, false.</param>
        public void SortPlayerList(SortTarget sortTarget, bool reverse)
        {
            switch (sortTarget)
            {
                // Sorting using Comparer.DefaultInvariant and lambda expressions for more compactable code.
                case SortTarget.player_id:
                    Players.Sort((player1, player2) =>
                    { return Comparer.DefaultInvariant.Compare(player1.Id, player2.Id); });
                    break;
                case SortTarget.username:
                    Players.Sort((player1, player2) =>
                    { return Comparer.DefaultInvariant.Compare(player1.Username, player2.Username); });
                    break;
                case SortTarget.level:
                    Players.Sort((player1, player2) =>
                    { return Comparer.DefaultInvariant.Compare(player1.Level, player2.Level); });
                    break;
                case SortTarget.game_score:
                    Players.Sort((player1, player2) =>
                    { return Comparer.DefaultInvariant.Compare(player1.GameScore, player2.GameScore); });
                    break;
                case SortTarget.guild:
                    Players.Sort((player1, player2) =>
                    { return Comparer.DefaultInvariant.Compare(player1.Guild, player2.Guild); });
                    break;
                case SortTarget.achievements_amount:
                    Players.Sort((player1, player2) =>
                    { return Comparer.DefaultInvariant.Compare(player1.Achievements.Count, player2.Achievements.Count); });
                    break;
            }

            if (reverse)
            {
                Players.Reverse();
            }
        }

        /// <summary>
        /// Retrieves a player from the list based on the specified player ID.
        /// </summary>
        /// <param name="id">The ID of the player to retrieve.</param>
        /// <returns>The player with the specified ID.</returns>
        public Player GetPlayerFromListById(string id)
        {
            foreach (Player player in Players)
            {
                if (player.Id == id)
                    return player;
            }
            // This scenario should never occur, but including it for completeness.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves an achievement from a player based on the specified achievement ID.
        /// </summary>
        /// <param name="id">The ID of the achievement to retrieve.</param>
        /// <param name="player">The player containing the achievement.</param>
        /// <returns>The achievement with the specified ID.</returns>
        public Achievement GetAchievementFromPlayerById(string id, Player player)
        {
            foreach (Achievement achievement in player.Achievements)
            {
                if (achievement.Id == id)
                    return achievement;
            }
            // This scenario should never occur, but including it for completeness.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a player from the list.
        /// </summary>
        /// <param name="playerToRemove">The player to remove.</param>
        public void RemovePlayerFromList(Player playerToRemove)
        {
            Players.Remove(playerToRemove);
            Updated?.Invoke(this, new UpdatedEventArgs());
            ConsoleHandler.PrintMessage("Player successfully deleted!", 1700);
        }

        /// <summary>
        /// Removes an achievement from a player's list of achievements.
        /// </summary>
        /// <param name="achievement">The achievement to remove.</param>
        /// <param name="player">The player containing the achievement.</param>
        public void RemoveAchievementFromPlayer(Achievement achievement, Player player)
        {
            player.Achievements.Remove(achievement);
            Updated?.Invoke(this, new UpdatedEventArgs());
            ConsoleHandler.PrintMessage("Achievement successfully deleted!", 1700);
        }

        /// <summary>
        /// Adds an achievement to a player's list of achievements.
        /// </summary>
        /// <param name="achievement">The achievement to add.</param>
        /// <param name="player">The player to whom the achievement is added.</param>
        public void AddAchievementToPlayer(Achievement achievement, Player player)
        {
            player.Achievements.Add(achievement);
            autoSaver.SubscribeToAchievementUpdateEvent(achievement);
            Updated?.Invoke(this, new UpdatedEventArgs());
            ConsoleHandler.PrintMessage("New achievement was generated! You can now edit it.");
        }

        /// <summary>
        /// Adds a player to the player list.
        /// </summary>
        /// <param name="newPlayer">The player to add.</param>
        public void AddPlayerToPlayerList(Player newPlayer)
        {
            Players.Add(newPlayer);
            autoSaver.SubscribeToPlayerUpdateEvent(newPlayer);
            Updated?.Invoke(this, new UpdatedEventArgs());
            ConsoleHandler.PrintMessage("New player was generated! You can now edit it.");
        }

        /// <summary>
        /// Try to read JSON from filepath given by user.
        /// </summary>
        /// <returns>True if file successfully readen, False otherwise.</returns>
        private bool ReadJSONFromFile()
        {
            try
            {
                string path = ConsoleHandler.GetFilePathFromUser();
                OriginalFilePath = path;
                Players = JsonWorker.ReadJson(path);
                autoSaver = new AutoSaver(Players, Path.GetFileNameWithoutExtension(path));
                Updated += autoSaver.OnFileUpdatedHandler!;
                return true;
            }
            catch (FieldAccessException)
            {
                Console.WriteLine("Something wrong with data in the file, please fix it!");
                return false;
            }
            catch (IOException)
            {
                Console.WriteLine("Something went wrong when trying to open the file.");
                Console.WriteLine("Make sure everything is ok and repeat!");
                return false;
            }
            catch
            {
                Console.WriteLine("Something went wrong. Make sure everything is ok and repeat!");
                return false;
            }
        }

        public PlayerListWorker(bool shouldAskToGenerate)
        {
            // Protects from null.
            Players = new List<Player>();
            autoSaver = new AutoSaver();
            OriginalFilePath = string.Empty;

            while (!ReadJSONFromFile())
            {
                // Asks for file untill it successfully read.
                Thread.Sleep(2500);
            }
        }

        public PlayerListWorker()
        {
            // Protects from null.
            Players = new List<Player>();
            autoSaver = new AutoSaver();
            OriginalFilePath = string.Empty;
        }
    }
}
