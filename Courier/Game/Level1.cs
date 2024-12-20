using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.EnemyCode;
using Courier.Game.ParcelCode;
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
            new Vector2(0, 1050),
            new Vector2(100, 1150),
            new Vector2(600, 1150),
            new Vector2(750, 1200),
            new Vector2(950, 1150),
            new Vector2(1300, 1050),
            new Vector2(1700, 1050),
            new Vector2(2100, 900),
            new Vector2(2300, 850),
            new Vector2(2500, 1050),
            new Vector2(2700, 1100),
            new Vector2(3000, 1100),
            new Vector2(3500, 850),
            new Vector2(3800, 850),
            new Vector2(4200, 750),
            new Vector2(4600, 850),
            new Vector2(4800, 850),
            new Vector2(5200, 600),
            new Vector2(5600, 600),
            new Vector2(6000, 500),
            new Vector2(6400, 750),
            new Vector2(6700, 1050),
            new Vector2(7100, 1200),
            new Vector2(7300, 1400),
            new Vector2(7600, 1750),
            new Vector2(7900, 2100),
            new Vector2(8100, 2150),
            new Vector2(8200, 2200),
            new Vector2(8300, 2180),
            new Vector2(8500, 2190),
            new Vector2(8600, 2200),
            new Vector2(8800, 2250),
            new Vector2(9100, 2150),
            new Vector2(9400, 2000),
            new Vector2(9800, 1950),
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
            var weightDisplayElement = new WeightDisplayElement(screenSpaceRoot);
            weightDisplayElement.LocalPosition = new Vector2(100, 50);
            var wantedLevelElement = new WantedLevelElement(screenSpaceRoot);
            wantedLevelElement.LocalPosition = new Vector2(100, 150);
            var playerHealthElement = new PlayerHealthElement(screenSpaceRoot);
            playerHealthElement.LocalPosition = new Vector2(100, 200);

            var skyBackground = new SkyBackground(screenSpaceRoot, camera);

            screenSpaceRoot.Children.Add(skyBackground);
            screenSpaceRoot.Children.Add(fuelMeterElement);
            screenSpaceRoot.Children.Add(weightDisplayElement);
            screenSpaceRoot.Children.Add(wantedLevelElement);
            screenSpaceRoot.Children.Add(playerHealthElement);
        }

        /// <summary>
        /// Creates all Nodes that are drawn in world space and used for gameplay.
        /// </summary>
        private void CreateGameplayNodes()
        {
            var towns = CreateTowns();
            var townManager = new TownManager(worldSpaceRoot, towns);
            var parcelManager = new ParcelManager(worldSpaceRoot);

            Player player = new Player(
                null,
                camera,
                () => this.Initialize()
            );
            player.LocalPosition = new Vector2(0, 650);

            var ground = new Ground(worldSpaceRoot, groundPoints);
            var bulletManager = new BulletManager(worldSpaceRoot);
            var enemies = CreateEnemies(player);
            var enemyManager = new EnemyManager(worldSpaceRoot, enemies);
            var missleManager = new MissleManager(worldSpaceRoot, player);
            var refuelPoint = new RefuelPoint(worldSpaceRoot);
            refuelPoint.LocalPosition = groundPoints[19];

            worldSpaceRoot.Children.Add(player);
            worldSpaceRoot.Children.Add(ground);
            worldSpaceRoot.Children.Add(bulletManager);
            worldSpaceRoot.Children.Add(enemyManager);
            worldSpaceRoot.Children.Add(townManager);
            worldSpaceRoot.Children.Add(parcelManager);
            worldSpaceRoot.Children.Add(refuelPoint);
            worldSpaceRoot.Children.Add(missleManager);
        }

        /// <summary>
        /// Creates and returns all the enemies to be used in the level.
        /// </summary>
        private List<EnemyBase> CreateEnemies(Player player)
        {
            float enemyYOffset = 10;
            return new List<EnemyBase>
            {
                // Gunner 1
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[4].X, groundPoints[4].Y - enemyYOffset)
                },
                // Gunner 2
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[9].X, groundPoints[9].Y - enemyYOffset)
                },
                // Gunner 3
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[13].X, groundPoints[13].Y - enemyYOffset)
                },
                // Gunner 4
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[15].X, groundPoints[15].Y - enemyYOffset)
                },
                // Gunner 4
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[20].X, groundPoints[20].Y - enemyYOffset)
                },
                // Gunner 5
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[22].X, groundPoints[22].Y - enemyYOffset)
                },
                // Gunner 6
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[23].X, groundPoints[23].Y - enemyYOffset)
                },
                // Gunner 7
                new Gunner(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[27].X, groundPoints[27].Y - enemyYOffset)
                },
                // Tank 1
                new Tank(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[5].X, groundPoints[5].Y - enemyYOffset)
                },
                // Tank 2
                new Tank(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[10].X, groundPoints[10].Y - enemyYOffset)
                },
                // Tank 3
                new Tank(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[14].X, groundPoints[14].Y - enemyYOffset)
                },
                // Tank 5
                new Tank(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[17].X, groundPoints[17].Y - enemyYOffset)
                },
                // Tank 6
                new Tank(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[25].X, groundPoints[25].Y - enemyYOffset)
                },
                // Tank 7
                new Tank(worldSpaceRoot, player)
                {
                    LocalPosition = new Vector2(groundPoints[32].X, groundPoints[32].Y - enemyYOffset)
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
                // Town 3
                new Town(worldSpaceRoot)
                {
                    LocalPosition = groundPoints[16],
                },
                // Town 4
                new Town(worldSpaceRoot)
                {
                    LocalPosition = groundPoints[18],
                },
                // Town 5
                new Town(worldSpaceRoot)
                {
                    LocalPosition = groundPoints[28],
                },
                // Town 6
                new Town(worldSpaceRoot)
                {
                    LocalPosition = groundPoints[29],
                },
            };
        }
    }
}
