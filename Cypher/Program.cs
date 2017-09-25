using System;
using System.IO;

namespace CeaserCypher
{
	/// <summary>
	/// This program implements the most basic message encryption technique.  
	/// It is called a Ceaser Cypher (See Wikipedia for more details)
	/// 
	/// Your job is to read and understand this code.
	/// Annotate this example code by commenting on every line that you have not 
	/// seen before in this class.  There are many new things here.  Tell me
	/// what new methods do, and why (you think) they are used here.
	/// 
	/// Run the program, give it some example input, see what happens.
	/// 
	/// There are new methods, opperators, and implicit data conversions here.
	/// 
	/// Make sure to read the rubric on Schoology so you know you have 
	/// covered all the points I'm looking for!
	/// 
	/// </summary>
	class MainClass
	{

		/// <summary>
		/// The alphabet in lower case.
		/// </summary>
		public static String alphabet = "abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// The alphabet in upper case.
		/// </summary>
		public static String ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Encrypts the character using the given key in the given character set.
		/// </summary>
		/// <returns>The character.</returns>
		/// <param name="charSet">Character set which the given character is a part of.</param>
		/// <param name="ch">The character.</param>
		/// <param name="key">Key for the cypher.</param>
		public static char EncryptCharacter(String charSet, char ch, int key)   //sets parameters for method
		{
			if (charSet.IndexOf(ch) >= 0)
			{
				// Get the position of the character ch in the alphabet.
				int charNumber = charSet.IndexOf(ch);

				// add the key to the char number (this is the Ceaser Cipher step!)
				// the % 26 wraps around the alphabet so 'x' + 5 => 'c'
				int encryptedCharNumber = (charNumber + key) % charSet.Length;

                //allows user to enter a negative key by decided if it is negative and if it is, adding 26 
                if (encryptedCharNumber < 0)
                {
                    encryptedCharNumber += 26; 
                }

				// get the character associated with the encrypted character number in the lowercase alphabet.
				char encryptedChar = charSet[encryptedCharNumber];

				// stick that encrypted character on the end of the encryptedMessage.
				return encryptedChar;
			}
			else
			{
				// ignore it if they character is not a part of the given character set
                // you have to return every parameter you put in 
				return ch;
			}
		}

		/// <summary>
		/// Reads text from file and returns it as a String.
		/// </summary>
		/// <returns>The contents from file.</returns>
		/// <param name="fileName">Full file path.</param>
		public static String ReadFromFile(String fileName)
		{
			String contents = "";
			StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open));

			while (!reader.EndOfStream)
			{
				contents += String.Format("{0}{1}", reader.ReadLine(), Environment.NewLine);
			}

			reader.Close();

			return contents;
		}

        //this method reverses the encryption process to decrypt a code with the same key it was encrypted with 
		public static char DecryptCharacter(String charSet, char ch, int key)
		{
            if (charSet.IndexOf(ch) >= 0)
            {
                int charNumber = charSet.IndexOf(ch);

                //instead of added the key to the charNumber, it is subtracted 
				int encryptedCharNumber = (charNumber - key) % charSet.Length;

				if (encryptedCharNumber < 0)
				{
					encryptedCharNumber += 26;
				}

                char encryptedChar = charSet[encryptedCharNumber];

				return encryptedChar;
            }else{
                return ch;
            }

		}

		public static void Main(string[] args)
		{
            // ask the user if they would like to encrypt or decrypt a message 
            Console.WriteLine("Would you like to encyrpt or decrypt a message?");
            string c = Console.ReadLine();

            // if the first letter of the string the user entered starts with an 'e' (i.e. "encrypt"), then run the encryptCharacter method
            if (c[0] == 'e')
            {
				// Get a secret message from the user.
				Console.WriteLine("Enter a message to encrypt:");
				String message = Console.ReadLine();

                // Get the KEY with which to encrypt this secret message.  This must be an INTEGER. (can be positive or negative) 
				Console.Write("Enter the KEY to encrypt this message with >> ");
				int key = Convert.ToInt32(Console.ReadLine());

				// Make a place to store the encrypted message while I'm working on building it.
				String encryptedMessage = "";

				// Iterate through the message that the user typed one character at a time.
				foreach (char ch in message.ToCharArray())
				{
					// if this is a lowercase letter, encrypt the character using the lowercase alphabet string.
					// IndexOf returns -1 if the character does not appear in the string!
					if (alphabet.IndexOf(ch) >= 0)
					{

						// stick that encrypted character on the end of the encryptedMessage.
						encryptedMessage += EncryptCharacter(alphabet, ch, key);
					}

					// Do the same thing, if the character is a Capital letter.
					else if (ALPHABET.IndexOf(ch) >= 0)
					{
						encryptedMessage += EncryptCharacter(ALPHABET, ch, key);
					}

                    // If the character is neither a capital nor lowercase, then ignore it and don't try to encrypt. (i.e. a space or punctuation)
					else
					{
						encryptedMessage += ch;
					}


				}


				// Print the encrypted message!
				Console.WriteLine("_________________________________________________");
				Console.WriteLine("Your encrypted message is:\n\n{0}", encryptedMessage);
				Console.ReadKey();

            }
            else if (c[0] == 'd'){
                
				// Get a secret message from the user.
				Console.WriteLine("Enter a message to decrypt:");
				String message = Console.ReadLine();

				// Get the KEY with which to encrypt this secret message.  This must be an INTEGER.
				Console.Write("Enter the KEY this message was encrypted with >> ");
				int key = Convert.ToInt32(Console.ReadLine());

				// Make a place to store the encrypted message while I'm working on building it.
				String decryptedMessage = "";

				// Iterate through the message that the user typed one character at a time.
				foreach (char ch in message.ToCharArray())
				{
					// if this is a lowercase letter, encrypt the character using the lowercase alphabet string.
					// IndexOf returns -1 if the character does not appear in the string!
					if (alphabet.IndexOf(ch) >= 0)
					{

						// stick that encrypted character on the end of the encryptedMessage.
						decryptedMessage += DecryptCharacter(alphabet, ch, key);
					}

					// Do the same thing, if the character is a Capital letter.
					else if (ALPHABET.IndexOf(ch) >= 0)
					{
						decryptedMessage += DecryptCharacter(ALPHABET, ch, key);
					}

					// If the character is neither a capital nor lowercase, then ignore it and don't try to encrypt.
					else
					{
						decryptedMessage += ch;
					}
				}


				// Print the encrypted message!
				Console.WriteLine("_________________________________________________");
				Console.WriteLine("Your decrypted message is:\n\n{0}", decryptedMessage);
				Console.ReadKey();
            }


		}
	}
}


// This program makes use of abstraction through the methods encryptCharacter and decryptCharacter. 
// Instead of have to repeat the code the encryptCharacter contains within the main code block, the method can be called to make the code 
// appear cleaner. It is the same for decryptCharacter. In the main code block, the code can be read that the character is encrypted 
// and added to the end of the encrypted message for each character in the string entered by the user, but the code that actually encrypted each character
// with caesars cipher is not written in that code block. 

// Not only does this program use abstactions created by the writer, but it makes use of abstractions available in c# like Console.WriteLine and
// Console.ReadLine and Console.ReadKey. These were created by microsoft that make it easier for the user to write code without 
// having to write all of the code for those methods over and over again... making the program simpler and easier to read. 