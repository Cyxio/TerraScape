using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Guthan
{
    public class Guthanbag : ModItem
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
        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Guthan>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Guthansummon>()));

            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Weapons.Melee.Guthanspear>(),
                ModContent.ItemType<Items.Armor.Guthanhelm>(),
                ModContent.ItemType<Items.Armor.Guthanbody>(),
                ModContent.ItemType<Items.Armor.Guthanlegs>()
                ));
        }
    }
    [AutoloadBossHead]
    public class Guthan : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan the Infested");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 45;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.npcSlots = 15f;
            NPC.lavaImmune = true;
            NPC.damage = 80;
            NPC.defense = 50;
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

        public override void OnHitPlayer(Player target, int damage, bool crit) // heal bby
        {
            NPC.HealEffect(damage * 10);
            NPC.life += damage * 10;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 35000 + 2500 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.4f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Guthan the Infested";
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Guthansummon>()));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Guthanbag>()));

            var notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            notExpert.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Weapons.Melee.Guthanspear>(),
                ModContent.ItemType<Items.Armor.Guthanhelm>(),
                ModContent.ItemType<Items.Armor.Guthanbody>(),
                ModContent.ItemType<Items.Armor.Guthanlegs>()
                ));

            npcLoot.Add(notExpert);
        }
        public override void OnKill()
        {
            OSRSworld.downedGuthan = true;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.CountsAsClass(DamageClass.Ranged))
            {
                damage = (int)(damage * 0.9f);
            }
            if (projectile.CountsAsClass(DamageClass.Magic))
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
                if (AI_Timer > (180 - 40 * healthmod) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_Timer = 0;
                    int meme = Main.rand.Next(1, 3 + healthmod);
                    if (meme == 1)
                    {
                        NPC.velocity = new Vector2(12 * NPC.direction, 8 * Main.rand.Next(-1, 2));
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
                        NPC.velocity *= 0f;
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else if (meme == 4)
                    {
                        NPC.velocity.X = NPC.direction * 20;
                        AI_State = Attack;
                        attacknum = meme;
                        NPC.netUpdate = true;
                    }
                    else if (meme == 5)
                    {
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, NPC.velocity.X * 0.01f, NPC.velocity.Y * 0.01f);
                    if (AI_Timer == 1)
                    {
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 14;
                    }
                    if (AI_Timer % 8 == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd.RotatedBy(MathHelper.ToRadians(AI_Timer)), Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f);
                        }
                    }
                    if (AI_Timer > 40)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 2)
                {
                    NPC.TargetClosest(true);
                    Player target = Main.player[NPC.target];
                    if (AI_Timer == 3)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(target.MountedCenter.X + 960, target.MountedCenter.Y), new Vector2(-20, 0), Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f, 0, 2, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(target.MountedCenter.X - 960, target.MountedCenter.Y), new Vector2(20, 0), Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f, 0, 2, 0);
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
                    if (AI_Timer == 2)
                    {
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        spd = new Vector2(target.MountedCenter.X + 960, target.MountedCenter.Y - 590);
                    }
                    if (AI_Timer % 6 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(spd.X - AI_Timer * 10, spd.Y), new Vector2(0, 16), Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f, 0, 1, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2((spd.X - 1920) + AI_Timer * 10, spd.Y), new Vector2(0, 16), Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f, 0, 1, 0);             
                    }
                    if (AI_Timer > 90)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 4)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, NPC.velocity.X * 0.01f, NPC.velocity.Y * 0.01f);
                    if (AI_Timer % 8 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f, 0, 3, 0);
                    }
                    if (AI_Timer > 40)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 5)
                {
                    if (AI_Timer == 2)
                    {
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        spd = target.MountedCenter;
                    }
                    if (AI_Timer > 2)
                    {
                        Vector2 rotat = new Vector2(0, 350);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), spd + rotat.RotatedBy(MathHelper.ToRadians(AI_Timer * 10)), -rotat.RotatedBy(MathHelper.ToRadians(AI_Timer * 10)) * 0.0001f, Mod.Find<ModProjectile>("Ghostspear").Type, (NPC.damage / 4), 0f, 0, 4, 0);
                    }
                    if (AI_Timer > 38)
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
        const int Fly5 = 4;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter < 5)
            {
                NPC.frame.Y = Fly1 * frameHeight;
            }
            else if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = Fly2 * frameHeight;
            }
            else if (NPC.frameCounter < 15)
            {
                NPC.frame.Y = Fly3 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = Fly4 * frameHeight;
            }
            else if (NPC.frameCounter < 25)
            {
                NPC.frame.Y = Fly5 * frameHeight;
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
