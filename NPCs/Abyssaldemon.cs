using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class Abyssaldemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Demon");
            Main.npcFrameCount[NPC.type] = 7;
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 60;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.lifeMax = 450;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath5;
            NPC.value = Item.buyPrice(0, 2);
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0.2f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.Underworld.Chance * 0.1f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Abyssalwhip>(), 100));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0] = 0;
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.LightGray);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.LightGray);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Abyssaldemon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Abyssaldemon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Abyssaldemon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Abyssaldemon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Abyssaldemon1").Type, NPC.scale);
            }
        }
        public override void AI()
        {
            float maxspeed = 4f;
            float acceleration = 0.2f;
            if (NPC.ai[0] == 0)
            {
                NPC.ai[2]++;
                if (NPC.direction != NPC.oldDirection)
                {
                    NPC.velocity.X *= 0.5f;
                }
                NPC.TargetClosest(true);
                if (NPC.collideX)
                {
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.2f, -6f);
                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
                if (NPC.HasValidTarget)
                {
                    if (NPC.velocity.X < maxspeed && NPC.velocity.X > -maxspeed)
                    {
                        NPC.velocity.X += NPC.direction * acceleration;
                    }
                    else
                    {
                        NPC.velocity.X *= 0.9f;
                    }
                    if (NPC.ai[2] > 60 * 8 && NPC.Distance(Main.player[NPC.target].position) < 1200f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[0] = 2;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                }
                if (Main.player[NPC.target].position.Y > NPC.Bottom.Y)
                {
                    int x = (int)(NPC.position.X / 16f);
                    int y = (int)(NPC.BottomLeft.Y / 16f);
                    if (TileID.Sets.Platforms[Main.tile[x, y].TileType])
                    {
                        NPC.position.Y += 1;
                    }
                }
                if (NPC.velocity.Y > 1f)
                {
                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[0] == 1)
            {
                NPC.ai[2]++;
                if (NPC.velocity.X < 1 && NPC.velocity.X > -1)
                {
                    NPC.velocity.X = 1 * NPC.direction;
                }
                if (NPC.velocity.Y == 0)
                {
                    NPC.ai[0] = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[0] == 2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.velocity *= 0.9f;
                Lighting.AddLight(NPC.Center, new Vector3(0.14f * 3, 0, 0.22f * 3));
                NPC.ai[2]++;
                if (NPC.ai[2] > 14)
                {
                    Vector2 distance = new Vector2(0, Main.rand.Next(-300, -150)).RotateRandom(MathHelper.ToRadians(65));
                    Vector2 pos = Main.player[NPC.target].Center + distance;
                    int x = (int)(pos.X / 16f);
                    int y = (int)(pos.Y / 16f);
                    if (Main.tile[x, y].TileType == 0 && !Main.tile[x, y].HasTile)
                    {
                        NPC.position = pos;
                        for (int i = 0; i < 10; i++)
                        {
                            Dust.NewDust(pos, 0, 0, DustID.Shadowflame);
                        }
                        NPC.ai[0] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                }                    
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.ai[0] == 0)
            {
                NPC.frameCounter += Math.Abs(NPC.velocity.X / 2);
                int frameSpeed = 8;
                if (NPC.frameCounter < frameSpeed)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < frameSpeed * 2)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.frameCounter < frameSpeed * 3)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
                else if (NPC.frameCounter < frameSpeed * 4)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else if (NPC.ai[0] == 2)
            {
                if (NPC.ai[2] < 8)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
                else if (NPC.ai[2] < 8 * 2)
                {
                    NPC.frame.Y = frameHeight * 5;
                }
                else if (NPC.ai[2] < 8 * 3)
                {
                    NPC.frame.Y = frameHeight * 6;
                }
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }
    }
}
