using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm //texture 257x168
{
    [AutoloadBossHead]
    public class Olm : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Great Olm");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 300;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.npcSlots = 40f;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.lifeMax = 150000;
            NPC.damage = 300;
            NPC.defense = 40;
            NPC.friendly = false;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Olmdeath");
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Ichor] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.lavaImmune = true;
            Music = OldSchoolRuneScape.olmMusic;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 200000 + 25000 * numPlayers;
            NPC.damage = 300;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.7f;
            return true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Great Olm";
            potionType = ItemID.SuperHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<OlmBag>()));

            var notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            notExpert.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Magic.Kodaiinsignia>(),
                ModContent.ItemType<Items.Weapons.Melee.Dinhbulwark>(),
                ModContent.ItemType<Items.Weapons.Ranged.Twistedbow>(),
                ModContent.ItemType<Items.Weapons.Melee.Dclaws>()
                ));
            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Dragonstone>(), 3, 1, 4)
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Onyx>(), 2, 1, 3)
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Zenyte>(), 1, 1, 2)
                )));
            npcLoot.Add(notExpert);
        }
        public override void OnKill()
        {
            OSRSworld.downedOlm = true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm5").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Olm5").Type, 1f);
            }
        }
        public float state
        {
            get { return NPC.ai[0]; }
            set { NPC.ai[0] = value; }
        }
        public float timer
        {
            get { return NPC.ai[1]; }
            set { NPC.ai[1] = value; }
        }
        public float attack
        {
            get { return NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }
        public float healthMode
        {
            get { return NPC.ai[3]; }
            set { NPC.ai[3] = value; }
        }
        const int fly = 0;
        const int attk = 1;
        const int dash = 2;
        const int stationary = 3;
        const int flee = 4;
        private Vector2 accel = Vector2.Zero;
        private Vector2 spd = Vector2.Zero;
        private float MAXSPEED = 12f;

        public override void AI()
        {
            Vector2 Mouth = NPC.Center + new Vector2(35 * NPC.direction, -80).RotatedBy(NPC.rotation);
            float oldHealth = healthMode;
            if (NPC.life < NPC.lifeMax * 0.8f)
            {
                healthMode = 1;
            }
            if (NPC.life < NPC.lifeMax * 0.6f)
            {
                healthMode = 2;
            }
            if (NPC.life < NPC.lifeMax * 0.4f)
            {
                healthMode = 3;
                MAXSPEED = 13f;
            }
            if (NPC.life < NPC.lifeMax * 0.3f)
            {
                healthMode = 4;
            }
            if (NPC.life < NPC.lifeMax * 0.2f)
            {
                healthMode = 5;
                MAXSPEED = 14f;
            }
            if (oldHealth != healthMode)
            {
                timer = 0;
                if (healthMode == 1)
                {
                    attack = 50;
                }
                if (healthMode == 2)
                {
                    attack = 51;
                }
                if (healthMode == 3)
                {
                    attack = 52;                   
                }
                if (healthMode == 4)
                {
                    attack = 53;
                }
                if (healthMode == 5)
                {
                    attack = 54;
                }
                state = stationary;
                NPC.netUpdate = true;
            }
            if (state == flee)
            {
                NPC.velocity.X = 0;
                NPC.velocity.Y = -6f;
            }
            if (state == fly)
            {
                timer++;
                NPC.TargetClosest();
                if (!NPC.HasValidTarget)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = -6f;
                    state = flee;
                    NPC.netUpdate = true;
                }
                NPC.dontTakeDamage = false;
                Player target = Main.player[NPC.target];
                accel = target.MountedCenter - NPC.Center;
                accel.Normalize();
                NPC.velocity += accel * 0.25f;
                if (NPC.velocity.Length() > MAXSPEED || (NPC.direction == 1 && NPC.velocity.X < 0) || (NPC.direction == -1 && NPC.velocity.X > 0))
                {
                    NPC.velocity *= 0.98f;
                }
                if (timer > 90 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.rand.NextBool(5))
                    {
                        state = dash;                       
                    }
                    else
                    {
                        state = attk;
                        attack = Main.rand.Next(4 + (int)healthMode);
                    }
                    timer = 0;
                    NPC.netUpdate = true;
                }
            }
            if (state == dash)
            {
                timer++;
                if (timer < 30)
                {
                    spd = Main.player[NPC.target].MountedCenter;
                }
                float distance = NPC.Distance(spd);
                if (distance > 50f)
                {
                    distance = 50f;
                    Dust.NewDust(NPC.Center - new Vector2(25, 25), 50, 50, DustID.TerraBlade);
                }
                Vector2 speede = spd - NPC.Center;
                speede.Normalize();
                speede *= MAXSPEED * 1.4f * (distance / 50f);
                NPC.velocity = speede;
                if (distance < 10f)
                {
                    state = fly;
                    timer = 0;
                    NPC.netUpdate = true;
                }
            }
            if (state == attk)
            {
                NPC.TargetClosest();
                if (!NPC.HasValidTarget)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = -6f;
                    state = flee;
                    NPC.netUpdate = true;
                }
                Lighting.AddLight(Mouth, new Vector3(0, 1, 0));
                timer++;
                if (attack == 0) // Basic
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        spd.Normalize();
                        if (true)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 11, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, (spd * 11).RotatedBy(MathHelper.ToRadians(-25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, (spd * 11).RotatedBy(MathHelper.ToRadians(25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                            }
                            else
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 30f, ModContent.ProjectileType<BasicOrb>(), 300 / 4, 0f, 0, healthMode);
                            }
                        }                     
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        if (healthMode > 2)
                        {
                            timer = 45;
                        }
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 1) // Spheres
                {
                    if (timer == 21)
                    {
                        spd = Main.player[NPC.target].MountedCenter;
                        if (true)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spd + new Vector2(250), Vector2.Zero, ModContent.ProjectileType<RGBsphere>(), 200 / 4, 0f, 0, 0, 0);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spd + new Vector2(250).RotatedBy(MathHelper.ToRadians(120)), Vector2.Zero, ModContent.ProjectileType<RGBsphere>(), 200 / 4, 0f, 0, 1, 0);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spd + new Vector2(250).RotatedBy(MathHelper.ToRadians(240)), Vector2.Zero, ModContent.ProjectileType<RGBsphere>(), 200 / 4, 0f, 0, 2, 0);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 2) // Shattering Crystal
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 30f, ModContent.ProjectileType<CrystalShatter>(), 300 / 4, 0f);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 3) // Firebreath
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        if (true)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 20f, ModContent.ProjectileType<RGBfire>(), 200 / 4, 0f, 0, Main.rand.Next(3), 0);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 45;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 4) // Lightning
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, (spd * 8f).RotatedBy(Math.PI / 6), ModContent.ProjectileType<OlmLightning>(), 300 / 4, 0f);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 5) // Channel
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter;
                    if (timer % 20 == 0)
                    {
                        if (true)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spd, Vector2.Zero, ModContent.ProjectileType<OlmChannel>(), 1, 0f, 0, 0, 0);
                        }
                    }
                    if (timer > 60)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 6) // Rocks
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter + new Vector2(Main.rand.Next(-400, 400), -700);
                    if (timer % 20 == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spd, new Vector2(0, 700 / 60) * Main.rand.NextFloat(0.9f, 1.5f), ModContent.ProjectileType<OlmRock>(), 300 / 4, 0f);
                        }
                    }
                    if (timer > 100)
                    {
                        timer = 45;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 7) // Firewall
                {
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter;
                    if (timer == 10)
                    {
                        if (true)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(spd.X + 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, NPC.target);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(spd.X - 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, NPC.target);
                        }     
                    }
                    if (timer % 45 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        spd = Main.player[NPC.target].MountedCenter - Mouth;
                        spd.Normalize();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 13, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                    }
                    if (timer > 500)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                else if (attack == 8) // Burn with me
                {
                    NPC.velocity *= 0f;
                    if (timer == 5)
                    {
                        Main.NewText("[c/95AD5F:Burn with me!]");
                    }
                    for (int i = 0; i < 60; i++)
                    {
                        int dust = Dust.NewDust(NPC.Center + new Vector2(400).RotatedBy(MathHelper.ToRadians(6 * i + timer)), 0, 0, DustID.CursedTorch);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].velocity *= 0f;
                    }
                    if (timer == 150)
                    {
                        for (int i = 0; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
                        {
                            Player p = Main.player[i];
                            if (p.Distance(NPC.Center) > 580f)
                            {
                                p.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(NPC.whoAmI), 300, 0);
                            }
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            int dust = Dust.NewDust(NPC.Center + new Vector2(400).RotatedBy(MathHelper.ToRadians(6 * i + timer)), 0, 0, DustID.CursedTorch);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].velocity *= new Vector2(7).RotatedBy(MathHelper.ToRadians(6 * i + timer));
                        }
                    }
                    if (timer > 153)
                    {
                        timer = 0;
                        attack = 6;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    timer = 0;
                    state = fly;
                    NPC.netUpdate = true;
                }
            }
            if (state == stationary)
            {
                timer++;
                int dust = Dust.NewDust(NPC.Center + new Vector2(Main.rand.Next(25, 200)).RotateRandom(Math.PI), 0, 0, DustID.TerraBlade);
                Main.dust[dust].scale = 0.1f;
                Main.dust[dust].fadeIn = 1.5f;
                Main.dust[dust].velocity *= 0f;
                NPC.velocity *= 0f;
                if (attack == 50)
                {
                    spd = Main.player[NPC.target].MountedCenter - Mouth;
                    if (timer % 10 == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, (spd * 12f).RotatedBy(Math.PI / 6).RotateRandom(MathHelper.ToRadians(20)), ModContent.ProjectileType<OlmLightning>(), 200 / 4, 0f);
                        }
                    }
                    if (timer > 90)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attack == 51)
                {
                    NPC.dontTakeDamage = true;
                    spd = Main.player[NPC.target].MountedCenter;
                    if (timer % 75 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), spd, Vector2.Zero, ModContent.ProjectileType<OlmChannel>(), 1, 0f, 0, 0, 0);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {                          
                            if (timer < 100)
                            {
                                Vector2 ps = NPC.position + (new Vector2(NPC.width, NPC.height) * Main.rand.NextFloat(1f));
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Greendragon>());
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Reddragon>());
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Bluedragon>());
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Blackdragon>());
                            }
                        }                     
                    }
                    if (timer > 675 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.dontTakeDamage = false;
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attack == 52)
                {
                    NPC.dontTakeDamage = true;
                    NPC.velocity *= 0f;
                    spd = Main.player[NPC.target].MountedCenter + new Vector2(Main.rand.Next(-400, 400), -700);
                    if (timer % 50 == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spd, new Vector2(0, 700 / 60) * Main.rand.NextFloat(0.9f, 1.5f), ModContent.ProjectileType<OlmRock>(), 300 / 4, 0f);
                            if (timer < 60)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Vector2 ps = NPC.position + (new Vector2(NPC.width, NPC.height) * Main.rand.NextFloat(1f));
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Lavadragon>());
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Blackdragon>());
                                }
                            }
                        }
                    }
                    if (timer > 1000 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.dontTakeDamage = false;
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attack == 53)
                {
                    NPC.velocity *= 0f;
                    if (timer == 10)
                    {
                        spd = Main.player[NPC.target].MountedCenter;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(spd.X + 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, NPC.target);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(spd.X - 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, NPC.target);
                    }
                    if (timer % 45 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        spd = Main.player[NPC.target].MountedCenter - Mouth;
                        spd.Normalize();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 13, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, (spd * 13).RotatedBy(MathHelper.ToRadians(-25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, (spd * 13).RotatedBy(MathHelper.ToRadians(25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                    }
                    if (timer > 900 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attack == 54)
                {
                    NPC.dontTakeDamage = true;
                    NPC.velocity *= 0f;
                    if (timer == 10 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 ps = NPC.position + (new Vector2(NPC.width, NPC.height) * Main.rand.NextFloat(1f));
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)ps.X, (int)ps.Y, ModContent.NPCType<Demonicgorilla>());
                    }
                    if (timer % 100 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        spd = Main.player[NPC.target].MountedCenter - Mouth;
                        spd.Normalize();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), Mouth, spd * 13, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                    }
                    if (timer > 900 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.dontTakeDamage = false;
                        timer = 0;
                        state = fly;
                        NPC.netUpdate = true;
                    }
                }
            }
        }
        
        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = NPC.velocity.X * 0.05f;                     
            if (state == fly || state == flee)
            {
                NPC.frame = new Rectangle(0, 0, 514, 336);
                int frameSpeed = 6;
                NPC.frameCounter++;
                if (NPC.frameCounter < frameSpeed)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < frameSpeed * 2)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.frameCounter < frameSpeed * 3)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
                else if (NPC.frameCounter < frameSpeed * 4)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            if (state == dash)
            {
                NPC.frame = new Rectangle(0, 0, 514, 336);
                NPC.frame.Y = frameHeight * 3;
                NPC.rotation = NPC.velocity.ToRotation() + (float)(Math.PI / 2);
            }
            if (state == attk || state == stationary)
            {
                NPC.frame = new Rectangle(514, 0, 514, 336);
                if (timer == 1)
                {
                    NPC.frameCounter = 1;
                }
                int frameSpeed = 7;
                NPC.frameCounter++;
                if (NPC.frameCounter < frameSpeed)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < frameSpeed * 2)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.frameCounter < frameSpeed * 3)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
                else if (NPC.frameCounter < frameSpeed * 4)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects e = SpriteEffects.None;
            if (NPC.direction == -1)
            {
                e = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, new Vector2(257, 168), NPC.scale, e, 0f);
            return false;
        }
    }
}
