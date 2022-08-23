using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class Broodoovictim : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broodoo victim");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
        }
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 14;
            NPC.defense = 6;
            NPC.lifeMax = 75;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath36;
            NPC.value = 100f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.Zombie;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime)
            {
                return SpawnCondition.SurfaceJungle.Chance * 0.8f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Tribalmask>(), 15));
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
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, new Color(109, 255, 0));
                }
            }
            else
            {
                if (Main.netMode == NetmodeID.Server)
                {
                    return;
                }
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, new Color(109, 255, 0));
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Broodoo").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Broodoo2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Broodoo2").Type, 1f);
            }

        }
        public override void AI()
        {
            float maxspeed = 2f;
            float acceleration = 0.1f;
            if (NPC.ai[0] == 0)
            {
                if (NPC.direction != NPC.oldDirection)
                {
                    NPC.velocity.X *= 0.5f;
                }
                NPC.TargetClosest(true);
                if (NPC.collideX && NPC.collideY)
                {
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.2f, -6f);
                    NPC.ai[0] = 0;
                    NPC.netUpdate = true;
                }
                if (!NPC.HasValidTarget || !Main.dayTime)
                {
                    NPC.velocity.X = NPC.direction * maxspeed;
                    NPC.ai[0] = 2;
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
                }
                if (NPC.velocity.Y > 1f)
                {
                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
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
                if (Main.player[NPC.target].Distance(NPC.Center) < 400f)
                {
                    if (Main.rand.NextBool(180)&& Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.velocity = new Vector2(NPC.direction * maxspeed, -6f);
                        NPC.ai[0] = 1;
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (NPC.ai[0] == 1)
            {
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
            else if (NPC.ai[0] == 2)
            {
                if (NPC.collideX)
                {
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.2f, -6f);
                    NPC.position.X += NPC.direction;
                }
                if (Main.rand.NextBool(600)&& Main.dayTime && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
            }
        }
    }
}
