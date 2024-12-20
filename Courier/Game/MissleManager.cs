using Courier.Engine.Nodes;
using Courier.Game.EnemyCode;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class MissleManager : Node
    {
        public MissleManager(Node parent, Player player) : base(parent)
        {
            // Create a test missle for now
            var testMissle = new Missle(parent, player);
            testMissle.LocalPosition = new Vector2(100, 900);
            Children.Add(testMissle);
        }
    }
}
