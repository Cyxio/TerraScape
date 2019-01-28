using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Elvarg
{
    public class ElvargFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvarg's firebreath");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 28;
            projectile.height = 28;
            projectile.damage = 0;
            projectile.penetrate = 1;
            projectile.aiStyle = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 150;
            projectile.alpha = 1;
        }
        public override void AI()
        {
            if (projectile.alpha == 1)
            {
                Main.PlaySound(SoundID.DD2_BetsyFlameBreath, projectile.Center);
                projectile.alpha -= 1;
            }
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(255 * 0.005f, 124 * 0.005f, 0));
            projectile.velocity.Y = projectile.oldVelocity.Y;
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 127);
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 20; i++)
            {
                Dust.NewDust(projectile.Center, 0, 0, 127, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                Main.PlaySound(SoundID.Item20.WithPitchVariance(0.5f), projectile.position);
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
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 3;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Elvarg"));
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Elvarg"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddIngredient(ItemID.Obsidian, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
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
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 3;
            item.expert = true;
            bossBagNPC = mod.NPCType("Elvarg");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            Item.NewItem(player.Center, mod.ItemType<Tiles.ElvargMusicBoxItem>(), 1, false, 0);
            player.QuickSpawnItem(mod.ItemType("Greendhide"), Main.rand.Next(24, 45));
            player.QuickSpawnItem(mod.ItemType<Items.Mysticcomponents>(), Main.rand.Next(6, 14));
            int ch = Main.rand.Next(5);
            if (ch == 0)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Accessories.Boltenchant>());
            }
            if (ch == 1)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Excalibur>());
            }
        }
    }
    [AutoloadBossHead]
    public class Elvarg : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvarg");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.width = 150;
            npc.height = 100;
            npc.aiStyle = -1;
            npc.npcSlots = 10f;
            npc.lavaImmune = true;
            npc.damage = 40;
            npc.defense = 0;
            npc.lifeMax = 7000;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Dragonhit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ElvargDeath");
            npc.value = 5000f;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            music = MusicID.Boss1; //mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Attack2");
            bossBag = mod.ItemType("ElvargBag");
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int AI_AttackTimer_Slot = 2;
        const int Fly = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float AI_State
        {
            get { return npc.ai[AI_State_Slot]; }
            set { npc.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return npc.ai[AI_Timer_Slot]; }
            set { npc.ai[AI_Timer_Slot] = value; }
        }

        public float AI_Attack
        {
            get { return npc.ai[AI_AttackTimer_Slot]; }
            set { npc.ai[AI_AttackTimer_Slot] = value; }
        }

        public float speed = 4f;
        public float accelerate = 0.1f;

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000 + 1000 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
            speed = 5f;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Vector3(0.5f, 0.5f, 0.5f));
            int healthmod = 0;
            if (npc.life < npc.lifeMax * 0.5f)
            {
                healthmod = 1;
                accelerate = 0.2f;
            }
            if (npc.life < npc.lifeMax * 0.15f)
            {
                healthmod = 2;
                speed = 6f;
                accelerate = 0.3f;
                npc.defense = 15;
            }
            if (AI_State == Fly)
            {
                AI_Attack++;
                if (AI_Attack > 150 - (40*healthmod) && Main.netMode != 1)
                {
                    AI_Timer = 0;
                    AI_Attack = 0;
                    AI_State = Attack;
                    npc.netUpdate = true;
                }
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float distance = npc.Distance(target.MountedCenter);
                if (npc.velocity.X > speed || npc.velocity.Y > speed || npc.velocity.X < -speed || npc.velocity.Y < -speed)
                {
                    npc.velocity *= 0.99f;
                }
                if (distance > 200 - 50*healthmod)
                {
                    if (target.MountedCenter.Y > npc.Center.Y && npc.velocity.Y < speed)
                    {
                        npc.velocity.Y += accelerate;
                    }
                    if (target.MountedCenter.Y < npc.Center.Y && npc.velocity.Y > -speed)
                    {
                        npc.velocity.Y -= accelerate;
                    }
                    if (target.MountedCenter.X > npc.Center.X && npc.velocity.X < speed)
                    {
                        npc.velocity.X += accelerate;
                    }
                    if (target.MountedCenter.X < npc.Center.X && npc.velocity.X > -speed)
                    {
                        npc.velocity.X -= accelerate;
                    }
                }
                if (!npc.HasValidTarget)
                {
                    npc.velocity *= 0;
                    AI_State = Flee;
                    npc.netUpdate = true;
                }
            }
            if (AI_State == Attack)
            {
                AI_Timer++;
                if (AI_Timer > 20)
                {
                    AI_State = Fly;
                    npc.netUpdate = true;
                }
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float speedX = target.MountedCenter.X - npc.Center.X;
                float speedY = target.MountedCenter.Y - npc.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 12 + 2*healthmod;
                if (AI_Timer == 1)
                {
                    npc.velocity.Y *= 0;
                    if (healthmod == 2)
                    {
                        npc.velocity *= 0;
                    }
                    Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20), spd, mod.ProjectileType("ElvargFire"), (npc.damage / 3), 0f);
                    npc.netUpdate = true;
                }
                if (AI_Timer == 8 && healthmod > 0)
                {
                    if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        npc.velocity = spd * 1.1f;
                        npc.netUpdate = true;
                    }
                    Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20), spd, mod.ProjectileType("ElvargFire"), (npc.damage / 3), 0f);
                }
                if (AI_Timer == 15 && healthmod == 2 && Main.netMode != 1)
                {
                    Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20), spd, mod.ProjectileType("ElvargFire"), (npc.damage / 3), 0f);
                }
            }
            if (AI_State == Flee)
            {
                npc.velocity.Y -= 0.1f;
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
            npc.spriteDirection = -npc.direction;
            if (AI_State == Fly || AI_State == Flee)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 4)
                {
                    npc.frame.Y = Fly1 * frameHeight;
                }
                else if (npc.frameCounter < 8)
                {
                    npc.frame.Y = Fly2 * frameHeight;
                }
                else if (npc.frameCounter < 12)
                {
                    npc.frame.Y = Fly3 * frameHeight;
                }
                else if (npc.frameCounter < 16)
                {
                    npc.frame.Y = Fly4 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = Fly5 * frameHeight;
                }
                else if (npc.frameCounter < 24)
                {
                    npc.frame.Y = Fly6 * frameHeight;
                }
                else if (npc.frameCounter < 28)
                {
                    npc.frame.Y = Fly7 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            if (AI_State == Attack)
            {
                if (AI_Timer < 4)
                {
                    npc.frame.Y = Attack1 * frameHeight;
                }
                else if (AI_Timer < 8)
                {
                    npc.frame.Y = Attack2 * frameHeight;
                }
                else if (AI_Timer < 12)
                {
                    npc.frame.Y = Attack3 * frameHeight;
                }
                else if (AI_Timer < 16)
                {
                    npc.frame.Y = Attack4 * frameHeight;
                }
                else if (AI_Timer < 20)
                {
                    npc.frame.Y = Attack5 * frameHeight;
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
        public override void NPCLoot()
        {
            OSRSworld.downedElvarg = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem(npc.Hitbox, mod.ItemType("Greendhide"), Main.rand.Next(15, 30));
                Item.NewItem(npc.Hitbox, mod.ItemType<Items.Mysticcomponents>(), Main.rand.Next(4, 12));
                int ch = Main.rand.Next(8);
                if (ch == 0)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Accessories.Boltenchant>());
                }
                if (ch == 1)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Excalibur>());
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Elvarg"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Elvarg2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Elvarg3"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Elvarg1"), 1f);
            }
        }
    }
}