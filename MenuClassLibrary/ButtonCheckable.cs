// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace MenuClassLibrary
{
    /// <summary>
    /// Class for button with checkbox.
    /// </summary>
    public class ButtonCheckable : Button
    {
        protected bool _isChecked;
        // Property without set, because check/uncheck only through internal logic.
        public bool IsChecked { get { return _isChecked; } }

        // Checked/Unchecked boxes fields.
        protected string _fieldChecked = "[\u221A]";
        protected string _fieldUnchecked = "[ ]";

        /// <summary>
        /// Method if button is being clicked.
        /// </summary>
        public override void Click()
        {
            // Checks/Unchecks button.
            _isChecked = !_isChecked;
        }

        /// <summary>
        /// Prints button.
        /// </summary>
        public override void Print()
        {
            Console.Write(_isChecked ? _fieldChecked : _fieldUnchecked);
            PrintButtonText();
        }

        public ButtonCheckable(string text)
        {
            Text = text;
        }

        public ButtonCheckable() { }
    }
}
