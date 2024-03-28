// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlayerJSONClassLibrary
{
    /// <summary>
    /// Represents a player object as in given JSON.
    /// </summary>
    public class Player
    {
        // Events to notify when properties are updated.
        public event EventHandler<UpdatedEventArgs>? Updated;

        // Private fields for player properties, that can be changed.
        string _username;
        int _level;
        int _gameScore;
        string _guild;
        List<Achievement> _achievements;

        // Unchangable Property.
        [JsonPropertyName("player_id")]
        public string Id { get; init; }

        [JsonPropertyName("username")]
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                // Fires Updated event when Userame is changed.
                _username = value;
                OnFileUpdated();
            }
        }

        [JsonPropertyName("level")]
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                // Fires Updated event when Level is changed.
                _level = value;
                OnFileUpdated();
                // Not as in TechTask, but since level is also used in game_points update, I feel like it's needed.
                PointsUpdatedHandler(this, EventArgs.Empty);
            }
        }

        [JsonPropertyName("game_score")]
        public int GameScore
        {
            get
            {
                return _gameScore;
            }
            init
            {
                _gameScore = value;
            }
        }

        [JsonPropertyName("achievements")]
        public List<Achievement> Achievements
        {
            get
            {
                return _achievements;
            }
            set
            {
                _achievements = value;
                SubscribeToAllAchievements();
            }
        }

        [JsonPropertyName("guild")]
        public string Guild
        {
            get
            {
                return _guild;
            }
            set
            {
                // Fires Updated event when Guild is changed.
                _guild = value;
                OnFileUpdated();
            }
        }

        /// <summary>
        /// Converts the player object to its JSON representation.
        /// </summary>
        /// <returns>The JSON representation of the player object.</returns>
        public string ToJSON()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Raises the Updated event when player data is updated.
        /// </summary>
        private void OnFileUpdated()
        {
            Updated?.Invoke(this, new UpdatedEventArgs());
        }

        /// <summary>
        /// Handles the PointsUpdated event for achievements and updates the game score accordingly.
        /// </summary>
        private void PointsUpdatedHandler(object? sender, EventArgs e)
        {
            UpdateGameScore();
        }

        /// <summary>
        /// Updates the game score based on achievements and level.
        /// </summary>
        private void UpdateGameScore()
        {
            if (Achievements is null)
            {
                _gameScore = 0;
                return;
            }

            int sumPoints = 0;
            foreach (Achievement achievement in Achievements)
            {
                sumPoints += achievement.Points;
            }
            _gameScore = sumPoints * Level;
        }

        /// <summary>
        /// Subscribes to the PointsUpdated event for all achievements.
        /// </summary>
        private void SubscribeToAllAchievements()
        {
            if (_achievements is null)
            {
                return;
            }
            foreach (Achievement achievement in _achievements)
            {
                achievement.PointsUpdated += PointsUpdatedHandler;
            }
        }

        public Player(string id, string username, int level, int gamescore, List<Achievement> achievements, string guild)
        {
            Id = id;
            _username = username;
            _level = level;
            GameScore = gamescore;
            _achievements = achievements;
            SubscribeToAllAchievements();
            _guild = guild;
            UpdateGameScore();
        }

        public Player() : this("", "", 0, 0, new List<Achievement>(), "") { }
    }
}