
using System;

namespace csdiff
{
	
	
	public class Diff<T> : LCS<T>
	{
		
		public Diff( T[] a, T[]b ) : base( a, b )
		{
		}
	}
}
