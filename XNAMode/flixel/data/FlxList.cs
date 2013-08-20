using System;
using System.Collections.Generic;
using System.Text;

namespace org.flixel
{
    /// <summary>
    /// The world's smallest linked list class.
    /// Useful for optimizing time-critical or highly repetitive tasks!
    /// See <code>FlxQuadTree</code> for how to use it, IF YOU DARE.
    /// </summary>
    public class FlxList
    {
		/// <summary>
        /// Stores a reference to a <code>FlxObject</code>.
		/// </summary>
		public FlxObject @object;
		/// <summary>
        /// Stores a reference to the next link in the list.
		/// </summary>
        public FlxList next;
		
		/// <summary>
        /// Creates a new link, and sets <code>object</code> and <code>next</code> to <code>null</null>.
		/// </summary>
		public FlxList()
		{
			@object = null;
			next = null;
		}
    }
}
