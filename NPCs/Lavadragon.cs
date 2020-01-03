using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Lavadragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Dragon");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 150;
            npc.height = 35;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.scale = 1.1f;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.defense = 30;
            npc.lifeMax = 1250;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Dragonhit");
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 1000f;
            npc.noGravity = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && NPC.downedPlantBoss)
            {
                return SpawnCondition.Underworld.Chance * 0.1f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Blackdhide>(), Main.rand.Next(3, 7));
            if (Main.rand.Next(150) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Onyx>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lavadragon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lavadragon1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Lavadragon1"), npc.scale);
            }
        }

        const int Fly = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float AI_State
        {
            get { return npc.ai[0]; }
            set { npc.ai[0] = value; }
        }

        public float AI_Timer
        {
            get { return npc.ai[1]; }
            set { npc.ai[1] = value; }
        }
        public float attack
        {
            get { return npc.ai[2]; }
            set { npc.ai[2] = value; }
        }

        private float speed = 4f;
        private float accelerate = 0.25f;

        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Vector3(0.5f, 0.5f, 0.5f));
            if (AI_State == Fly)
            {
                npc.rotation = 0;
                AI_Timer++;
                if (AI_Timer > 140)
                {
                    AI_Timer = 0;
                    AI_State = Attack;
                    npc.netUpdate = true;
                }
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                while (npc.velocity.X > speed || npc.velocity.Y > speed || npc.velocity.X < -speed || npc.velocity.Y < -speed)
                {
                    npc.velocity *= 0.9f;
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
                    npc.velocity.X += accelerate * 2f;
                }
                if (target.MountedCenter.X < npc.Center.X && npc.velocity.X > -speed)
                {
                    npc.velocity.X -= accelerate * 2f;
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
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float speedX = target.MountedCenter.X - (npc.Center.X + 80 * npc.direction);
                float speedY = target.MountedCenter.Y - (npc.Center.Y);
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 18;
                if (AI_Timer == 3 && Main.netMode != 1)
                {
                    attack = Main.rand.Next(2);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Dragonhit"), npc.Center);
                    npc.netUpdate = true;
                }
                if (attack == 0)
                {
                    npc.velocity *= 0;
                    if (npc.direction == 1)
                    {
                        npc.rotation = spd.ToRotation();
                    }
                    else if (npc.direction == -1)
                    {
                        npc.rotation = spd.ToRotation() + MathHelper.ToRadians(180);
                    }
                    if(AI_Timer % 5 == 0)
                    {
                        Vector2 rotates = new Vector2(80 * npc.direction, 0).RotatedBy(npc.rotation);
                        Projectile.NewProjectile(npc.Center + rotates, spd.RotateRandom(MathHelper.ToRadians(10)), mod.ProjectileType("Dragonfire"), (npc.damage / 4), 0f, 0, 0, 0);
                    }
                    if (AI_Timer > 30)
                    {
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attack == 1)
                {
                    if (AI_Timer == 5)
                    {
                        speedX = target.MountedCenter.X - npc.Center.X;
                        speedY = target.MountedCenter.Y - npc.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        npc.velocity = spd * 20f;
                        if (npc.direction == 1)
                        {
                            npc.rotation = npc.velocity.ToRotation();
                        }
                        else if (npc.direction == -1)
                        {
                            npc.rotation = npc.velocity.ToRotation() + MathHelper.ToRadians(180);
                        }
                    }
                    if (AI_Timer > 40)
                    {
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
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
                if (attack == 1)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 4 * frameHeight;
                }
            }
        }
    }
}