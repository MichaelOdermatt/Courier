using Courier.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Level1 : Scene
    {
        public Level1()
        {
            var player = new Player();

            Root = new Node(
                new List<Node>
                {
                    player
                }
            );
        }
    }
}
