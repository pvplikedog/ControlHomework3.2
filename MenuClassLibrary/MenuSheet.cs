// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace MenuClassLibrary
{
    /// <summary>
    /// Class that makes whole menu sheed, using all buttons, and handles control.
    /// </summary>
    public class MenuSheet
    {
        // Private fields for sheed properties
        private string[] _sheetHeadings;
        private Button[] _buttons;

        /// <summary>
        /// Prints menu in console.
        /// </summary>
        private void PrintMenu()
        {
            WriteHeading();

            Console.ForegroundColor = ConsoleColor.White;
            foreach (string heading in _sheetHeadings)
            {
                Console.WriteLine(heading);
            }

            Console.ResetColor();
            foreach (Button button in _buttons)
            {
                button.Print();
            }
        }

        /// <summary>
        /// Writes heading by who programm was made.
        /// </summary>
        private void WriteHeading()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string(' ', 40) + "by Anohin Anton BPI2311");
        }

        /// <summary>
        /// Handles menu control.
        /// </summary>
        public void HandleMenu()
        {
            ResetAllButtons();

            int curButton = 0;
            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;
                switch (keyPressed)
                {
                    case ConsoleKey.Escape:
                        // Protection from Escape button so menu prints correctly.
                        PrintMenu();
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Enter:
                        _buttons[curButton].Click();
                        break;
                    case ConsoleKey.DownArrow:
                        ChangeCurrentButton(ref curButton, +1);
                        break;
                    case ConsoleKey.UpArrow:
                        ChangeCurrentButton(ref curButton, -1);
                        break;
                }

                PrintMenu();
            }
        }

        /// <summary>
        /// Changes current button.
        /// </summary>
        /// <param name="curButton">Current button index by ref.</param>
        /// <param name="change">Increase or decrease index.</param>
        private void ChangeCurrentButton(ref int curButton, int change)
        {
            _buttons[curButton].Active = false;
            curButton = ((curButton + change) % _buttons.Length + _buttons.Length) % _buttons.Length;
            _buttons[curButton].Active = true;
        }

        /// <summary>
        /// Resets all buttons if menu is reopened.
        /// </summary>
        private void ResetAllButtons()
        {
            foreach (Button button in _buttons)
            {
                button.Active = false;

                if (button is ButtonChoosable)
                {
                    ((ButtonChoosable)button).UncheckAllButtonsButFirst();
                }
                else if (button is ButtonCheckable)
                {
                    // Unchecks checkable button if it's checked.
                    if (((ButtonCheckable)button).IsChecked)
                    {
                        button.Click();
                    }
                }
            }

            // Sets the first button in sheet active.
            _buttons[0].Active = true;

            PrintMenu();
        }

        /// <summary>
        /// Gets menu options from sheet(used to get info from checkable and choosable buttons).
        /// </summary>
        /// <returns>Bool array with is checked buttons info.</returns>
        public bool[] GetMenuOptions()
        {
            bool[] options = new bool[_buttons.Length];

            int current = 0;
            foreach (Button button in _buttons)
            {
                // If button is checkable or clickable.
                if (!(button is ButtonClickable))
                {
                    // If this button is in menu, obviously it's not null.
                    ButtonCheckable buttonCheck = (button as ButtonCheckable)!;
                    options[current] = buttonCheck!.IsChecked;
                }

                current++;
            }

            return options;
        }

        public MenuSheet(string[] sheetHeadings, Button[] buttons)
        {
            _sheetHeadings = sheetHeadings;
            _buttons = buttons;
        }

        public MenuSheet()
        {
            // Protection from null.
            _sheetHeadings = Array.Empty<string>();
            _buttons = Array.Empty<Button>();
        }
    }
}
