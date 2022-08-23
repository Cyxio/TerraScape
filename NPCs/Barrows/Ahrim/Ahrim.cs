using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Ahrim
{
    public class Ahrimbag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Pink;
            Item.expert = true;
        }

        public override int BossBagNPC => ModContent.NPCType<Ahrim>();

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
            int helm = ModContent.ItemType<Items.Armor.Ahrimhelm>();
            int body = ModContent.ItemType<Items.Armor.Ahrimbody>();
            int legs = ModContent.ItemType<Items.Armor.Ahrimlegs>();
            int wep = ModContent.ItemType<Items.Magic.Ahrimstaff>();
            int drop = 0;
            for (int i = 0; i < 200; i++)
            {
                bool allcheck = false;
                if (player.HasItem(helm) && player.HasItem(body) && player.HasItem(legs) && player.HasItem(wep))
                {
                    allcheck = true;
                }
                int ch = Main.rand.Next(4);
                if (ch == 0 && (allcheck || !player.HasItem(helm)))
                {
                    drop = helm;
                    break;
                }
                if (ch == 1 && (allcheck || !player.HasItem(body)))
                {
                    drop = body;
                    break;
                }
                if (ch == 2 && (allcheck || !player.HasItem(legs)))
                {
                    drop = legs;
                    break;
                }
                if (ch == 3 && (allcheck || !player.HasItem(wep)))
                {
                    drop = wep;
                    break;
                }
            }
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), drop);
        }
    }
    [AutoloadBossHead]
    public class Ahrim : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim the Blighted");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.npcSlots = 15f;
            NPC.lavaImmune = true;
            NPC.damage = 85;
            NPC.defense = 30;
            NPC.lifeMax = 25000;
            NPC.scale = 1.5f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 35000f;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Ichor] = true;
            Music = OldSchoolRuneScape.barrowsMusic;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 30000 + 2500 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.4f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Ahrim the Blighted";
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Ahrimsummon>()));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Ahrimbag>()));

            var notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            notExpert.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Magic.Ahrimstaff>(),
                ModContent.ItemType<Items.Armor.Ahrimhelm>(),
                ModContent.ItemType<Items.Armor.Ahrimbody>(),
                ModContent.ItemType<Items.Armor.Ahrimlegs>()
                ));

            npcLoot.Add(notExpert);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.CountsAsClass(DamageClass.Melee))
            {
                damage = (int)(damage * 0.9f);
            }
            if (projectile.CountsAsClass(DamageClass.Ranged))
            {
                damage = (int)(damage * 1.1f);
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

        public int attacknum
        {
            get { return (int)NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }

        public float speed = 7f;
        public float accelerate = 0.2f;
        public int healthmod = 0;
        public Vector2 spd = Vector2.Zero;

        public override void AI()
        {
            if (Main.dayTime)
            {
                AI_State = Flee;
            }
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.UnholyWater);
            }
            Lighting.AddLight(NPC.Center, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (NPC.life < NPC.lifeMax * 0.75f)
            {
                healthmod = 1;
            }
            if (NPC.life < NPC.lifeMax * 0.5f)
            {
                healthmod = 2;
            }
            if (NPC.life < NPC.lifeMax * 0.2f)
            {
                healthmod = 3;
            }
            if (AI_State == Fly)
            {
                while (NPC.velocity.X > speed || NPC.velocity.Y > speed || NPC.velocity.X < -speed || NPC.velocity.Y < -speed)
                {
                    NPC.velocity *= 0.97f;
                }
                AI_Timer++;
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                if (target.MountedCenter.Y > NPC.Center.Y && NPC.velocity.Y < speed)
                {
                    NPC.velocity.Y += accelerate;
                }
                if (target.MountedCenter.Y < NPC.Center.Y && NPC.velocity.Y > -speed)
                {
                    NPC.velocity.Y -= accelerate;
                }
                if (target.MountedCenter.X > NPC.Center.X && NPC.velocity.X < speed)
                {
                    NPC.velocity.X += accelerate;
                }
                if (target.MountedCenter.X < NPC.Center.X && NPC.velocity.X > -speed)
                {
                    NPC.velocity.X -= accelerate;
                }
                if (AI_Timer > (115 - 25 * healthmod) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_Timer = 0;
                    int meme = Main.rand.Next(1, 3 + healthmod);
                    if (meme == 1)
                    {
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else if (meme == 2)
                    {
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else if (meme == 3)
                    {
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else if (meme == 4)
                    {
                        NPC.velocity *= 0f;
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else if (meme == 5)
                    {
                        NPC.velocity *= 0f;
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else
                    {
                        AI_State = Fly;
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
                if (attacknum == 1)
                {
                    if (AI_Timer == 3)
                    {
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 16;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f);
                        if (healthmod > 1)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd.RotatedBy(MathHelper.ToRadians(90)), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd.RotatedBy(MathHelper.ToRadians(180)), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd.RotatedBy(MathHelper.ToRadians(270)), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f);
                        }
                    }
                    if (AI_Timer > 5)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 2)
                {
                    if (AI_Timer == 3)
                    {
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 12;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 1, 0);
                    }
                    if (AI_Timer > 5)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 3)
                {
                    if (AI_Timer == 3)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 5), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 1, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, -5), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 1, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(5, 0), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 1, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(-5, 0), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 1, 0);
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 20;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 66, 0);
                    }
                    if (AI_Timer > 5)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 4)
                {
                    if (AI_Timer < 40)
                    {
                        SoundEngine.PlaySound(SoundID.Item13, NPC.Center);
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, 0, -6, 0, default(Color), 1.5f);
                    }
                    if (AI_Timer % 2 == 0 && AI_Timer > 40)
                    {
                        SoundEngine.PlaySound(SoundID.Item15, NPC.Center);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(spd.X / 20, spd.Y / 20).RotatedBy(-45), ModContent.ProjectileType<Ghostspell>(), 3, 0f, 0, 2, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(spd.X / 20, spd.Y / 20).RotatedBy(45), ModContent.ProjectileType<Ghostspell>(), 3, 0f, 0, 3, 0);
                    }
                    if (AI_Timer > 120)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 5)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, 0, -6, 0, default(Color), 1.5f);
                    NPC.TargetClosest(true);
                    Player target = Main.player[NPC.target];
                    spd = new Vector2(target.position.X - 960, target.position.Y + 560);
                    if (AI_Timer % 8 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(spd.X + Main.rand.Next(1920), spd.Y), new Vector2(0, -17), ModContent.ProjectileType<Ghostspell>(), NPC.damage / 4, 0f, 0, 4, 0);
                    }
                    if (AI_Timer > 120)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
            }
            if (AI_State == Flee)
            {
                NPC.velocity.Y += 0.05f;
            }
        }


        const int Fly1 = 0;
        const int Fly2 = 1;
        const int Fly3 = 2;
        const int Fly4 = 3;

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter < 8)
            {
                NPC.frame.Y = Fly1 * frameHeight;
            }
            else if (NPC.frameCounter < 16)
            {
                NPC.frame.Y = Fly2 * frameHeight;
            }
            else if (NPC.frameCounter < 24)
            {
                NPC.frame.Y = Fly3 * frameHeight;
            }
            else if (NPC.frameCounter < 32)
            {
                NPC.frame.Y = Fly4 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }

        public override bool PreKill()
        {
            for (int i = 0; i < 120; i++)
            {
                Vector2 rotata = new Vector2(0, 8).RotatedBy(MathHelper.ToRadians(3 * i));
                Dust.NewDust(NPC.Center + rotata, 0, 0, DustID.Enchanted_Pink, rotata.X, rotata.Y, 0, default(Color), 1.5f);
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Barrowsspirit>()))
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<Barrowsspirit>())
                    {
                        Vector2 spd = Main.npc[i].Center - NPC.Center;
                        spd.Normalize();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd * 5f, ModContent.ProjectileType<Barrowsdamageproj>(), 100, 0, Main.player[NPC.target].whoAmI);
                    }
                }
            }
            return true;
        }
    }
}
