using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Greaterdemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greater Demon");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 76;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 15;
            npc.lifeMax = 200;
            npc.scale = 1.25f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Demon");
            npc.value = 500f;
            npc.knockBackResist = 0.3f;
            npc.lavaImmune = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime && NPC.downedBoss3)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.05f;
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
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Greaterdemon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lesserdemon2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lesserdemon3"), npc.scale);
            }
        }

        public override void NPCLoot()
        {
            int ch = Main.rand.Next(100);
            if (ch < 20)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Firerune"), Main.rand.Next(30, 120)); 
            }
            else if (ch < 30)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Chaosrune"), 5);
            }
            else if (ch < 34)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Deathrune"), 3);
            }
            else if (ch < 35)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), ItemID.DemonScythe);
            }
            else if (ch < 36)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Runeplatelegs"));
            }
        }
        int i = 0;
        public override void AI()
        {
            if (AI_State == State_Walk)
            {
                while (npc.velocity.X > 2f || npc.velocity.X < -2f)
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
                if (Main.player[npc.target].position.Y > npc.Bottom.Y)
                {
                    int x = (int)(npc.position.X / 16f);
                    int y = (int)(npc.BottomLeft.Y / 16f);
                    if (TileID.Sets.Platforms[Main.tile[x, y].type])
                    {
                        npc.position.Y += 1;
                    }
                }
                if (npc.HasValidTarget)
                {
                    if (npc.velocity.X < 2f && npc.direction == 1)
                    {
                        npc.velocity.X += 0.14f;
                    }
                    if (npc.velocity.X > -2f && npc.direction == -1)
                    {
                        npc.velocity.X -= 0.14f;
                    }
                }
                if (Main.player[npc.target].Distance(npc.Center) < 25f)
                {
                    AI_State = State_Attack;
                    AI_Timer = 0;
                    npc.netUpdate = true;
                }
                if (Main.player[npc.target].Distance(npc.Center) < 350f && Main.player[npc.target].Distance(npc.Center) > 300f && npc.velocity.Y == 0)
                {
                    npc.velocity = new Vector2(npc.direction * 5f, -5f);
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
                if (Main.rand.Next(600) == 0 && !Main.dayTime)
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
                if (npc.frameCounter < 8)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 16)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 24)
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
