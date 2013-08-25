using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BloomPostprocess;

namespace org.flixel
{
    /// <summary>
    /// Call back for when animations finish.
    /// </summary>
    /// <param name="Name">name of the call back</param>
    /// <param name="Frame">frame to call back on</param>
    /// <param name="FrameIndex">Frame index ?</param>
    public delegate void FlxAnimationCallback(string Name, uint Frame, int FrameIndex);

    /// <summary>
    /// This is a global helper class full of useful functions for audio,
    /// input, basic info, and the camera system among other things.
    /// </summary>
    public class FlxG
    {
        /// <summary>
        /// FlxG.bloom is a bloom effect across everything!
        /// </summary>
        public static BloomComponent bloom;

        //@benbaird Global, XNA-specific stuff that any component should be able
        // to access from anywhere. (As read-only properties, generally.)
        public static Game Game;

        /// <summary>
        /// Content Manager
        /// </summary>
        private static ContentManager _content;
        private static Texture2D _xnatiles;
        private static SpriteFont _font;
        private static SpriteBatch _spriteBatch;

        /// <summary>
        /// If you build and maintain your own version of flixel,
        /// you can give it your own name here.  Appears in the console.
        /// </summary>
		static public string LIBRARY_NAME = "X-flixel-initials";
        /// <summary>
        /// Assign a major version to your library.
        /// Appears before the decimal in the console.
        /// </summary>
		static public uint LIBRARY_MAJOR_VERSION = 2;

        /// <summary>
        /// Assign a minor version to your library.
        /// Appears after the decimal in the console.
        /// </summary>
		static public uint LIBRARY_MINOR_VERSION = 43;

        /// <summary>
        /// Internal tracker for game object (so we can pause & unpause)
        /// </summary>
        static protected internal FlxGame _game;
		/// <summary>
        /// Internal tracker for game pause state.
		/// </summary>
		static protected bool _pause;

        /// <summary>
        /// Whether you are running in Debug or Release mode.
        /// Set automatically by <code>FlxFactory</code> during startup.
        /// </summary>
		static public bool debug;
		/// <summary>
        /// Set <code>showBounds</code> to true to display the bounding boxes of the in-game objects.
		/// </summary>
		static public bool showBounds;

        /// <summary>
        /// Elapsed as a gametime
        /// </summary>
        public static GameTime elapsedAsGameTime;
        /// <summary>
        ///  Represents the amount of time in seconds that passed since last frame.
        /// </summary>
        public static float elapsed = 0f;
        /// <summary>
        /// @benbaird compatibility with AS3's getTimer()
        /// </summary>
        public static uint getTimer = 0;
		/// <summary>
        /// Essentially locks the framerate to a minimum value - any slower and you'll get slowdown instead of frameskip; default is 1/30th of a second.
		/// </summary>
		static public float maxElapsed;
		/// <summary>
        /// How fast or slow time should pass in the game; default is 1.0.
		/// </summary>
		static public float timeScale;

        /// <summary>
        /// A holder for the level of zoom the game is.
        /// </summary>
        static public int zoom;


        /// <summary>
        /// A reference or pointer to the current FlxState object being used by the game
        /// </summary>
        public static FlxState state
        {
            get
            {
                return _game._state;
            }
            set
            {
                _game.switchState(value);
            }
        }

        /// <summary>
        /// The width of the screen in game pixels.
        /// </summary>
        public static int width = 1280;
        /// <summary>
        /// The height of the screen in game pixels.
        /// </summary>
        public static int height = 720;

        /// <summary>
        /// BG color
        /// </summary>
        public static Color backColor = Color.Black;

		/// <summary>
        /// Setting this to true will disable/skip stuff that isn't necessary for mobile platforms like Android (or Windows Phone 7). [BETA]
		/// </summary>
		static public bool mobile; 

        /// <summary>
        /// <code>FlxG.levels</code> and <code>FlxG.scores</code> are generic
        /// global variables that can be used for various cross-state stuff.
        /// </summary>
        static public List<int> levels = new List<int>();
        static public int level;

        /// <summary>
        /// <code>FlxG.levels</code> and <code>FlxG.scores</code> are generic
        /// global variables that can be used for various cross-state stuff.
        /// </summary>
        static public List<int> scores = new List<int>();
        static public int score;

        /// <summary>
        /// Stores the players username, 
        /// </summary>
        public static string username;
		/**
		 * <code>FlxG.saves</code> is a generic bucket for storing
		 * FlxSaves so you can access them whenever you want.
		 */
#if !WINDOWS_PHONE
        static public List<FlxSave> saves = new List<FlxSave>(); 
        static public int save;
#endif

