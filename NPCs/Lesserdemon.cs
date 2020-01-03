using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Lesserdemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lesser Demon");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 76;
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 10;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Demon");
            npc.value = 250f;
            npc.knockBackResist = 0.5f;
            npc.lavaImmune = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime && NPC.downedBoss1)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.1f;
            }
            else
            {
                return 0;
            }
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
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5);
                }
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lesserdemon"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lesserdemon2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lesserdemon3"), 1f);
            }
        }

        public override void NPCLoot()
        {
            int ch = Main.rand.Next(100);
            if (ch < 20)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Firerune"), Main.rand.Next(10, 20)); 
            }
            else if (ch < 30)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Chaosrune"), 1);
            }
            else if (ch < 31)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Runehelm"));
            }
        }
        int i = 0;
        public override void AI()
        {
            if (AI_State == State_Walk)
            {
                while (npc.velocity.X > 1.4f || npc.velocity.X < -1.4f)
                {
                    npc.velocity *= 0.9f;
                }
                npc.TargetClosest(true);
                if (npc.velocity.X == 0)
                {
                    i++;
                    if(i > 600)
                    {
                        npc.direction *= -1;
                        AI_State = State_Flee;
                        i = 0;
                        npc.netUpdate = true;
                    }
                }
                if (npc.HasValidTarget)
                {
                    if (npc.velocity.X < 1.4f && npc.direction == 1)
                    {
                        npc.velocity.X += 0.1f;
                    }
                    if (npc.velocity.X > -1.4f && npc.direction == -1)
                    {
                        npc.velocity.X -= 0.1f;
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
                if (Main.player[npc.target].Distance(npc.Center) < 25f)
                {
                    AI_State = State_Attack;
                    AI_Timer = 0;
                    npc.netUpdate = true;
                }
                if (Main.player[npc.target].Distance(npc.Center) < 200f && Main.player[npc.target].Distance(npc.Center) > 150f && npc.velocity.Y == 0)
                {
                    npc.velocity = new Vector2(npc.direction * 4f, -5f);
                    AI_State = State_Jump;
                    npc.netUpdate = true;
                }
                if (npc.collideX || (npc.velocity.X == 0 && Main.rand.Next(4) == 0 && Main.netMode != 1))
                {
                    npc.velocity.Y = -6f;
                    npc.velocity.X = npc.direction * 2f;
                    AI_State = State_Jump;
                    npc.netUpdate = true;
                }
                if (!npc.HasValidTarget || Main.dayTime)
                {
                    npc.direction *= -1;
                    AI_State = State_Flee;
                    npc.netUpdate = true;
                }
            }
            else if (AI_State == State_Attack)
            {
                AI_Timer++;
                if (AI_Timer < 27)
                {
                    npc.velocity.X = 0;
                }
                else
                {
                    AI_State = State_Walk;
                    npc.netUpdate = true;
                }
            }
            else if (AI_State == State_Jump)
            {
                if (npc.velocity.X < 2 && npc.velocity.X > -2)
                {
                    npc.velocity.X = 2 * npc.direction;
                }
                if (npc.velocity.Y == 0)
                {
                    AI_State = State_Walk;
                    npc.netUpdate = true;
                }
            }
            else if (AI_State == State_Flee)
            {
                npc.velocity.X = npc.direction * 2f;
                if (Main.rand.Next(600) == 0 && !Main.dayTime && Main.netMode != 1)
                {
                    AI_State = State_Walk;
                    npc.netUpdate = true;
                }
                if (npc.collideX || (npc.velocity.X == 0 && Main.rand.Next(4) == 0 && Main.netMode != 1))
                {
                    npc.velocity.Y = -6f;
                    npc.velocity.X = npc.direction * 2f;
                    AI_State = State_Jump;
                    npc.netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction;
            if (npc.velocity.Y != 0)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter < 9)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 18)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 27)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }
    }
}
