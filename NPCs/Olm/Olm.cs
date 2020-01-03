using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 140;
            npc.height = 300;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.npcSlots = 40f;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.lifeMax = 150000;
            npc.damage = 300;
            npc.defense = 40;
            npc.friendly = false;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Olmdeath");
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.BetsysCurse] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.lavaImmune = true;
            music = OldSchoolRuneScape.olmMusic;
            bossBag = mod.ItemType("OlmBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 200000 + 25000 * numPlayers;
            npc.damage = 300;
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
        public override void NPCLoot()
        {
            OSRSworld.downedOlm = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int ch = Main.rand.Next(4);
                if (ch == 0)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Kodaiinsignia>());
                }
                if (ch == 1)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Dinhbulwark>());
                }
                if (ch == 2)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Twistedbow>());
                }
                if (ch == 3)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Dclaws>());
                }
                if (Main.rand.Next(4) == 0)
                {
                    int supply = Main.rand.Next(3);
                    if (supply == 0)
                    {
                        Item.NewItem(npc.Hitbox, mod.ItemType("Dragonstone"), Main.rand.Next(1, 5));
                    }
                    if (supply == 1)
                    {
                        Item.NewItem(npc.Hitbox, mod.ItemType("Onyx"), Main.rand.Next(1, 4));
                    }
                    if (supply == 2)
                    {
                        Item.NewItem(npc.Hitbox, mod.ItemType("Zenyte"), Main.rand.Next(1, 3));
                    }
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm1"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm3"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm4"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm5"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Olm5"), 1f);
            }
        }
        public float state
        {
            get { return npc.ai[0]; }
            set { npc.ai[0] = value; }
        }
        public float timer
        {
            get { return npc.ai[1]; }
            set { npc.ai[1] = value; }
        }
        public float attack
        {
            get { return npc.ai[2]; }
            set { npc.ai[2] = value; }
        }
        public float healthMode
        {
            get { return npc.ai[3]; }
            set { npc.ai[3] = value; }
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
            Vector2 Mouth = npc.Center + new Vector2(35 * npc.direction, -80).RotatedBy(npc.rotation);
            float oldHealth = healthMode;
            if (npc.life < npc.lifeMax * 0.8f)
            {
                healthMode = 1;
            }
            if (npc.life < npc.lifeMax * 0.6f)
            {
                healthMode = 2;
            }
            if (npc.life < npc.lifeMax * 0.4f)
            {
                healthMode = 3;
                MAXSPEED = 13f;
            }
            if (npc.life < npc.lifeMax * 0.3f)
            {
                healthMode = 4;
            }
            if (npc.life < npc.lifeMax * 0.2f)
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
                npc.netUpdate = true;
            }
            if (state == flee)
            {
                npc.velocity.X = 0;
                npc.velocity.Y = -6f;
            }
            if (state == fly)
            {
                timer++;
                npc.TargetClosest();
                if (!npc.HasValidTarget)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = -6f;
                    state = flee;
                    npc.netUpdate = true;
                }
                npc.dontTakeDamage = false;
                Player target = Main.player[npc.target];
                accel = target.MountedCenter - npc.Center;
                accel.Normalize();
                npc.velocity += accel * 0.25f;
                if (npc.velocity.Length() > MAXSPEED || (npc.direction == 1 && npc.velocity.X < 0) || (npc.direction == -1 && npc.velocity.X > 0))
                {
                    npc.velocity *= 0.98f;
                }
                if (timer > 90 && Main.netMode != 1)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        state = dash;                       
                    }
                    else
                    {
                        state = attk;
                        attack = Main.rand.Next(4 + (int)healthMode);
                    }
                    timer = 0;
                    npc.netUpdate = true;
                }
            }
            if (state == dash)
            {
                timer++;
                if (timer < 30)
                {
                    spd = Main.player[npc.target].MountedCenter;
                }
                float distance = npc.Distance(spd);
                if (distance > 50f)
                {
                    distance = 50f;
                    Dust.NewDust(npc.Center - new Vector2(25, 25), 50, 50, 107);
                }
                Vector2 speede = spd - npc.Center;
                speede.Normalize();
                speede *= MAXSPEED * 1.4f * (distance / 50f);
                npc.velocity = speede;
                if (distance < 10f)
                {
                    state = fly;
                    timer = 0;
                    npc.netUpdate = true;
                }
            }
            if (state == attk)
            {
                npc.TargetClosest();
                if (!npc.HasValidTarget)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y = -6f;
                    state = flee;
                    npc.netUpdate = true;
                }
                Lighting.AddLight(Mouth, new Vector3(0, 1, 0));
                timer++;
                if (attack == 0) // Basic
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        spd.Normalize();
                        if (true)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                Projectile.NewProjectile(Mouth, spd * 11, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                                Projectile.NewProjectile(Mouth, (spd * 11).RotatedBy(MathHelper.ToRadians(-25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                                Projectile.NewProjectile(Mouth, (spd * 11).RotatedBy(MathHelper.ToRadians(25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                            }
                            else
                            {
                                Projectile.NewProjectile(Mouth, spd * 30f, ModContent.ProjectileType<BasicOrb>(), 300 / 4, 0f, 0, healthMode);
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
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 1) // Spheres
                {
                    if (timer == 21)
                    {
                        spd = Main.player[npc.target].MountedCenter;
                        if (true)
                        {
                            Projectile.NewProjectile(spd + new Vector2(250), Vector2.Zero, ModContent.ProjectileType<RGBsphere>(), 200 / 4, 0f, 0, 0, 0);
                            Projectile.NewProjectile(spd + new Vector2(250).RotatedBy(MathHelper.ToRadians(120)), Vector2.Zero, ModContent.ProjectileType<RGBsphere>(), 200 / 4, 0f, 0, 1, 0);
                            Projectile.NewProjectile(spd + new Vector2(250).RotatedBy(MathHelper.ToRadians(240)), Vector2.Zero, ModContent.ProjectileType<RGBsphere>(), 200 / 4, 0f, 0, 2, 0);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 2) // Shattering Crystal
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        if (Main.netMode != 1)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(Mouth, spd * 30f, ModContent.ProjectileType<CrystalShatter>(), 300 / 4, 0f);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 3) // Firebreath
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        if (true)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(Mouth, spd * 20f, ModContent.ProjectileType<RGBfire>(), 200 / 4, 0f, 0, Main.rand.Next(3), 0);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 45;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 4) // Lightning
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter - Mouth;
                    if (timer == 21)
                    {
                        if (Main.netMode != 1)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(Mouth, (spd * 8f).RotatedBy(Math.PI / 6), ModContent.ProjectileType<OlmLightning>(), 300 / 4, 0f);
                        }
                    }
                    if (timer > 28)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 5) // Channel
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter;
                    if (timer % 20 == 0)
                    {
                        if (true)
                        {
                            Projectile.NewProjectile(spd, Vector2.Zero, ModContent.ProjectileType<OlmChannel>(), 1, 0f, 0, 0, 0);
                        }
                    }
                    if (timer > 60)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 6) // Rocks
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter + new Vector2(Main.rand.Next(-400, 400), -700);
                    if (timer % 20 == 0)
                    {
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(spd, new Vector2(0, 700 / 60) * Main.rand.NextFloat(0.9f, 1.5f), ModContent.ProjectileType<OlmRock>(), 300 / 4, 0f);
                        }
                    }
                    if (timer > 100)
                    {
                        timer = 45;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 7) // Firewall
                {
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter;
                    if (timer == 10)
                    {
                        if (true)
                        {
                            Projectile.NewProjectile(new Vector2(spd.X + 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, npc.target);
                            Projectile.NewProjectile(new Vector2(spd.X - 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, npc.target);
                        }     
                    }
                    if (timer % 45 == 0 && Main.netMode != 1)
                    {
                        spd = Main.player[npc.target].MountedCenter - Mouth;
                        spd.Normalize();
                        Projectile.NewProjectile(Mouth, spd * 13, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                    }
                    if (timer > 500)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                else if (attack == 8) // Burn with me
                {
                    npc.velocity *= 0f;
                    if (timer == 5)
                    {
                        Main.NewText("[c/95AD5F:Burn with me!]");
                    }
                    for (int i = 0; i < 60; i++)
                    {
                        int dust = Dust.NewDust(npc.Center + new Vector2(400).RotatedBy(MathHelper.ToRadians(6 * i + timer)), 0, 0, 75);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].velocity *= 0f;
                    }
                    if (timer == 150)
                    {
                        for (int i = 0; i < Main.ActivePlayersCount; i++)
                        {
                            Player p = Main.player[i];
                            if (p.Distance(npc.Center) > 580f)
                            {
                                p.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(npc.whoAmI), 300, 0);
                            }
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            int dust = Dust.NewDust(npc.Center + new Vector2(400).RotatedBy(MathHelper.ToRadians(6 * i + timer)), 0, 0, 75);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].velocity *= new Vector2(7).RotatedBy(MathHelper.ToRadians(6 * i + timer));
                        }
                    }
                    if (timer > 153)
                    {
                        timer = 0;
                        attack = 6;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    timer = 0;
                    state = fly;
                    npc.netUpdate = true;
                }
            }
            if (state == stationary)
            {
                timer++;
                int dust = Dust.NewDust(npc.Center + new Vector2(Main.rand.Next(25, 200)).RotateRandom(Math.PI), 0, 0, 107);
                Main.dust[dust].scale = 0.1f;
                Main.dust[dust].fadeIn = 1.5f;
                Main.dust[dust].velocity *= 0f;
                npc.velocity *= 0f;
                if (attack == 50)
                {
                    spd = Main.player[npc.target].MountedCenter - Mouth;
                    if (timer % 10 == 0)
                    {
                        if (Main.netMode != 1)
                        {
                            spd.Normalize();
                            Projectile.NewProjectile(Mouth, (spd * 12f).RotatedBy(Math.PI / 6).RotateRandom(MathHelper.ToRadians(20)), ModContent.ProjectileType<OlmLightning>(), 200 / 4, 0f);
                        }
                    }
                    if (timer > 90)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                if (attack == 51)
                {
                    npc.dontTakeDamage = true;
                    spd = Main.player[npc.target].MountedCenter;
                    if (timer % 75 == 0)
                    {
                        Projectile.NewProjectile(spd, Vector2.Zero, ModContent.ProjectileType<OlmChannel>(), 1, 0f, 0, 0, 0);
                        if (Main.netMode != 1)
                        {                          
                            if (timer < 100)
                            {
                                Vector2 ps = npc.position + (new Vector2(npc.width, npc.height) * Main.rand.NextFloat(1f));
                                NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Greendragon>());
                                NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Reddragon>());
                                NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Bluedragon>());
                                NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Blackdragon>());
                            }
                        }                     
                    }
                    if (timer > 675 && Main.netMode != 1)
                    {
                        npc.dontTakeDamage = false;
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                if (attack == 52)
                {
                    npc.dontTakeDamage = true;
                    npc.velocity *= 0f;
                    spd = Main.player[npc.target].MountedCenter + new Vector2(Main.rand.Next(-400, 400), -700);
                    if (timer % 50 == 0)
                    {
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(spd, new Vector2(0, 700 / 60) * Main.rand.NextFloat(0.9f, 1.5f), ModContent.ProjectileType<OlmRock>(), 300 / 4, 0f);
                            if (timer < 60)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Vector2 ps = npc.position + (new Vector2(npc.width, npc.height) * Main.rand.NextFloat(1f));
                                    NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Lavadragon>());
                                    NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Blackdragon>());
                                }
                            }
                        }
                    }
                    if (timer > 1000 && Main.netMode != 1)
                    {
                        npc.dontTakeDamage = false;
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                if (attack == 53)
                {
                    npc.velocity *= 0f;
                    if (timer == 10)
                    {
                        spd = Main.player[npc.target].MountedCenter;
                        Projectile.NewProjectile(new Vector2(spd.X + 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, npc.target);
                        Projectile.NewProjectile(new Vector2(spd.X - 360, spd.Y), Vector2.Zero, ModContent.ProjectileType<OlmFire>(), 350 / 4, 0f, 0, 77, npc.target);
                    }
                    if (timer % 45 == 0 && Main.netMode != 1)
                    {
                        spd = Main.player[npc.target].MountedCenter - Mouth;
                        spd.Normalize();
                        Projectile.NewProjectile(Mouth, spd * 13, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                        Projectile.NewProjectile(Mouth, (spd * 13).RotatedBy(MathHelper.ToRadians(-25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                        Projectile.NewProjectile(Mouth, (spd * 13).RotatedBy(MathHelper.ToRadians(25)), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                    }
                    if (timer > 900 && Main.netMode != 1)
                    {
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
                if (attack == 54)
                {
                    npc.dontTakeDamage = true;
                    npc.velocity *= 0f;
                    if (timer == 10 && Main.netMode != 1)
                    {
                        Vector2 ps = npc.position + (new Vector2(npc.width, npc.height) * Main.rand.NextFloat(1f));
                        NPC.NewNPC((int)ps.X, (int)ps.Y, ModContent.NPCType<Demonicgorilla>());
                    }
                    if (timer % 100 == 0 && Main.netMode != 1)
                    {
                        spd = Main.player[npc.target].MountedCenter - Mouth;
                        spd.Normalize();
                        Projectile.NewProjectile(Mouth, spd * 13, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
                    }
                    if (timer > 900 && Main.netMode != 1)
                    {
                        npc.dontTakeDamage = false;
                        timer = 0;
                        state = fly;
                        npc.netUpdate = true;
                    }
                }
            }
        }
        
        public override void FindFrame(int frameHeight)
        {
            npc.rotation = npc.velocity.X * 0.05f;                     
            if (state == fly || state == flee)
            {
                npc.frame = new Rectangle(0, 0, 514, 336);
                int frameSpeed = 6;
                npc.frameCounter++;
                if (npc.frameCounter < frameSpeed)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter < frameSpeed * 2)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.frameCounter < frameSpeed * 3)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else if (npc.frameCounter < frameSpeed * 4)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            if (state == dash)
            {
                npc.frame = new Rectangle(0, 0, 514, 336);
                npc.frame.Y = frameHeight * 3;
                npc.rotation = npc.velocity.ToRotation() + (float)(Math.PI / 2);
            }
            if (state == attk || state == stationary)
            {
                npc.frame = new Rectangle(514, 0, 514, 336);
                if (timer == 1)
                {
                    npc.frameCounter = 1;
                }
                int frameSpeed = 7;
                npc.frameCounter++;
                if (npc.frameCounter < frameSpeed)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter < frameSpeed * 2)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.frameCounter < frameSpeed * 3)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else if (npc.frameCounter < frameSpeed * 4)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects e = SpriteEffects.None;
            if (npc.direction == -1)
            {
                e = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, new Vector2(257, 168), npc.scale, e, 0f);
            return false;
        }
    }
}
