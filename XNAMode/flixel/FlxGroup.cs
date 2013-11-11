﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    /// <summary>
    /// This class wraps the baseline .NET collection and adds a couple of extra functions...
    /// </summary>
    public class FlxGroup : FlxObject
    {
		/// <summary>
        /// Array of all the <code>FlxObject</code>s that exist in this layer.
		/// </summary>
		public List<FlxObject> members;
		/// <summary>
        /// Helpers for moving/updating group members.
		/// </summary>
		protected Vector2 _last;
        /// <summary>
        /// Helpers for moving/updating group members.
        /// </summary>
		protected bool _first;

		/// <summary>
        /// Constructor
		/// </summary>
		public FlxGroup()
            : base()
		{
			_group = true;
			solid = false;
            members = new List<FlxObject>();
            _last = Vector2.Zero;
			_first = true;
		}

        /// <summary>
        /// Adds a new <code>FlxObject</code> subclass (FlxSprite, FlxBlock, etc) to the list of children
        /// </summary>
        /// <param name="Object">The object you want to add</param>
        /// 
        public FlxObject add(FlxObject Object)
        {
            return add(Object, false);
        }

        /// <summary>
        /// Adds a new <code>FlxObject</code> subclass (FlxSprite, FlxBlock, etc) to the list of children
        /// </summary>
        /// <param name="Object">The object you want to add</param>
        /// <param name="ShareScroll">Whether or not this FlxObject should sync up with this layer's scrollFactor</param>
        /// <returns>The same <code>FlxObject</code> object that was passed in.</returns>
        public FlxObject add(FlxObject Object, bool ShareScroll)
		{
            if (members.IndexOf(Object) < 0)
                members.Add(Object);
				//members[members.Count] = Object;
			if(ShareScroll)
				Object.scrollFactor = scrollFactor;
			return Object;
		}

        /// <summary>
        /// Replaces an existing <code>FlxObject</code> with a new one.
        /// </summary>
        /// <param name="OldObject">The object you want to replace.</param>
        /// <param name="NewObject">The new object you want to use instead.</param>
        /// <returns>The new object.</returns>
		public FlxObject replace(FlxObject OldObject, FlxObject NewObject)
		{
			int index = members.IndexOf(OldObject);
			if((index < 0) || (index >= members.Count))
				return null;
			members[index] = NewObject;
			return NewObject;
		}

        /// <summary>
        /// Removes an object from the group.
        /// </summary>
        /// <param name="Object">The <code>FlxObject</code> you want to remove.</param>
        /// <returns></returns>
        public FlxObject remove(FlxObject Object)
        {
            return remove(Object, false);
        }
        /// <summary>
        /// Removes an object from the group.
        /// </summary>
        /// <param name="Object">The <code>FlxObject</code> you want to remove.</param>
        /// <param name="Splice">Whether the object should be cut from the array entirely or not.</param>
        /// <returns>The removed object.</returns>
		public FlxObject remove(FlxObject Object, bool Splice)
		{
            int index = members.IndexOf(Object);
			if((index < 0) || (index >= members.Count))
				return null;
			if(Splice)
				members.RemoveAt(index);
			else
				members[index] = null;
			return Object;
		}

        /// <summary>
        /// Call this function to sort the group according to a particular value and order.
        /// Due to differences in language capabilities between AS3/C#, you must implement
        /// your own IComparer interface for each sorting operation you want to perform.
        /// 
        /// For example, to sort game objects for Zelda-style overlaps you might call
        /// sort by an objects "y" member at the bottom of your <code>FlxState.update()</code>
        /// override.  To sort all existing objects after a big explosion or bomb attack,
        /// you might sort by "exists."
        /// </summary>
        /// <param name="Sorter">Sorter	The <code>IComparer</code> object which will receive the sorting comparisons. </param>
        public void sort(IComparer<FlxObject> Sorter)
		{
            members.Sort(Sorter);
		}

        /// <summary>
        /// Call this function to retrieve the first object with exists == false in the group.
        /// This is handy for recycling in general, e.g. respawning enemies.
        /// </summary>
        /// <returns>	A <code>FlxObject</code> currently flagged as not existing.</returns>
		public FlxObject getFirstAvail()
		{
			int i = 0;
			FlxObject o;
            int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && !o.exists)
					return o;
			}
			return null;
		}

        /// <summary>
        /// Call this function to retrieve the first index set to 'null'.
        /// Returns -1 if no index stores a null object.
        /// </summary>
        /// <returns>An <code>int</code> indicating the first null slot in the group.</returns>
		public int getFirstNull()
		{
			int i = 0;
			int ml = members.Count;
			while(i < ml)
			{
				if(members[i] == null)
					return i;
				else
					i++;
			}
			return -1;
		}

        /// <summary>
        /// Finds the first object with exists == false and calls reset on it.
        /// </summary>
        /// <param name="X">The new X position of this object.</param>
        /// <param name="Y">The new Y position of this object.</param>
        /// <returns>Whether a suitable <code>FlxObject</code> was found and reset.</returns>
		public bool resetFirstAvail(int X, int Y)
		{
			FlxObject o = getFirstAvail();
			if(o == null)
				return false;
			o.reset(X,Y);
			return true;
		}

        /// <summary>
        /// Call this function to retrieve the first object with exists == true in the group.
        /// This is handy for checking if everything's wiped out, or choosing a squad leader, etc.
        /// </summary>
        /// <returns>A <code>FlxObject</code> currently flagged as existing.</returns>
		public FlxObject getFirstExtant()
		{
			int i = 0;
			FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && o.exists)
					return o;
			}
			return null;
		}

        /// <summary>
        /// Call this function to retrieve the first object with dead == false in the group.
        /// This is handy for checking if everything's wiped out, or choosing a squad leader, etc.
        /// </summary>
        /// <returns>A <code>FlxObject</code> currently flagged as not dead.</returns>
		public FlxObject getFirstAlive()
		{
			int i = 0;
			FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && o.exists && !o.dead)
					return o;
			}
			return null;
		}

        /// <summary>
        /// Call this function to retrieve the first object with dead == true in the group.
        /// This is handy for checking if everything's wiped out, or choosing a squad leader, etc.
        /// </summary>
        /// <returns>A <code>FlxObject</code> currently flagged as dead.</returns>
		public FlxObject getFirstDead()
		{
			int i = 0;
			FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && o.dead)
					return o;
			}
			return null;
		}

        /// <summary>
        /// Call this function to find out how many members of the group are not dead.
        /// </summary>
        /// <returns>The number of <code>FlxObject</code>s flagged as not dead.  Returns -1 if group is empty.</returns>
		public int countLiving()
		{
			int count = -1;
			int i = 0;
            FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if(o != null)
				{
					if(count < 0)
						count = 0;
					if(o.exists && !o.dead)
						count++;
				}
			}
			return count;
		}

        public int countLivingOfType(string typeToUse)
        {
            int count = -1;
            int i = 0;
            FlxObject o;
            int ml = members.Count;
            while (i < ml)

            {
                o = members[i++] as FlxObject;
                if (o != null)
                {
                    if (count < 0)
                        count = 0;
                    if (o.exists && !o.dead)
                    {
                        var a = o.GetType().ToString();

                        //Console.WriteLine("a " + a + " type to Use " + typeToUse);


                        if (a == typeToUse)
                        {
                            count++;
                        }

                        
                    }
                }
            }
            return count;
        }
        /// <summary>
        /// Call this function to find out how many members of the group are dead.
        /// </summary>
        /// <returns>The number of <code>FlxObject</code>s flagged as dead.  Returns -1 if group is empty.</returns>
		public int countDead()
		{
			int count = -1;
			int i = 0;
            FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if(o != null)
				{
					if(count < 0)
						count = 0;
					if(o.dead)
						count++;
				}
			}
			return count;
		}

        /// <summary>
        /// Returns a count of how many objects in this group are on-screen right now.
        /// </summary>
        /// <returns>The number of <code>FlxObject</code>s that are on screen.  Returns -1 if group is empty.</returns>
		public int countOnScreen()
		{
			int count = -1;
			int i = 0;
            FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if(o != null)
				{
					if(count < 0)
						count = 0;
					if(o.onScreen())
						count++;
				}
			}
			return count;
		}		

        /// <summary>
        /// Returns a member at random from the group.
        /// </summary>
        /// <returns>A <code>FlxObject</code> from the members list.</returns>
		public FlxObject getRandom()
		{
			int c = 0;
			FlxObject o = null;
			int l = members.Count;
			int i = (int)(FlxU.random()*l);
			while((o == null) && (c < members.Count))
			{
				o = members[(++i)%l] as FlxObject;
				c++;
			}
			return o;
		}

		/// <summary>
        /// Internal function, helps with the moving/updating of group members.
		/// </summary>
        protected void saveOldPosition()
		{
			if(_first)
			{
				_first = false;
				_last.X = 0;
				_last.Y = 0;
				return;
			}
			_last.X = x;
			_last.Y = y;
		}

        /// <summary>
        /// Internal function that actually goes through and updates all the group members.
        /// Depends on <code>saveOldPosition()</code> to set up the correct values in <code>_last</code> in order to work properly.
        /// </summary>
		virtual protected void updateMembers()
		{
			float mx = 0;
			float my = 0;
			bool moved = false;
			if((x != _last.X) || (y != _last.Y))
			{
				moved = true;
				mx = x - _last.X;
				my = y - _last.Y;
			}
			int i = 0;
			FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && o.exists)
				{
					if(moved)
					{
                        if (o._group)
                            o.reset((o.x + mx), (o.y + my));
                        else
                        {
                            o.x += mx;
                            o.y += my;
                        }
					}
					if(o.active)
						o.update();
					if(moved && o.solid)
					{
						o.colHullX.width += ((mx>0)?mx:-mx);
						if(mx < 0)
							o.colHullX.x += mx;
						o.colHullY.x = x;
						o.colHullY.height += ((my>0)?my:-my);
						if(my < 0)
							o.colHullY.y += my;
						o.colVector.X += mx;
						o.colVector.Y += my;
					}
				}
			}
		}

        /// <summary>
        /// Automatically goes through and calls update on everything you added,
        /// override this function to handle custom input and perform collisions.
        /// </summary>
        override public void update()
		{
			saveOldPosition();
			updateMotion();
			updateMembers();
			updateFlickering();
		}

		/// <summary>
        /// Internal function that actually loops through and renders all the group members.
		/// </summary>
		/// <param name="spriteBatch">Sprite Batch</param>
		protected void renderMembers(SpriteBatch spriteBatch)
		{
			int i = 0;
			FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && o.exists && o.visible)
					o.render(spriteBatch);
			}
		}

        /// <summary>
        /// Automatically goes through and calls render on everything you added,
        /// override this loop to control render order manually.
        /// </summary>
        /// <param name="spriteBatch">Sprite Batch</param>
        override public void render(SpriteBatch spriteBatch)
		{
            renderMembers(spriteBatch);
		}

		/// <summary>
        /// Internal function that calls kill on all members.
		/// </summary>
		protected void killMembers()
		{
			int i = 0;
            FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if(o != null)
					o.kill();
			}
		}

		/// <summary>
        /// Calls kill on the group and all its members.
		/// </summary>
        override public void kill()
		{
			killMembers();
			base.kill();
		}

		/// <summary>
        /// Internal function that actually loops through and destroys each member.
		/// </summary>
		protected void destroyMembers()
		{
			int i = 0;
			FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if(o != null)
					o.destroy();
			}
            members.Clear();
		}

        /// <summary>
        /// Override this function to handle any deleting or "shutdown" type operations you might need,
        /// such as removing traditional Flash children like Sprite objects.
        /// </summary>
        override public void destroy()
		{
			destroyMembers();
			base.destroy();
		}

        /// <summary>
        /// If the group's position is reset, we want to reset all its members too.
        /// </summary>
        /// <param name="X">The new X position of this object.</param>
        /// <param name="Y">The new Y position of this object.</param>
		override public void reset(float X, float Y)
		{
			saveOldPosition();
			base.reset(X,Y);
			float mx = 0;
			float my = 0;
			bool moved = false;
			if((x != _last.X) || (y != _last.Y))
			{
				moved = true;
				mx = x - _last.X;
				my = y - _last.Y;
			}
			int i = 0;
            FlxObject o;
			int ml = members.Count;
			while(i < ml)
			{
				o = members[i++] as FlxObject;
				if((o != null) && o.exists)
				{
					if(moved)
					{
                        if (o._group)
                            o.reset((o.x + mx), (o.y + my));
                        else
                        {
                            o.x += mx;
                            o.y += my;
                            if (solid)
                            {
                                o.colHullX.width += ((mx > 0) ? mx : -mx);
                                if (mx < 0)
                                    o.colHullX.x += mx;
                                o.colHullY.x = x;
                                o.colHullY.height += ((my > 0) ? my : -my);
                                if (my < 0)
                                    o.colHullY.y += my;
                                o.colVector.X += mx;
                                o.colVector.Y += my;
                            }
                        }
					}
				}
			}
		}
    }
}
