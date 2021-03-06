
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
			
			Console.WriteLine( "LCS Length {0} ", l.Length );
		}
		
		[Test()]
		public void Strings()
		{
			Console.WriteLine("Test Strings");
			
			string[] ax = "h u m a n".Split(' ');
			string[] bx = "c h i m p a n z e e".Split(' ');
			
			LCS<string> l = new LCS<string>( ax, bx );
			
			Console.WriteLine( "LCS Length {0}" , l.Length );
			
			string seq = String.Join("", l.Sequence.ToArray() );
			
			if ( !seq.Equals("hman") )
				throw new Exception("lcs failed!");
		}

		
	}
}
