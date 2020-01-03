using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.NPCs
{
    public class Saradominwizard : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saradomin Wizard");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 44;
            npc.damage = 50;
            npc.defense = 20;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 40);
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.3f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.OverworldDay.Chance * 0.08f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(33) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Sarastaff>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                if (Main.rand.Next(10) == 0 && Main.netMode != 1)
                {
                    npc.ai[0] = 3;
                    npc.netUpdate = true;
                }
            }
            else
            {
                Gore.NewGore(npc.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), mod.GetGoreSlot("Gores/Zamorakwizard"), 1f);
                Gore.NewGore(npc.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), mod.GetGoreSlot("Gores/Saradominwizard1"), 1f);
                Gore.NewGore(npc.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), mod.GetGoreSlot("Gores/Saradominwizard1"), 1f);
            }

        }
        public override void AI()
        {
            float maxspeed = 1.5f;
            float acceleration = 0.15f;
            if (npc.ai[0] == 0)
            {
                if (npc.direction != npc.oldDirection)
                {
                    npc.velocity.X *= 0.5f;
                }
                npc.TargetClosest(true);
                if (npc.collideX && (npc.collideY || npc.velocity.Y == 0))
                {
                    npc.velocity = new Vector2(npc.velocity.X * 1.2f, -6f);
                    npc.ai[0] = 0;
                }
                if (npc.HasValidTarget)
                {
                    if (npc.velocity.X < maxspeed && npc.velocity.X > -maxspeed)
                    {
                        npc.velocity.X += npc.direction * acceleration;
                    }
                    else
                    {
                        npc.velocity.X *= 0.9f;
                    }
                }
                if (Main.player[npc.target].position.Y > npc.Bottom.Y)
                {
                    int x = (int)(npc.position.X / 16f);
                    int y = (int)(npc.BottomLeft.Y / 16f);
                    if (TileID.Sets.Platforms[Main.tile[x, y].type])
                    {
                        npc.position.Y += 1;
                    }
                }
                if (npc.velocity.Y > 1f)
                {
                    npc.ai[0] = 1;
                }
                if (Main.player[npc.target].Distance(npc.Center) < 400f)
                {
                    if (Main.rand.Next(120) == 0 && Main.netMode != 1)
                    {
                        npc.ai[0] = 3;
                        npc.netUpdate = true;
                    }
                }
            }
            else if (npc.ai[0] == 1)
            {
                if (npc.velocity.X < 1 && npc.velocity.X > -1)
                {
                    npc.velocity.X = 1 * npc.direction;
                }
                if (npc.velocity.Y == 0)
                {
                    npc.ai[0] = 0;
                }
            }
            else if (npc.ai[0] == 3)
            {
                npc.ai[1]++;
                npc.velocity.X *= 0.9f;
                if (npc.ai[1] == 30)
                {
                    npc.TargetClosest(true);
                    Player target = Main.player[npc.target];
                    float speedX = target.MountedCenter.X - npc.Center.X;
                    float speedY = target.MountedCenter.Y - npc.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    spd *= 14;
                    int proj = Projectile.NewProjectile(npc.Center, spd, 126, npc.damage / 4, 0f);
                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].netUpdate = true;
                }
                if (npc.ai[1] > 40)
                {
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                    npc.netUpdate = true;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.ai[0] == 0)
            {
                int frameSpeed = 7;
                npc.frameCounter += Math.Abs(npc.velocity.X / 2);
                if (npc.frameCounter < frameSpeed)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter < frameSpeed * 2)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.frameCounter < frameSpeed * 3)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (npc.ai[0] == 3)
            {
                if (npc.ai[1] < 10)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else if (npc.ai[1] < 20)
                {
                    npc.frame.Y = frameHeight * 4;
                }
                else
                {
                    npc.frame.Y = frameHeight * 5;
                }
            }
            else
            {
                npc.frame.Y = frameHeight;
            }
        }
    }
}
