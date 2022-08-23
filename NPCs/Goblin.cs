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
    public class Goblin : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 52;
            NPC.aiStyle = -1;
            NPC.damage = 10;
            NPC.defense = 0;
            NPC.lifeMax = 30;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Goblin");
            NPC.value = 50f;
            NPC.knockBackResist = 0.6f;
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
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, new Color(109, 255, 0));
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, new Color(109, 255, 0));
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Goblin1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Goblin2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Goblin3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Goblin3").Type, 1f);
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Airrune>(), 10, 2, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Firerune>(), 10, 2, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Earthrune>(), 10, 2, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Waterrune>(), 10, 2, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Mindrune>(), 10, 2, 5));
        }

        public override void AI()
        {
            if (AI_State == State_Walk)
            {
                if (NPC.direction != NPC.oldDirection)
                {
                    NPC.velocity.X *= 0.5f;
                }
                NPC.TargetClosest(true);
                if (NPC.collideX || (NPC.velocity.X == 0 && Main.rand.NextBool(4)&& Main.netMode != NetmodeID.MultiplayerClient))
                {
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.2f, -6f);
                    AI_State = State_Jump;
                    NPC.netUpdate = true;
                }
                if (NPC.life < 10 || !NPC.HasValidTarget || !Main.dayTime)
                {
                    NPC.velocity.X = NPC.direction * -1.5f;
                    AI_State = State_Flee;
                    NPC.netUpdate = true;
                }
                if (NPC.HasValidTarget)
                {
                    if (NPC.velocity.X < 1.5f && NPC.velocity.X > -1.5f)
                    {
                        NPC.velocity.X += NPC.direction * 0.07f;
                    }
                    else
                    {
                        NPC.velocity.X *= 0.9f;
                    }
                }
                if (NPC.velocity.Y > 1f)
                {
                    AI_State = State_Jump;
                    NPC.netUpdate = true;
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
                if (Main.player[NPC.target].Distance(NPC.Center) < 400f)
                {
                    if (Main.rand.NextBool(180)&& Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.velocity = new Vector2(NPC.direction * 1.5f, -6f);
                        AI_State = State_Jump;
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (AI_State == State_Jump)
            {
                if (NPC.velocity.X < 1 && NPC.velocity.X > -1)
                {
                    NPC.velocity.X = 1 * NPC.direction;
                }
                if (NPC.velocity.Y == 0)
                {
                    AI_State = State_Walk;
                    NPC.netUpdate = true;
                }
            }
            else if (AI_State == State_Flee)
            {
                if (NPC.velocity.X < 1.5f && NPC.velocity.X > -1.5f)
                {
                    NPC.velocity.X += NPC.direction * 0.07f;
                }
                if (NPC.collideX)
                {
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.2f, -6f);
                    NPC.position.X += NPC.direction;
                }
                if (Main.rand.NextBool(600)&& Main.dayTime && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_State = State_Walk;
                    NPC.netUpdate = true;
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
            NPC.spriteDirection = -NPC.direction;
            if ((AI_State == State_Walk || AI_State == State_Flee) && NPC.velocity.X != 0)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 7)
                {
                    NPC.frame.Y = Frame_Walk_1 * frameHeight;
                }
                else if (NPC.frameCounter < 14)
                {
                    NPC.frame.Y = Frame_Walk_2 * frameHeight;
                }
                else if (NPC.frameCounter < 21)
                {
                    NPC.frame.Y = Frame_Walk_3 * frameHeight;
                }
                else if (NPC.frameCounter < 28)
                {
                    NPC.frame.Y = Frame_Walk_4 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else if (AI_State == State_Jump)
            {
                NPC.frame.Y = Frame_Walk_5 * frameHeight;
            }
        }
    }
}
