
using System;
using System.Collections.Generic;

namespace csdiff
{
	/// <summary>
	/// differences between elements
	/// </summary>
	public enum ChangeMaker 
	{
		NONE,
		ADD,
		REMOVE,
		CHANGE,
	}

	/// <summary>
	/// A single change and expression of difference
	/// </summary>
	public struct Change<T> {
		public int Index;
		public ChangeMaker Mark;
		public T changed;
	}
	
	
	/// <summary>
	/// Tool to compute the difference between two sequences.
	/// </summary>
	public class Diff<T> : LCS<T>
	{
		List<Change<T>> changes;
	
		/// <summary>
		/// Comparison function to sort a list of changes
		/// </summary>
		/// <param name="a">
		/// A <see cref="Change"/>
		/// </param>
		/// <param name="b">
		/// A <see cref="Change"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		private static int SortChange( Change<T> a, Change<T> b )
		{
			if ( a.Index > b.Index )
				return 1;
			
			if ( a.Index == b.Index )
				return 0;
				
			return -1;
		}
	
		/// <summary>
		/// Calculate the diff between two sequences seq_a and seq_b
		/// </summary>
		/// <param name="seq_a">
		/// A <see cref="T"/>
		/// </param>
		/// <param name="seq_b">
		/// A <see cref="T"/>
		/// </param>
		public Diff ( T[] seq_a, T[] seq_b ) : base ( seq_a, seq_b )
		{		 
		}
	
		/// <summary>
		/// Output the simple string representation of this diff if possible.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public override string ToString()
		{
			string ret = null;
			if ( this.Changes.Count > 0 )
				ret = "";
			foreach( Change<T> c in this.Changes ){
				string cm = "";
				if ( c.Mark.Equals(ChangeMaker.ADD) )
					cm = "+";
				if ( c.Mark.Equals(ChangeMaker.REMOVE) )
					cm = "-";
					
				ret += String.Format("{0} {1} {2}\n", c.Index, cm, c.changed );
			}
			return ret;
		}
		
		//// <value>
		/// The changes betweem the two sequences
		/// </value>
		public List<Change<T>> Changes
		{
			get {
				if ( changes == null )
					changes = this.ComputeChanges();
				return changes;
			}
		}
	
		private List<Change<T>> ComputeChanges()
		{
			int j = 0;
			
			List<Change<T>> chg = new List<Change<T>>();
			Console.Write("  ");
			foreach ( T x in a ){
				Console.Write(" {0} ",x );
			}
			Console.Write("\n");
			
			while ( j <= ( end_b - start_b ) ){
				Console.Write("{0}  ", b[j+start_b]);
				int i = 0;
				while ( i <= ( end_a - start_a ) ){
				  string mv = "0";
				  switch( lengths[i,j].Move ){
					case Move.NORTH: // insertion
					mv = "^";
					break;
					
					case Move.WEST: // deletion
					mv = "<";
					break;
					
					case Move.NORTHWEST:
					mv = "`";
					break;
					
					default:
					mv = "?";
					break;
				  }
				  mv = String.Format("{0}{1} ",mv,lengths[i,j].Length);
				  Console.Write(mv);
				  i++;
				}
				Console.Write("\n");
				j++;
			}
			
			chg.Sort( SortChange );
			return chg;
			
		}
		
		
		
	}
}
