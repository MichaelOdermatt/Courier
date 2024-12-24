using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Courier.Engine.PubSubCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.UI
{
    public class WeightDisplayElement : Node
    {
        private readonly Hub hub;
        private Text text;

        public WeightDisplayElement(Node parent) : base(parent)
        {
            text = new Text(this, "GameplayFont");
            Children.Add(text);

            hub = Hub.Default;
            hub.Subscribe<UpdateWeightEvent>(OnUpdateWeightEvent);
        }

        private void OnUpdateWeightEvent(UpdateWeightEvent eventData)
        {
            text.StringValue = eventData.NewWeightValue + " kgs";
        }
    }
}
