using System.Collections.Generic;
using FlowersHub.Interfaces;
using FlowersHub.Model;

namespace FlowersHub.Services
{
    public static class FlowerHelper
    {
        private static readonly Dictionary<string, IFlowerUpdater> Updaters = new Dictionary<string, IFlowerUpdater>()
        {
            {nameof(FlowerUaUpdater), new FlowerUaUpdater()}
        };

        public static void Update(this Flower flower)
        {
            Updaters[flower.Updater].Update(flower);
        }
    }
}
