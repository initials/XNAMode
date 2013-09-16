using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoXNADemo.Objects
{
    class CollisionEventArgs : EventArgs
    {
        public CollisionEventArgs(GameObject collider)
        {
            this.Collider = collider;
        }

        public GameObject Collider { get; set; }
    }
}
