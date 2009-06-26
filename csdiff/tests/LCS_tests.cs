
using System;
using NUnit.Framework;

namespace csdiff
{
	
	
	[TestFixture()]
	public class LCS_tests
	{
		[Test()]
		public void Ints()
		{
			Console.WriteLine("Test Integers");
			
			int[] ax = new int[]{ 2,2,3,1,2,3,4,5,6,7,8,9,0,6,6 };
			int[] bx = new int[]{ 2,1,3,3,4,5,6,7,9,1,1,2,3,5,6,6 };
			
			LCS<int> l = new LCS<int>( ax, bx );
			
			Console.WriteLine( "LCS {0} ", l.Length );
		}
		
		[Test()]
		public void Strings()
		{
			Console.WriteLine("Test Strings");
			
			string[] ax = "foo bar Baz Bob Cob Flob Zob flob flob wobble cobble Flob Flob wobble floo boo moo zoo boo goo zoo too".Split(' ');
			string[] bx = "zoo car Baz fob Cob Flob xob xlob flob wobble wobble Flob Flob xoo zoo too".Split(' ');
			
			LCS<string> l = new LCS<string>( ax, bx );
			
			Console.WriteLine( "LCS {0}" , l.Length );
		}

		
	}
}
