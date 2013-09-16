using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoXNA;
using Microsoft.Xna.Framework;
using OgmoXNA.Values;
using Microsoft.Xna.Framework.Graphics;
using OgmoXNADemo.Levels;

namespace OgmoXNADemo.Objects
{
    class MovingPlatform : GameObject
    {
        List<OgmoNode> nodes = new List<OgmoNode>();
        Vector2 direction = Vector2.Zero;
        int currentNode = 0;
        float speed = 0;

        public MovingPlatform(OgmoObject obj, Level level)
            : base(obj, level)
        {
            nodes.AddRange(obj.Nodes);
            if (nodes.Count > 0)
            {
                direction = Vector2.Normalize(nodes[currentNode].Position - this.Position);
                nodes.Add(new OgmoNode(this.Position));
            }
            OgmoNumberValue speedValue = obj.GetValue<OgmoNumberValue>("speed");
            if (speedValue != null)
                speed = speedValue.Value;
        }

        public override void Update(float dt)
        {
            this.Position += this.direction * speed;
            if (Vector2.Distance(this.Position, nodes[currentNode].Position) <= 1)
            {
                currentNode++;
                if (currentNode > nodes.Count - 1)
                    currentNode = 0;
                direction = Vector2.Normalize(nodes[currentNode].Position - this.Position);
            }
            base.Update(dt);
        }
    }
}
