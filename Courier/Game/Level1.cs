using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.EnemyCode;
using Courier.Game.PlayerCode;
using Courier.Game.TownCode;
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
        /// <summary>
        /// The points to use for the level's Ground object.
        /// </summary>
        private readonly Vector2[] groundPoints =
        {
            new Vector2(0, 300),
            new Vector2(100, 400),
            new Vector2(300, 400),
            new Vector2(400, 450),
            new Vector2(800, 300),
            new Vector2(1000, 500),
            new Vector2(1200, 450),
            new Vector2(1300, 400),
            new Vector2(1400, 400),
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
        }

        public override void Initialize()
        {
            screenSpaceRoot = new Node(null);
            worldSpaceRoot = new Node(null);

            CreateScreenSpaceNodes();
            CreateGameplayNodes();
        }

        /// <summary>
        /// Creates all Nodes that are drawn in screen space and used for UI.
        /// </summary>
        private void CreateScreenSpaceNodes()
        {
            var fuelMeterElement = new FuelMeterElement(screenSpaceRoot);
            fuelMeterElement.LocalPosition = new Vector2(10, 10);

            var skyBackground = new SkyBackground(screenSpaceRoot);

            screenSpaceRoot.Children.Add(skyBackground);
            screenSpaceRoot.Children.Add(fuelMeterElement);
        }

        /// <summary>
        /// Creates all Nodes that are drawn in world space and used for gameplay.
        /// </summary>
        private void CreateGameplayNodes()
        {
            var towns = CreateTowns();
            var townManager = new TownManager(worldSpaceRoot, towns);

            Player player = new Player(
                null,
                camera,
                townManager,
                () => this.Initialize()
            );
            this.player = player;

            var ground = new Ground(worldSpaceRoot, groundPoints);
            var bulletPool = new BulletPool(worldSpaceRoot, 15);
            var enemies = CreateEnemies(player, bulletPool);

            worldSpaceRoot.Children.Add(ground);
            worldSpaceRoot.Children.Add(bulletPool);
            worldSpaceRoot.Children.AddRange(enemies);
            worldSpaceRoot.Children.Add(townManager);
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
                    LocalPosition = groundPoints[9],
                },
                // Tank 1
                new Tank(worldSpaceRoot, player, bulletPool)
                {
                    LocalPosition = groundPoints[5],
                },
                // Tank 2
                new Tank(worldSpaceRoot, player, bulletPool)
                {
                    LocalPosition = groundPoints[10],
                },
            };
        }

        /// <summary>
        /// Creates and returns all the towns to be used in the level.
        /// </summary>
        private List<Town> CreateTowns()
        {
            return new List<Town>
            {
                // Town 1
                new Town(worldSpaceRoot)
                {
                    LocalPosition = groundPoints[8],
                },
                // Town 2
                new Town(worldSpaceRoot)
                {
                    LocalPosition = groundPoints[12],
                },
            };
        }
    }
}
