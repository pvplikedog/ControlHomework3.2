// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace MenuClassLibrary
{
    /// <summary>
    /// Class for buttons, which you can choose only one option.
    /// </summary>
    public class ButtonChoosable : ButtonCheckable
    {
        // Link for all choosable buttons in current menu.
        ButtonChoosable[] _buttonsChoosable;

        public ButtonChoosable[] ButtonsChoosable { set { _buttonsChoosable = value; } }

        /// <summary>
        /// Method if button is being clicked.
        /// </summary>
        public override void Click()
        {
            _isChecked = true;
            UpdateAllChoosableButtons();
        }

        /// <summary>
        /// Unchecks all other linked buttons but current one.
        /// </summary>
        private void UpdateAllChoosableButtons()
        {
            foreach (ButtonChoosable button in _buttonsChoosable)
            {
                if (button != this)
                    button._isChecked = false;
            }
        }

        /// <summary>
        /// Resets all checked options(sets check for first button).
        /// </summary>
        public void UncheckAllButtonsButFirst()
        {
            _buttonsChoosable[0].Click();
        }

        /// <summary>
        /// Static method, that links all choosable buttons with their list.
        /// </summary>
        /// <param name="buttonsList">List of buttons, which should be linked.</param>
        public static void LinkAllButtons(ButtonChoosable[] buttonsList)
        {
            buttonsList[0]._isChecked = true;
            foreach (ButtonChoosable button in buttonsList)
            {
                button.ButtonsChoosable = buttonsList;
            }
        }

        public ButtonChoosable(string text) : base(text)
        {
            _fieldChecked = "(o)";
            _fieldUnchecked = "( )";

            // Linking atleast this button to protect from null list.
            _buttonsChoosable = new ButtonChoosable[] { this };
        }

        public ButtonChoosable()
        {
            _buttonsChoosable = new ButtonChoosable[] { this };
        }
    }
}
