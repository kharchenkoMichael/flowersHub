using System.Collections.Generic;
using System.Threading.Tasks;
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

        public static async Task Update(this Flower flower)
        {
            await Updaters[flower.Updater].Update(flower);
        }
    }
}
