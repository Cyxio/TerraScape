using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OldSchoolRuneScape.Common.DropRules;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Slayer
{
    public class SpectreSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oozing Slime");
            Tooltip.SetDefault("Smells like death...");
        }
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.LightRed;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<AbhorrentSpectre>()) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AbhorrentSpectre>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return null;
        }
    }

    public class SpectreStationary : ModProjectile
    {
        public override string Texture => "OldSchoolRuneScape/Projectiles/Groundproj";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Spawn");
        }
        public override void SetDefaults()
        {
            Projectile.timeLeft = 540;
            Projectile.alpha = 0;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                for (int i = 0; i < 60; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 spede = new Vector2(0, 4);
                    spede = spede.RotatedBy(MathHelper.ToRadians(i * 6));
                    spede *= Main.rand.NextFloat(1f, 2f);
                    dust = Main.dust[Terraria.Dust.NewDust(Projectile.Center, 6, 6, DustID.PoisonStaff, spede.X, spede.Y, 0, new Color(255, 255, 255), 2f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1f;
                }
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                Projectile.alpha = 255;
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 6, 6, DustID.PoisonStaff, 0f, 0f, 0, new Color(255, 255, 255), 2f)];
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = Main.rand.Next(1, 4);
            }
            if (Projectile.ai[0] == 1 && Projectile.timeLeft < 480)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && Projectile.timeLeft % 30 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 spede = new Vector2(0, 3);
                        spede = spede.RotatedBy(MathHelper.ToRadians(i * 120));
                        spede = spede.RotateRandom(MathHelper.ToRadians(120));
                        spede *= Main.rand.NextFloat(1f, 1.3f);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, spede, ModContent.ProjectileType<SpectreSpore>(), Projectile.damage, 1f);
                    }
                }
            }
            if (Projectile.ai[0] == 2 && Projectile.timeLeft < 480)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && Projectile.timeLeft % 15 == 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Vector2 spede = new Vector2(0, 4);
                        spede = spede.RotateRandom(MathHelper.ToRadians(360));
                        spede *= Main.rand.NextFloat(1f, 2f);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, spede, ModContent.ProjectileType<SpectreCloud>(), Projectile.damage, 1f);
                    }
                }
            }
            if (Projectile.ai[0] == 3 && Projectile.timeLeft < 480)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && Projectile.timeLeft % 60 == 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Vector2 spede = new Vector2(0, 3);
                        spede = spede.RotateRandom(MathHelper.ToRadians(360));
                        spede *= Main.rand.NextFloat(1f, 1.3f);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, spede, ModContent.ProjectileType<SpectreOrb>(), Projectile.damage, 1f);
                    }
                }
            }
        }
    }

    public class SpectreCloud : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Cloud");
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(228);
            Projectile.timeLeft = 360;
        }
        public override void AI()
        {
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.velocity *= 0.985f;
            if (Projectile.timeLeft < 120)
            {
                Projectile.alpha += 2;
            }
        }
    }

    public class SpectreSpore : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Spore");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(568);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 170;
            Projectile.hostile = true;
            Projectile.friendly = false;
        }
        public override void AI()
        {
            Projectile.alpha += 2;
            Projectile.rotation += Projectile.velocity.Length() * 0.01f;
            Projectile.velocity *= 0.98f;
        }
    }

    public class SpectreOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Orb");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ChlorophyteOrb);
            Projectile.timeLeft = 300;
        }
        Player target = null;
        public override void AI()
        {
            Projectile.hostile = true;
            Projectile.friendly = false;
            if (target == null)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.player[i].active && Vector2.Distance(Projectile.position, Main.player[i].position) < 750f)
                    {
                        target = Main.player[i];
                    }
                }
            }
            if (target != null)
            {
                Vector2 toTarg = target.MountedCenter - Projectile.Center;
                toTarg.Normalize();
                Projectile.velocity += toTarg * 0.1f;
            }
            if (Projectile.velocity.Length() > 6f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, DustID.PoisonStaff, 0f, 0f, 0, new Color(255, 255, 255), 2f)];
                dust.noGravity = true;
            }
            if (Projectile.velocity.Length() > 7f)
            {
                Projectile.velocity *= 0.95f;
            }
        }
    }

    [AutoloadBossHead]
    public class AbhorrentSpectre : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abhorrent Spectre");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 130;
            NPC.height = 300;
            NPC.aiStyle = -1;
            NPC.npcSlots = 10f;
            NPC.lavaImmune = true;
            NPC.damage = 40;
            NPC.defense = 20;
            NPC.lifeMax = 9000;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath23;
            NPC.value = Item.buyPrice(0, 10, 0, 0) / 2.5f;
            NPC.buffImmune[BuffID.Confused] = true;
            Music = OldSchoolRuneScape.slayerMusic;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 12000 + 2000 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Abhorrent Spectre";
            potionType = ItemID.GreaterHealingPotion;
        }

        const int Move = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float State
        {
            get { return NPC.ai[0]; }
            set { NPC.ai[0] = value; }
        }

        public float Timer
        {
            get { return NPC.ai[1]; }
            set { NPC.ai[1] = value; }
        }

        public float AttackNum
        {
            get { return NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }

        public float speed = 3f;
        public float accelerate = 0.05f;
        public bool halfHealth = false;

        public override bool CheckDead()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void AI()
        {
            if (Timer > 0)
            {
                Timer--;
            }
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            int HealthPrc = (int)(100f * (NPC.life / (float)NPC.lifeMax));
            if (NPC.ai[3] > 0f)
            {
                State = 4;
                NPC.alpha++;
                NPC.velocity *= 0.96f;
                NPC.ai[3]++;
                if (NPC.ai[3] % 20 == 0)
                {
                    SoundEngine.PlaySound(NPC.HitSound.Value);
                    if (OSRSworld.slayBossProgress < 3)
                    {
                        Item.NewItem(NPC.GetSource_FromAI(), NPC.position, new Vector2(NPC.width, NPC.height), ModContent.ItemType<Items.SlayerToken>(), 10);
                    }
                }
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = NPC.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, NPC.width, NPC.height, DustID.PoisonStaff, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
                }
                if (NPC.ai[3] > 180f)
                {
                    for (int i = 0; i < 180; i++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 spede = new Vector2(0, 8);
                        spede = spede.RotatedBy(MathHelper.ToRadians(i * 2));
                        spede *= Main.rand.NextFloat(1f, 2f);
                        dust = Main.dust[Terraria.Dust.NewDust(NPC.Center, 10, 10, DustID.PoisonStaff, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.9868421f;
                    }
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
            if (Vector2.Distance(target.Center, NPC.Center) > 1200f && HealthPrc < 100)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && Timer % 20 == 0)
                {
                    Vector2 velo = new Vector2(0, 250);
                    velo = velo.RotateRandom(Math.PI * 2);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), target.Center + velo, Vector2.Zero, ModContent.ProjectileType<SpectreOrb>(), NPC.damage / 4, 1f);
                }
            }
            if (State == Move)
            {
                if (target.dead || !target.active || !NPC.HasValidTarget || Main.dayTime)
                {
                    NPC.velocity *= 0;
                    Timer = 85;
                    State = Flee;
                    NPC.netUpdate = true;
                }
                if ((target.Top.Y > NPC.Bottom.Y) && NPC.collideY) //platforms
                {
                    NPC.position.Y++;
                }
                if (NPC.collideX)
                {
                    NPC.velocity.Y = -4f;
                }
                if (HealthPrc < 60)
                {
                    speed = 3.5f;
                    accelerate = 0.055f;
                }
                if (HealthPrc < 30)
                {
                    speed = 4f;
                    accelerate = 0.06f;
                }
                Vector2 toTarget = target.MountedCenter - NPC.Center;
                toTarget.Normalize();
                toTarget *= (speed * accelerate);
                toTarget.Y = 0;
                NPC.velocity += toTarget;
                if (NPC.velocity.Length() > speed)
                {
                    NPC.velocity *= 0.95f;
                }
                if (Timer <= 0 && HealthPrc < 100 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int i = Main.rand.Next(4);
                    if (HealthPrc <= 50 && !halfHealth)
                    {
                        halfHealth = true;
                        i = 4;
                    }
                    AttackNum = i;
                    if (i == 0)
                    {
                        //Main.NewText("orb");
                        Timer = 90;
                    }
                    else if (i == 1)
                    {
                        //Main.NewText("spore");
                        Timer = 90;
                    }
                    else if (i == 2)
                    {
                        //Main.NewText("cloud");
                        Timer = 90;
                    }
                    else if (i == 3)
                    {
                        //Main.NewText("stationary");
                        Timer = 90;
                    }
                    else if (i == 4)
                    {
                        //Main.NewText("Half Health");
                        Timer = 600;
                        NPC.dontTakeDamage = true;
                    }
                    State = Attack;
                    NPC.netUpdate = true;
                }
            }
            if (State == Attack)
            {
                int CD = 60;
                NPC.velocity *= 0.985f;
                if (AttackNum == 0)
                {
                    if (Timer % 45 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 velo = target.Center - NPC.Center;
                            velo.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velo * 5f, ModContent.ProjectileType<SpectreOrb>(), NPC.damage / 4, 1f);
                        }
                    }
                }
                if (AttackNum == 1)
                {
                    if (Timer % 20 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 velo = target.Center - NPC.Center;
                                velo.Normalize();
                                velo = velo.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30)));
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velo * 17f, ModContent.ProjectileType<SpectreSpore>(), NPC.damage / 4, 1f);
                            }
                        }
                    }
                    CD = 120;
                }
                if (AttackNum == 2)
                {
                    if (Timer % 50 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Vector2 spede = new Vector2(0, 10);
                                spede = spede.RotatedBy(MathHelper.ToRadians(i * 30));
                                spede *= Main.rand.NextFloat(1f, 1.5f);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spede, ModContent.ProjectileType<SpectreCloud>(), NPC.damage / 4, 1f);
                            }
                        }
                    }
                }
                if (AttackNum == 3 || HealthPrc < 20)
                {
                    if (Timer == 45)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 spede = new Vector2(0, 200);
                            spede = spede.RotateRandom(Math.PI * 2);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), target.MountedCenter + spede, Vector2.Zero, ModContent.ProjectileType<SpectreStationary>(), NPC.damage / 4, 1f);
                        }
                        Timer = 0;
                    }
                    CD = 90;
                }
                if (AttackNum == 4)
                {
                    if (Timer % 100 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 spede = new Vector2(0, 200);
                            spede = spede.RotateRandom(Math.PI * 2);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), target.MountedCenter + spede, Vector2.Zero, ModContent.ProjectileType<SpectreStationary>(), NPC.damage / 4, 1f);
                        }
                    }
                    else if (Timer % 100 == 25)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Vector2 spede = new Vector2(0, 10);
                                spede = spede.RotatedBy(MathHelper.ToRadians(i * 30));
                                spede *= Main.rand.NextFloat(1f, 1.5f);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spede, ModContent.ProjectileType<SpectreCloud>(), NPC.damage / 4, 1f);
                            }
                        }
                    }
                    else if (Timer % 100 == 50)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 velo = target.Center - NPC.Center;
                                velo.Normalize();
                                velo = velo.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30)));
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velo * 17f, ModContent.ProjectileType<SpectreSpore>(), NPC.damage / 4, 1f);
                            }
                        }
                    }
                    else if (Timer % 100 == 75)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 velo = target.Center - NPC.Center;
                            velo.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velo * 5f, ModContent.ProjectileType<SpectreOrb>(), NPC.damage / 4, 1f);
                        }
                    }
                    CD = 180;
                }
                if (Timer <= 0)
                {
                    State = Move;
                    NPC.dontTakeDamage = false;
                    int i = 0;
                    if (HealthPrc < 50)
                    {
                        i = (int)(CD / 10f);
                    }
                    Timer = CD - (i * 3);
                    NPC.netUpdate = true;
                }
            }
            if (State == Flee)
            {
                NPC.alpha = (85 - (int)(Timer)) * 3;
                if (Timer < 10)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frame = new Rectangle(0, 0, 150, 313);
            if (State == Attack)
            {
                NPC.frame.X = 150;
                NPC.frameCounter++;
                if (NPC.frameCounter < 10)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 20)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 30)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else
            {
                float amount = NPC.velocity.X / speed;    
                NPC.frameCounter += Math.Abs(amount);
                if (NPC.frameCounter < 10)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 20)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 30)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else if (NPC.frameCounter < 40)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var leadingCondition = new LeadingConditionRule(new SlayerBossFirstKillCondition(3));
            int itemType = ModContent.ItemType<Items.SlayerToken>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 5,
                MinimumItemDropsCount = 50,
                MaximumItemDropsCount = 50,
            };
            leadingCondition.OnSuccess(new DropOneByOne(itemType, parameters));
            npcLoot.Add(leadingCondition);
        }
        public override void OnKill()
        {
            if (OSRSworld.slayBossProgress < 3)
            {
                OSRSworld.slayBossProgress = 3;
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<SlayerMasterVannaka>())
                {
                    Main.npc[i].life = 1;
                    Main.npc[i].AddBuff(BuffID.ShadowFlame, 180);
                    Main.npc[i].netUpdate = true;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects e = SpriteEffects.None;
            if (NPC.direction == 1)
            {
                e = SpriteEffects.FlipHorizontally;
            }
            double Sin = Math.Sin(MathHelper.ToRadians(360f * ((float)Main.time % 60f) / 59f));
            float scales = (float)(1.06f + (0.03f*Sin));
            Color c = NPC.GetAlpha(Color.White);
            spriteBatch.Draw(ModContent.Request<Texture2D>("NPCs/Slayer/AbhorrentSpectreBG").Value, NPC.Center - Main.screenPosition, NPC.frame, Color.White, NPC.rotation, new Vector2(75, 156), scales, e, 0f);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - Main.screenPosition, NPC.frame, c, NPC.rotation, new Vector2(75, 156), NPC.scale, e, 0f);
            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 1200f && NPC.life < NPC.lifeMax)
            {
                Vector2 endPoint = Main.player[NPC.target].Center;
                Vector2 unit = endPoint - NPC.Center;
                float length = unit.Length();
                unit.Normalize();
                for (float k = 0; k <= length; k += 8f)
                {
                    Vector2 drawPos = NPC.Center + unit * k - Main.screenPosition;
                    Color alpha = Color.White;
                    spriteBatch.Draw(ModContent.Request<Texture2D>("NPCs/Slayer/AbhorrentSpectreBG").Value, drawPos, new Rectangle(66, 4, 4, 4), alpha, k, new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}
