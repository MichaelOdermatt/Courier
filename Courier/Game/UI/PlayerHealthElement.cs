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
    public class PlayerHealthElement : Node
    {
        private readonly Hub hub;
        private Text text;

        public PlayerHealthElement(Node parent) : base(parent)
        {
            text = new Text(this, "GameplayFont");
            Children.Add(text);

            hub = Hub.Default;
            hub.Subscribe<UpdatePlayerHealthEvent>(OnPlayerHealthLevelUpdated);
        }

        private void OnPlayerHealthLevelUpdated(UpdatePlayerHealthEvent eventData)
        {
            text.StringValue = "Health: " + eventData.NewHealthValue;
        }
    }
}
