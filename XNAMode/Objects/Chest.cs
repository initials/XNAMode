using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoXNA;
using OgmoXNA.Values;
using OgmoXNADemo.Levels;

namespace OgmoXNADemo.Objects
{
    class Chest : GameObject
    {
        public int Coins;

        public Chest(OgmoObject obj, Level level)
            : base(obj, level)
        {
            OgmoIntegerValue coins = obj.GetValue<OgmoIntegerValue>("coins");
            if (coins != null)
                this.Coins = coins.Value;
            this.Collision += new EventHandler<CollisionEventArgs>(Chest_Collision);
        }

        void Chest_Collision(object sender, CollisionEventArgs e)
        {
            if (e.Collider != null && e.Collider.Name.Equals("ogmo"))
            {
                Ogmo ogmo = e.Collider as Ogmo;
                ogmo.Coins += this.Coins;
                this.OnDestroy();
            }
        }
    }
}
