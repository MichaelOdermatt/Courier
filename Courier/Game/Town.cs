﻿using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Town : Node
    {
        public readonly Sprite sprite;

        public Town(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "Town")
            {
                Offset = new Vector2(0, -11)
            };
            Children.Add(sprite);
        }
    }
}