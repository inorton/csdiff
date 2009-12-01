
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
			int i = start_a;
			int j = start_b;
			int c = 0;
			int n = start_a; // index of new change
									
			List<Change<T>> chg = new List<Change<T>>();
			
			while ( (i < end_a) && (j < end_b) ){
				if ( Sequence[c].Equals( a[i] ) ){ // A matches LCS
					if ( Sequence[c].Equals( b[j] ) ){ // A B C are same
						// move all
						c++; i++; j++; n++;
					} else {
						// B not in LCS
						Change<T> ch = new Change<T>();
						ch.Mark = ChangeMaker.ADD;
						ch.changed = b[j++];
						ch.Index = n++;
						chg.Add( ch );
					}
				} else { // A is not in LCS
					if ( Sequence[c].Equals( b[j] )) { // but B matches LCS
						// remove A
						Change<T> ch = new Change<T>();
						ch.Mark = ChangeMaker.REMOVE;;
						ch.changed = a[i];
						ch.Index = n;
						chg.Add( ch );
						i++;
					} else {
						// A, B, C all differ,
						Change<T> ch = new Change<T>();
						ch.Mark = ChangeMaker.REMOVE;
						ch.changed = a[i];
						ch.Index = n;
						chg.Add( ch );
						
						ch = new Change<T>();
						ch.Mark = ChangeMaker.ADD;
						ch.Index = n;
						ch.changed = b[j];
						chg.Add( ch );
						i++; j++; n++;
					}
				}
			}
			
			
			while ( i < ( a.Length - 1 ) ){
				// remove remaining a
				Change<T> ch = new Change<T>();
				ch.Mark = ChangeMaker.REMOVE;
				ch.changed = a[i++];
				ch.Index = n;
				chg.Add( ch );
			}
			
			
			
			while ( j < ( b.Length - 1 ) ){
				// add remaining b
				Change<T> ch = new Change<T>();
				ch.Mark = ChangeMaker.ADD;
				ch.changed = b[++j];
				ch.Index = n++;
				chg.Add ( ch );
			}
			
			
			Console.Write("\n");
			
			
			
			chg.Sort( SortChange );
			return chg;
			
		}
		
		
		
	}
}
