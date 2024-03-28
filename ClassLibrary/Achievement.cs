// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlayerJSONClassLibrary
{
    /// <summary>
    /// Represents achivement object as in given JSON.
    /// </summary>
    public class Achievement
    {
        // Events to notify when properties are updated.
        public event EventHandler<UpdatedEventArgs>? Updated;
        public event EventHandler<EventArgs>? PointsUpdated;

        // Private fields for achievement properties, that can be changed.
        string _name;
        string _description;
        int _points;

        // Unchangable Property.
        [JsonPropertyName("achievement_id")]
        public string Id { get; init; }

        [JsonPropertyName("achievement_name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                // Fires Updated event when Name is changed.
                OnFileUpdated();
            }
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                // Fires Updated event when Description is changed.
                OnFileUpdated();
            }
        }

        [JsonPropertyName("points")]
        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                // Raise Updated and PointsUpdated event when Points is changed.
                OnFileUpdated();
                PointsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Convert object to JSON string.
        /// </summary>
        /// <returns>JSON like string file.</returns>
        public string ToJSON()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Fires Updated event.
        /// </summary>
        private void OnFileUpdated()
        {
            Updated?.Invoke(this, new UpdatedEventArgs());
        }

        public Achievement(string id, string name, string description, int points)
        {
            Id = id;
            _name = name;
            _description = description;
            Points = points;
        }

        public Achievement() : this("", "", "", 0) { }
    }
}
