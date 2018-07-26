/*
 * Created by SharpDevelop.
 * User: HolewinskiT
 * Date: 7/26/2018
 * Time: 9:22 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BrainFuck
{
	class Program
	{
		public static void Main(string[] args)
		{
			JIT.Run(new System.IO.FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\helloworld.bf"), "");
		}
	}
}