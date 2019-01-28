using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 3;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Chaoselemental"));
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Chaoselemental"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(mod.ItemType<Items.Magic.Airrune>());
            r.AddIngredient(mod.ItemType<Items.Magic.Waterrune>());
            r.AddIngredient(mod.ItemType<Items.Magic.Firerune>());
            r.AddIngredient(mod.ItemType<Items.Magic.Earthrune>());
            r.AddIngredient(mod.ItemType<Items.Magic.Chaosrune>());
            r.AddIngredient(ItemID.SoulofLight);
            r.AddIngredient(ItemID.SoulofNight);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
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
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.expert = true;
            bossBagNPC = mod.NPCType("Chaoselemental");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            Item.NewItem(player.Center, mod.ItemType<Tiles.ChaosMusicBoxItem>(), 1, false, 0);
            int ch = Main.rand.Next(7);
            if(ch == 0)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dragon2h>());
            }
            if (ch == 1)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dds>());
            }
            if (ch == 2)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Magic.Ibansstaff>());
            }
            if (ch == 3)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Magicshortbow>());
            }
            if (ch == 4)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dragonhuntercrossbow>());
            }
            if (ch == 5)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dragonspear>());
            }
            if (ch == 6)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dragonscimitar>());
            }
            if (Main.rand.Next(3) == 0)
            {
                int supply = Main.rand.Next(3);
                if (supply == 0)
                {
                    player.QuickSpawnItem(mod.ItemType("Dragonstone"), Main.rand.Next(1, 5));
                }
                if (supply == 1)
                {
                    player.QuickSpawnItem(mod.ItemType("Chaosrune"), Main.rand.Next(100, 250));
                }
                if (supply == 2)
                {
                    player.QuickSpawnItem(mod.ItemType("Dragonstonebolt"), Main.rand.Next(100, 250));
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
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 122;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.npcSlots = 20f;
            npc.lavaImmune = true;
            npc.damage = 60;
            npc.defense = 35;
            npc.lifeMax = 16000;
            npc.scale = 1.5f;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 10000f;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            music = MusicID.Boss2; //mod.GetSoundSlot(SoundType.Music, "Sounds/Music/EverlastingFire");
            bossBag = mod.ItemType("ChaosBag");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 20000 + 1000 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
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

        private float speed = 4f;
        private float accelerate = 0.05f;
        private int healthmod = 0;

        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 52);
            }
            AI_Timer++;
            Lighting.AddLight(npc.Center, new Vector3(148 * 0.01f, 56 * 0.01f, 255 * 0.01f));
            if (npc.life < npc.lifeMax * 0.6f)
            {
                healthmod = 1;
                accelerate = 0.1f;
            }
            if (npc.life < npc.lifeMax * 0.3f)
            {
                healthmod = 2;
                accelerate = 0.2f;
            }
            if(AI_State == Fly)
            {
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float distance = npc.Distance(target.MountedCenter);
                if (npc.velocity.X > speed || npc.velocity.Y > speed)
                {
                    npc.velocity *= 0.95f;
                }
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
                if (AI_Timer > (75 - 10 * healthmod) && Main.netMode != 1)
                {
                    AI_Timer = 0;
                    AI_State = Attack;
                    npc.netUpdate = true;
                }
                if (!npc.HasValidTarget)
                {
                    npc.velocity *= 0;
                    AI_State = Flee;
                }
            }
            if (AI_State == Attack)
            {
                AI_Timer++;
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float speedX = target.MountedCenter.X - npc.Center.X;
                float speedY = target.MountedCenter.Y - npc.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 8;
                if (AI_Timer == 1)
                {
                    Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f);
                    if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType("Chaosdebu"), 1, 0f);
                        if (Main.rand.Next(2) == 0 && Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType("Chaostele"), 1, 0f);
                        }

                    }
                }
                if (Main.rand.Next(90 - 20*healthmod) == 0 && Main.netMode != 1)
                {
                    Vector2 rotat = spd.RotatedBy(MathHelper.ToRadians(-30));
                    for (int i = 0; i < 8; i++)
                    {
                        if (true)
                        {
                            Projectile.NewProjectile(npc.Center, rotat.RotatedBy(MathHelper.ToRadians(10 * i)), mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f);
                        }
                    }
                }
                if (healthmod > 0)
                {
                    if (AI_Timer == 29)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(spd.X, spd.Y - 7f), mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f, 0, 1, 0);
                        Projectile.NewProjectile(npc.Center, new Vector2(spd.X, spd.Y + 7f), mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f, 0, 2, 0);
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f);
                    }
                    if (Main.rand.Next(30) == 0 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType("Chaosdebu"), 1, 0f);
                        Main.PlaySound(SoundID.Item20, npc.Center);
                    }
                }
                if (healthmod > 1)
                {
                    if(Main.rand.Next(30) == 0 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f);
                    }
                    if (AI_Timer == 21)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(spd.X, spd.Y - 7f), mod.ProjectileType("Chaosbase"), (npc.damage / 4), 0f, 0, 1, 0);
                    }
                    if (Main.rand.Next(60) == 0 && Main.netMode != 1)
                    {
                        for (int i = 0; i < 36; i++)
                        {
                            Projectile.NewProjectile(npc.Center + (spd*10).RotatedBy(MathHelper.ToRadians(10 * i)), (spd / 3).RotatedBy(MathHelper.ToRadians(10*i)), mod.ProjectileType("Chaostele"), 1, 0f, 0, 1, 0);
                        }
                    }
                }
                if (AI_Timer > 30)
                {
                    AI_Timer = 0;
                    AI_State = Fly;
                    npc.netUpdate = true;
                }
            }
            if (AI_State == Flee)
            {
                npc.velocity.Y += 0.05f;
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
            npc.spriteDirection = -npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = Fly1 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = Fly2 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = Fly3 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = Fly4 * frameHeight;
            }
            else if (npc.frameCounter < 25)
            {
                npc.frame.Y = Fly5 * frameHeight;
            }
            else if (npc.frameCounter < 30)
            {
                npc.frame.Y = Fly6 * frameHeight;
            }
            else if (npc.frameCounter < 35)
            {
                npc.frame.Y = Fly7 * frameHeight;
            }
            else if (npc.frameCounter < 40)
            {
                npc.frame.Y = Fly8 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
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
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int ch = Main.rand.Next(7);
                if (ch == 0)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Dragon2h>());
                }
                if (ch == 1)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Dds>());
                }
                if (ch == 2)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Magic.Ibansstaff>());
                }
                if (ch == 3)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Magicshortbow>());
                }
                if (ch == 4)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Dragonhuntercrossbow>());
                }
                if (ch == 5)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Dragonspear>());
                }
                if (ch == 6)
                {
                    Item.NewItem(npc.Hitbox, mod.ItemType<Items.Dragonscimitar>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    int supply = Main.rand.Next(3);
                    if (supply == 0)
                    {
                        Item.NewItem(npc.Hitbox, mod.ItemType("Dragonstone"), Main.rand.Next(1, 3));
                    }
                    if (supply == 1)
                    {
                        Item.NewItem(npc.Hitbox, mod.ItemType("Chaosrune"), Main.rand.Next(50, 150));
                    }
                    if (supply == 2)
                    {
                        Item.NewItem(npc.Hitbox, mod.ItemType("Dragonstonebolt"), Main.rand.Next(50, 120));
                    }
                }
            }
        }
        public override bool PreNPCLoot()
        {
            for (int i = 0; i < 100; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 52);
            }
            return true;
        }
    }
}
