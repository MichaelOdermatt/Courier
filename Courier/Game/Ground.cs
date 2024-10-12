using Courier.Content;
using Courier.Engine;
using Courier.Engine.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Ground : Node
    {
        private Vector2[] points;

        private string textureKey = "LineTexture";
        private float layerDepth = 0.0f;
        private float lineThickness = 5.0f;
        private LineSegment[] lineSegments;

        private StaticBody staticBody;

        public Ground(Node parent, Vector2[] points): base(parent)
        {
            this.points = points;
            lineSegments = CreateLineSegments(points);
            staticBody = new StaticBody(this, new CollisionSegmentedBoundry(points, SegmentedBoundryDirection.Down));
            Children.Add(staticBody);
        }

        /// <summary>
        /// Draws the line for the Ground and calls the Draw function on any child Nodes.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch, AssetManager assetManager)
        {
            // Draw all child Nodes
            base.Draw(spriteBatch, assetManager);

            var texture = assetManager.Textures[textureKey];

            for (int i = 0; i < lineSegments.Length; i++)
            {
                var segmentVector = lineSegments[i].LocalVector;
                var segmentScale = new Vector2(lineSegments[i].SegmentLength, lineThickness);
                var segmentRotation = MathF.Atan(segmentVector.Y / segmentVector.X);

                spriteBatch.Draw(
                    texture, 
                    GlobalPosition + lineSegments[i].StartPos,
                    null, 
                    Color.White, 
                    segmentRotation, 
                    Vector2.Zero, 
                    segmentScale, 
                    SpriteEffects.None, 
                    layerDepth
                );
            }
        }

        /// <summary>
        /// Converts the given array of points into an array of LineSegment objects. The array of LineSegments = points.length - 1.
        /// </summary>
        /// <param name="points">The points to convert into LineSegments.</param>
        /// <returns>The array of LineSegment objects.</returns>
        private LineSegment[] CreateLineSegments(Vector2[] points)
        {
            LineSegment[] lineSegments = new LineSegment[points.Length - 1];

            for (int i = 0; i < points.Length - 1; i++)
            {
                var segmentStartPos = points[i];
                var segmentEndPos = points[i + 1];
                var segmentVector = segmentEndPos - segmentStartPos;
                var segmentLength = segmentVector.Length();
                var segmentScale = new Vector2(segmentLength, lineThickness);
                var segmentRotation = MathF.Atan(segmentVector.Y / segmentVector.X);

                lineSegments[i] = new(points[i], points[i + 1]);
            }

            return lineSegments;
        }
    }
}
