using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using org.flixel;
using XNATweener;


namespace FourChambers
{
    class PlayHud : FlxGroup
    {
        /// <summary>
        /// use setArrowsRemaining(Member)
        /// </summary>
        public FlxText arrowsRemaining;

        public FlxText healthText;
        /// <summary>
        /// Use score.text = "" to set the score.
        /// </summary>
        public FlxText score;
        public FlxText combo;

        public FlxText nestsRemaining;
        
        private Tweener tweenPos;
        private Tweener tweenScale;

        public FlxSprite currentAnimatedObj;

        private float ypos;

        public LevelBeginText comboOnScreen;

        public PlayHud()
        {

            currentAnimatedObj = new FlxSprite();

            ypos = FlxG.height * FlxG.zoom-30;

			#if __ANDROID__

			ypos -= 60;

			#endif

            FlxSprite hudGraphic = new FlxSprite(0,0, FlxG.Content.Load<Texture2D>("fourchambers/hudElements"));
            hudGraphic.scrollFactor.X = 0;
            hudGraphic.scrollFactor.Y = 0;
            hudGraphic.scale = 2;
            add(hudGraphic);
            hudGraphic.x = hudGraphic.width / 2;
            hudGraphic.y = ypos - hudGraphic.height /2;

            //// these are treasures.
            //for (int i = 0; i < 11; i++)
            //{
            //    hudGraphic = new FlxSprite(100 + ((i * 16) + 1), ypos, null);
            //    hudGraphic.loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/treasures_16x16"), true, false, 16, 16);
            //    hudGraphic.width = 16;
            //    hudGraphic.height = 16;
            //    hudGraphic.addAnimation("off", new int[] { i*2 });
            //    hudGraphic.addAnimation("on", new int[] { i*2+1 });
            //    hudGraphic.play("off");
            //    hudGraphic.scrollFactor.X = 0;
            //    hudGraphic.scrollFactor.Y = 0;
            //    add(hudGraphic);
            //}

            arrowsRemaining = new FlxText(65, ypos-5, 100);
            arrowsRemaining.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            arrowsRemaining.text = "00";
            add(arrowsRemaining);

            healthText = new FlxText(30, ypos - 5, 100);
            healthText.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            healthText.text = "00";
            add(healthText);

            combo = new FlxText(140, ypos - 10, 100);
            combo.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            combo.text = "Combo: 0x";
            add(combo);

            score = new FlxText(320, ypos - 10, 100);
            score.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            score.text = "Score: 000000";
            add(score);

            nestsRemaining = new FlxText(520, ypos - 10, 100);
            nestsRemaining.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
			nestsRemaining.text = "";
            add(nestsRemaining);

            tweenScale = new Tweener(10, 1, TimeSpan.FromSeconds(1.0f), Linear.EaseOut);
            
            tweenPos = new Tweener(100, ypos, TimeSpan.FromSeconds(2.0f), Bounce.EaseOut);

            comboOnScreen = new LevelBeginText(200, 200, 100);
            comboOnScreen.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            comboOnScreen.text = "0";
            comboOnScreen.style = "up";
            comboOnScreen.limit = 1.2f;
            //comboOnScreen.setScrollFactors(0,0);
            add(comboOnScreen);


        }

        public override void update()
        {
            tweenScale.Update(FlxG.elapsedAsGameTime);
            tweenPos.Update(FlxG.elapsedAsGameTime);
            
            if (currentAnimatedObj != null)
            {
                currentAnimatedObj.scale = tweenScale.Position;
                currentAnimatedObj.y = tweenPos.Position;
            }

            int i2 = 0;
            int l2 = this.members.Count;
            
            while (i2 < l2)
            {
                if (this.members[i2].GetType().ToString() == "org.flixel.FlxText")
                {
                    if ((this.members[i2] as FlxText).scale > 2)
                    {
                        (this.members[i2] as FlxText).scale -= 0.1f;
                    }
                    else if ((this.members[i2] as FlxText).scale < 2)
                    {
                        (this.members[i2] as FlxText).scale = 2;
                    }
                }

                i2++;
            }





            base.update();
        }

        ///// <summary>
        ///// Use this to activate a member of the treasures.
        ///// </summary>
        ///// <param name="Member"></param>
        //public void collectTreasure (int Member)
        //{
        //    ((FlxSprite)this.members[Member]).play("on");
        //    currentAnimatedObj = ((FlxSprite)members[Member]);

        //    tweenScale = new Tweener(10, 1, TimeSpan.FromSeconds(1.0f), Linear.EaseOut);
        //    //tweenScale.Ended += StartDrop;
        //    tweenScale.Delay = 1.0f;

        //    tweenPos = new Tweener(100, ypos, TimeSpan.FromSeconds(2.0f), Bounce.EaseOut);
        //    tweenPos.Delay = 2.0f;
        //}

        public void setArrowsRemaining(int ArrowsRemaining)
        {
            if (ArrowsRemaining <= 4)
            {
                arrowsRemaining.color = Color.Red;
                arrowsRemaining.text = "0" + ArrowsRemaining.ToString();
            }
            else if (ArrowsRemaining <= 9)
            {
                arrowsRemaining.color = Color.Orange;
                arrowsRemaining.text = "0"+ ArrowsRemaining.ToString();
            }
            else if (ArrowsRemaining <= 99)
            {
                arrowsRemaining.color = Color.White;
                arrowsRemaining.text = ArrowsRemaining.ToString();
            }
            else
            {
                arrowsRemaining.color = Color.White;
                arrowsRemaining.text = "99+";
            }
        }




    }
}
