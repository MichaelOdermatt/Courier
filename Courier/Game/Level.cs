using Courier.Content;
using Courier.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Level
    {
        private Node root;

        public Level()
        {
            var player = new Player();
            player.Position = new Vector2(50, 50);

            // Create the level's node tree.
            root = new Node(
                new List<Node>()
                {
                    player
                }
            );
        }

        public void Draw(SpriteBatch spriteBatch, AssetManager assetManager)
        {
            // Draw the level, this works since Draw calls Draw on all its children.
            // Since root is the top level node, pass Vector2.Zero as its parent position.
            root.Draw(spriteBatch, assetManager, Vector2.Zero);
        }
    }
}
