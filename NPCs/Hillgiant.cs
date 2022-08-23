using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class Hillgiant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hill Giant");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.scale = 1.3f;
            NPC.width = 25;
            NPC.height = 60;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 80;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.value = Item.buyPrice(0, 0, 5);
            NPC.aiStyle = -1;
            AnimationType = NPCID.Zombie;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Longbone>(), 100));
            var supplyDrop = ItemDropRule.Common(ModContent.ItemType<Items.Magic.Lawrune>(), 7, 2, 2)
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Chaosrune>(), 6, 2, 2))
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Cosmicrune>(), 5, 2, 2))
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Mindrune>(), 4, 5, 10))
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Firerune>(), 3, 5, 10))
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Waterrune>(), 2, 5, 10))
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Earthrune>(), 1, 5, 10));
            npcLoot.Add(supplyDrop);
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
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.Beige);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.Beige);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Hillgiant").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Hillgiant1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Hillgiant1").Type, NPC.scale);
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Cavern.Chance * (Main.hardMode ? 0.05f : 0.2f);
        }
        public override void AI()
        {
            float maxspeed = 1.2f;
            float acceleration = 0.5f;
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
            }
        }
    }
}