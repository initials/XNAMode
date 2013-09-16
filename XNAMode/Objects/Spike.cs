using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoXNA;
using OgmoXNADemo.Levels;

namespace OgmoXNADemo.Objects
{
    class Spike : GameObject
    {
        public Spike(OgmoObject obj, Level level)
            : base(obj, level)
        {
            this.Collision += new EventHandler<CollisionEventArgs>(Spike_Collision);
        }

        void Spike_Collision(object sender, CollisionEventArgs e)
        {
            if (e.Collider is Ogmo)
            {
                Ogmo ogmo = e.Collider as Ogmo;
                ogmo.DoDie();
                ogmo.OnDestroy();
            }
        }
    }
}
