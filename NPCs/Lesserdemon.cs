using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class Lesserdemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lesser Demon");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 76;
            NPC.aiStyle = -1;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Demon");
            NPC.value = 250f;
            NPC.knockBackResist = 0.5f;
            NPC.lavaImmune = true;
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
            get { return NPC.ai[AI_State_Slot]; }
            set { NPC.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return NPC.ai[AI_Timer_Slot]; }
            set { NPC.ai[AI_Timer_Slot] = value; }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            AI_State = State_Jump;
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lesserdemon").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lesserdemon2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lesserdemon3").Type, 1f);
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Firerune>(), 5, 10, 20));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Chaosrune>(), 10, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Runehelm>(), 100));
        }
        int i = 0;
        public override void AI()
        {
            if (AI_State == State_Walk)
            {
                while (NPC.velocity.X > 1.4f || NPC.velocity.X < -1.4f)
                {
                    NPC.velocity *= 0.9f;
                }
                NPC.TargetClosest(true);
                if (NPC.velocity.X == 0)
                {
                    i++;
                    if(i > 600)
                    {
                        NPC.direction *= -1;
                        AI_State = State_Flee;
                        i = 0;
                        NPC.netUpdate = true;
                    }
                }
                if (NPC.HasValidTarget)
                {
                    if (NPC.velocity.X < 1.4f && NPC.direction == 1)
                    {
                        NPC.velocity.X += 0.1f;
                    }
                    if (NPC.velocity.X > -1.4f && NPC.direction == -1)
                    {
                        NPC.velocity.X -= 0.1f;
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
                if (Main.player[NPC.target].Distance(NPC.Center) < 25f)
                {
                    AI_State = State_Attack;
                    AI_Timer = 0;
                    NPC.netUpdate = true;
                }
                if (Main.player[NPC.target].Distance(NPC.Center) < 200f && Main.player[NPC.target].Distance(NPC.Center) > 150f && NPC.velocity.Y == 0)
                {
                    NPC.velocity = new Vector2(NPC.direction * 4f, -5f);
                    AI_State = State_Jump;
                    NPC.netUpdate = true;
                }
                if (NPC.collideX || (NPC.velocity.X == 0 && Main.rand.NextBool(4)&& Main.netMode != NetmodeID.MultiplayerClient))
                {
                    NPC.velocity.Y = -6f;
                    NPC.velocity.X = NPC.direction * 2f;
                    AI_State = State_Jump;
                    NPC.netUpdate = true;
                }
                if (!NPC.HasValidTarget || Main.dayTime)
                {
                    NPC.direction *= -1;
                    AI_State = State_Flee;
                    NPC.netUpdate = true;
                }
            }
            else if (AI_State == State_Attack)
            {
                AI_Timer++;
                if (AI_Timer < 27)
                {
                    NPC.velocity.X = 0;
                }
                else
                {
                    AI_State = State_Walk;
                    NPC.netUpdate = true;
                }
            }
            else if (AI_State == State_Jump)
            {
                if (NPC.velocity.X < 2 && NPC.velocity.X > -2)
                {
                    NPC.velocity.X = 2 * NPC.direction;
                }
                if (NPC.velocity.Y == 0)
                {
                    AI_State = State_Walk;
                    NPC.netUpdate = true;
                }
            }
            else if (AI_State == State_Flee)
            {
                NPC.velocity.X = NPC.direction * 2f;
                if (Main.rand.NextBool(600)&& !Main.dayTime && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_State = State_Walk;
                    NPC.netUpdate = true;
                }
                if (NPC.collideX || (NPC.velocity.X == 0 && Main.rand.NextBool(4)&& Main.netMode != NetmodeID.MultiplayerClient))
                {
                    NPC.velocity.Y = -6f;
                    NPC.velocity.X = NPC.direction * 2f;
                    AI_State = State_Jump;
                    NPC.netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -NPC.direction;
            if (NPC.velocity.Y != 0)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 9)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 18)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 27)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
        }
    }
}
