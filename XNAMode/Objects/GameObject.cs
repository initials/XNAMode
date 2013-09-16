using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OgmoXNA;
using Microsoft.Xna.Framework.Graphics;
using OgmoXNADemo.Levels;

namespace OgmoXNADemo.Objects
{
    abstract class GameObject
    {
        public int Height;
        public string Name;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public Rectangle Source;
        public Texture2D Texture;
        public Color Tint = Color.White;
        public int Width;
        public bool IsTiled;

        public Level Level;

        public event EventHandler<CollisionEventArgs> Collision;
        public event EventHandler Destroy;

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)this.Position.X,
                    (int)this.Position.Y,
                    this.Width,
                    this.Height);
            }
        }

        protected GameObject(OgmoObject obj, Level level)
        {
            this.Level = level;
            this.Height = obj.Height;
            this.Name = obj.Name;
            this.Origin = obj.Origin;
            this.Position = obj.Position;
            this.Source = obj.Source;
            this.Texture = obj.Texture;
            this.Width = obj.Width;
            this.IsTiled = obj.IsTiled;
        }

        public bool CollidesWith(GameObject obj)
        {
            return this.BoundingRectangle.Intersects(obj.BoundingRectangle);
        }

        public virtual void Draw(float dt, SpriteBatch spriteBatch)
        {
            if (this.Texture != null)
            {
                if (this.IsTiled)
                {
                    spriteBatch.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
                    spriteBatch.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;
                }
                spriteBatch.Draw(this.Texture,
                    this.Position,
                    this.Source,
                    this.Tint,
                    this.Rotation,
                    this.Origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }
        }

        public virtual void OnCollision(GameObject collider)
        {
            if (this.Collision != null)
                this.Collision(this, new CollisionEventArgs(collider));
        }

        public virtual void OnDestroy()
        {
            if (this.Destroy != null)
                this.Destroy(this, EventArgs.Empty);
        }

        public virtual void Update(float dt)
        {
        }
    }
}
