using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace Hangman
{
    class Program
    {
        static Random randomSeed = new Random();
        static System.Text.StringBuilder incorrectLetters;
        static char[] correctLetters;
        static string secretWord;
        static string[] defaultWords = { "tunnelbana", "bussterminal", "apelsin", "tågstation", "metallica" };
        static int nrOfTries = 0;

        static bool AlreadyGuessed(string guess)
        {
            if (new string(correctLetters).Contains(guess))
                return true;
            else if (incorrectLetters.ToString().Contains(guess))
                return true;
            else
                return false;
        }

        static string SetWordToGuess()
        {
            return defaultWords[randomSeed.Next() % defaultWords.Length].ToLower();
        }

        static char[] InitCorrectArray(int length)
        {
            // create the new char[]
            char[] charArray = new char[length];
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = '*';
            }
            return charArray;
        }

        private static void UpdateCorrectLetters(char input)
        {
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == input)
                    correctLetters[i] = secretWord[i];
            }
        }
        

        static void PrintWinningresut()
        {
            WriteLine("**********   Grattis*************");
            WriteLine("Rätt gissat, ordet var " + secretWord + "\n Det krävdes " + nrOfTries + " försök");
            WriteLine("**************************************************");
        }

        private static void UpdateIncorrectLetters(string input)
        {
            incorrectLetters.Append(input);
        }

        static void PrintStatus()
        {
            WriteLine("\nOkänt ord: " + new string(correctLetters));
            WriteLine("Dessa bokstäver har du gissat på:" + incorrectLetters);
            WriteLine("Gjorda försök: " + nrOfTries);
            WriteLine("Ange enskild bokstav eller hela ordet");
        }
        
        private static void PrintLoss()
        {
            WriteLine("**************************************************");
            WriteLine("Sorry du har förbrukat alla dina försök");
            WriteLine("Du kom så här långt: " + new string(correctLetters));
            WriteLine("Det hemliga ordet var:" + secretWord);
            WriteLine("Känn dig hängd!");
            WriteLine("**************************************************");
        }

        static void Main(string[] args)
        {
            nrOfTries = 0;
            secretWord = SetWordToGuess();
            correctLetters = InitCorrectArray(secretWord.Length);
            incorrectLetters = new System.Text.StringBuilder();
            bool keepOnRunningBaby = true;
            WriteLine("Välkommen till hänga gubbar och gummor, god lycka!");
            while (keepOnRunningBaby)
            {
                PrintStatus();
                string input = ReadLine().ToLower(); ;
                
                if (input.Length == 0)
                    continue;

                if (!Regex.IsMatch(input, "^[a-öA-Ö]+$"))
                {
                    WriteLine("Ange endast bokstäver");
                    continue;

                }
                if (input.Length == 1)
                {
                    if (AlreadyGuessed(input))
                    {
                        WriteLine("Den bokstaven har du redan gissat på");
                        continue;
                    }

                    if (secretWord.Contains(input))
                        UpdateCorrectLetters(input[0]);
                    else
                        UpdateIncorrectLetters(input);
                }

                else
                {
                    if (input.Contains(secretWord))
                    {
                        correctLetters = secretWord.ToCharArray(); ;
                    }
                }
                nrOfTries++;
                if ((new string(correctLetters).Contains(secretWord)))
                {
                    PrintWinningresut();
                    break;
                }
                else if (nrOfTries == 10)
                {
                    PrintLoss();
                    break;
                }
            }

            WriteLine("Vill du spela igen? Tryck y + enter");
            if (ReadLine() == "y")
                Main(null);
            else
                System.Environment.Exit(0);
        }
    }
 }