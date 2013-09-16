using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoXNA;
using OgmoXNA.Layers;
using OgmoXNADemo.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace OgmoXNADemo.Levels
{
    class Level
    {
        List<Tile> tiles = new List<Tile>();
        List<GameObject> allObjects = new List<GameObject>();
        List<GameObject> removeQueue = new List<GameObject>();
        int[,] collisionGrid;
        SpriteFont font;
        Ogmo ogmo;
        int ogmoDeaths;

        public Level(OgmoLevel level)
        {
            // Get the loaded grid data and use it for a collision layer.
            collisionGrid = level.GetLayer<OgmoGridLayer>("floors").RawData;
            // Create tiles from some tile layer data.
            foreach (OgmoTile tile in level.GetLayer<OgmoTileLayer>("tiles_bg").Tiles)
                tiles.Add(new Tile(tile, false));
            // Create more tiles from some more tile layer data.
            foreach (OgmoTile tile in level.GetLayer<OgmoTileLayer>("tiles_floors").Tiles)
                tiles.Add(new Tile(tile, true));
            // Create our objects from the object layer data.
            foreach (OgmoObject obj in level.GetLayer<OgmoObjectLayer>("objects").Objects)
            {
                if (obj.Name.Equals("ogmo"))
                {
                    Ogmo ogmo = new Ogmo(obj, this);
                    ogmo.Destroy += new EventHandler(ogmo_Destroy);
                    allObjects.Add(ogmo);
                    this.ogmo = ogmo;
                }
                if (obj.Name.Equals("chest"))
                {
                    Chest chest = new Chest(obj, this);
                    chest.Destroy += new EventHandler(DoDestroy);
                    allObjects.Add(chest);
                }
                if (obj.Name.Equals("moving_platform"))
                    allObjects.Add(new MovingPlatform(obj, this));
                if (obj.Name.StartsWith("spike"))
                    allObjects.Add(new Spike(obj, this));
            }
        }

        void ogmo_Destroy(object sender, EventArgs e)
        {
            ogmoDeaths++;
        }

        public GameObject[] Objects
        {
            get { return allObjects.ToArray(); }
        }

        public List<Tile> Tiles
        {
            get { return tiles; }
        }

        public void Draw(float dt, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tiles)
                if (tile != null)
                    spriteBatch.Draw(tile.Texture, tile.Position, tile.Source, tile.Tint);
            foreach (GameObject obj in allObjects)
                if (obj != null)
                    obj.Draw(dt, spriteBatch);
            spriteBatch.DrawString(font,
                string.Format("Coins: {0:00}", ogmo.Coins),
                new Microsoft.Xna.Framework.Vector2(10f, 10f),
                Color.White);
            spriteBatch.DrawString(font,
                string.Format("Deaths: {0:00}", ogmoDeaths),
                new Microsoft.Xna.Framework.Vector2(10f, 35f),
                Color.White);
        }

        public void Load(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("font");
        }

        public void Update(float dt)
        {
            if (removeQueue.Count > 0)
            {
                foreach (GameObject obj in removeQueue)
                    allObjects.Remove(obj);
                removeQueue.Clear();
            }
            foreach (GameObject obj in allObjects)
                if (obj != null)
                    obj.Update(dt);
        }

        public bool GetCollision(int x, int y)
        {
            if (x < 0 || x > collisionGrid.GetUpperBound(0))
                return true;
            if (y < 0 || y > collisionGrid.GetUpperBound(1))
                return true;
            if (collisionGrid[x, y] > 0)
                return true;
            return false;
        }

        void DoDestroy(object sender, EventArgs e)
        {
            removeQueue.Add(sender as GameObject);
        }
    }
}
