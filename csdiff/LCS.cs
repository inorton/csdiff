
using System;
using System.Collections.Generic;
using System.Collections;

namespace csdiff
{

	/// <summary>
	/// Tool to compute and extract the Longest Common Substring of two 
	/// sequences by dynamic programming. This method uses quadratic time 
	/// and space and attempts to reduce the problem set.
	/// </summary>
	public class LCS<T> {
		
		public enum Move : byte {
			NONE,
			NORTH,
			WEST,
			NORTHWEST,
			
		}
		
		public struct Cell {
			public int Length;
			public Move  Move;
		}
		
		protected Cell[,] lengths;
		protected int start_a = -1;
		protected int start_b = -1;
		protected int end_a   = -1;
		protected int end_b   = -1;
		
		protected T[] a;
		protected T[] b;
		
		private int lcs_len = -1;
		private List<T> lcs_seq = null;
		
		//// <value>
		/// Length of Longest Common Subsequence
		/// </value>
		public int Length
		{
			get {
				if ( lcs_len < 0 ){
					lcs_len = this.ComputeLength();
				}
				return lcs_len;
			}
		}
		
		//// <value>
		/// Longest Common Subsequence
		/// </value>
		public List<T> Sequence
		{
			get {
				if ( lcs_seq == null ){
					lcs_seq = ComputeSequence();
				}
				return lcs_seq;
			}
		}
		
		/// <summary>
		/// Calculate the Longest Common Subsequence between seq_a and seq_b
		/// </summary>
		/// <param name="seq_a">
		/// A <see cref="T"/>
		/// </param>
		/// <param name="seq_b">
		/// A <see cref="T"/>
		/// </param>
		public LCS ( T[] seq_a, T[] seq_b )
		{
			a = seq_a;
			b = seq_b;
		}
		
		/// <summary>
		/// Compute the LCS for the supplied sequences
		/// </summary>
		/// <returns>
		/// A <see cref="List"/>
		/// </returns>
		private List<T> ComputeSequence()
		{
			List<T> seq = new List<T>(this.Length);
			
			int i = end_a;
			int j = end_b;
			
			while ( ( j >= start_b ) && ( i >= start_a ) ){
				if ( lengths[i,j].Move == Move.NONE )
				{
					j--;i--;
				}
			
				if ( lengths[i,j].Move == Move.NORTHWEST ){
					seq.Add( a[i] );
					i--; j--;
				} else {
					if ( lengths[i,j].Move == Move.NORTH ){
						j--;
					}
					if ( lengths[i,j].Move == Move.WEST ){
						i--;
					}
				}
			}
			seq.Reverse();
			return seq;
		}

		private int ComputeLength()
		{
		
			// reduce problem set ( ignore common start and end data )
			start_a = -1;
			start_b = -1;	
			end_a   = a.Length;
			end_b   = b.Length;
			
			do {
				start_a++; start_b++;
			}
			while ( ( a[start_a].Equals(b[start_b]) ) && ( start_a < end_a ) && ( start_b < end_b ) );
			
			do {
				end_a--; end_b--;
			}
			while ( ( a[end_a].Equals(b[end_b]) ) && ( start_a < end_a ) && ( start_b < end_b ) );
			
	
			lengths = new Cell[ 2 + end_a - start_a , 2 + end_b - start_b ];
			
			// now actually find our longest common substring (length only)
			
			for ( int i = 0 ; i <= (end_a - start_a) ; i++ ){	
				for ( int j = 0 ; j <= (end_b - start_b) ; j++ ){
					int _len = 0;
					if ( ( i > 0 ) && ( j > 0 ) )
						_len = lengths[i-1,j-1].Length;
					
					if ( a[i+start_a].Equals( b[j + start_b] ) ){
						lengths[i,j].Move = Move.NORTHWEST;
						lengths[i,j].Length = _len+1;
					} else {
						int left = 0;
						int above = 0;
						if ( i > 0 )
							left = lengths[i-1,j].Length;
						if ( j > 0 )
							above = lengths[i,j-1].Length;
						
						if ( left < above ){
							lengths[i,j].Length = above;
							lengths[i,j].Move = Move.NORTH;
						} else {
							lengths[i,j].Length = left;
							lengths[i,j].Move = Move.WEST;
						}
					}
				}
			}
			
			Console.WriteLine("Grid Done");
			
			return lengths[0,0].Length;
			
		}
			
	}
}
