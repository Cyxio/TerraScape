using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Hillgiant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hill Giant");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.scale = 1.3f;
            npc.width = 25;
            npc.height = 60;
            npc.damage = 20;
            npc.defense = 10;
            npc.lifeMax = 80;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath8;
            npc.value = Item.buyPrice(0, 0, 5);
            npc.aiStyle = -1;
            animationType = NPCID.Zombie;
        }
        public override void NPCLoot()
        {
            int ch = Main.rand.Next(7);
            int item = ModContent.ItemType<Items.Magic.Airrune>();
            int stack = Main.rand.Next(5, 11);
            switch (ch)
            {
                case 0:
                    item = ModContent.ItemType<Items.Magic.Lawrune>();
                    stack = 2;
                    break;
                case 1:
                    item = ModContent.ItemType<Items.Magic.Waterrune>();
                    break;
                case 2:
                    item = ModContent.ItemType<Items.Magic.Earthrune>();
                    break;
                case 3:
                    item = ModContent.ItemType<Items.Magic.Firerune>();
                    break;
                case 4:
                    item = ModContent.ItemType<Items.Magic.Mindrune>();
                    break;
                case 5:
                    item = ModContent.ItemType<Items.Magic.Cosmicrune>();
                    stack = 2;
                    break;
                default:
                    item = ModContent.ItemType<Items.Magic.Chaosrune>();
                    stack = 2;
                    break;
            }
            Item.NewItem(npc.Hitbox, item, stack);
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Longbone>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0] = 0;
            if (npc.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.Beige);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.Beige);
                }
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Hillgiant"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Hillgiant1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Hillgiant1"), npc.scale);
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
            }
        }
    }
}