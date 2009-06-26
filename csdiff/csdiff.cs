
using System;
using System.Collections.Generic;
using System.Collections;

namespace csdiff
{
	public class LCS {
		
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
			
			// now actually find our longest common substring (length only)
			
			for ( int i = ( end_a - start_a -1 ) ; i >= 0; i-- ){	
				for ( int j = ( end_b - start_b -1 ) ; j >= 0 ; j-- ){
					if ( a[i + start_a].Equals( b[j + start_b ] ) ){
						lengths[i,j] = 1 + lengths[i+1,j+1];
					} else {
						int x = lengths[ i+1,j ];
						if ( lengths[i,j+1] > x )
							x = lengths[i,j+1];
						lengths[i,j] = x;
					}
				}
			}
			
			return lengths[0,0];
			
		}
			
		
		public void TestInts()
		{
			Console.WriteLine("Test Integers");
		
			int[] ax = new int[]{ 2,2,3,1,2,3,4,5,6,7,8,9,0,6,6 };
			int[] bx = new int[]{ 2,1,3,3,4,5,6,7,9,1,1,2,3,5,6,6 };
			
			Console.WriteLine( "LCS {0} ", Length( ref ax, ref bx ) );
		}
		
		public void TestStrings()
		{
			Console.WriteLine("Test Strings");
			
			string[] ax = "foo bar Baz Bob Cob Flob Zob flob flob wobble cobble Flob Flob wobble floo boo moo zoo boo goo zoo too".Split(' ');
			string[] bx = "zoo car Baz fob Cob Flob xob xlob flob wobble wobble Flob Flob xoo zoo too".Split(' ');
			
			Console.WriteLine( "LCS {0}" , Length( ref ax, ref bx ) );
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
