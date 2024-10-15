using Courier.Engine;
using Courier.Engine.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Level1 : Scene
    {
        /// <summary>
        /// The points to use for the level's Ground object.
        /// </summary>
        private Vector2[] groundPoints =
        {
            new Vector2(0, 300),
            new Vector2(100, 400),
            new Vector2(300, 400),
            new Vector2(400, 450),
            new Vector2(800, 300),
        };

        public Level1(Camera2D camera) : base(camera)
        {
            root = new Node(null);
            var ground = new Ground(root, groundPoints);

            root.Children.Add(ground);

            player = new Player(null, new CollisionSphere(25), camera);
        }
    }
}
