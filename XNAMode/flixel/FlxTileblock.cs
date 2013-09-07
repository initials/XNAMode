using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    /// <summary>
    /// This is the basic "environment object" class, used to create simple walls and floors.
    /// It can be filled with a random selection of tiles to quickly add detail.
    /// </summary>
    public class FlxTileblock : FlxSprite
    {
        //protected Texture2D _tex;
        protected Rectangle[] _rects;
        protected int _tileWidth;
        protected int _tileHeight;
        protected int _empties;
        protected Vector2 _p;
        protected bool _autoTile;

        /// <summary>
        /// Creates a new <code>FlxBlock</code> object with the specified position and size.
        /// </summary>
        /// <param name="X">The X position of the block.</param>
        /// <param name="Y">The Y position of the block.</param>
        /// <param name="Width">The width of the block.</param>
        /// <param name="Height">The height of the block.</param>
		public FlxTileblock(int X, int Y, int Width, int Height)
            : base(X,Y)
		{
			createGraphic(Width,Height,Color.Black);		
			@fixed = true;
            _autoTile = true;
            _color = Color.White;

		}

        /// <summary>
        /// Fills the block with a randomly arranged selection of graphics from the image provided.
        /// </summary>
        /// <param name="TileGraphic">The graphic class that contains the tiles that should fill this block.</param>
        /// <returns></returns>
        public FlxTileblock loadTiles(Texture2D TileGraphic)
        {
            return loadTiles(TileGraphic, 0, 0, 0);
        }
        /// <summary>
        /// Fills the block with a randomly arranged selection of graphics from the image provided.
        /// </summary>
        /// <param name="TileGraphic">The graphic class that contains the tiles that should fill this block.</param>
        /// <param name="TileWidth">Width of the tiles.</param>
        /// <param name="TileHeight">Height of the tiles.</param>
        /// <param name="Empties">The number of "empty" tiles to add to the auto-fill algorithm (e.g. 8 tiles + 4 empties = 1/3 of block will be open holes).</param>
        /// <returns></returns>
        public FlxTileblock loadTiles(Texture2D TileGraphic, int TileWidth, int TileHeight, int Empties)
        {
            if (TileGraphic == null)
                return this;

            if (TileWidth == 0) TileWidth = TileGraphic.Height;
            if (TileHeight == 0) TileHeight = TileGraphic.Height;

            _tex = TileGraphic;
            _tileWidth = TileWidth;
            _tileHeight = TileHeight;
            _empties = Empties;

            regenRects();

            moves = false;

            return this;
        }

        /// <summary>
        /// X-flixel only.
        /// </summary>
        private void regenRects()
        {
            int widthInTiles = ((int)width / _tileWidth);
            int heightInTiles = ((int)height / _tileHeight);
            width = widthInTiles * _tileWidth;
            height = heightInTiles * _tileHeight;

            int tileCount = widthInTiles * heightInTiles;
            int numGraphics = _tex.Width / _tileWidth;

            _rects = new Rectangle[tileCount];
            for (int i = 0; i < tileCount; i++)
            {
                if ((FlxU.random() * (numGraphics + _empties)) > _empties)
                {

                    if (_autoTile)
                    {

                        _rects[i] = new Rectangle(_tileWidth * (int)(FlxU.random() * numGraphics), 0, _tileWidth, _tileHeight);

                        /*
                        switch (i)
                        {
                            case 0:
                                _rects[i] = new Rectangle(0, 0, _tileWidth, _tileHeight);
                                break;
                            default:
                                _rects[i] = new Rectangle(20, 0, _tileWidth, _tileHeight);
                                break;

                        }
                         */ 
                    }
                    else
                    {
                        _rects[i] = new Rectangle(_tileWidth * (int)(FlxU.random() * numGraphics), 0, _tileWidth, _tileHeight);
                    }

                    
                    //to do: create auto tile tileblocks.

                    //_rects[i] = new Rectangle(0, 0, _tileWidth, _tileHeight);


                }
                else
                {
                    _rects[i] = Rectangle.Empty;
                }
            }
        }

        /// <summary>
        /// NOTE: MOST OF THE TIME YOU SHOULD BE USING LOADTILES(), NOT LOADGRAPHIC()!
        /// <code>LoadTiles()</code> has a lot more functionality, can load non-square tiles, etc.
        /// Load an image from an embedded graphic file and use it to auto-fill this block with tiles.
        /// </summary>
        /// <param name="Graphic">The image you want to use.</param>
        /// <returns>This FlxSprite instance (nice for chaining stuff together, if you're into that).</returns>
		override public FlxSprite loadGraphic(Texture2D Graphic) //,Animated:Boolean=false,Reverse:Boolean=false,Width:uint=0,Height:uint=0,Unique:Boolean=false)
		{
			loadTiles(Graphic);
			return this;
		}

        /// <summary>
        /// Draws this block
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void render(SpriteBatch spriteBatch)
        {
            if (_tex == null)
                return;

            _p = getScreenXY();
            int opx = (int)_p.X;
            for (int i = 0; i < _rects.Length; i++)
            {
                if (_rects[i] != Rectangle.Empty)
                {
                    spriteBatch.Draw(_tex, _p, _rects[i], _color);
                }
                _p.X += _tileWidth;
                if (_p.X >= opx + width)
                {
                    _p.X = opx;
                    _p.Y += _tileHeight;
                }
            }
        }
    }
}
