using Courier.Engine.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Nodes
{
    public class Text : Node
    {
        private readonly string fontKey;

        /// <summary>
        /// The value of the Text element.
        /// </summary>
        public string StringValue { get; set; } = "";

        public Text(Node parent, string fontKey) : base(parent)
        {
            this.fontKey = fontKey;
        }

        /// <summary>
        /// Draws the Text and calls the Draw function on any child Nodes.
        /// </summary>
        public override void Draw(SpriteRenderer spriteRenderer)
        {
            // Draw all child Nodes
            base.Draw(spriteRenderer);

            // Add the Text to the renderer so it can be drawn in the next draw call.
            spriteRenderer.AddText(new TextRenderData
            {
                FontKey = fontKey,
                Position = GlobalPosition,
                StringValue = StringValue,
            });
        }
    }
}
