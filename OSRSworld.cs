using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using System.Linq;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace OldSchoolRuneScape
{
    public class OSRSworld : ModSystem
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
        public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
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

        public override void SaveWorldData(TagCompound tag)
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
            tag.Add("downed", downed);
            tag.Add("slayBoss", slayBossProgress);
        }

        public override void LoadWorldData(TagCompound tag)
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
                tasks.Insert(ShiniesIndex + 1, new RuniteOrePass("Runite Ores", 237.4298f));
            }
            int AltarsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Altars"));
            if (AltarsIndex != -1)
            {
                tasks.Insert(AltarsIndex + 1, new RuneAltarPlacementPass("Runecrafting Altars", 237.4298f));
            }
        }

        public class RuniteOrePass : GenPass
        {
            public RuniteOrePass(string name, float loadWeight) : base(name, loadWeight)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                // progress.Message is the message shown to the user while the following code is running.
                // Try to make your message clear. You can be a little bit clever, but make sure it is descriptive enough for troubleshooting purposes.
                progress.Message = "Generating Runite ore";

                // Ores are quite simple, we simply use a for loop and the WorldGen.TileRunner to place splotches of the specified Tile in the world.
                // "6E-05" is "scientific notation". It simply means 0.00006 but in some ways is easier to read.
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                {
                    WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY), (double)WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), TileType<Tiles.Runiteore>(), false, 0f, 0f, false, true);
                    WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)WorldGen.worldSurfaceLow - 20), (double)WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), TileType<Tiles.Runiteore>(), false, 0f, 0f, false, true);
                }
            }
        }

        public class RuneAltarPlacementPass : GenPass
        {
            public RuneAltarPlacementPass(string name, float loadWeight) : base(name, loadWeight)
            {
            }
            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Placing Rune Altars";

                int surface = (int)WorldGen.worldSurfaceLow;
                int step = (Main.maxTilesY - 200 - surface) / 3;
                for (int i = 0; i < 15; i++)
                {
                    if (i < 6)
                    {
                        PlaceAltar(surface, surface + step, i);
                    }
                    else if (i < 10)
                    {
                        PlaceAltar(surface + step, surface + step + step, i);
                    }
                    else
                    {
                        PlaceAltar(surface + step + step, surface + step + step + step, i);
                    }
                }
            }
            protected void RCAltars(GenerationProgress progress)
            {
                progress.Message = "Placing Rune Altars";
                int surface = (int)WorldGen.worldSurfaceLow;
                int step = (Main.maxTilesY - 200 - surface) / 3;
                for (int i = 0; i < 15; i++)
                {
                    if (i < 6)
                    {
                        PlaceAltar(surface, surface + step, i);
                    }
                    else if (i < 10)
                    {
                        PlaceAltar(surface + step, surface + step + step, i);
                    }
                    else
                    {
                        PlaceAltar(surface + step + step, surface + step + step + step, i);
                    }
                }
            }
            protected void PlaceAltar(int upper, int lower, int index)
            {
                bool placeSuccessful = false;
                while (!placeSuccessful)
                {
                    int x = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                    int y = WorldGen.genRand.Next(upper, lower);
                    WorldGen.Place3x2(x, y, (ushort)(GetAltar(index)));
                    Tile tile = Main.tile[x, y];
                    placeSuccessful = tile.HasTile && tile.TileType == GetAltar(index);
                }
            }
            protected int GetAltar(int index)
            {
                switch (index)
                {
                    case 0:
                        return TileType<Tiles.AirAltar>();
                    case 1:
                        return TileType<Tiles.MindAltar>();
                    case 2:
                        return TileType<Tiles.WaterAltar>();
                    case 3:
                        return TileType<Tiles.EarthAltar>();
                    case 4:
                        return TileType<Tiles.FireAltar>();
                    case 5:
                        return TileType<Tiles.BodyAltar>();
                    case 6:
                        return TileType<Tiles.CosmicAltar>();
                    case 7:
                        return TileType<Tiles.ChaosAltar>();
                    case 8:
                        return TileType<Tiles.NatureAltar>();
                    case 9:
                        return TileType<Tiles.LawAltar>();
                    case 10:
                        return TileType<Tiles.BloodAltar>();
                    case 11:
                        return TileType<Tiles.SoulAltar>();
                    case 12:
                        return TileType<Tiles.DeathAltar>();
                    case 13:
                        return TileType<Tiles.WrathAltar>();
                    case 14:
                    default:
                        return TileType<Tiles.AstralAltar>();
                }
            }
        }
    }
}
