using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

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
            Projectile.hostile = true;
            Projectile.width = 75;
            Projectile.height = 75;
            Projectile.scale = 1f;
            Projectile.penetrate = 6;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.16f;
            Projectile.rotation += MathHelper.ToRadians(Projectile.velocity.X);
            if (Projectile.ai[0] == 1)
            {
                if (Projectile.timeLeft < 240)
                {
                    Player p = Main.player[(int)Projectile.ai[1]];
                    Vector2 sdpsdp = p.MountedCenter - Projectile.Center;
                    sdpsdp.Normalize();
                    Projectile.velocity = sdpsdp * 20f;
                    for (int o = 0; o < 36; o++)
                    {
                        int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.TerraBlade);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].velocity = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
                    }
                    Projectile.ai[0] = 0;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.X != Projectile.velocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -0.4f;
            }
            if (oldVelocity.Y != Projectile.velocity.Y)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.4f;
            }
            SoundEngine.PlaySound(SoundID.Dig);
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 75, 75, DustID.JungleGrass, 0f, 0f, 0, new Color(255, 255, 255), 1.578947f)];
                dust = Main.dust[Terraria.Dust.NewDust(position, 75, 75, DustID.Pot, 0f, 0f, 0, new Color(255, 255, 255), 1.578947f)];
            }
        }
    }
    public class Demonicgorilla : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonic Gorilla");
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 80;
            NPC.aiStyle = -1;
            NPC.damage = 150;
            NPC.scale = 1f;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.defense = 35;
            NPC.lifeMax = 7500;
            NPC.HitSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Dragonhit");
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Demon");
            NPC.value = Item.buyPrice(0, 10);
            NPC.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.damage = 200;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedPlantBoss)
            {
                return SpawnCondition.SurfaceJungle.Chance * 0.5f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Ballistalimbs>(),
                ModContent.ItemType<Items.Ballistaspring>(),
                ModContent.ItemType<Items.Heavyframe>(),
                ModContent.ItemType<Items.Lightframe>(),
                ModContent.ItemType<Items.Monkeytail>(),
                ModContent.ItemType<Items.Zenyteshard>()
                ));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Demonicgorilla").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Demonicgorilla1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Demonicgorilla1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Demonicgorilla2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Demonicgorilla2").Type, NPC.scale);
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (protection % 3 == 0 && item.CountsAsClass(DamageClass.Melee))
            {
                damage *= 0;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (protection % 3 == 0 && projectile.CountsAsClass(DamageClass.Melee))
            {
                damage *= 0;
            }
            if (protection % 3 == 1 && projectile.CountsAsClass(DamageClass.Ranged))
            {
                damage *= 0;
            }
            if (protection % 3 == 2 && projectile.CountsAsClass(DamageClass.Magic))
            {
                damage *= 0;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D draw = ModContent.Request<Texture2D>("NPCs/Protections").Value;
            Vector2 drawPos = NPC.position - Main.screenPosition + new Vector2((NPC.width / 2) - 25, -50);
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
            get { return NPC.ai[AI_State_Slot]; }
            set { NPC.ai[AI_State_Slot] = value; }
        }

        public float timer
        {
            get { return NPC.ai[AI_Timer_Slot]; }
            set { NPC.ai[AI_Timer_Slot] = value; }
        }
        public float attack
        {
            get { return NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }
        public float protection
        {
            get { return NPC.ai[3]; }
            set { NPC.ai[3] = value; }
        }
        private float speed = 3.5f;
        private float accelerate = 0.15f;
        private Vector2 spd = Vector2.Zero;

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, new Vector3(0.3f, 0.5f, 0.3f));
            if (state == Fly)
            {
                timer++;
                if (timer > 90 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.rand.NextBool(3))
                    {
                        NPC.velocity *= 0f;
                        state = Dash;
                    }
                    else
                    {
                        state = Attack;
                    }
                    timer = 0;
                    NPC.netUpdate = true;
                }
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                while (NPC.velocity.X > speed || NPC.velocity.Y > speed || NPC.velocity.X < -speed || NPC.velocity.Y < -speed)
                {
                    NPC.velocity *= 0.9f;
                }
                if (target.MountedCenter.Y> NPC.Center.Y && NPC.velocity.Y < speed)
                {
                    NPC.velocity.Y += accelerate * 0.75f;
                }
                if (target.MountedCenter.Y< NPC.Center.Y && NPC.velocity.Y > -speed)
                {
                    NPC.velocity.Y -= accelerate * 0.75f;
                }
                if (target.MountedCenter.X > NPC.Center.X && NPC.velocity.X < speed)
                {
                    NPC.velocity.X += accelerate * 1.5f;
                }
                if (target.MountedCenter.X < NPC.Center.X && NPC.velocity.X > -speed)
                {
                    NPC.velocity.X -= accelerate * 1.5f;
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
                    state = Flee;
                }
            }
            if (state == Attack)
            {
                timer++;
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float speedX = target.MountedCenter.X - NPC.Center.X;
                float speedY = target.MountedCenter.Y - NPC.Center.Y;
                spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 22;
                if (timer == 25)
                {
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Dragonhit"), NPC.Center);
                    if (attack < 3)
                    {                      
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, ModContent.ProjectileType<Gorillarock>(), NPC.damage / 4, 0f, 0, 0, 0);
                    }
                    else
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(10 * NPC.direction, -10), ModContent.ProjectileType<Gorillarock>(), NPC.damage / 4, 0f, 0, 1, NPC.target);
                        if (attack >= 5)
                        {
                            attack = 0;
                            protection++;
                            for (int o = 0; o < 36; o++)
                            {
                                int dust = Dust.NewDust(NPC.Center, 0, 0, DustID.TerraBlade);
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
                    NPC.netUpdate = true;
                }
            }
            if (state == Dash)
            {
                timer++;
                if (timer < 10)
                {
                    NPC.TargetClosest();
                    spd = Main.player[NPC.target].MountedCenter;
                    NPC.netUpdate = true;
                }
                else
                {
                    float distance = NPC.Distance(spd);
                    if (distance > 200f)
                    {
                        distance = 200f;
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TerraBlade);
                    }
                    Vector2 speede = spd - NPC.Center;
                    speede.Normalize();
                    speede *= 20f * (distance / 200f);
                    NPC.velocity = speede;
                    if (distance < 20f)
                    {
                        state = Fly;
                        NPC.netUpdate = true;
                    }
                }
            }
            if (state == Flee)
            {
                NPC.velocity.Y -= 0.1f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.08f;
            if (state == Attack)
            {
                if (timer < 10)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else if (timer < 20)
                {
                    NPC.frame.Y = 4 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 5 * frameHeight;
                }
            }
            else
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
        }
    }
}