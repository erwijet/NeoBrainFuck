/*
 * Created by SharpDevelop.
 * User: HolewinskiT
 * Date: 7/26/2018
 * Time: 9:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace BrainFuck
{
	/// <summary>
	/// Description of JIT.
	/// </summary>
	public static class JIT
	{
		public static void Run(FileInfo plaintext,  string bfpath)
		{
			var text = new List<char>(File.ReadAllText(plaintext.FullName).ToCharArray());			// Input text array
			var arr = new List<int>(); 																// Declare memory array
			var loops = new Stack<int>();
			
			int data_ptr = 0;
			
			arr.Add(0);																				// Give one cell to memory array by default
			
			for (int cmd_ptr = 0;  cmd_ptr < text.ToArray().Length; cmd_ptr++)
			{
				switch (text[cmd_ptr]) {
					case '>':																		// MOVE POINTER LEFT
						data_ptr++;
						if (arr.Count - 1 < data_ptr)
							arr.Add(0);
						break;
					case '<':																		// MOVE POINTER RIGHT
						if (data_ptr > 0)
							data_ptr--;
						break;
					case '+':																		// ADD ONE TO MEMORY CELL AT POINTER
						arr[data_ptr]++;
						break;
					case '-':																		// SUBTRACT ONE FROM MEMORY CELL AT POINTER
						if (arr[data_ptr] > 0)
							arr[data_ptr]--;
						break;
					case '.':																		// WRITE ASCII VALUE OF CELL AT POINTER TO THE CONSOLE
						Console.Write((char)arr[data_ptr]);
						break;
					case ',':																		// READ ASCII VALUE OF INPUT FROM CONSOLE AND SAVE TO CELL AT POINTER
						arr[data_ptr] = System.Text.Encoding.ASCII.GetBytes(new char[] { Console.ReadKey(true).KeyChar })[0];
						break;
					case '[':																		// START LOOP
						loops.Push(cmd_ptr);
						break;
					case ']':																		// END LOOP. GO BACK TO START LOOP IF CELL AT POINTER IS NOT 0.
						int loopStart = loops.Pop() - 1;
						if  (arr[data_ptr] != 0)
							cmd_ptr = loopStart;
						break;
				}
				Console.Clear();
				PrintMem(arr, data_ptr);
				PrintCode(new List<char>(File.ReadAllText(plaintext.FullName).ToCharArray()), data_ptr);
				Console.ReadKey(true);
			}

			ConsoleColor save = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("[TASK DONE]");
			Console.ForegroundColor = save;
			PrintMem(arr, data_ptr);
			Console.ReadKey(true);
		
		}
		
		public static void PrintCode(List<char> code, int data_ptr)
		{
			Console.WriteLine("MEMORY: ");
			
			int refIndex = 0;
			foreach (char value in code)
			{
				ConsoleColor old = Console.BackgroundColor;
				if (refIndex == data_ptr)
					Console.BackgroundColor = ConsoleColor.Green;
				Console.Write(value);
				refIndex++;
				Console.BackgroundColor = old;
			}
		}
		
		public static void PrintMem(List<int> arr, int data_ptr)
		{
			Console.WriteLine("MEMORY: ");
			
			int refIndex = 0;
			foreach (int value in arr)
			{
				ConsoleColor old = Console.BackgroundColor;
				if (refIndex == data_ptr)
					Console.BackgroundColor = ConsoleColor.Green;
				Console.Write("[" + value + "]");
				refIndex++;
				Console.BackgroundColor = old;
			}
		}
	}
}
