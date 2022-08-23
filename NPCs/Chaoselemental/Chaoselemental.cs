using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Chaoselemental
{
    public class Chaossummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Sigil");
            Tooltip.SetDefault("The four elements bound together by chaos \nSummons the Chaos Elemental");
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
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Chaoselemental").Type);
        }
        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Chaoselemental").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return null;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<Items.Magic.Airrune>());
            r.AddIngredient(ModContent.ItemType<Items.Magic.Waterrune>());
            r.AddIngredient(ModContent.ItemType<Items.Magic.Firerune>());
            r.AddIngredient(ModContent.ItemType<Items.Magic.Earthrune>());
            r.AddIngredient(ModContent.ItemType<Items.Magic.Chaosrune>());
            r.AddIngredient(ItemID.SoulofLight);
            r.AddIngredient(ItemID.SoulofNight);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
    public class ChaosBag : ModItem
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
            Item.rare = ItemRarityID.LightRed;
            Item.expert = true;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("Chaoselemental").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            var entitySource = player.GetSource_OpenItem(Item.type);
            Item.NewItem(entitySource, player.Center, ModContent.ItemType<Tiles.ChaosMusicBoxItem>(), 1, false, 0);
            int ch = Main.rand.Next(7);
            if(ch == 0)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Weapons.Melee.Dragon2h>());
            }
            if (ch == 1)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Weapons.Melee.Dds>());
            }
            if (ch == 2)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Magic.Ibansstaff>());
            }
            if (ch == 3)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Weapons.Ranged.Magicshortbow>());
            }
            if (ch == 4)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Weapons.Ranged.Dragonhuntercrossbow>());
            }
            if (ch == 5)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Weapons.Melee.Dragonspear>());
            }
            if (ch == 6)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Weapons.Melee.Dragonscimitar>());
            }
            if (Main.rand.NextBool(3))
            {
                int supply = Main.rand.Next(3);
                if (supply == 0)
                {
                    player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Dragonstone>(), Main.rand.Next(1, 5));
                }
                if (supply == 1)
                {
                    player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Magic.Chaosrune>(), Main.rand.Next(100, 250));
                }
                if (supply == 2)
                {
                    player.QuickSpawnItem(entitySource, ModContent.ItemType<Items.Ammo.Dragonstonebolt>(), Main.rand.Next(100, 250));
                }
            }
        }
    }
    [AutoloadBossHead]
    public class Chaoselemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Chaos Elemental");
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.width = 122;
            NPC.height = 80;
            NPC.aiStyle = -1;
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            NPC.damage = 60;
            NPC.defense = 35;
            NPC.lifeMax = 16000;
            NPC.scale = 1.5f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 10000f;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Ichor] = true;
            Music = OldSchoolRuneScape.chaoseleMusic;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 20000 + 1000 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
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

        private float speed = 4f;
        private float accelerate = 0.05f;
        private int healthmod = 0;

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.UnholyWater);
            }
            AI_Timer++;
            Lighting.AddLight(NPC.Center, new Vector3(148 * 0.01f, 56 * 0.01f, 255 * 0.01f));
            if (NPC.life < NPC.lifeMax * 0.6f)
            {
                healthmod = 1;
                accelerate = 0.1f;
            }
            if (NPC.life < NPC.lifeMax * 0.3f)
            {
                healthmod = 2;
                accelerate = 0.2f;
            }
            if(AI_State == Fly)
            {
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float distance = NPC.Distance(target.MountedCenter);
                if (NPC.velocity.X > speed || NPC.velocity.Y > speed)
                {
                    NPC.velocity *= 0.95f;
                }
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
                if (AI_Timer > (75 - 10 * healthmod) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AI_Timer = 0;
                    AI_State = Attack;
                    NPC.netUpdate = true;
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
                NPC.TargetClosest(true);
                Player target = Main.player[NPC.target];
                float speedX = target.MountedCenter.X - NPC.Center.X;
                float speedY = target.MountedCenter.Y - NPC.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 8;
                if (AI_Timer == 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f);
                    if (Main.rand.NextBool(3) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Chaosdebu").Type, 1, 0f);
                        if (Main.rand.NextBool(2) && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Chaostele").Type, 1, 0f);
                        }

                    }
                }
                if (Main.rand.NextBool(90 - 20 * healthmod) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 rotat = spd.RotatedBy(MathHelper.ToRadians(-30));
                    for (int i = 0; i < 8; i++)
                    {
                        if (true)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, rotat.RotatedBy(MathHelper.ToRadians(10 * i)), Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f);
                        }
                    }
                }
                if (healthmod > 0)
                {
                    if (AI_Timer == 29)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(spd.X, spd.Y - 7f), Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f, 0, 1, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(spd.X, spd.Y + 7f), Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f, 0, 2, 0);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f);
                    }
                    if (Main.rand.NextBool(30) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Chaosdebu").Type, 1, 0f);
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                    }
                }
                if (healthmod > 1)
                {
                    if(Main.rand.NextBool(30) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f);
                    }
                    if (AI_Timer == 21)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(spd.X, spd.Y - 7f), Mod.Find<ModProjectile>("Chaosbase").Type, (NPC.damage / 4), 0f, 0, 1, 0);
                    }
                    if (Main.rand.NextBool(60) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 36; i++)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + (spd*10).RotatedBy(MathHelper.ToRadians(10 * i)), (spd / 3).RotatedBy(MathHelper.ToRadians(10*i)), Mod.Find<ModProjectile>("Chaostele").Type, 1, 0f, 0, 1, 0);
                        }
                    }
                }
                if (AI_Timer > 30)
                {
                    AI_Timer = 0;
                    AI_State = Fly;
                    NPC.netUpdate = true;
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
        const int Fly6 = 5;
        const int Fly7 = 6;
        const int Fly8 = 7;

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -NPC.direction;
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
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = Fly6 * frameHeight;
            }
            else if (NPC.frameCounter < 35)
            {
                NPC.frame.Y = Fly7 * frameHeight;
            }
            else if (NPC.frameCounter < 40)
            {
                NPC.frame.Y = Fly8 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.4f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Chaos Elemental";
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ChaosBag>()));
            var notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            notExpert.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                ModContent.ItemType<Items.Weapons.Melee.Dragon2h>(),
                ModContent.ItemType<Items.Weapons.Melee.Dds>(),
                ModContent.ItemType<Items.Magic.Ibansstaff>(),
                ModContent.ItemType<Items.Weapons.Ranged.Magicshortbow>(),
                ModContent.ItemType<Items.Weapons.Ranged.Dragonhuntercrossbow>(),
                ModContent.ItemType<Items.Weapons.Melee.Dragonspear>(),
                ModContent.ItemType<Items.Weapons.Melee.Dragonscimitar>()
                ));
            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Dragonstone>(), 3, 1, 3)
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Chaosrune>(), 2, 50, 150)
                .OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Ammo.Dragonstonebolt>(), 1, 50, 150)
                )));
            npcLoot.Add(notExpert);
        }
        public override void OnKill()
        {
            OSRSworld.downedChaosEle = true;
        }
        public override bool PreKill()
        {
            for (int i = 0; i < 100; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.UnholyWater);
            }
            return true;
        }
    }
}
