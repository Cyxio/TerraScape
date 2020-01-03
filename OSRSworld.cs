using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using System.Linq;
using Terraria.ModLoader.IO;

namespace OldSchoolRuneScape
{
    public class OSRSworld : ModWorld
    {
        public static bool downedElvarg = false;
        public static bool downedChaosEle = false;
        public static bool downedVerac = false;
        public static bool downedDharok = false;
        public static bool downedTorag = false;
        public static bool downedAhrim = false;
        public static bool downedGuthan = false;
        public static bool downedKaril = false;
        public static bool downedBarSpirit = false;
        public static bool downedOlm = false;
        public static int slayBossProgress = 0;
        public override void Initialize()
        {
            downedElvarg = false;
            downedChaosEle = false;
            downedVerac = false;
            downedDharok = false;
            downedTorag = false;
            downedAhrim = false;
            downedGuthan = false;
            downedKaril = false;
            downedBarSpirit = false;
            downedOlm = false;
            slayBossProgress = 0;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedElvarg)
            {
                downed.Add("Elvarg");
            }
            if (downedChaosEle)
            {
                downed.Add("ChaosEle");
            }
            if (downedVerac)
            {
                downed.Add("Verac");
            }
            if (downedTorag)
            {
                downed.Add("Torag");
            }
            if (downedDharok)
            {
                downed.Add("Dharok");
            }
            if (downedAhrim)
            {
                downed.Add("Ahrim");
            }
            if (downedKaril)
            {
                downed.Add("Karil");
            }
            if (downedGuthan)
            {
                downed.Add("Guthan");
            }
            if (downedBarSpirit)
            {
                downed.Add("BarSpirit");
            }
            if (downedOlm)
            {
                downed.Add("Olm");
            }
            return new TagCompound { ["downed"] = downed , ["slayBoss"] = slayBossProgress};
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedElvarg = downed.Contains("Elvarg");
            downedChaosEle = downed.Contains("ChaosEle");
            downedVerac = downed.Contains("Verac");
            downedDharok = downed.Contains("Dharok");
            downedTorag = downed.Contains("Torag");
            downedAhrim = downed.Contains("Ahrim");
            downedGuthan = downed.Contains("Guthan");
            downedKaril = downed.Contains("Karil");
            downedBarSpirit = downed.Contains("BarSpirit");
            downedOlm = downed.Contains("Olm");
            slayBossProgress = tag.GetInt("slayBoss");
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Runite ore", delegate (GenerationProgress progress)
                {
                    progress.Message = "Runite ore";

                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY), (double)WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), mod.TileType("Runiteore"), false, 0f, 0f, false, true);
                        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)WorldGen.worldSurfaceLow - 20), (double)WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), mod.TileType("Runiteore"), false, 0f, 0f, false, true);
                    }
                }));
            }
        }

    }
}
