
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
		
		protected int[,] lengths;
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
			
			int i = 0;
			int j = 0;
			
			while (( i < ( end_a - start_a ) ) && ( j < ( end_b - start_b ) ) ){
				if ( a[start_a + i].Equals( b[start_b + j] ) ){
					seq.Add( a[start_a + i] ); // elements match, part of lcs
					i++; j++;
				} else {
					if ( lengths[i+1,j] < lengths[i,j] ) { // go up
						j++; 
					} else {
						i++;
					}
				}
			} 
			
			
			return seq;
		}

		private int ComputeLength()
		{
		
			// reduce problem set ( ignore common start and end data )
			start_a = 0;
			start_b = 0;	
			end_a   = a.Length - 1;
			end_b   = b.Length - 1;
			
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
			
	}
}
