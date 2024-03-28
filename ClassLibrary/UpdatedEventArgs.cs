// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace PlayerJSONClassLibrary
{
    /// <summary>
    /// Represents event arguments for updated events.
    /// </summary>
    public class UpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the date and time of the update.
        /// </summary>
        public DateTime DateTime { get; init; }

        public UpdatedEventArgs(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public UpdatedEventArgs() : this(DateTime.Now) { }
    }
}
