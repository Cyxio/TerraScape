using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Elvarg
{
    public class ElvargFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvarg's firebreath");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.damage = 0;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 150;
            Projectile.alpha = 1;
        }
        public override void AI()
        {
            if (Projectile.alpha == 1)
            {
                SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, Projectile.Center);
                Projectile.alpha -= 1;
            }
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(255 * 0.005f, 124 * 0.005f, 0));
            Projectile.velocity.Y = Projectile.oldVelocity.Y;
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare);
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }
        }
    }
    public class CrandorMap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crandor Map");
            Tooltip.SetDefault("The map shows the seaway to a volcanic island... \nSummons Elvarg");
        }
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Orange;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Elvarg").Type);
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Elvarg").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddIngredient(ItemID.Obsidian, 3);
            recipe.Register();
        }
    }
    public class ElvargBag : ModItem
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
            Item.rare = ItemRarityID.Orange;
            Item.expert = true;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("Elvarg").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            var source = player.GetSource_OpenItem(Item.type);
            Item.NewItem(source, player.Center, ModContent.ItemType<Tiles.ElvargMusicBoxItem>(), 1, false, 0);
            player.QuickSpawnItem(source, Mod.Find<ModItem>("Greendhide").Type, Main.rand.Next(24, 45));
            player.QuickSpawnItem(source, ModContent.ItemType<Items.Mysticcomponents>(), Main.rand.Next(6, 14));
            int ch = Main.rand.Next(5);
            if (ch == 0)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<Items.Accessories.Boltenchant>());
            }
            if (ch == 1)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<Items.Weapons.Melee.Excalibur>());
            }
        }
    }
    [AutoloadBossHead]
    public class Elvarg : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvarg");
            Main.npcFrameCount[NPC.type] = 12;
        }
        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.npcSlots = 10f;
            NPC.lavaImmune = true;
            NPC.damage = 40;
            NPC.defense = 0;
            NPC.lifeMax = 7000;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Dragonhit");
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/ElvargDeath");
            NPC.value = 5000f;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            Music = OldSchoolRuneScape.elvargMusic;
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int AI_AttackTimer_Slot = 2;
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

        public float AI_Attack
        {
            get { return NPC.ai[AI_AttackTimer_Slot]; }
            set { NPC.ai[AI_AttackTimer_Slot] = value; }
        }

        public float speed = 4f;
        public float accelerate = 0.1f;

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 10000 + 1000 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
            speed = 5f;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, new Vector3(0.5f, 0.5f, 0.5f));
            int healthmod = 0;
            if (NPC.life < NPC.lifeMax * 0.5f)
            {
                healthmod = 1;
                accelerate = 0.2f;
            }
            if (NPC.life < NPC.lifeMax * 0.15f)
            {
                healthmod = 2;
                speed = 6f;
                accelerate = 0.3f;
                NPC.defense = 15;
            }
            if (AI_State == Fly)
            {
                AI_Attack++;
                if (AI_Attack > 150 - (40*healthmod) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_Timer = 0;
                    AI_Attack = 0;
                    AI_State = Attack;
                    NPC.netUpdate = true;
                }
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float distance = NPC.Distance(target.MountedCenter);
                if (NPC.velocity.X > speed || NPC.velocity.Y > speed || NPC.velocity.X < -speed || NPC.velocity.Y < -speed)
                {
                    NPC.velocity *= 0.99f;
                }
                if (distance > 200 - 50*healthmod)
                {
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
                }
                if (!NPC.HasValidTarget)
                {
                    NPC.velocity *= 0;
                    AI_State = Flee;
                    NPC.netUpdate = true;
                }
            }
            if (AI_State == Attack)
            {
                AI_Timer++;
                if (AI_Timer > 20)
                {
                    AI_State = Fly;
                    NPC.netUpdate = true;
                }
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float speedX = target.MountedCenter.X - NPC.Center.X;
                float speedY = target.MountedCenter.Y - NPC.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 12 + 2*healthmod;
                if (AI_Timer == 1)
                {
                    NPC.velocity.Y *= 0;
                    if (healthmod == 2)
                    {
                        NPC.velocity *= 0;
                    }
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y - 20), spd, Mod.Find<ModProjectile>("ElvargFire").Type, (NPC.damage / 3), 0f);
                    NPC.netUpdate = true;
                }
                if (AI_Timer == 8 && healthmod > 0)
                {
                    if (Main.rand.NextBool(3)&& Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.velocity = spd * 1.1f;
                        NPC.netUpdate = true;
                    }
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y - 20), spd, Mod.Find<ModProjectile>("ElvargFire").Type, (NPC.damage / 3), 0f);
                }
                if (AI_Timer == 15 && healthmod == 2 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y - 20), spd, Mod.Find<ModProjectile>("ElvargFire").Type, (NPC.damage / 3), 0f);
                }
            }
            if (AI_State == Flee)
            {
                NPC.velocity.Y -= 0.1f;
            }
        }

        const int Fly1 = 0;
        const int Fly2 = 1;
        const int Fly3 = 2;
        const int Fly4 = 3;
        const int Fly5 = 4;
        const int Fly6 = 5;
        const int Fly7 = 6;
        const int Attack1 = 7;
        const int Attack2 = 8;
        const int Attack3 = 9;
        const int Attack4 = 10;
        const int Attack5 = 11;

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -NPC.direction;
            if (AI_State == Fly || AI_State == Flee)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 4)
                {
                    NPC.frame.Y = Fly1 * frameHeight;
                }
                else if (NPC.frameCounter < 8)
                {
                    NPC.frame.Y = Fly2 * frameHeight;
                }
                else if (NPC.frameCounter < 12)
                {
                    NPC.frame.Y = Fly3 * frameHeight;
                }
                else if (NPC.frameCounter < 16)
                {
                    NPC.frame.Y = Fly4 * frameHeight;
                }
                else if (NPC.frameCounter < 20)
                {
                    NPC.frame.Y = Fly5 * frameHeight;
                }
                else if (NPC.frameCounter < 24)
                {
                    NPC.frame.Y = Fly6 * frameHeight;
                }
                else if (NPC.frameCounter < 28)
                {
                    NPC.frame.Y = Fly7 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            if (AI_State == Attack)
            {
                if (AI_Timer < 4)
                {
                    NPC.frame.Y = Attack1 * frameHeight;
                }
                else if (AI_Timer < 8)
                {
                    NPC.frame.Y = Attack2 * frameHeight;
                }
                else if (AI_Timer < 12)
                {
                    NPC.frame.Y = Attack3 * frameHeight;
                }
                else if (AI_Timer < 16)
                {
                    NPC.frame.Y = Attack4 * frameHeight;
                }
                else if (AI_Timer < 20)
                {
                    NPC.frame.Y = Attack5 * frameHeight;
                }
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.3f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Elvarg";
            potionType = ItemID.HealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ElvargBag>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Greendhide>(), 1, 15, 30));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Mysticcomponents>(), 1, 4, 12));
            npcLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Accessories.Boltenchant>(),
                ModContent.ItemType<Items.Weapons.Melee.Excalibur>()
                ));
        }
        public override void OnKill()
        {
            OSRSworld.downedElvarg = true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Elvarg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Elvarg2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Elvarg3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Elvarg1").Type, 1f);
            }
        }
    }
}