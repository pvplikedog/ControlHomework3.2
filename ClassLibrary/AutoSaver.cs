// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace PlayerJSONClassLibrary
{
    // Class responsible for automatically saving changes made to player data.
    public class AutoSaver
    {
        // Last time the data was changed.
        DateTime LastDataChangedTime { get; set; }
        // File name of the original JSON file.
        string OriginalJSONFileName { get; init; }
        // List of players whose data is being managed.
        List<Player> _players;

        /// <summary>
        /// Handler method for the Updated event of players and their achievements.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="updatedEventArgs">Event arguments containing information about the update.</param>
        public void OnFileUpdatedHandler(object sender, UpdatedEventArgs updatedEventArgs)
        {
            if ((updatedEventArgs.DateTime - LastDataChangedTime).TotalSeconds <= 15)
            {
                // Checks if 15 second was not passed as in TechTask.
                SaveTempJSONFIle();
            }

            LastDataChangedTime = DateTime.Now;
        }

        /// <summary>
        /// Saves changes to a temporary JSON file.
        /// </summary>
        private void SaveTempJSONFIle()
        {
            JsonWorker.WriteJson(_players, $"{OriginalJSONFileName}_tmp.json");
        }

        public AutoSaver(List<Player> players, string originalJSONFileName)
        {
            OriginalJSONFileName = originalJSONFileName;
            _players = players;

            foreach (Player player in players)
            {
                SubscribeToPlayerUpdateEvent(player);
            }

            // Protects from autosave after first file change.
            LastDataChangedTime = DateTime.Now.AddSeconds(-15);
        }

        public AutoSaver()
        {
            // Protects from null.
            OriginalJSONFileName = string.Empty;
            _players = new List<Player>();
        }

        /// <summary>
        /// Subscribes to update events for a player.
        /// </summary>
        /// <param name="player">The player to subscribe to.</param>
        public void SubscribeToPlayerUpdateEvent(Player player)
        {
            player.Updated += OnFileUpdatedHandler!;
            foreach (Achievement achievement in player.Achievements)
            {
                SubscribeToAchievementUpdateEvent(achievement);
            }
        }

        /// <summary>
        /// Subscribes to update events for an achievement.
        /// </summary>
        /// <param name="achievement">The achievement to subscribe to.</param>
        public void SubscribeToAchievementUpdateEvent(Achievement achievement)
        {
            achievement.Updated += OnFileUpdatedHandler!;
        }
    }
}
