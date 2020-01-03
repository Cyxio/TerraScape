using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Abyssaldemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Demon");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 60;
            npc.damage = 80;
            npc.defense = 20;
            npc.lifeMax = 450;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.value = Item.buyPrice(0, 2);
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.2f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.Underworld.Chance * 0.1f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Abyssalwhip>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0] = 0;
            if (npc.life > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.LightGray);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.LightGray);
                }
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Abyssaldemon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Abyssaldemon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Abyssaldemon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Abyssaldemon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Abyssaldemon1"), npc.scale);
            }
        }
        public override void AI()
        {
            float maxspeed = 4f;
            float acceleration = 0.2f;
            if (npc.ai[0] == 0)
            {
                npc.ai[2]++;
                if (npc.direction != npc.oldDirection)
                {
                    npc.velocity.X *= 0.5f;
                }
                npc.TargetClosest(true);
                if (npc.collideX)
                {
                    npc.velocity = new Vector2(npc.velocity.X * 1.2f, -6f);
                    npc.ai[0] = 1;
                    npc.netUpdate = true;
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
                    if (npc.ai[2] > 60 * 8 && npc.Distance(Main.player[npc.target].position) < 1200f && Main.netMode != 1)
                    {
                        npc.ai[0] = 2;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
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
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 1)
            {
                npc.ai[2]++;
                if (npc.velocity.X < 1 && npc.velocity.X > -1)
                {
                    npc.velocity.X = 1 * npc.direction;
                }
                if (npc.velocity.Y == 0)
                {
                    npc.ai[0] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 2 && Main.netMode != 1)
            {
                npc.velocity *= 0.9f;
                Lighting.AddLight(npc.Center, new Vector3(0.14f * 3, 0, 0.22f * 3));
                npc.ai[2]++;
                if (npc.ai[2] > 14)
                {
                    Vector2 distance = new Vector2(0, Main.rand.Next(-300, -150)).RotateRandom(MathHelper.ToRadians(65));
                    Vector2 pos = Main.player[npc.target].Center + distance;
                    int x = (int)(pos.X / 16f);
                    int y = (int)(pos.Y / 16f);
                    if (Main.tile[x, y].type == 0 && !Main.tile[x, y].active())
                    {
                        npc.position = pos;
                        for (int i = 0; i < 10; i++)
                        {
                            Dust.NewDust(pos, 0, 0, 27);
                        }
                        npc.ai[0] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                    }
                }                    
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.ai[0] == 0)
            {
                npc.frameCounter += Math.Abs(npc.velocity.X / 2);
                int frameSpeed = 8;
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
                else if (npc.frameCounter < frameSpeed * 4)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (npc.ai[0] == 2)
            {
                if (npc.ai[2] < 8)
                {
                    npc.frame.Y = frameHeight * 4;
                }
                else if (npc.ai[2] < 8 * 2)
                {
                    npc.frame.Y = frameHeight * 5;
                }
                else if (npc.ai[2] < 8 * 3)
                {
                    npc.frame.Y = frameHeight * 6;
                }
            }
            else
            {
                npc.frame.Y = 0;
            }
        }
    }
}
