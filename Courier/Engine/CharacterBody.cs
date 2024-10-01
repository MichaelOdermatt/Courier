using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    /// <summary>
    /// Base class for all physical game objects which can be manipulated and moved via code.
    /// </summary>
    public class CharacterBody : Node
    {
        public Vector2 Velocity { get; set; }
    }
}
