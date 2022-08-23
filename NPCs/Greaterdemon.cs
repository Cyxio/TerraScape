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
    public class Greaterdemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greater Demon");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 76;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 15;
            NPC.lifeMax = 200;
            NPC.scale = 1.25f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Demon");
            NPC.value = 500f;
            NPC.knockBackResist = 0.3f;
            NPC.lavaImmune = true;
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
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Greaterdemon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lesserdemon2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lesserdemon3").Type, NPC.scale);
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Firerune>(), 5, 30, 120));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Chaosrune>(), 10, 12, 15));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Deathrune>(), 25, 3, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Runeplatelegs>(), 100));
            npcLoot.Add(ItemDropRule.Common(ItemID.DemonScythe, 100));
        }
        int i = 0;
        public override void AI()
        {
            if (AI_State == State_Walk)
            {
                while (NPC.velocity.X > 2f || NPC.velocity.X < -2f)
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
                if (Main.player[NPC.target].position.Y > NPC.Bottom.Y)
                {
                    int x = (int)(NPC.position.X / 16f);
                    int y = (int)(NPC.BottomLeft.Y / 16f);
                    if (TileID.Sets.Platforms[Main.tile[x, y].TileType])
                    {
                        NPC.position.Y += 1;
                    }
                }
                if (NPC.HasValidTarget)
                {
                    if (NPC.velocity.X < 2f && NPC.direction == 1)
                    {
                        NPC.velocity.X += 0.14f;
                    }
                    if (NPC.velocity.X > -2f && NPC.direction == -1)
                    {
                        NPC.velocity.X -= 0.14f;
                    }
                }
                if (Main.player[NPC.target].Distance(NPC.Center) < 25f)
                {
                    AI_State = State_Attack;
                    AI_Timer = 0;
                    NPC.netUpdate = true;
                }
                if (Main.player[NPC.target].Distance(NPC.Center) < 350f && Main.player[NPC.target].Distance(NPC.Center) > 300f && NPC.velocity.Y == 0)
                {
                    NPC.velocity = new Vector2(NPC.direction * 5f, -5f);
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
                if (Main.rand.NextBool(600)&& !Main.dayTime)
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
                if (NPC.frameCounter < 8)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 16)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 24)
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
