
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace csdiff
{
	
	
	[TestFixture()]
	public class Diff_tests
	{
		[Test()]
		public void Ints()
		{
			Console.WriteLine("Diff Integers");
			
			int[] ax = new int[]{ 3,3,3,3,3,3 };
			int[] bx = new int[]{ 2,3,3,1,2 };
			
			Diff<int> l = new Diff<int>( ax, bx );
			Console.Out.Write("LCS ");
			foreach ( int x in l.Sequence ){
				Console.Out.Write( x.ToString() + " " );
			}
			Console.Out.Write("\n");
			Console.WriteLine("Changes");
			Console.WriteLine( l.ToString() );
			
		}
		
		[Test()]
		public void Chars()
		{
			Console.WriteLine("Diff Chars");
			
			char[] ax = "human".ToCharArray();
			char[] bx = "chimpanzee".ToCharArray();
			
			Diff<char> l = new Diff<char>( ax, bx );
		
			Console.Write("LCS ");
			foreach ( char c in l.Sequence ){
				Console.Write( c );
			}
			Console.Write("\n");
			Console.WriteLine("Changes");
			Console.WriteLine( l.ToString() );
			
		}
		
		[Test()]
		public void Strings()
		{
			Console.WriteLine("Diff Strings");
			
			string[] ax = "H U M A N".Split(' ');
			string[] bx = "C H I M P A N Z E E".Split(' ');
			
			Diff<string> l = new Diff<string>( ax, bx );
			Console.WriteLine( "LCS {0} ", String.Join(" ", l.Sequence.ToArray() ) );
			Console.WriteLine("Changes");
			Console.WriteLine( l.ToString() );
		}

	}
}
