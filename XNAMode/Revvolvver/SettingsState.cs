using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#if !WINDOWS_PHONE
using Microsoft.Xna.Framework.Storage;
#endif
using org.flixel;

namespace Revvolvver
{
    /// <summary>
    /// The main menu for the game Mode.
    /// </summary>
    public class SettingsState : FlxState
    {
        //private static bool _alreadySaved = false;

        private Texture2D ImgGibs;
        private Texture2D ImgCursor;

        private FlxEmitter _gibs;
        private bool _ok2;
        private FlxGroup textGrp;

        private int frameCount;
        private int clickCount;

        private const string SndClick = "Revvolvver/sfx/gunclick";
        private const string SndGun1 = "Revvolvver/sfx/gunshot1";
        private const string SndGun2 = "Revvolvver/sfx/gunshot2";
        private const string SndChord1 = "Revvolvver/sfx/chord_01";
        private const string SndChord2 = "Revvolvver/sfx/chord_02";
        private const string SndChord3 = "Revvolvver/sfx/chord_03";
        private const string SndChord4 = "Revvolvver/sfx/chord_04";
        private const string SndChord5 = "Revvolvver/sfx/chord_05";
        private const string SndChord6 = "Revvolvver/sfx/chord_06";
        private const string SndChord7 = "Revvolvver/sfx/chord_07";

        private string[] Snds;

        private const string Music = "Revvolvver/sfx/asong";

        private FlxSound SndFlxClick;

        private FlxSprite flower;

        private FlxText playersText;
        private FlxText playersTextx;

        private int currentTextSelected = 0;

        //#if !WINDOWS_PHONE
        //        FlxSave save;
        //        private bool hasCheckedSaveFile = false;
        //#endif

        override public void create()
        {

            FlxG.bloom.Visible = false;



            FlxG.backColor = new Color(0xdb, 0xd8, 0xac);

            base.create();

            flower = new FlxSprite(FlxG.width / 2 - 290 / 2, FlxG.height / 2 - 290 / 2 + 60);
            flower.loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/flower"), false, false, 290, 290);
            add(flower);
            flower.visible = false;
            flower.alpha = 0.1f;

            flower.angularDrag = 1500;


            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs = FlxXMLReader.readAttributesFromOelFile("Revvolvver/titlescreen.oel", "level/NonDestructable");
            FlxTilemap _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 16, 16);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            add(_tileMap);

            FlxG.hideHud();

            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgCursor = FlxG.Content.Load<Texture2D>("initials/crosshair");

            _gibs = new FlxEmitter(0, FlxG.height - 30);
            _gibs.setSize(FlxG.width, 30);
            _gibs.setYSpeed(-1400, -1020);
            _gibs.setRotation(-720, 720);
            _gibs.gravity = 50;
            _gibs.createSprites(ImgGibs, 1000);
            add(_gibs);


            textGrp = new FlxGroup();

            for (int i = 0; i < Revvolvver_Globals.GameSettings.Length; i++)
            {
                string value = Revvolvver_Globals.GameSettings[i].Name + ": " + Revvolvver_Globals.GameSettings[i].DefaultAmount;
                playersText = new FlxText(70, 40 + (i*20), FlxG.width, value);
                playersText.alignment = FlxJustification.Left;
                playersText.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0xff, 0x6e, 0x55), FlxJustification.Left, new Color(0xff, 0x6e, 0x55));
                playersText.shadow = new Color(0xff, 0x6e, 0x55);
                playersText.scale = 1; // size = 32
                playersText.color = new Color(0xff, 0x6e, 0x55);
                playersText.antialiasing = false;
                textGrp.add(playersText);
                if (i == 0) playersText.color = Color.Red;

            }

            add(textGrp);
            

        }



        override public void update()
        {
            
            base.update();

            if (FlxG.keys.justPressed(Keys.Up))
            {
                currentTextSelected--;
                if (currentTextSelected < 0) currentTextSelected = textGrp.members.Count - 1;
            }
            if (FlxG.keys.justPressed(Keys.Down))
            {
                currentTextSelected++;
                if (currentTextSelected > textGrp.members.Count - 1) currentTextSelected = 0;
            }

            if (FlxG.keys.RIGHT )
            {
                Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount += 1.0f;

                if (Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount > Revvolvver_Globals.GameSettings[currentTextSelected].MaxAmount)
                {
                    Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount = Revvolvver_Globals.GameSettings[currentTextSelected].MaxAmount;
                }

                string value = Revvolvver_Globals.GameSettings[currentTextSelected].Name + ": " + Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount.ToString();
                ((FlxText)(textGrp.members[currentTextSelected])).text = value;
            }
            if (FlxG.keys.LEFT)
            {
                Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount -= 1.0f;

                if (Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount < Revvolvver_Globals.GameSettings[currentTextSelected].MinAmount)
                {
                    Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount = Revvolvver_Globals.GameSettings[currentTextSelected].MinAmount;
                }

                string value = Revvolvver_Globals.GameSettings[currentTextSelected].Name + ": " + Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount.ToString();
                ((FlxText)(textGrp.members[currentTextSelected])).text = value;
            }


            for (int i = 0; i < textGrp.members.Count; i++)
            {






                ((FlxText)(textGrp.members[i])).color = Color.White;
            }

            ((FlxText)(textGrp.members[currentTextSelected])).color = Color.Red;

            if (FlxG.keys.justPressed(Keys.Enter) || FlxG.gamepads.isNewButtonPress(Buttons.Start))
            {
                FlxG.play(SndGun2, 0.35f);
                FlxG.fade.start(new Color(0xd1, 0x6e, 0x55), 1f, onFade, false);
                return;
            }


            


        }

        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.state = new MenuState();
            
        }

    }
}