        /// <summary>
        /// @benbaird X-flixel only. Returns the scale of the screen size in comparison to the actual game size.
        /// </summary>
        private static float _scale = 0;
        public static float scale
        {
            get { return _scale; }
        }

		/// <summary>
        /// A reference to a <code>FlxMouse</code> object.  Important for input!
		/// </summary>
		static public FlxMouse mouse = new FlxMouse();
		/// <summary>
        ///  A reference to a <code>FlxKeyboard</code> object.  Important for input!
		/// </summary>
		static public FlxKeyboard keys = new FlxKeyboard();
		/// <summary>
        /// An array of <code>FlxGamepad</code> objects.  Important for input!
		/// </summary>
		static public FlxGamepad gamepads = new FlxGamepad();

        /// <summary>
        /// @benbaird Used for compatibility with Xbox input standards
        /// </summary>
        public static PlayerIndex? controllingPlayer
        {
            get;
            set;
        }

		/// <summary>
        /// A handy container for a background music object.
		/// </summary>
		static public FlxSound music;
		/// <summary>
        /// A list of all the sounds being played in the game.
		/// </summary>
		static public List<FlxSound> sounds = new List<FlxSound>();
		/// <summary>
        /// Internal flag for whether or not the game is muted.
		/// </summary>
		static protected bool _mute;
		/// <summary>
        /// Internal volume level, used for global sound control.
		/// </summary>
		static protected float _volume;

        /// <summary>
        /// Adjusts the angle of the entire game.
        /// Not working since moving to 3x pixel size.
        /// </summary>
        static public float angle;

		/// <summary>
        /// Tells the camera to follow this <code>FlxCore</code> object around.
		/// </summary>
		static public FlxObject followTarget;
		/// <summary>
        /// Used to force the camera to look ahead of the <code>followTarget</code>.
		/// </summary>
		static public Vector2 followLead;
		/// <summary>
        /// Used to smoothly track the camera as it follows.
		/// </summary>
		static public float followLerp;
		/// <summary>
        /// Stores the top and left edges of the camera area.
		/// </summary>
		static public Point followMin;
		/// <summary>
        /// Stores the bottom and right edges of the camera area.
		/// </summary>
		static public Point followMax;
		/// <summary>
        /// Internal, used to assist camera and scrolling.
		/// </summary>
		static protected Vector2 _scrollTarget;

        /// <summary>
        /// Stores the basic parallax scrolling values.
        /// </summary>
        static public Vector2 scroll;

		/**
		 * Reference to the active graphics buffer.
		 * Can also be referenced via <code>FlxState.screen</code>.
		 */
        //static public var buffer:BitmapData;
		/**
		 * Internal storage system to prevent graphics from being used repeatedly in memory.
		 */
        //static protected var _cache:Object;

		/**
		 * The support panel (twitter, reddit, stumbleupon, paypal, etc) visor thing
		 */
        //static public FlxPanel panel;
		/// <summary>
        /// A special effect that shakes the screen.  Usage: FlxG.quake.start();
		/// </summary>
		static public FlxQuake quake;
		/// <summary>
        /// A special effect that flashes a color on the screen.  Usage: FlxG.flash.start();
		/// </summary>
		static public FlxFlash flash;
		/// <summary>
        /// A special effect that fades a color onto the screen.  Usage: FlxG.fade.start();
		/// </summary>
        static public FlxFade fade;

        /// <summary>
        /// Does a checkboard transition.
        /// </summary>
        static public FlxTransition transition;

        /// <summary>
        /// Log data to the developer console.
        /// </summary>
        /// <param name="Data">Anything you want to log to the console.</param>
        public static void log(string Data) { _game._console.log(Data); }

        /// <summary>
        /// public access to showing the hud
        /// </summary>
        public static void showHud() { _game._hud.showHud(); }

        /// <summary>
        /// public access to hiding the hud
        /// </summary>
        public static void hideHud() { _game._hud.hideHud(); }

        /// <summary>
        /// Set's the hud text to, for the player number
        /// </summary>
        /// <param name="Player">Number of the player you want the hud text to be.</param>
        /// <param name="Data">String to enter.</param>
        public static void setHudText(int Player, string Data) { _game._hud.setHudText(Player, Data); }

        /// <summary>
        /// resets strings to "" and positions to zero
        /// </summary>
        public static void resetHud() { _game._hud.reset(); }

