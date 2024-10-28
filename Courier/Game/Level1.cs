using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
            new Vector2(1000, 500),
            new Vector2(1200, 450),
            new Vector2(1300, 400),
            new Vector2(1600, 400),
            new Vector2(1800, 350),
            new Vector2(1900, 450),
            new Vector2(2200, 500),
            new Vector2(2400, 500),
            new Vector2(2600, 400),
            new Vector2(3200, 550),
            new Vector2(3500, 450),
            new Vector2(3700, 500),
            new Vector2(4000, 400),
        };

        public Level1(Camera2D camera) : base(camera)
        {
            Player player = new Player(null, camera);
            this.player = player;

            root = new Node(null);

            var ground = new Ground(root, groundPoints);
            var bulletPool = new BulletPool(root, 15);
            var gunners = CreateGunners(player, bulletPool);

            root.Children.Add(ground);
            root.Children.Add(bulletPool);
            root.Children.AddRange(gunners);
        }

        /// <summary>
        /// Creates and returns all the gunners to be used in the level.
        /// </summary>
        public List<Gunner> CreateGunners(Player player, BulletPool bulletPool)
        {
            return new List<Gunner>
            {
                // Gunner 1
                new Gunner(root, player, bulletPool)
                {
                    LocalPosition = groundPoints[4],
                },
                // Gunner 2
                new Gunner(root, player, bulletPool)
                {
                    LocalPosition = groundPoints[8],
                },
            };
        }
    }
}
