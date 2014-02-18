using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Lemonade
{
    public class IntroState : FlxState
    {

        override public void create()
        {
            base.create();

            FlxTilemap bgMap = new FlxTilemap();
            bgMap.auto = FlxTilemap.STRING;
            bgMap.indexOffset = -1;
            bgMap.loadTMXMap("Lemonade/levels/slf2/newyork_intro.tmx", "map", "bg", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap.boundingBoxOverride = false;
            bgMap.setScrollFactors(1, 1);
            add(bgMap);

            bgMap = new FlxTilemap();
            bgMap.auto = FlxTilemap.STRING;
            bgMap.indexOffset = -1;
            bgMap.loadTMXMap("Lemonade/levels/slf2/newyork_intro.tmx", "map", "bg2", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap.boundingBoxOverride = false;
            bgMap.setScrollFactors(1, 1);
            add(bgMap);

            FlxTilemap bgMap3 = new FlxTilemap();
            bgMap3.auto = FlxTilemap.STRING;
            bgMap3.indexOffset = -1;
            bgMap3.loadTMXMap("Lemonade/levels/slf2/newyork_intro.tmx", "map", "stars", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap3.boundingBoxOverride = false;
            bgMap3.setScrollFactors(1, 1);
            add(bgMap3);

            FlxTilemap bgMap4 = new FlxTilemap();
            bgMap4.auto = FlxTilemap.STRING;
            bgMap4.indexOffset = -1;
            bgMap4.loadTMXMap("Lemonade/levels/slf2/newyork_intro.tmx", "map", "city", FlxXMLReader.TILES, FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            bgMap4.boundingBoxOverride = false;
            bgMap4.setScrollFactors(1, 1);
            add(bgMap4);

            FlxSprite follower = new FlxSprite(0, 0);
            follower.visible = false;
            add(follower);
            follower.velocity.Y = 450;

            FlxG.follow(follower, 20.0f);
            FlxG.followBounds(0, 0, int.MaxValue, 2000);


            /*
             *     logoText = [FlxText textWithWidth:FlxG.width
                                 text:@"Initials Video Games\nPresents"
                                 font:SMALLPIXEL
                                 size:16.0];
                    logoText.color = 0xffed008e;
                    logoText.alignment = CENTER_ALIGN;
                    logoText.scrollFactor = CGPointMake(1.05, 1.05);
                    logoText.x = 0;
                    logoText.y = FlxG.height/2;
                    logoText.scale = CGPointMake(0, 0.5);
                    logoText.alpha = 0 ;
             */

            //FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL")

            FlxText text1 = new FlxText(0, FlxG.height / 2, FlxG.width, "Initials Video Games Presents");
            text1.setFormat(null, 3, Color.Pink, FlxJustification.Center, Color.Black);
            text1.setScrollFactors(1.5f, 1.5f);
            text1.color = new Color(237, 0, 142);
            add(text1);

            //rgb(237, 0, 142)


        }

        override public void update()
        {




            base.update();
        }


    }
}
