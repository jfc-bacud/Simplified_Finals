using System.ComponentModel.Design;

namespace ConsoleApp8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] alphabetList = new char[26, 26];

            List<char> recordedMessage = new List<char>();
            List<char> recordedKey = new List<char>();
            List<char> recordedkeyStream = new List<char>();
            List<char> processedMessage = new List<char>();

            // MATRIX CREATION
            for (int x = 0; x < 26; x++)
            {
                int index = 0;
                for (int y = 0; y < 26; y++)
                {
                    if (x == 0) // IF AT FIRST ROW
                    {
                        alphabetList[x, y] = (char)(65 + y);
                    }
                    else if (x > 0) // IF PAST FIRST ROW
                    {
                        if ((x + y) < 26) // INPUTS SUCCEEDING LETTERS
                        {
                            alphabetList[x, y] = alphabetList[0, x + y];
                        }
                        else // IF ALPHABET RUNS OUT, RETURNS TO START
                        {
                            alphabetList[x, y] = alphabetList[0, index];
                            index++;
                        }
                    }
                }
            }

            Console.WriteLine("==== SIMPLE MESSAGE ENCRYPTER / DECRYPTER ====\n");
            Console.WriteLine("[1] Encrypt a Message\n[2] Decrypt a]Message\n");
            Console.Write("Input: ");

            int choiceInput = int.Parse(Console.ReadLine());

            if (choiceInput == 1) // ENCRYPTION
            {
                Console.WriteLine("\nChosen Mode: Encryption\n");
                Console.Write("Input your desired message: ");
                string Message = Console.ReadLine().ToUpper();

                Console.Write("Input your desired key: ");
                string Key = Console.ReadLine().ToUpper();

                // CHECKING FOR SPECIAL CHARACTERS
                for (int x = 0; x < Message.Length; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (Message[x] == 65 + y)
                        {
                            recordedMessage.Add(Message[x]);
                            break;
                        }
                    }
                }

                for (int x = 0; x < Key.Length; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (Key[x] == 65 + y)
                        {
                            recordedKey.Add(Key[x]);
                            break;
                        }
                    }
                }

                // CREATION OF KEY MASK
                int Index = 0;
                for (int x = 0; x <= recordedMessage.Count - 1; x++)
                {
                    if (Index > recordedKey.Count - 1)
                    {
                        Index = 0;
                    }
                    recordedkeyStream.Add(recordedKey[Index]);
                    Index++;
                }

                Console.Write("\nGenerated Key Mask: ");
                foreach (char letter in recordedkeyStream)
                {
                    Console.Write(letter);
                }

                // ENCRYPTION
                for (int x = 0; x < recordedMessage.Count; x++)
                {
                    int index1 = (((int)(recordedMessage[x])) - 65);
                    int index2 = (((int)(recordedkeyStream[x])) - 65);

                    processedMessage.Add(alphabetList[index1, index2]);
                }

                Console.Write("\nProcessed Message: ");
                foreach (char letter in processedMessage)
                {
                    Console.Write(letter);
                }
            }

            else if (choiceInput == 2)  // DECRYPTION
            {
                Console.WriteLine("\nChosen Mode: Decryption\n");

                Console.Write("Input your encrypted message: ");
                string Message = Console.ReadLine().ToUpper();

                // CHECKING FOR SPECIAL CHARACTERS
                for (int x = 0; x < Message.Length; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (Message[x] == 65 + y)
                        {
                            recordedMessage.Add(Message[x]);
                            break;
                        }
                    }
                }

                Console.Write("Input your Key: ");
                string Key = Console.ReadLine().ToUpper();

                // CHECKING FOR SPECIAL CHARACTERS
                for (int x = 0; x < Key.Length; x++)
                    {
                     for (int y = 0; y < 26; y++)
                      {
                        if (Key[x] == 65 + y)
                        {
                                recordedKey.Add(Key[x]);
                                break;
                        }
                     }
                }

                // CREATION OF KEY MASK FROM KEY
                int index = 0;
                for (int x = 0; x <= recordedMessage.Count - 1; x++)
                {
                  if (index > recordedKey.Count - 1)
                  {
                    index = 0;
                  }
                  recordedkeyStream.Add(recordedKey[index]);
                  index++;
                }

                Console.Write("\nGenerated Key Mask: ");
                foreach (char letter in recordedkeyStream)
                {
                    Console.Write(letter);
                }

                for (int x = 0; x < recordedkeyStream.Count; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (alphabetList[(((int)(recordedkeyStream[x])) - 65), y] == recordedMessage[x])
                        {
                            processedMessage.Add((char)(y + 65));
                            break;
                        }
                    }
                }

                Console.Write("\nProcessed Message: ");
                foreach (char letter in processedMessage)
                {
                    Console.Write(letter);
                }
            }
            Console.WriteLine("\n");
        }
    }

}