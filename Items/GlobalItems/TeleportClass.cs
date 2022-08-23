using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;

namespace OldSchoolRuneScape.Items.GlobalItems
{
    public class TeleportClass : GlobalItem //thanks to AlchemistNPC's open source code for this teleport functionality
    {
        public static void HandleTeleport(int teleportType = 0, bool forceHandle = false, int whoAmI = 0)
        {
            bool syncData = forceHandle || Main.netMode == NetmodeID.SinglePlayer;
            if (syncData)
            {
                TeleportPlayer(teleportType, forceHandle, whoAmI);
            }
            else
            {
                SyncTeleport(teleportType);
            }
        }

        private static void SyncTeleport(int teleportType = 0)
        {
            var netMessage = OldSchoolRuneScape.instance.GetPacket();
            netMessage.Write((byte)OldSchoolRuneScape.OSRSmessage.Teleport);
            netMessage.Write(teleportType);
            netMessage.Send();
        }

        private static void TeleportPlayer(int teleportType = 0, bool syncData = false, int whoAmI = 0)
        {
            Player player;
            if (!syncData)
            {
                player = Main.LocalPlayer;
            }
            else
            {
                player = Main.player[whoAmI];
            }
            switch (teleportType)
            {
                case 0:
                    FaladorTeleport(player, syncData);
                    break;
                case 1:
                    CamelotTeleport(player, syncData);
                    break;
                case 2:
                    LumbridgeTeleport(player, syncData);
                    break;
                case 3:
                    VarrockTeleport(player, syncData);
                    break;
                default:
                    break;
            }
        }

        private static void FaladorTeleport(Player player, bool syncData = false)
        {
            RunTeleport(player, new Vector2(Main.dungeonX, Main.dungeonY), syncData, true);
        }

