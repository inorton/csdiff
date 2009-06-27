
using System;
using System.Collections.Generic;

namespace csdiff
{
	public enum ChangeMaker 
	{
		NONE,
		ADD,
		REMOVE,
	}

	public struct Change<T> {
		public int Index;
		public ChangeMaker Mark;
		public T changed;
	}
	
	
	public class Diff<T> : LCS<T>
	{
		List<Change<T>> changes;
	
		private static int SortChange( Change<T> a, Change<T> b )
		{
			if ( a.Index > b.Index )
				return 1;
			
			if ( a.Index == b.Index )
				return 0;
				
			return -1;
		}
	
		public Diff ( T[] seq_a, T[] seq_b ) : base ( seq_a, seq_b )
		{		
		}
	
		public string ToString()
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
			int s = 0;
			List<Change<T>> chg = new List<Change<T>>();
			T[] seq = this.Sequence.ToArray();
			
			while (i <= end_a ){
				Change<T> c;
				c.Index = i;
				c.Mark = ChangeMaker.REMOVE;
				c.changed = a[i];
				if ( s < seq.Length ){
					if ( seq[s].Equals( a[i] ) ){
						s++;
					} else {
						chg.Add( c );		
					}
				} else {
					chg.Add( c );
				}
				i++;
			}
			
			s = 0;
			while (j <= end_b ){
				Change<T> c;
				c.Index = j;
				c.Mark = ChangeMaker.ADD;
				c.changed = b[j];
				if ( s < seq.Length ){
					if ( seq[s].Equals( b[j] ) ){
						s++;
					} else {
						chg.Add( c );
					}
				} else {
					chg.Add( c );
				}
				j++;
			}
			
			chg.Sort( SortChange );
			return chg;
			
		}
		
		
		
	}
}
