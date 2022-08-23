using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;

namespace OldSchoolRuneScape.NPCs
{
    public class Zamorakwizard : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zamorak Wizard");
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 44;
            NPC.damage = 50;
            NPC.defense = 20;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 40);
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0.3f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.OverworldDay.Chance * 0.08f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Zamorakstaff>(), 33));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                if (Main.rand.NextBool(10)&& Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0] = 3;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), Mod.Find<ModGore>("Zamorakwizard").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), Mod.Find<ModGore>("Zamorakwizard1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), Mod.Find<ModGore>("Zamorakwizard1").Type, 1f);
            }

        }
        public override void AI()
        {
            float maxspeed = 1.5f;
            float acceleration = 0.15f;
            if (NPC.ai[0] == 0)
            {
                if (NPC.direction != NPC.oldDirection)
                {
                    NPC.velocity.X *= 0.5f;
                }
                NPC.TargetClosest(true);
                if (NPC.collideX && (NPC.collideY || NPC.velocity.Y == 0))
                {
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.2f, -6f);
                    NPC.ai[0] = 0;
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
                }
                if (Main.player[NPC.target].Distance(NPC.Center) < 400f)
                {
                    if (Main.rand.NextBool(120)&& Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[0] = 3;
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
                }
            }
            else if (NPC.ai[0] == 3)
            {
                NPC.ai[1]++;
                NPC.velocity.X *= 0.9f;
                if (NPC.ai[1] == 30 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.TargetClosest(true);
                    Player target = Main.player[NPC.target];
                    float speedX = target.MountedCenter.X - NPC.Center.X;
                    float speedY = target.MountedCenter.Y - NPC.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    spd *= 14;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, ProjectileID.Fireball, NPC.damage / 4, 0f);
                }
                if (NPC.ai[1] > 40)
                {
                    NPC.ai[1] = 0;
                    NPC.ai[0] = 0;
                    NPC.netUpdate = true;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.ai[0] == 0)
            {
                int frameSpeed = 7;
                NPC.frameCounter += Math.Abs(NPC.velocity.X / 2);
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
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else if (NPC.ai[0] == 3)
            {
                if (NPC.ai[1] < 10)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
                else if (NPC.ai[1] < 20)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
                else
                {
                    NPC.frame.Y = frameHeight * 5;
                }
            }
            else
            {
                NPC.frame.Y = frameHeight;
            }
        }
    }
}
