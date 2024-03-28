// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace MenuClassLibrary
{
    /// <summary>
    /// Class for button, which you can click and event should be triggered.
    /// </summary>
    public class ButtonClickable : Button
    {
        // Event that fires on click. Using default EventArgs, because no need in some complex logic.
        public event EventHandler<EventArgs>? OnClick;

        /// <summary>
        /// If button is being clicked.
        /// </summary>
        public override void Click()
        {
            Active = false;
            OnClick?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Prints button.
        /// </summary>
        public override void Print()
        {
            PrintButtonText();
        }

        public ButtonClickable(string text, EventHandler<EventArgs> onClickMethod)
        {
            Text = text;
            OnClick += onClickMethod;
        }

        public ButtonClickable() { }
    }
}