        private static void CamelotTeleport(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            int start = 0;
            int limit = 340;
            if (Main.rand.NextBool())
            {
                start = Main.maxTilesX - limit;
                limit = Main.maxTilesX;
            }
            for (int y = 0; y < Main.worldSurface; ++y)
            {
                for (int x = start; x < limit; ++x)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType == TileID.Sand
                        && Main.tile[x, y - 1].TileType == 0
                        && Main.tile[x, y - 1].LiquidAmount == 0)
                    {
                        pos = new Vector2(x * 16, (y - 6) * 16);
                        break;
                    }
                }
                if (pos != prePos)
                {
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void VarrockTeleport(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            List<Vector2> blocks = new List<Vector2>();
            for (int y = 0; y < Main.maxTilesY / 3; y++)
            {
                for (int x = 0; x < Main.maxTilesX; x++)
                {
                    if (Main.tile[x, y].TileType == TileID.RainCloud)
                    {
                        blocks.Add(new Vector2(x, y));
                    }
                }
            }
            Vector2[] blockks = blocks.ToArray();
            Vector2 teleport = blockks[Main.rand.Next(0, blockks.Length)];
            blocks.Clear();
            for (int i = 0; i < 120; i++)
            {
                if (Main.tile[(int)teleport.X, (int)teleport.Y - i].TileType == 0 && !Main.tile[(int)teleport.X, (int)teleport.Y - i].HasTile)
                {
                    pos = new Vector2(teleport.X * 16, (teleport.Y - i) * 16 - 56);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void LumbridgeTeleport(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            for (int x = 0; x < Main.tile.Width; ++x)
            {
                for (int y = 0; y < Main.tile.Height; ++y)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType != 75) continue;
                    pos = new Vector2((x - 3) * 16, (y + 2) * 16);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleHellTeleportLeft(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            for (int x = Main.tile.Width / 2; x > 0; --x)
            {
                for (int y = 0; y < Main.tile.Height; ++y)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType != 75) continue;
                    pos = new Vector2((x + 3) * 16, (y + 2) * 16);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleBeachTeleportRight(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            int x = Main.maxTilesX - 32;
            for (int y = 0; y < Main.tile.Height; y += 16)
            {
                if (Main.tile[x, y] == null) continue;
                if (Main.tile[x, y].LiquidAmount != 255) continue;
                if (Main.tile[x, y].LiquidAmount == 255)
                {
                    do
                    {
                        x -= 16;
                    } while (Main.tile[x, y].LiquidAmount == 255);
                }
                if (Main.tile[x, y] != null && Main.tile[x, y].LiquidAmount == 0 && Main.tile[x, y].HasTile)
                {
                    do
                    {
                        y -= 16;
                    } while (Main.tile[x, y] != null && Main.tile[x, y].LiquidAmount == 0 && Main.tile[x, y].HasTile);
                }
                pos = new Vector2(x * 16, (y - 2) * 16);
                break;
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleBeachTeleportLeft(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            int x = 16;
            for (int y = 0; y < Main.maxTilesY; y += 16)
            {
                if (Main.tile[x, y] == null) continue;
                if (Main.tile[x, y].LiquidAmount != 255) continue;
                if (Main.tile[x, y].LiquidAmount == 255)
                {
                    do
                    {
                        x += 16;
                    } while (Main.tile[x, y].LiquidAmount == 255);
                }
                if (Main.tile[x, y] != null && Main.tile[x, y].LiquidAmount == 0 && Main.tile[x, y].HasTile)
                {
                    do
                    {
                        y -= 16;
                    } while (Main.tile[x, y] != null && Main.tile[x, y].LiquidAmount == 0 && Main.tile[x, y].HasTile);
                }
                pos = new Vector2(x * 16, (y - 2) * 16);
                break;
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleJungleTeleport(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            for (int y = Main.maxTilesY; y > Main.worldSurface - 150; --y)
            {
                for (int x = 0; x < Main.maxTilesX; ++x)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType != 233) continue;
                    pos = new Vector2(x * 16, (y - 2) * 16);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleJungleTeleportLeft(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            for (int y = Main.maxTilesY; y > 0; --y)
            {
                for (int x = Main.maxTilesX; x > 0; --x)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType != 384) continue;
                    pos = new Vector2(x * 16, (y - 2) * 16);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleTempleTeleport(Player player, bool syncData = false)
        {
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            for (int x = 0; x < Main.tile.Width; ++x)
            {
                for (int y = 0; y < Main.tile.Height; ++y)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType != 237) continue;
                    pos = new Vector2((x + 2) * 16, y * 16);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void HandleBeaconTeleport(Player player, bool syncData = false)
        {
            Mod mod = ModLoader.GetMod("AlchemistNPC");
            Vector2 prePos = player.position;
            Vector2 pos = prePos;
            for (int x = 0; x < Main.tile.Width; ++x)
            {
                for (int y = 0; y < Main.tile.Height; ++y)
                {
                    if (Main.tile[x, y] == null) continue;
                    if (Main.tile[x, y].TileType != mod.Find<ModTile>("Beacon").Type) continue;
                    pos = new Vector2((x - 1) * 16, (y - 2) * 16);
                    break;
                }
            }
            if (pos != prePos)
            {
                RunTeleport(player, new Vector2(pos.X, pos.Y), syncData, false);
            }
            else return;
        }

        private static void RunTeleport(Player player, Vector2 pos, bool syncData = false, bool convertFromTiles = false)
        {
            bool postImmune = player.immune;
            int postImmunteTime = player.immuneTime;

            if (convertFromTiles)
                pos = new Vector2(pos.X * 16 + 8 - player.width / 2, pos.Y * 16 - player.height);

            LeaveDust(player);

            //Kill hooks
            player.grappling[0] = -1;
            player.grapCount = 0;
            for (int index = 0; index < 1000; ++index)
            {
                if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
                    Main.projectile[index].Kill();
            }

            player.Teleport(pos, 2, 0);
            player.velocity = Vector2.Zero;
            player.immune = postImmune;
            player.immuneTime = postImmunteTime;

            LeaveDust(player);

            if (Main.netMode != NetmodeID.Server)
                return;

            if (syncData)
            {
                RemoteClient.CheckSection(player.whoAmI, player.position, 1);
                NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, pos.X, pos.Y, 3, 0, 0);
            }
        }

        private static void LeaveDust(Player player)
        {
            //Leave dust
            for (int o = 0; o < 25; o++)
            {
                Dust.NewDust(player.position, 32, 48, DustID.ManaRegeneration);
            }
            Main.TeleportEffect(player.getRect(), 3);
        }
    }
}
