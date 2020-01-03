using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Gorillarock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gorilla's Rock");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 75;
            projectile.height = 75;
            projectile.scale = 1f;
            projectile.penetrate = 6;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;
            projectile.alpha = 0;
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.16f;
            projectile.rotation += MathHelper.ToRadians(projectile.velocity.X);
            if (projectile.ai[0] == 1)
            {
                if (projectile.timeLeft < 240)
                {
                    Player p = Main.player[(int)projectile.ai[1]];
                    Vector2 sdpsdp = p.MountedCenter - projectile.Center;
                    sdpsdp.Normalize();
                    projectile.velocity = sdpsdp * 20f;
                    for (int o = 0; o < 36; o++)
                    {
                        int dust = Dust.NewDust(projectile.Center, 0, 0, 107);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].velocity = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
                    }
                    projectile.ai[0] = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.X != projectile.velocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -0.4f;
            }
            if (oldVelocity.Y != projectile.velocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.4f;
            }
            Main.PlaySound(SoundID.Dig);
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Dust dust;
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 75, 75, 39, 0f, 0f, 0, new Color(255, 255, 255), 1.578947f)];
                dust = Main.dust[Terraria.Dust.NewDust(position, 75, 75, 22, 0f, 0f, 0, new Color(255, 255, 255), 1.578947f)];
            }
        }
    }
    public class Demonicgorilla : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonic Gorilla");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 80;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.damage = 150;
            npc.scale = 1f;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.defense = 35;
            npc.lifeMax = 7500;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Dragonhit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Demon");
            npc.value = Item.buyPrice(0, 10);
            npc.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 200;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedPlantBoss)
            {
                return SpawnCondition.SurfaceJungle.Chance * 0.5f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            int ch = Main.rand.Next(6);
            if (ch == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Ballistalimbs>());
            }
            if (ch == 1)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Ballistaspring>());
            }
            if (ch == 2)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Heavyframe>());
            }
            if (ch == 3)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Lightframe>());
            }
            if (ch == 4)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Monkeytail>());
            }
            if (ch == 5)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Zenyteshard>());
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Demonicgorilla"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Demonicgorilla1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Demonicgorilla1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Demonicgorilla2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Demonicgorilla2"), npc.scale);
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (protection % 3 == 0 && item.melee)
            {
                damage *= 0;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (protection % 3 == 0 && projectile.melee)
            {
                damage *= 0;
            }
            if (protection % 3 == 1 && projectile.ranged)
            {
                damage *= 0;
            }
            if (protection % 3 == 2 && projectile.magic)
            {
                damage *= 0;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D draw = mod.GetTexture("NPCs/Protections");
            Vector2 drawPos = npc.position - Main.screenPosition + new Vector2((npc.width / 2) - 25, -50);
            spriteBatch.Draw(draw, drawPos, new Rectangle(0, (int)(50 * (protection % 3)), 50, 50), new Color(200, 200, 200, 0), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }


        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int Fly = 0;
        const int Attack = 1;
        const int Dash = 2;
        const int Flee = 3;

        public float state
        {
            get { return npc.ai[AI_State_Slot]; }
            set { npc.ai[AI_State_Slot] = value; }
        }

        public float timer
        {
            get { return npc.ai[AI_Timer_Slot]; }
            set { npc.ai[AI_Timer_Slot] = value; }
        }
        public float attack
        {
            get { return npc.ai[2]; }
            set { npc.ai[2] = value; }
        }
        public float protection
        {
            get { return npc.ai[3]; }
            set { npc.ai[3] = value; }
        }
        private float speed = 3.5f;
        private float accelerate = 0.15f;
        private Vector2 spd = Vector2.Zero;

        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Vector3(0.3f, 0.5f, 0.3f));
            if (state == Fly)
            {
                timer++;
                if (timer > 90 && Main.netMode != 1)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        npc.velocity *= 0f;
                        state = Dash;
                    }
                    else
                    {
                        state = Attack;
                    }
                    timer = 0;
                    npc.netUpdate = true;
                }
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                while (npc.velocity.X > speed || npc.velocity.Y > speed || npc.velocity.X < -speed || npc.velocity.Y < -speed)
                {
                    npc.velocity *= 0.9f;
                }
                if (target.MountedCenter.Y> npc.Center.Y && npc.velocity.Y < speed)
                {
                    npc.velocity.Y += accelerate * 0.75f;
                }
                if (target.MountedCenter.Y< npc.Center.Y && npc.velocity.Y > -speed)
                {
                    npc.velocity.Y -= accelerate * 0.75f;
                }
                if (target.MountedCenter.X > npc.Center.X && npc.velocity.X < speed)
                {
                    npc.velocity.X += accelerate * 1.5f;
                }
                if (target.MountedCenter.X < npc.Center.X && npc.velocity.X > -speed)
                {
                    npc.velocity.X -= accelerate * 1.5f;
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
                    state = Flee;
                }
            }
            if (state == Attack)
            {
                timer++;
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float speedX = target.MountedCenter.X - npc.Center.X;
                float speedY = target.MountedCenter.Y - npc.Center.Y;
                spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 22;
                if (timer == 25)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Dragonhit"), npc.Center);
                    if (attack < 3)
                    {                      
                        Projectile.NewProjectile(npc.Center, spd, ModContent.ProjectileType<Gorillarock>(), npc.damage / 4, 0f, 0, 0, 0);
                    }
                    else
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(10 * npc.direction, -10), ModContent.ProjectileType<Gorillarock>(), npc.damage / 4, 0f, 0, 1, npc.target);
                        if (attack >= 5)
                        {
                            attack = 0;
                            protection++;
                            for (int o = 0; o < 36; o++)
                            {
                                int dust = Dust.NewDust(npc.Center, 0, 0, 107);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].scale = 2.5f;
                                Main.dust[dust].velocity = new Vector2(5).RotatedBy(MathHelper.ToRadians(10 * o));
                            }
                        }
                    }
                    attack++;         
                }
                if (timer > 30)
                {
                    state = Fly;
                    npc.netUpdate = true;
                }
            }
            if (state == Dash)
            {
                timer++;
                if (timer < 10)
                {
                    npc.TargetClosest();
                    spd = Main.player[npc.target].MountedCenter;
                    npc.netUpdate = true;
                }
                else
                {
                    float distance = npc.Distance(spd);
                    if (distance > 200f)
                    {
                        distance = 200f;
                        Dust.NewDust(npc.position, npc.width, npc.height, 107);
                    }
                    Vector2 speede = spd - npc.Center;
                    speede.Normalize();
                    speede *= 20f * (distance / 200f);
                    npc.velocity = speede;
                    if (distance < 20f)
                    {
                        state = Fly;
                        npc.netUpdate = true;
                    }
                }
            }
            if (state == Flee)
            {
                npc.velocity.Y -= 0.1f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.rotation = npc.velocity.X * 0.08f;
            if (state == Attack)
            {
                if (timer < 10)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else if (timer < 20)
                {
                    npc.frame.Y = 4 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 5 * frameHeight;
                }
            }
            else
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
        }
    }
}