
using System;
using NUnit.Framework;
using NUnit.Core;
using System.Collections.Generic;
using System.Collections;

namespace csdiff
{
	public class LCS {
		
		private int iterations = 0;
		
		public int Iterations {
			get { return iterations; }
		}
		
		public int[,] lengths;
		
		public int Length<T>( ref T[] a, ref T[] b )
		{
		
			// reduce problem set ( ignore common start and end data )
			int start_a = 0;
			int start_b = 0;	
			int end_a   = a.Length - 1;
			int end_b   = b.Length - 1;
			
			while ( ( a[start_a].Equals(b[start_b]) ) && ( start_a < end_a ) && ( start_b < end_b ) )
				start_a++; start_b++;
	
			while ( ( a[end_a].Equals(b[end_b]) ) && ( start_a < end_a ) && ( start_b < end_b ) )
				end_a--; end_b--;
	
			lengths = new int[ 1 + end_a - start_a , 1 + end_b - start_b ];
			
			{
				int x = end_a - start_a;
				while ( x > 0 ){
					--x;
					int y = end_b - start_b;
					while ( y > 0 )
						lengths[x,--y] = -1;	
				}
			}
	
			// now actually find our longest common substring (length only)
			return SubLength( ref a, start_a, end_a, ref b, start_b, end_b );
		}
		
		private int SubLength<T>( ref T [] a, int a_start, int a_end, ref T[] b, int b_start, int b_end )
		{
			if ( a_start >= a_end )
				return 0;
			if ( b_start >= b_end ) 
				return 0;
			
			if ( lengths[ a_start , b_start ] > -1)
				return lengths[ a_start , b_start ];
		
			iterations++;
			int rt;
			
			if ( a[a_start].Equals(b[b_start]) ){
				rt = 1 + SubLength( ref a, ++a_start, a_end, ref b, ++b_start, b_end );
			} else {
				int al = SubLength ( ref a, ++a_start, a_end, ref b, b_start, b_end );
				int bl = SubLength ( ref a, a_start, a_end, ref b, ++b_start, b_end );
				rt = bl;
			
				if ( al > bl )
					rt = al;
				
			}
			
			lengths[ a_start, b_start ] = rt;
				
			return rt;
		}
		
		
		
		[Test]
		public void TestInts()
		{
			Console.WriteLine("Test Integers");
		
			int[] ax = new int[]{ 2,2,3,1,2,3,4,5,6,7,8,9,0,6,6 };
			int[] bx = new int[]{ 2,1,3,3,4,5,6,7,9,1,1,2,3,5,6,6 };
			
			Console.WriteLine( "LCS {0} ", Length( ref ax, ref bx ) );
			Console.WriteLine( "iterations {0}",Iterations );
		}
		
		[Test]
		public void TestStrings()
		{
			Console.WriteLine("Test Strings");
			
			string[] ax = "foo bar Baz Bob Cob Flob Zob flob flob wobble cobble Flob Flob".Split(' ');
			string[] bx = "zoo car Baz fob Cob Flob xob xlob flob wobble wobble Flob Flob".Split(' ');
			
			Console.WriteLine( "LCS {0}" , Length( ref ax, ref bx ) );
			Console.WriteLine( "iterations {0}", Iterations );
		}
		
		
		public static int Main( string[] argv )
		{
			LCS l = new LCS();
			l.TestInts();
			
			l = new LCS();
			
			l.TestStrings();
			
			return 0;
		}
	}
}