        /// <summary>
        /// Sets the gamepad hud (non scaled) graphic to a button, and X and Y values.
        /// Use FlxButton.GamePad** to choose a button
        /// </summary>
        /// <param name="Button">The button you'd like, use FlxButton.GamePad** to choose.</param>
        /// <param name="X">X Position</param>
        /// <param name="Y">Y Position</param>
        public static void setHudGamepadButton(int Button, float X, float Y) { _game._hud.setHudGamepadButton(Button, X , Y); }

        /// <summary>
        /// Not working yet!
        /// </summary>
        public static void takeScreenshot() { _game.takeScreenshot(); }


        /// <summary>
        /// Tint the entire game
        /// <example>FlxG.color(Color.Tomato);</example>
        /// </summary>
        /// <param name="co">To this color</param>
        public static void color(Color co) { _game.color = co; }


        /// <summary>
        /// Set <code>pause</code> to true to pause the game, all sounds, and display the pause popup.
        /// </summary>
        static public bool pause
        {
            get { return _pause; }
            set
            {
                if (_pause != value)
                {
                    _pause = value;
                    if (_pause)
                    {
                        _game.pauseGame();
                        pauseSounds();
                    }
                    else
                    {
                        _game.unpauseGame();
                        playSounds();
                    }
                }
            }
        }

        /// <summary>
        /// @benbaird Begin XNA-specific public static properties
        /// 
        /// whether to automatically handle user pause requests. Typically you'd set this to true only for gameplay states, and set to false for all others (menus, etc.)
        /// </summary>
        public static bool autoHandlePause = false; 

        /// <summary>
        /// Content manager
        /// </summary>
        public static ContentManager Content
        {
            get { return _content; }
        }
        public static SpriteFont Font
        {
            get { return _font; }
        }
        public static SpriteBatch spriteBatch
        {
            get { return _spriteBatch; }
        }
        public static Texture2D XnaSheet
        {
            get { return _xnatiles; }
        }

        /// <summary>
        /// Load Content
        /// </summary>
        /// <param name="gd"> Graphics device. </param>
        public static void LoadContent(GraphicsDevice gd)
        {
            _content = Game.Content;

            _spriteBatch = new SpriteBatch(gd);
            _font = _content.Load<SpriteFont>("flixel/deffont");
            _xnatiles = _content.Load<Texture2D>("flixel/xna_tiles");

            _scale = ((float)_game.targetWidth / (float)width);
            FlxG.quake = new FlxQuake((int)_scale);
            FlxG.flash = new FlxFlash();
            FlxG.fade = new FlxFade();
            FlxG.transition = new FlxTransition();
            FlxG.transition.createSprites(null, Color.DarkViolet, 12, 7, 30, 30, 0, 20, 0.03f);

        }
        //@benbaird End XNA-specific public static properties

		/// <summary>
        /// Reset the input helper objects (useful when changing screens or states)
		/// </summary>
		static public void resetInput()
		{
			keys.reset();
			mouse.reset();
            gamepads.reset();
		}

        /// <summary>
        /// Set up and play a looping background soundtrack.
        /// </summary>
        /// <param name="Music">The sound file you want to loop in the background.</param>
        static public void playMusic(string Music)
        {
            playMusic(Music, 1.0f);
        }
        /// <summary>
        /// Set up and play a looping background soundtrack.
        /// </summary>
        /// <param name="Music">The sound file you want to loop in the background.</param>
        /// <param name="Volume">How loud the sound should be, from 0 to 1.</param>
		static public void playMusic(string Music, float Volume)
		{
			if(music == null)
				music = new FlxSound();
			else if(music.active)
				music.stop();
			music.loadEmbedded(Music,true);
			music.volume = Volume;
			music.survive = true;
			music.play();
		}

        /// <summary>
        /// Creates a new sound object from an embedded <code>Class</code> object.
        /// </summary>
        /// <param name="EmbeddedSound">The sound you want to play.</param>
        /// <returns>A <code>FlxSound</code> object.</returns>
        static public FlxSound play(string EmbeddedSound)
        {
            return play(EmbeddedSound, 1.0f, false);
        }
        /// <summary>
        /// Creates a new sound object from an embedded <code>Class</code> object.
        /// </summary>
        /// <param name="EmbeddedSound">The sound you want to play.</param>
        /// <param name="Volume">How loud to play it (0 to 1).</param>
        /// <returns>A <code>FlxSound</code> object.</returns>
        static public FlxSound play(string EmbeddedSound, float Volume)
        {
            return play(EmbeddedSound, Volume, false);
        }
        /// <summary>
        /// Creates a new sound object from an embedded <code>Class</code> object.
        /// </summary>
        /// <param name="EmbeddedSound">The sound you want to play.</param>
        /// <param name="Volume">How loud to play it (0 to 1).</param>
        /// <param name="Looped">Whether or not to loop this sound.</param>
        /// <returns>A <code>FlxSound</code> object.</returns>
        static public FlxSound play(string EmbeddedSound, float Volume, bool Looped)
		{
			int i = 0;
			int sl = sounds.Count;
			while(i < sl)
			{
				if(!(sounds[i] as FlxSound).active)
					break;
				i++;
			}
			if(i >= sl)
				sounds.Add(new FlxSound());
            sounds[i].loadEmbedded(EmbeddedSound, Looped);
            sounds[i].volume = Volume;
            sounds[i].play();
            return sounds[i];
		}

