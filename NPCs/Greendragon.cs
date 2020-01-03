﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Greendragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragon");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 150;
            npc.height = 35;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.scale = 0.8f;
            npc.knockBackResist = 0.3f;
            npc.defense = 10;
            npc.lifeMax = 150;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Dragonhit");
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 250f;
            npc.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * 0.8f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3)
            {
                return SpawnCondition.OverworldDay.Chance * 0.06f + SpawnCondition.OverworldNightMonster.Chance * 0.03f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Greendhide>());
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Elvarg.CrandorMap>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Greendragon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Greendragon1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Greendragon1"), npc.scale);
            }
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int Fly = 0;
        const int Attack = 1;
        const int Flee = 2;

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

        private float speed = 3f;
        private float accelerate = 0.08f;

        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Vector3(0.5f, 0.5f, 0.5f));
            if (AI_State == Fly)
            {
                AI_Timer++;
                if (AI_Timer > 140 && Main.rand.Next(6) == 0 && Main.netMode != 1)
                {
                    AI_Timer = 0;
                    AI_State = Attack;
                    npc.netUpdate = true;
                }
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                if (npc.velocity.X > speed)
                {
                    npc.velocity.X *= 0.9f;
                }
                if (npc.velocity.Y > speed)
                {
                    npc.velocity.Y *= 0.9f;
                }
                if (target.MountedCenter.Y - 320 > npc.Center.Y && npc.velocity.Y < speed)
                {
                    npc.velocity.Y += accelerate * 0.75f;
                }
                if (target.MountedCenter.Y - 200 < npc.Center.Y && npc.velocity.Y > -speed)
                {
                    npc.velocity.Y -= accelerate * 0.75f;
                }
                if (target.MountedCenter.X > npc.Center.X && npc.velocity.X < speed)
                {
                    npc.velocity.X += accelerate;
                }
                if (target.MountedCenter.X < npc.Center.X && npc.velocity.X > -speed)
                {
                    npc.velocity.X -= accelerate;
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
                if (!npc.HasValidTarget)
                {
                    npc.velocity *= 0;
                    AI_State = Flee;
                }
            }
            if (AI_State == Attack)
            {
                AI_Timer++;
                if (AI_Timer > 16)
                {
                    AI_State = Fly;
                    npc.netUpdate = true;
                }
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float speedX = target.MountedCenter.X - (npc.Center.X + 65 * npc.direction);
                float speedY = target.MountedCenter.Y - (npc.Center.Y);
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 12;
                if (AI_Timer == 8)
                {
                    Projectile.NewProjectile(new Vector2(npc.Center.X + 65 * npc.direction, npc.Center.Y), spd, mod.ProjectileType("Dragonfire"), (npc.damage / 4), 0f, 0, 0, 0);
                }
            }
            if (AI_State == Flee)
            {
                npc.velocity.Y -= 0.1f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (AI_State == Fly || AI_State == Flee)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 6)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter < 12)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.frameCounter < 18)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            if (AI_State == Attack)
            {
                if (AI_Timer < 8)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else if (AI_Timer < 16)
                {
                    npc.frame.Y = 4 * frameHeight;
                }               
            }
        }
    }
}