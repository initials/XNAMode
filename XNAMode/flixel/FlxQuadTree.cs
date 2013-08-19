﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace org.flixel
{
    /// <summary>
    /// A fairly generic quad tree structure for rapid overlap checks.
    /// FlxQuadTree is also configured for single or dual list operation.
    /// You can add items either to its A list or its B list.
    /// When you do an overlap check, you can compare the A list to itself,
    /// or the A list against the B list.  Handy for different things!
    /// </summary>
    public class FlxQuadTree
    {
        /// <summary>
        /// X Rect
        /// </summary>
        public float x;
        /// <summary>
        /// Y Rect
        /// </summary>
        public float y;
        /// <summary>
        /// Width of Rect
        /// </summary>
        public float width;
        /// <summary>
        /// Height of rect
        /// </summary>
        public float height;

        public float left
        {
            get { return x; }
        }
        public float top
        {
            get { return y; }
        }
        public float right
        {
            get { return x + width; }
        }
        public float bottom
        {
            get { return y + height; }
        }


		/// <summary>
        /// Set this to null to force it to refresh on the next collide.
		/// </summary>
		static public FlxQuadTree quadTree;

        /// <summary>
        /// This variable stores the dimensions of the root of the quad tree.
        /// This is the eligible game collision space.
        /// </summary>
		static public FlxRect bounds = new FlxRect(0,0,0,0);
		/// <summary>
        /// Controls the granularity of the quad tree.  Default is 3 (decent performance on large and small worlds).
		/// </summary>
		static public uint divisions;
		/// <summary>
        /// Flag for specifying that you want to add an object to the A list.
		/// </summary>
		public const uint A_LIST = 0;
		/// <summary>
        /// Flag for specifying that you want to add an object to the B list.
		/// </summary>
		public const uint B_LIST = 1;
		
		/// <summary>
        /// Whether this branch of the tree can be subdivided or not.
		/// </summary>
		protected bool _canSubdivide;
		
        /// <summary>
        /// These variables refer to the internal A and B linked lists,
        /// which are used to store objects in the leaves.
        /// </summary>
		protected FlxList _headA;
		protected FlxList _tailA;
		protected FlxList _headB;
		protected FlxList _tailB;

		/// <summary>
        /// These variables refer to the potential child quadrants for this node.
		/// </summary>
		static protected uint _min;
		protected FlxQuadTree _nw;
		protected FlxQuadTree _ne;
		protected FlxQuadTree _se;
		protected FlxQuadTree _sw;		
		protected float _l;
		protected float _r;
		protected float _t;
		protected float _b;
		protected float _hw;
		protected float _hh;
		protected float _mx;
		protected float _my;
		
		/// <summary>
        /// These objects are used to reduce recursive parameters internally.
		/// </summary>
		static protected FlxObject _o;
		static protected float _ol;
		static protected float _ot;
		static protected float _or;
		static protected float _ob;
		static protected uint _oa;
        static protected SpriteCollisionEvent _oc;

        /// <summary>
        /// Instantiate a new Quad Tree node.
        /// </summary>
        /// <param name="X">The X-coordinate of the point in space.</param>
        /// <param name="Y">The Y-coordinate of the point in space.</param>
        /// <param name="Width">Desired width of this node.</param>
        /// <param name="Height">Desired height of this node.</param>
        /// <param name="Parent">The parent branch or node.  Pass null to create a root.</param>
		public FlxQuadTree(float X, float Y, float Width, float Height, FlxQuadTree Parent)
		{
            x = X;
            y = Y;
            width = Width;
            height = Height;
            //x = 0;
            //y = 0;
            //width = 0;
            //height = 0;

			_headA = _tailA = new FlxList();
			_headB = _tailB = new FlxList();
			
			/*DEBUG: draw a randomly colored rectangle indicating this quadrant (may induce seizures)
			var brush:FlxSprite = new FlxSprite().createGraphic(Width,Height,0xffffffff*FlxU.random());
			FlxState.screen.draw(brush,X+FlxG.scroll.x,Y+FlxG.scroll.y);//*/

			//Copy the parent's children (if there are any)
            if (Parent != null)
            {
                FlxList itr;
                FlxList ot;
                if (Parent._headA.@object != null)
                {
                    itr = Parent._headA;
                    while (itr != null)
                    {
                        if (_tailA.@object != null)
                        {
                            ot = _tailA;
                            _tailA = new FlxList();
                            ot.next = _tailA;
                        }
                        _tailA.@object = itr.@object;
                        itr = itr.next;
                    }
                }
                if (Parent._headB.@object != null)
                {
                    itr = Parent._headB;
                    while (itr != null)
                    {
                        if (_tailB.@object != null)
                        {
                            ot = _tailB;
                            _tailB = new FlxList();
                            ot.next = _tailB;
                        }
                        _tailB.@object = itr.@object;
                        itr = itr.next;
                    }
                }
            }
            else
                _min = (uint)(width + height) / (2 * divisions);
            _canSubdivide = (width > _min) || (height > _min);
			
			//Set up comparison/sort helpers
			_nw = null;
			_ne = null;
			_se = null;
			_sw = null;
			_l = x;
            _r = x + width;
            _hw = width / 2;
            _mx = _l + _hw;
			_t = y;
            _b = y + height;
            _hh = height / 2;
            _my = _t + _hh;
		}

        /// <summary>
        /// Call this function to add an object to the root of the tree.
        /// This function will recursively add all group members, but
        /// not the groups themselves.
        /// </summary>
        /// <param name="Object">The <code>FlxObject</code> you want to add.  <code>FlxGroup</code> objects will be recursed and their applicable members added automatically.</param>
        /// <param name="List">A <code>uint</code> flag indicating the list to which you want to add the objects.  Options are <code>A_LIST</code> and <code>B_LIST</code>.</param>
		public void add(FlxObject Object, uint List)
		{
			_oa = List;
			if(Object._group)
			{
				int i = 0;
				FlxObject m;
				List<FlxObject> members = (Object as FlxGroup).members;
                int l = members.Count;
				while(i < l)
				{
					m = members[i++] as FlxObject;
					if((m != null) && m.exists)
					{
						if(m._group)
							add(m,List);
						else if(m.solid)
						{
							_o = m;
							_ol = _o.x;
							_ot = _o.y;
							_or = _o.x + _o.width;
							_ob = _o.y + _o.height;
							addObject();
						}
					}
				}
			}
			if(Object.solid)
			{
				_o = Object;
				_ol = _o.x;
				_ot = _o.y;
				_or = _o.x + _o.width;
				_ob = _o.y + _o.height;
				addObject();
			}
		}

        /// <summary>
        ///  Internal function for recursively navigating and creating the tree
        ///  while adding objects to the appropriate nodes.
        /// </summary>
		protected void addObject()
		{
			//If this quad (not its children) lies entirely inside this object, add it here
			if(!_canSubdivide || ((_l >= _ol) && (_r <= _or) && (_t >= _ot) && (_b <= _ob)))
			{
				addToList();
				return;
			}
			
			//See if the selected object fits completely inside any of the quadrants
			if((_ol > _l) && (_or < _mx))
			{
				if((_ot > _t) && (_ob < _my))
				{
					if(_nw == null)
                        _nw = new FlxQuadTree((int)_l, (int)_t, (int)_hw, (int)_hh, this);
					_nw.addObject();
					return;
				}
				if((_ot > _my) && (_ob < _b))
				{
					if(_sw == null)
                        _sw = new FlxQuadTree((int)_l, (int)_my, (int)_hw, (int)_hh, this);
					_sw.addObject();
					return;
				}
			}
			if((_ol > _mx) && (_or < _r))
			{
				if((_ot > _t) && (_ob < _my))
				{
					if(_ne == null)
                        _ne = new FlxQuadTree((int)_mx, (int)_t, (int)_hw, (int)_hh, this);
					_ne.addObject();
					return;
				}
				if((_ot > _my) && (_ob < _b))
				{
					if(_se == null)
                        _se = new FlxQuadTree((int)_mx, (int)_my, (int)_hw, (int) _hh, this);
					_se.addObject();
					return;
				}
			}
			
			//If it wasn't completely contained we have to check out the partial overlaps
			if((_or > _l) && (_ol < _mx) && (_ob > _t) && (_ot < _my))
			{
				if(_nw == null)
                    _nw = new FlxQuadTree((int)_l, (int)_t, (int)_hw, (int)_hh, this);
				_nw.addObject();
			}
			if((_or > _mx) && (_ol < _r) && (_ob > _t) && (_ot < _my))
			{
				if(_ne == null)
                    _ne = new FlxQuadTree((int)_mx, (int)_t, (int)_hw, (int)_hh, this);
				_ne.addObject();
			}
			if((_or > _mx) && (_ol < _r) && (_ob > _my) && (_ot < _b))
			{
				if(_se == null)
                    _se = new FlxQuadTree((int)_mx, (int)_my, (int)_hw, (int)_hh, this);
				_se.addObject();
			}
			if((_or > _l) && (_ol < _mx) && (_ob > _my) && (_ot < _b))
			{
				if(_sw == null)
                    _sw = new FlxQuadTree((int)_l, (int)_my, (int)_hw, (int)_hh, this);
				_sw.addObject();
			}
		}

		/// <summary>
        /// Internal function for recursively adding objects to leaf lists.
		/// </summary>
		protected void addToList()
		{
			FlxList ot;
			if(_oa == A_LIST)
			{
				if(_tailA.@object != null)
				{
					ot = _tailA;
					_tailA = new FlxList();
					ot.next = _tailA;
				}
				_tailA.@object = _o;
			}
			else
			{
				if(_tailB.@object != null)
				{
					ot = _tailB;
					_tailB = new FlxList();
					ot.next = _tailB;
				}
				_tailB.@object = _o;
			}
			if(!_canSubdivide)
				return;
			if(_nw != null)
				_nw.addToList();
			if(_ne != null)
				_ne.addToList();
			if(_se != null)
				_se.addToList();
			if(_sw != null)
				_sw.addToList();
		}

        /// <summary>
        /// <code>FlxQuadTree</code>'s other main function.  Call this after adding objects
        /// using <code>FlxQuadTree.add()</code> to compare the objects that you loaded.
        /// </summary>
        /// <param name="BothLists">Whether you are doing an A-B list comparison, or comparing A against itself.</param>
        /// <param name="Callback">A function with two <code>FlxObject</code> parameters - e.g. <code>myOverlapFunction(Object1:FlxObject,Object2:FlxObject);</code>  If no function is provided, <code>FlxQuadTree</code> will call <code>kill()</code> on both objects.</param>
        /// <returns></returns>
		public bool overlap(bool BothLists, SpriteCollisionEvent Callback)
		{
			_oc = Callback;
			bool c = false;
			FlxList itr;
			if(BothLists)
			{
				//An A-B list comparison
				_oa = B_LIST;
				if(_headA.@object != null)
				{
					itr = _headA;
					while(itr != null)
					{
						_o = itr.@object;
						if(_o.exists && _o.solid && overlapNode())
							c = true;
						itr = itr.next;
					}
				}
				_oa = A_LIST;
				if(_headB.@object != null)
				{
					itr = _headB;
					while(itr != null)
					{
						_o = itr.@object;
						if(_o.exists && _o.solid)
						{
							if((_nw != null) && _nw.overlapNode())
								c = true;
							if((_ne != null) && _ne.overlapNode())
								c = true;
							if((_se != null) && _se.overlapNode())
								c = true;
							if((_sw != null) && _sw.overlapNode())
								c = true;
						}
						itr = itr.next;
					}
				}
			}
			else
			{
				//Just checking the A list against itself
				if(_headA.@object != null)
				{
					itr = _headA;
					while(itr != null)
					{
						_o = itr.@object;
						if(_o.exists && _o.solid && overlapNode(itr.next))
							c = true;
						itr = itr.next;
					}
				}
			}
			
			//Advance through the tree by calling overlap on each child
			if((_nw != null) && _nw.overlap(BothLists,_oc))
				c = true;
			if((_ne != null) && _ne.overlap(BothLists,_oc))
				c = true;
			if((_se != null) && _se.overlap(BothLists,_oc))
				c = true;
			if((_sw != null) && _sw.overlap(BothLists,_oc))
				c = true;
			
			return c;
		}

		/**
		 * An internal function for comparing an object against the contents of a node.
		 * 
		 * @param	Iterator	An optional pointer to a linked list entry (for comparing A against itself).
		 * 
		 * @return	Whether or not any overlaps were found.
		 */
        protected bool overlapNode()
        {
            return overlapNode(null);
        }
		protected bool overlapNode(FlxList Iterator)
		{
			//member list setup
			bool c = false;
			FlxObject co;
			FlxList itr = Iterator;
			if(itr == null)
			{
				if(_oa == A_LIST)
					itr = _headA;
				else
					itr = _headB;
			}
			
			//Make sure this is a valid list to walk first!
			if(itr.@object != null)
			{
				//Walk the list and check for overlaps
				while(itr != null)
				{
					co = itr.@object;
					if( (_o == co) || !co.exists || !_o.exists || !co.solid || !_o.solid ||
						(_o.x + _o.width  < co.x + FlxU.roundingError) ||
						(_o.x + FlxU.roundingError > co.x + co.width) ||
						(_o.y + _o.height < co.y + FlxU.roundingError) ||
						(_o.y + FlxU.roundingError > co.y + co.height) )
					{
						itr = itr.next;
						continue;
					}
					if(_oc == null)
					{
						_o.kill();
						co.kill();
						c = true;
					}
                    //else if(_oc(_o,co))
                    else if (_oc(this, new FlxSpriteCollisionEvent(_o, co)))
                    {
                        c = true;
                    }
					itr = itr.next;
				}
			}
			
			return c;
		}

    }
}
