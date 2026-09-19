/*
 * EXCEEDING REQUIREMENTS:
 * The program only selects words that have not already been hidden, preventing
 * the program from randomly choosing the same hidden word multiple times.
 */
using System;

class Program
{
    static void Main(string[] args)
    {
        /*
         * EXCEEDING REQUIREMENTS:
         * The program only selects words that have not already been hidden.
         * This prevents the program from randomly choosing the same hidden
         * word multiple times.
         */

        Reference reference = new Reference("Proverbs", 3, 5, 6);

        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding. " +
                      "In all thy ways acknowledge him, and he shall direct thy paths.";

        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            // End after displaying the fully hidden scripture
            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            Console.Write("Press enter to continue or type 'quit' to finish: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);
        }
    }
}