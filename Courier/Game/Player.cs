using Courier.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Player : CharacterBody
    {
        private Sprite sprite;

        public Player()
        {
            sprite = new Sprite("ball");
            Children.Add(sprite);
        }
    }
}