        /// <summary>
        /// Set <code>mute</code> to true to turn off the sound.
        /// 
        /// default = false;
        /// </summary>
        public static bool mute
        {
            get { return _mute; }
            set { _mute = value; changeSounds(); }
        }

        /// <summary>
        /// Get a number that represents the mute state that we can multiply into a sound transform.
        /// </summary>
        /// <returns>An unsigned integer - 0 if muted, 1 if not muted.</returns>
		static public int getMuteValue()
		{
			if(_mute)
				return 0;
			else
				return 1;
		}

        /// <summary>
        /// Volume control.
        /// </summary>
        static public float volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                if (_volume < 0)
                    _volume = 0;
                else if (_volume > 1)
                    _volume = 1;
                changeSounds();
            }
        }

        /// <summary>
        /// Called by FlxGame on state changes to stop and destroy sounds.
        /// </summary>
        /// <param name="ForceDestroy">Kill sounds even if they're flagged <code>survive</code>.</param>
		static internal void destroySounds(bool ForceDestroy)
		{
			if(sounds == null)
				return;
			if((music != null) && (ForceDestroy || !music.survive))
				music.destroy();
			int i = 0;
			FlxSound s;
			int sl = sounds.Count;
			while(i < sl)
			{
				s = sounds[i++] as FlxSound;
				if((s != null) && (ForceDestroy || !s.survive))
					s.destroy();
			}
		}

		/// <summary>
        /// An internal function that adjust the volume levels and the music channel after a change.
		/// </summary>
		static protected void changeSounds()
		{
			if((music != null) && music.active)
				music.updateTransform();
			int i = 0;
			FlxSound s;
			int sl = sounds.Count;
			while(i < sl)
			{
				s = sounds[i++] as FlxSound;
				if((s != null) && s.active)
					s.updateTransform();
			}
		}

		/// <summary>
        /// Called by the game loop to make sure the sounds get updated each frame.
		/// </summary>
		static internal void updateSounds()
		{
			if((music != null) && music.active)
				music.update();
			int i = 0;
			FlxSound s;
			int sl = sounds.Count;
			while(i < sl)
			{
				s = sounds[i++] as FlxSound;
				if((s != null) && s.active)
					s.update();
			}
		}

		/// <summary>
        /// Internal helper, pauses all game sounds.
		/// </summary>
		static protected void pauseSounds()
		{
			if((music != null) && music.active)
				music.pause();
			int i = 0;
			FlxSound s;
			int sl = sounds.Count;
			while(i < sl)
			{
				s = sounds[i++] as FlxSound;
				if((s != null) && s.active)
					s.pause();
			}
		}

		/// <summary>
        /// Internal helper, pauses all game sounds.
		/// </summary>
		static protected void playSounds()
		{
			if((music != null) && music.active)
				music.play();
			int i = 0;
            FlxSound s;
			int sl = sounds.Count;
			while(i < sl)
			{
				s = sounds[i++] as FlxSound;
				if((s != null) && s.active)
					s.play();
			}
		}

        /// <summary>
        /// Tells the camera subsystem what FlxCore object to follow
        /// </summary>
        /// <param name="Target">The object to follow</param>
        /// <param name="Lerp">How much lag the camera should have (can help smooth out the camera movement)</param>
		static public void follow(FlxObject Target, float Lerp)
		{
			followTarget = Target;
			followLerp = Lerp;

            if (Target == null)
                return;

            scroll.X = _scrollTarget.X = (width >> 1) - followTarget.x - ((int)followTarget.width >> 1);
            scroll.Y = _scrollTarget.Y = (height >> 1) - followTarget.y - ((int)followTarget.height >> 1);
		}
		
        /// <summary>
        /// Specify an additional camera component - the velocity-based "lead", or amount the camera should track in front of a sprite
        /// </summary>
        /// <param name="LeadX">Percentage of X velocity to add to the camera's motion</param>
        /// <param name="LeadY">Percentage of Y velocity to add to the camera's motion</param>
		static public void followAdjust(float LeadX, float LeadY)
		{
			followLead = new Vector2(LeadX, LeadY);
		}

        /// <summary>
        /// Specify the boundaries of the level or where the camera is allowed to move.
        /// </summary>
        /// <param name="MinX">The smallest X value of your level (usually 0).</param>
        /// <param name="MinY">The smallest Y value of your level (usually 0).</param>
        /// <param name="MaxX">The largest X value of your level (usually the level width).</param>
        /// <param name="MaxY">The largest Y value of your level (usually the level height).</param>
        static public void followBounds(int MinX, int MinY, int MaxX, int MaxY)
        {
            followBounds(MinX, MinY, MaxX, MaxY, true);
        }
        /// <summary>
        /// Specify the boundaries of the level or where the camera is allowed to move.
        /// </summary>
        /// <param name="MinX">The smallest X value of your level (usually 0).</param>
        /// <param name="MinY">The smallest Y value of your level (usually 0).</param>
        /// <param name="MaxX">The largest X value of your level (usually the level width).</param>
        /// <param name="MaxY">The largest Y value of your level (usually the level height).</param>
        /// <param name="UpdateWorldBounds">Whether the quad tree's dimensions should be updated to match.</param>
        static public void followBounds(int MinX, int MinY, int MaxX, int MaxY, bool UpdateWorldBounds)
		{
            followMin = new Point(-MinX, -MinY);
            followMax = new Point(-MaxX + width, -MaxY + height);
            if (followMax.X > followMin.X)
                followMax.X = followMin.X;
            if (followMax.Y > followMin.Y)
                followMax.Y = followMin.Y;
            if (UpdateWorldBounds)
                FlxU.setWorldBounds(MinX, MinY, MaxX - MinX, MaxY - MinY);
            doFollow();
        }

        /// <summary>
        /// Stops and resets the camera.
        /// </summary>
        internal static void unfollow()
        {
            followTarget = null;
            followLead = Vector2.Zero;
            followLerp = 1;
            followMin = Point.Zero;
            followMax = Point.Zero;
            scroll = new Vector2();
            _scrollTarget = new Vector2();
        }

        /// <summary>
        /// Called by <code>FlxGame</code> to set up <code>FlxG</code> during <code>FlxGame</code>'s constructor.
        /// </summary>
        /// <param name="Game">Game</param>
        /// <param name="Width">W</param>
        /// <param name="Height">H</param>
        static internal void setGameData(FlxGame Game, int Width, int Height)
		{
            _game = Game;
			width = Width;
			height = Height;

            _mute = false;
            _volume = 0.5f;

			unfollow();

            level = 0;
            score = 0;

            pause = false;
            timeScale = 1.0f;
            maxElapsed = 0.0333f;
            FlxG.elapsed = 0;
            showBounds = false;
#if !WINDOWS_PHONE
            mobile = false;
#else
            mobile = true;
#endif
            FlxU.setWorldBounds(0, 0, FlxG.width, FlxG.height);
        }


        /// <summary>
        /// Internal function that updates the camera and parallax scrolling.
        /// </summary>
        internal static void doFollow()
		{
			if(followTarget != null)
			{
				if(followTarget.exists && !followTarget.dead)
				{
                    _scrollTarget.X = (width >> 1) - followTarget.x - ((int)followTarget.width >> 1);
                    _scrollTarget.Y = (height >> 1) - followTarget.y - ((int)followTarget.height >> 1);
					if((followLead != null) && (followTarget is FlxSprite))
					{
                        _scrollTarget.X -= (followTarget as FlxSprite).velocity.X * followLead.X;
                        _scrollTarget.Y -= (followTarget as FlxSprite).velocity.Y * followLead.Y;
					}
				}
                scroll.X += (_scrollTarget.X - scroll.X) * followLerp * FlxG.elapsed;
                scroll.Y += (_scrollTarget.Y - scroll.Y) * followLerp * FlxG.elapsed;

				if(followMin != null)
				{
					if(scroll.X > followMin.X)
						scroll.X = followMin.X;
					if(scroll.Y > followMin.Y)
						scroll.Y = followMin.Y;
				}
				
				if(followMax != null)
				{
					if(scroll.X < followMax.X)
						scroll.X = followMax.X;
					if(scroll.Y < followMax.Y)
                        scroll.Y = followMax.Y;
				}
			}
		}
    }
}
