using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Verac
{
    public class Veracbag : ModItem
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Veracsummon>()));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Verac>()));

            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Weapons.Melee.Veracflail>(),
                ModContent.ItemType<Items.Armor.Verachelm>(),
                ModContent.ItemType<Items.Armor.Veracbody>(),
                ModContent.ItemType<Items.Armor.Veraclegs>()
                ));
        }
    }
    [AutoloadBossHead]
    public class Verac : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac the Defiled");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 112;
            NPC.aiStyle = -1;
            NPC.npcSlots = 15f;
            NPC.lavaImmune = true;
            NPC.damage = 60;
            NPC.defense = 50;
            NPC.lifeMax = 25000;
            NPC.scale = 1.5f;
            NPC.knockBackResist = 0f;
            NPC.alpha = 1;
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
            name = "Verac the Defiled";
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Veracsummon>()));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Veracbag>()));

            var notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            notExpert.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Weapons.Melee.Veracflail>(),
                ModContent.ItemType<Items.Armor.Verachelm>(),
                ModContent.ItemType<Items.Armor.Veracbody>(),
                ModContent.ItemType<Items.Armor.Veraclegs>()
                ));

            npcLoot.Add(notExpert);
        }
        public override void OnKill()
        {
            OSRSworld.downedVerac = true;
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

        public int healthmod
        {
            get { return (int)NPC.ai[3]; }
            set { NPC.ai[3] = value; }
        }

        public float speed = 7f;
        public float accelerate = 0.2f;
        public Vector2 spd = Vector2.Zero;

        public override void AI()
        {
            if (Main.dayTime)
            {
                AI_State = Flee;
            }
            if (NPC.alpha == 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("Ghostflail").Type, NPC.damage / 4, 0f, 0, 0, NPC.whoAmI);
                NPC.alpha -= 1;
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
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);
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
                        NPC.velocity.X *= 0f;
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
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 14;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Ghostball").Type, (NPC.damage / 4), 0f, 0, 0, 0);
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
                    if (AI_Timer % 5 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(-11, -11).RotatedBy(MathHelper.ToRadians(AI_Timer / 5 * 10)), Mod.Find<ModProjectile>("Ghostball").Type, (NPC.damage / 4), 0f, 0, 1, 0);
                    }
                    if (AI_Timer > 40)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 4)
                {
                    if (AI_Timer == 5)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 5), Mod.Find<ModProjectile>("Ghostball").Type, (NPC.damage / 4), 0f, 0, 2, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, -5), Mod.Find<ModProjectile>("Ghostball").Type, (NPC.damage / 4), 0f, 0, 2, 0);
                    }
                    if (AI_Timer > 6)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        NPC.netUpdate = true;
                    }
                }
                if (attacknum == 5)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, NPC.velocity.X * 0.01f, NPC.velocity.Y * 0.01f);
                    if (AI_Timer % 30 == 0)
                    {
                        NPC.TargetClosest(true);
                        Player target = Main.player[NPC.target];
                        float speedX = target.MountedCenter.X - NPC.Center.X;
                        float speedY = target.MountedCenter.Y - NPC.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        NPC.velocity = spd * 18;
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    }
                    if (AI_Timer > 118)
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

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter < 5)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.frameCounter < 15)
            {
                NPC.frame.Y = 3 * frameHeight;
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
