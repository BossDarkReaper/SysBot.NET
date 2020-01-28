﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PKHeX.Core;

namespace SysBot.Pokemon
{
    public static class PokeTradeBotUtil
    {
        /// <summary>
        /// Initializes a <see cref="SysBot"/> and starts executing a <see cref="SurpriseTradeBot"/>.
        /// </summary>
        /// <param name="lines">Lines to initialize with</param>
        /// <param name="token">Token to indicate cancellation.</param>
        /// <param name="queue">Queue to consume from; added to from another thread.</param>
        public static async Task RunBotAsync(string[] lines, CancellationToken token, PokeTradeQueue<PK8> queue)
        {
            var bot = CreateNewPokeTradeBot(lines, queue);
            await bot.RunAsync(token).ConfigureAwait(false);
        }

        /// <summary>
        /// Initializes a <see cref="SysBot"/> but does not start it.
        /// </summary>
        /// <param name="lines">Lines to initialize with</param>
        /// <param name="queue"></param>
        public static PokeTradeBot CreateNewPokeTradeBot(string[] lines, PokeTradeQueue<PK8> queue)
        {
            var cfg = new PokeTradeBotConfig(lines);

            var bot = new PokeTradeBot(queue, cfg);
            if (cfg.DumpFolder != null && Directory.Exists(cfg.DumpFolder))
                bot.DumpFolder = cfg.DumpFolder;
            return bot;
        }
    }
}