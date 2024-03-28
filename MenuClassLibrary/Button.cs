// ControlHomework 3.2 option 13 by Anohin Anton BPI2311.

namespace MenuClassLibrary
{
    /// <summary>
    /// Abstract class, which is used as Parent for other Buttons.
    /// </summary>
    public abstract class Button
    {
        // Property that holds information is button Active.
        public bool Active { get; set; }
        // Propert that holds button text.
        public string Text { get; init; }

        public abstract void Print();
        public abstract void Click();

        /// <summary>
        /// Prints button, using information is it active or not.
        /// </summary>
        protected void PrintButtonText()
        {
            if (Active)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(Text);
            Console.ResetColor();
        }

        // Default constructor to make sure Text is not null.
        public Button()
        {
            Text = string.Empty;
        }
    }
}
