using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Broodoovictim : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broodoo victim");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
        }
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;
            npc.damage = 14;
            npc.defense = 6;
            npc.lifeMax = 75;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath36;
            npc.value = 100f;
            npc.aiStyle = -1;
            animationType = NPCID.Zombie;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime)
            {
                return SpawnCondition.SurfaceJungle.Chance * 0.8f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(15) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Armor.Tribalmask>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0] = 0;
            if (npc.life > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, new Color(109, 255, 0));
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, new Color(109, 255, 0));
                }
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Broodoo"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Broodoo2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Broodoo2"), 1f);
            }

        }
        public override void AI()
        {
            float maxspeed = 2f;
            float acceleration = 0.1f;
            if (npc.ai[0] == 0)
            {
                if (npc.direction != npc.oldDirection)
                {
                    npc.velocity.X *= 0.5f;
                }
                npc.TargetClosest(true);
                if (npc.collideX && npc.collideY)
                {
                    npc.velocity = new Vector2(npc.velocity.X * 1.2f, -6f);
                    npc.ai[0] = 0;
                    npc.netUpdate = true;
                }
                if (!npc.HasValidTarget || !Main.dayTime)
                {
                    npc.velocity.X = npc.direction * maxspeed;
                    npc.ai[0] = 2;
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
                }
                if (npc.velocity.Y > 1f)
                {
                    npc.ai[0] = 1;
                    npc.netUpdate = true;
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
                if (Main.player[npc.target].Distance(npc.Center) < 400f)
                {
                    if (Main.rand.Next(180) == 0 && Main.netMode != 1)
                    {
                        npc.velocity = new Vector2(npc.direction * maxspeed, -6f);
                        npc.ai[0] = 1;
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
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 2)
            {
                if (npc.collideX)
                {
                    npc.velocity = new Vector2(npc.velocity.X * 1.2f, -6f);
                    npc.position.X += npc.direction;
                }
                if (Main.rand.Next(600) == 0 && Main.dayTime && Main.netMode != 1)
                {
                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }
        }
    }
}
