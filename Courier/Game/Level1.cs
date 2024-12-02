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
            new Vector2(0, 300),
            new Vector2(100, 400),
            new Vector2(600, 400),
            new Vector2(750, 450),
            new Vector2(950, 400),
            new Vector2(1300, 300),
            new Vector2(1700, 300),
            new Vector2(2100, 150),
            new Vector2(2300, 100),
            new Vector2(2500, 300),
            new Vector2(2700, 350),
            new Vector2(3000, 350),
            new Vector2(3500, 100),
            new Vector2(3800, 100),
            new Vector2(4200, 0),
            new Vector2(4600, 100),
            new Vector2(4800, 100),
            new Vector2(5200, -150),
            new Vector2(5600, -150),
            new Vector2(6000, -250),
            new Vector2(6400, -50),
            new Vector2(6700, 200),
            new Vector2(7100, 350),
            new Vector2(7300, 450),
            new Vector2(7600, 600),
            new Vector2(7900, 850),
            new Vector2(8100, 900),
            new Vector2(8200, 800),
            new Vector2(8300, 750),
            new Vector2(8500, 775),
            new Vector2(8600, 850),
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

            var skyBackground = new SkyBackground(screenSpaceRoot, camera);

            screenSpaceRoot.Children.Add(skyBackground);
            screenSpaceRoot.Children.Add(fuelMeterElement);
            screenSpaceRoot.Children.Add(weightDisplayElement);
            screenSpaceRoot.Children.Add(wantedLevelElement);
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

            var ground = new Ground(worldSpaceRoot, groundPoints);
            var bulletManager = new BulletManager(worldSpaceRoot);
            var enemies = CreateEnemies(player);
            var enemyManager = new EnemyManager(worldSpaceRoot, enemies);

            worldSpaceRoot.Children.Add(player);
            worldSpaceRoot.Children.Add(ground);
            worldSpaceRoot.Children.Add(bulletManager);
            worldSpaceRoot.Children.Add(enemyManager);
            worldSpaceRoot.Children.Add(townManager);
            worldSpaceRoot.Children.Add(parcelManager);
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
                    LocalPosition = groundPoints[19],
                },
            };
        }
    }
}
