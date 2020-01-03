using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Goblin : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 52;
            npc.aiStyle = -1;
            npc.damage = 10;
            npc.defense = 0;
            npc.lifeMax = 30;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Goblin");
            npc.value = 50f;
            npc.knockBackResist = 0.6f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDaySlime.Chance * (Main.hardMode ? 0.1f : 0.35f);
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int State_Walk = 0;
        const int State_Jump = 1;
        const int State_Attack = 2;
        const int State_Flee = 3;

        public float AI_State
        {
            get { return npc.ai[AI_State_Slot]; }
            set { npc.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return npc.ai[AI_Timer_Slot]; }
            set { npc.ai[AI_Timer_Slot] = value; }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            AI_State = State_Jump;
            if (npc.life > 0)
            {
                for (int i = 0; i < 4; i++)
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
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Goblin1"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Goblin2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Goblin3"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Goblin3"), 1f);
            }
        }

        public override void NPCLoot()
        {
            int ch = Main.rand.Next(150);
            if (ch < 20)
            {
                Item.NewItem(npc.position, new Vector2(60, 68), mod.ItemType("Airrune"), Main.rand.Next(2, 5));
            }
            else if (ch < 40)
            {
                Item.NewItem(npc.position, new Vector2(60, 68), mod.ItemType("Firerune"), Main.rand.Next(2, 5));
            }
            else if (ch < 60)
            {
                Item.NewItem(npc.position, new Vector2(60, 68), mod.ItemType("Earthrune"), Main.rand.Next(2, 5));
            }
            else if (ch < 80)
            {
                Item.NewItem(npc.position, new Vector2(60, 68), mod.ItemType("Waterrune"), Main.rand.Next(2, 5));
            }
            else if (ch < 100)
            {
                Item.NewItem(npc.position, new Vector2(60, 68), mod.ItemType("Mindrune"), Main.rand.Next(2, 5));
            }
        }

        public override void AI()
        {
            if (AI_State == State_Walk)
            {
                if (npc.direction != npc.oldDirection)
                {
                    npc.velocity.X *= 0.5f;
                }
                npc.TargetClosest(true);
                if (npc.collideX || (npc.velocity.X == 0 && Main.rand.Next(4) == 0 && Main.netMode != 1))
                {
                    npc.velocity = new Vector2(npc.velocity.X * 1.2f, -6f);
                    AI_State = State_Jump;
                    npc.netUpdate = true;
                }
                if (npc.life < 10 || !npc.HasValidTarget || !Main.dayTime)
                {
                    npc.velocity.X = npc.direction * -1.5f;
                    AI_State = State_Flee;
                    npc.netUpdate = true;
                }
                if (npc.HasValidTarget)
                {
                    if (npc.velocity.X < 1.5f && npc.velocity.X > -1.5f)
                    {
                        npc.velocity.X += npc.direction * 0.07f;
                    }
                    else
                    {
                        npc.velocity.X *= 0.9f;
                    }
                }
                if (npc.velocity.Y > 1f)
                {
                    AI_State = State_Jump;
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
                        npc.velocity = new Vector2(npc.direction * 1.5f, -6f);
                        AI_State = State_Jump;
                        npc.netUpdate = true;
                    }
                }
            }
            else if (AI_State == State_Jump)
            {
                if (npc.velocity.X < 1 && npc.velocity.X > -1)
                {
                    npc.velocity.X = 1 * npc.direction;
                }
                if (npc.velocity.Y == 0)
                {
                    AI_State = State_Walk;
                    npc.netUpdate = true;
                }
            }
            else if (AI_State == State_Flee)
            {
                if (npc.velocity.X < 1.5f && npc.velocity.X > -1.5f)
                {
                    npc.velocity.X += npc.direction * 0.07f;
                }
                if (npc.collideX)
                {
                    npc.velocity = new Vector2(npc.velocity.X * 1.2f, -6f);
                    npc.position.X += npc.direction;
                }
                if (Main.rand.Next(600) == 0 && Main.dayTime && Main.netMode != 1)
                {
                    AI_State = State_Walk;
                    npc.netUpdate = true;
                }
            }
        }

        const int Frame_Walk_1 = 0;
        const int Frame_Walk_2 = 1;
        const int Frame_Walk_3 = 2;
        const int Frame_Walk_4 = 3;
        const int Frame_Walk_5 = 4;

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction;
            if ((AI_State == State_Walk || AI_State == State_Flee) && npc.velocity.X != 0)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 7)
                {
                    npc.frame.Y = Frame_Walk_1 * frameHeight;
                }
                else if (npc.frameCounter < 14)
                {
                    npc.frame.Y = Frame_Walk_2 * frameHeight;
                }
                else if (npc.frameCounter < 21)
                {
                    npc.frame.Y = Frame_Walk_3 * frameHeight;
                }
                else if (npc.frameCounter < 28)
                {
                    npc.frame.Y = Frame_Walk_4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (AI_State == State_Jump)
            {
                npc.frame.Y = Frame_Walk_5 * frameHeight;
            }
        }
    }
}
