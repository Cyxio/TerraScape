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
    public class Lavadragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Dragon");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 35;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.scale = 1.1f;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.defense = 30;
            NPC.lifeMax = 1250;
            NPC.HitSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Dragonhit");
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 1000f;
            NPC.noGravity = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && NPC.downedPlantBoss)
            {
                return SpawnCondition.Underworld.Chance * 0.1f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Blackdhide>(), 1, 5, 9));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Onyx>(), 150));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lavadragon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lavadragon1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Lavadragon1").Type, NPC.scale);
            }
        }

        const int Fly = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float AI_State
        {
            get { return NPC.ai[0]; }
            set { NPC.ai[0] = value; }
        }

        public float AI_Timer
        {
            get { return NPC.ai[1]; }
            set { NPC.ai[1] = value; }
        }
        public float attack
        {
            get { return NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }

        private float speed = 4f;
        private float accelerate = 0.25f;

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, new Vector3(0.5f, 0.5f, 0.5f));
            if (AI_State == Fly)
            {
                NPC.rotation = 0;
                AI_Timer++;
                if (AI_Timer > 140)
                {
                    AI_Timer = 0;
                    AI_State = Attack;
                    NPC.netUpdate = true;
                }
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                while (NPC.velocity.X > speed || NPC.velocity.Y > speed || NPC.velocity.X < -speed || NPC.velocity.Y < -speed)
                {
                    NPC.velocity *= 0.9f;
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
                    NPC.velocity.X += accelerate * 2f;
                }
                if (target.MountedCenter.X < NPC.Center.X && NPC.velocity.X > -speed)
                {
                    NPC.velocity.X -= accelerate * 2f;
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
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float speedX = target.MountedCenter.X - (NPC.Center.X + 80 * NPC.direction);
                float speedY = target.MountedCenter.Y - (NPC.Center.Y);
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 18;
                if (AI_Timer == 3 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    attack = Main.rand.Next(2);
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Dragonhit"), NPC.Center);
                    NPC.netUpdate = true;
                }
                if (attack == 0)
                {
                    NPC.velocity *= 0;
                    if (NPC.direction == 1)
                    {
                        NPC.rotation = spd.ToRotation();
                    }
                    else if (NPC.direction == -1)
                    {
                        NPC.rotation = spd.ToRotation() + MathHelper.ToRadians(180);
                    }
                    if(AI_Timer % 5 == 0)
                    {
                        Vector2 rotates = new Vector2(80 * NPC.direction, 0).RotatedBy(NPC.rotation);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + rotates, spd.RotateRandom(MathHelper.ToRadians(10)), Mod.Find<ModProjectile>("Dragonfire").Type, (NPC.damage / 4), 0f, 0, 0, 0);
                    }
                    if (AI_Timer > 30)
                    {
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attack == 1)
                {
                    if (AI_Timer == 5)
                    {
                        speedX = target.MountedCenter.X - NPC.Center.X;
                        speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        NPC.velocity = spd * 20f;
                        if (NPC.direction == 1)
                        {
                            NPC.rotation = NPC.velocity.ToRotation();
                        }
                        else if (NPC.direction == -1)
                        {
                            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(180);
                        }
                    }
                    if (AI_Timer > 40)
                    {
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
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
                if (attack == 1)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 4 * frameHeight;
                }
            }
        }
    }
}