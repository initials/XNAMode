using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
#if !WINDOWS_PHONE
//using Microsoft.Xna.Framework.GamerServices;
#endif
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using org.flixel;
using System.IO;

namespace Loader_Revvolvver
{
    /// <summary>
    /// Starts the game
    /// </summary>
    public class FlxFactory : Microsoft.Xna.Framework.Game
    {




        

        //primary display buffer constants
#if !WINDOWS_PHONE

        /// <summary>
        /// DO NOT CHANGE THESE VALUES!!
        /// your game should only be concerned with the
        /// resolution parameters used when you call
        /// initGame() in your FlxGame class.
        /// </summary>
        private int resX = 1280;
        /// <summary>
        /// DO NOT CHANGE THESE VALUES!!
        /// your game should only be concerned with the
        /// resolution parameters used when you call
        /// initGame() in your FlxGame class.
        /// </summary>
        private int resY = 720;
#else
        private int resX = 480; //DO NOT CHANGE THESE VALUES!!
        private int resY = 800;  //your game should only be concerned with the
                                 //resolution parameters used when you call
                                 //initGame() in your FlxGame class.
#endif

#if XBOX360
        private bool _fullScreen = true;
#else
        private bool _fullScreen = false;
#endif
        //graphics management
        public GraphicsDeviceManager _graphics;
        //other variables
        private FlxGame _flixelgame;

        //nothing much to see here, typical XNA initialization code
        public FlxFactory()
        {
            //Read the GAMESETTINGS.txt file

            string gameSettings = File.ReadAllText("GAMESETTINGS.txt");
            string[] splitter = gameSettings.Split('\n');
            //Console.WriteLine(splitter[0]);

            resX = Convert.ToInt32(splitter[0].Substring(2));
            resY = Convert.ToInt32(splitter[1].Substring(2));
            if (splitter[2].Substring(11) == "1")
                _fullScreen = true;


            //set up the graphics device and the content manager
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (_fullScreen)
            {
                //resX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                //resY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                if (GraphicsAdapter.DefaultAdapter.IsWideScreen)
                {
                    //if user has it set to widescreen, let's make sure this
                    //is ACTUALLY a widescreen resolution.
                    if (((resX / 16) * 9) != resY)
                    {
                        resX = (resY / 9) * 16;
                    }
                }
            }

            //we don't need no new-fangled pixel processing
            //in our retro engine!
            _graphics.PreferMultiSampling = false;
            //set preferred screen resolution. This is NOT
            //the same thing as the game's actual resolution.
            _graphics.PreferredBackBufferWidth = resX;
            _graphics.PreferredBackBufferHeight = resY;
            //make sure we're actually running fullscreen if
            //fullscreen preference is set.
            if (_fullScreen && _graphics.IsFullScreen == false)
            {
                _graphics.ToggleFullScreen();
            }
            _graphics.ApplyChanges();

            FlxG.Game = this;
#if !WINDOWS_PHONE
            //Components.Add(new GamerServicesComponent(this));
#endif
        }
        /// <summary>
        /// load up the master class, and away we go!
        /// </summary>
        protected override void Initialize()
        {
            //load up the master class, and away we go!

            //_flixelgame = new FlxGame();
            _flixelgame = new FlixelEntryPoint2(this);

            FlxG.bloom = new BloomPostprocess.BloomComponent(this);

            Components.Add(_flixelgame);
            Components.Add(FlxG.bloom);

            base.Initialize();
        }

    }

    #region Application entry point

    static class Program
    {
        //application entry point
        static void Main(string[] args)
        {
            using (FlxFactory game = new FlxFactory())
            {
                game.Run();
            }
        }
    }

    #endregion
}
