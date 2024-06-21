using System.ComponentModel.Design;

namespace ConsoleApp8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] alphabetList = new char[26, 26];
            string Message = "";
            string EncryptedMessage = "";
            string Key = "";
            string KeyMask = "";

            List<char> recordedMessage = new List<char>();
            List<char> recordedKey = new List<char>();
            List<char> recordedkeyStream = new List<char>();
            List<char> recordedEncrypted = new List<char>();
            List<char> recordedDecrypted = new List<char>();

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
            Console.WriteLine("[1] Encrypt Message\n[2] Decrypt Message\n");
            Console.Write("Input: ");

            int choiceInput = int.Parse(Console.ReadLine());

            if (choiceInput == 1)
            {
                Console.WriteLine("\nChosen Mode: Encryption\n");


                if (File.Exists(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\Message.txt"))
                {
                    Console.Write("Detected File 'Encrypted Message.txt'!");
                    using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\Message.txt"))
                    {
                        Message = sr.ReadLine();
                    }
                }

                else
                {
                    Console.Write("Input your desired message: ");
                    Message = Console.ReadLine().ToUpper();

                    using (StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\Message.txt"))
                    {
                        sw.WriteLine(Message);
                    }
                }

                if (File.Exists("Key.txt"))
                {
                    Console.Write("Detected File 'Key.txt'!");

                    using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\Key.txt"))
                    {
                        Key = sr.ReadLine();
                    }

                }

                else
                {
                    Console.Write("Input your desired key: ");
                    Key = Console.ReadLine().ToUpper();

                    using (StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\Key.txt"))
                    {
                        sw.WriteLine(Message);
                    }
                }


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

                // ENCRYPTION

                for (int x = 0; x < recordedMessage.Count; x++)
                {
                    int index1 = (((int)(recordedMessage[x])) - 65);
                    int index2 = (((int)(recordedkeyStream[x])) - 65);

                    recordedEncrypted.Add(alphabetList[index1, index2]);
                }

                Console.Write("Encrypted Message: ");
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\EncryptedMessage.txt"))
                {
                    foreach (char letter in recordedEncrypted)
                    {
                        sw.Write(letter);
                        Console.Write(letter);
                    }
                }

                Console.Write("Key Mask: ");
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\KeyMask.txt"))
                {
                    foreach (char letter in recordedkeyStream)
                    {
                        sw.Write(letter);
                        Console.Write(letter);
                    }
                }
            }
            else if (choiceInput == 2)
            {
                Console.WriteLine("\nChosen Mode: Decryption\n");

                // CHECKING IF FILE EXISTS

                if (File.Exists(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\EncryptedMessage.txt"))
                {
                    using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\EncryptedMessage.txt"))
                    {
                        EncryptedMessage = sr.ReadLine();
                    }
                }

                else
                {
                    Console.Write("Input your encrypted message: ");
                    EncryptedMessage = Console.ReadLine().ToUpper();
                }

                // CHECKING FOR SPECIAL CHARACTERS

                for (int x = 0; x < EncryptedMessage.Length; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (EncryptedMessage[x] == 65 + y)
                        {
                            recordedEncrypted.Add(EncryptedMessage[x]);
                            break;
                        }
                    }
                }

                // CHECKING IF FILE EXISTS

                if (File.Exists(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\KeyMask.txt"))
                {
                    using (StreamReader sr = new StreamReader(@"C:\Users\Admin\source\repos\Simplified_Finals\ConsoleApp8\KeyMask.txt"))
                    {
                        KeyMask = sr.ReadLine();
                    }

                    for (int x = 0; x < KeyMask.Length; x++)
                    {
                        recordedkeyStream.Add(KeyMask[x]);
                    }
                }
                else
                {

                    Console.Write("Input your Key: ");
                    Key = Console.ReadLine().ToUpper();

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
                    for (int x = 0; x <= recordedEncrypted.Count - 1; x++)
                    {
                        if (index > recordedKey.Count - 1)
                        {
                            index = 0;
                        }
                        recordedkeyStream.Add(recordedKey[index]);
                        index++;
                    }
                }

                for (int x = 0; x < recordedkeyStream.Count; x++)
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (alphabetList[(((int)(recordedkeyStream[x])) - 65), y] == recordedEncrypted[x])
                        {
                            recordedDecrypted.Add((char)(y + 65));
                            break;
                        }
                    }
                }

                Console.WriteLine("\n");
                Console.Write("Decrypted Message: ");
                foreach (char letter in recordedDecrypted)
                {
                    Console.Write(letter);
                }

                Console.WriteLine();
                Console.Write("Key Mask: ");
                foreach (char letter in recordedkeyStream)
                {
                    Console.Write(letter);
                }
            }
            Console.WriteLine("\n");
        }
    }

}

    
