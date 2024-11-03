using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.EnemyCode;
using Courier.Game.PlayerCode;
using Courier.Game.UI;
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
        private FuelMeterElement fuelMeterElement;

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
            CreateUINodes();
            CreateGameplayNodes();
        }

        /// <summary>
        /// Creates all Nodes that are drawn in screen space and used for UI.
        /// </summary>
        private void CreateUINodes()
        {
            screenSpaceRoot = new Node(null);

            fuelMeterElement = new FuelMeterElement(screenSpaceRoot);
            fuelMeterElement.LocalPosition = new Vector2(10, 10);

            screenSpaceRoot.Children.Add(fuelMeterElement);
        }

        /// <summary>
        /// Creates all Nodes that are drawn in world space and used for gameplay.
        /// </summary>
        private void CreateGameplayNodes()
        {
            Player player = new Player(null, camera, fuelMeterElement);
            this.player = player;

            worldSpaceRoot = new Node(null);

            var ground = new Ground(worldSpaceRoot, groundPoints);
            var bulletPool = new BulletPool(worldSpaceRoot, 15);
            var enemies = CreateEnemies(player, bulletPool);

            var tank = new Tank(worldSpaceRoot, player, bulletPool);
            tank.LocalPosition = groundPoints[9];

            worldSpaceRoot.Children.Add(ground);
            worldSpaceRoot.Children.Add(bulletPool);
            worldSpaceRoot.Children.AddRange(enemies);
        }

        /// <summary>
        /// Creates and returns all the enemies to be used in the level.
        /// </summary>
        private List<EnemyBase> CreateEnemies(Player player, BulletPool bulletPool)
        {
            return new List<EnemyBase>
            {
                // Gunner 1
                new Gunner(worldSpaceRoot, player, bulletPool)
                {
                    LocalPosition = groundPoints[4],
                },
                // Gunner 2
                new Gunner(worldSpaceRoot, player, bulletPool)
                {
                    LocalPosition = groundPoints[8],
                },
                // Tank 1
                new Tank(worldSpaceRoot, player, bulletPool)
                {
                    LocalPosition = groundPoints[3],
                },
                // Tank 2
                new Tank(worldSpaceRoot, player, bulletPool)
                {
                    LocalPosition = groundPoints[10],
                },
            };
        }
    }
}
