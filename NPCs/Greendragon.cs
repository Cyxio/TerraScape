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
    public class Greendragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragon");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 35;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.scale = 0.8f;
            NPC.knockBackResist = 0.3f;
            NPC.defense = 10;
            NPC.lifeMax = 150;
            NPC.HitSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Dragonhit");
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 250f;
            NPC.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.damage = (int)(NPC.damage * 0.8f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3)
            {
                return SpawnCondition.OverworldDay.Chance * 0.06f + SpawnCondition.OverworldNightMonster.Chance * 0.03f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Greendhide>(), 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Elvarg.CrandorMap>(), 10));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Greendragon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Greendragon1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Greendragon1").Type, NPC.scale);
            }
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int Fly = 0;
        const int Attack = 1;
        const int Flee = 2;

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

        private float speed = 3f;
        private float accelerate = 0.08f;

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, new Vector3(0.5f, 0.5f, 0.5f));
            if (AI_State == Fly)
            {
                AI_Timer++;
                if (AI_Timer > 140 && Main.rand.NextBool(6)&& Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_Timer = 0;
                    AI_State = Attack;
                    NPC.netUpdate = true;
                }
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                if (NPC.velocity.X > speed)
                {
                    NPC.velocity.X *= 0.9f;
                }
                if (NPC.velocity.Y > speed)
                {
                    NPC.velocity.Y *= 0.9f;
                }
                if (target.MountedCenter.Y - 320 > NPC.Center.Y && NPC.velocity.Y < speed)
                {
                    NPC.velocity.Y += accelerate * 0.75f;
                }
                if (target.MountedCenter.Y - 200 < NPC.Center.Y && NPC.velocity.Y > -speed)
                {
                    NPC.velocity.Y -= accelerate * 0.75f;
                }
                if (target.MountedCenter.X > NPC.Center.X && NPC.velocity.X < speed)
                {
                    NPC.velocity.X += accelerate;
                }
                if (target.MountedCenter.X < NPC.Center.X && NPC.velocity.X > -speed)
                {
                    NPC.velocity.X -= accelerate;
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
                if (!NPC.HasValidTarget)
                {
                    NPC.velocity *= 0;
                    AI_State = Flee;
                }
            }
            if (AI_State == Attack)
            {
                AI_Timer++;
                if (AI_Timer > 16)
                {
                    AI_State = Fly;
                    NPC.netUpdate = true;
                }
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float speedX = target.MountedCenter.X - (NPC.Center.X + 65 * NPC.direction);
                float speedY = target.MountedCenter.Y - (NPC.Center.Y);
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 12;
                if (AI_Timer == 8)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 65 * NPC.direction, NPC.Center.Y), spd, Mod.Find<ModProjectile>("Dragonfire").Type, (NPC.damage / 4), 0f, 0, 0, 0);
                }
            }
            if (AI_State == Flee)
            {
                NPC.velocity.Y -= 0.1f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (AI_State == Fly || AI_State == Flee)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 6)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < 12)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.frameCounter < 18)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            if (AI_State == Attack)
            {
                if (AI_Timer < 8)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else if (AI_Timer < 16)
                {
                    NPC.frame.Y = 4 * frameHeight;
                }               
            }
        }
    }
}