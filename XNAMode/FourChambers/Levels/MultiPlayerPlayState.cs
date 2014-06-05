using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class MultiPlayerPlayState : BasePlayStateFromOel
    {
        override public void create()
        {
            base.create();

            //FlxG.playMusic("music/" + FourChambers_Globals.MUSIC_TUTORIAL, 1.0f);

            FlxG.hideHud();
            FourChambers_Globals.seraphineHasBeenKilled = true;

            marksman.canFly = false;
            marksman.hasMeleeWeapon = true;
            marksman.hasRangeWeapon = true;
            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));


            mistress.startPlayingBack("FourChambers/ActorRecording/mistress.txt");
            //warlock.startPlayingBack("FourChambers/ActorRecording/warlock.txt");
            unicorn.startPlayingBack("FourChambers/ActorRecording/unicorn.txt");
            executor.startPlayingBack("FourChambers/ActorRecording/executor.txt");

            vampire.startPlayingBack("FourChambers/ActorRecording/vampire.txt");
            paladin.startPlayingBack("FourChambers/ActorRecording/paladin.txt");



            mistress.health = 10;
            unicorn.health = 10;
            vampire.health = 10;
            paladin.health = 10;


            FlxG.unfollow();


        }
        override public void update()
        {


            base.update();
        }
    }

}
