using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Karil
{
    public class Karilbag : ModItem
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
            item.rare = 5;
            item.expert = true;
            bossBagNPC = mod.NPCType("Karil");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            int helm = mod.ItemType<Items.Armor.Karilhelm>();
            int body = mod.ItemType<Items.Armor.Karilbody>();
            int legs = mod.ItemType<Items.Armor.Karillegs>();
            int wep = mod.ItemType<Items.Karilcrossbow>();
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
            player.QuickSpawnItem(drop);
        }
    }
    [AutoloadBossHead]
    public class Karil : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil the Tainted");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 45;
            npc.height = 100;
            npc.aiStyle = -1;
            npc.npcSlots = 15f;
            npc.lavaImmune = true;
            npc.damage = 70;
            npc.defense = 40;
            npc.lifeMax = 25000;
            npc.scale = 1.5f;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 35000f;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            music = MusicID.Boss3; //mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DangerousWay");
            bossBag = mod.ItemType("Karilbag");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 33000 + 2500 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.4f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Karil the Tainted";
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.Hitbox, mod.ItemType<Karilsummon>());
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int drop = 0;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        drop = mod.ItemType<Items.Karilcrossbow>();
                        break;
                    case 1:
                        drop = mod.ItemType<Items.Armor.Karilhelm>();
                        break;
                    case 2:
                        drop = mod.ItemType<Items.Armor.Karilbody>();
                        break;
                    case 3:
                        drop = mod.ItemType<Items.Armor.Karillegs>();
                        break;
                    default:
                        break;
                }
                Item.NewItem(npc.Hitbox, drop);
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.magic)
            {
                damage = (int)(damage * 0.9f);
            }
            if (projectile.melee)
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
            get { return npc.ai[AI_State_Slot]; }
            set { npc.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return npc.ai[AI_Timer_Slot]; }
            set { npc.ai[AI_Timer_Slot] = value; }
        }

        public int attacknum
        {
            get { return (int)npc.ai[2]; }
            set { npc.ai[2] = value; }
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
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 52);
            }
            Lighting.AddLight(npc.Center, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (npc.life < npc.lifeMax * 0.75f)
            {
                healthmod = 1;
            }
            if (npc.life < npc.lifeMax * 0.5f)
            {
                healthmod = 2;
            }
            if (npc.life < npc.lifeMax * 0.25f)
            {
                healthmod = 3;
            }
            if (AI_State == Fly)
            {
                while (npc.velocity.X > speed || npc.velocity.Y > speed || npc.velocity.X < -speed || npc.velocity.Y < -speed)
                {
                    npc.velocity *= 0.97f;
                }
                AI_Timer++;
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
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
                if (AI_Timer % (75 - 10*healthmod) == 0)
                {
                    float speedX = target.MountedCenter.X - npc.Center.X;
                    float speedY = target.MountedCenter.Y - npc.Center.Y;
                    spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    spd *= 25;
                    Projectile.NewProjectile(npc.Center, spd.RotateRandom(MathHelper.ToRadians(10)), mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, 0, 0);
                }
                if (AI_Timer > (300 - 30 * healthmod) && Main.netMode != 1)
                {
                    AI_Timer = 0;
                    int meme = Main.rand.Next(1, 3 + healthmod);
                    if (meme == 1)
                    {

                    }
                    else if (meme == 2)
                    {

                    }
                    else if (meme == 3)
                    {
                        
                    }
                    else if (meme == 4)
                    {
                        npc.velocity *= 0f;
                    }
                    else if (meme == 5)
                    {
                        
                    }
                    else
                    {
                        AI_State = Fly;
                    }
                    AI_State = Attack;
                    attacknum = meme;
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
                if (attacknum == 1)
                {
                    if (AI_Timer == 3)
                    {
                        npc.TargetClosest(true);
                        Player target = Main.player[npc.target];
                        float speedX = target.MountedCenter.X - npc.Center.X;
                        float speedY = target.MountedCenter.Y - npc.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 18;
                        for (int i = 0; i < 2 + 3 * healthmod; i++)
                        {
                            Projectile.NewProjectile(npc.Center, spd.RotateRandom(MathHelper.ToRadians(75)), mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, 1, 0);
                        }
                    }
                    if (AI_Timer > 5)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 2)
                {
                    npc.TargetClosest(true);
                    Player target = Main.player[npc.target];
                    if (AI_Timer < 60 - 15 * healthmod)
                    {
                        Main.PlaySound(SoundID.Item13, npc.Center);
                        npc.velocity *= 0.9f;
                        float speedX = target.MountedCenter.X - npc.Center.X;
                        float speedY = target.MountedCenter.Y - npc.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 18;
                    }
                    else if (AI_Timer % 3 == 0)
                    {
                        Projectile.NewProjectile(npc.Center, spd.RotateRandom(MathHelper.ToRadians(15)), mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, Main.rand.Next(3), 0);
                    }
                    if (AI_Timer > 80)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 3)
                {
                    npc.velocity *= 0.95f;
                    npc.TargetClosest(true);
                    Player target = Main.player[npc.target];
                    float speedX = target.MountedCenter.X - npc.Center.X;
                    float speedY = target.MountedCenter.Y - npc.Center.Y;
                    spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    spd *= 18;
                    if (AI_Timer % 15 == 0)
                    {
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, 2, 0);
                    }
                    if (AI_Timer % 15 == 3 && healthmod > 1)
                    {
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, 2, 0);
                    }
                    if (AI_Timer % 15 == 6 && healthmod > 2)
                    {
                        Projectile.NewProjectile(npc.Center, spd, mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, 2, 0);
                    }
                    if (AI_Timer > 60 * healthmod)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 4)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 58, 0, -8);
                    if (AI_Timer % 3 == 0)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(0, 10 * healthmod).RotateRandom(Math.PI * 2), mod.ProjectileType<Ghostbolt>(), npc.damage / 4, 0f, 0, 2, 0);
                    }
                    if (AI_Timer > 60 * healthmod)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 5)
                {
                    npc.TargetClosest(true);
                    Player target = Main.player[npc.target];
                    for (int i = 0; i < 36; i++)
                    {
                        Vector2 rotate = new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(i * 10));
                        Dust.NewDust(target.Center + rotate, 0, 0, 58, rotate.X * 0.001f, rotate.Y * 0.001f);
                    }
                    target.velocity = target.velocity.RotateRandom(Math.PI * 2) * 3;
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
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override bool PreNPCLoot()
        {
            for (int i = 0; i < 120; i++)
            {
                Vector2 rotata = new Vector2(0, 8).RotatedBy(MathHelper.ToRadians(3 * i));
                Dust.NewDust(npc.Center + rotata, 0, 0, 58, rotata.X, rotata.Y, 0, default(Color), 1.5f);
            }
            if (NPC.AnyNPCs(mod.NPCType<Barrowsspirit>()))
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == mod.NPCType<Barrowsspirit>())
                    {
                        Vector2 spd = Main.npc[i].Center - npc.Center;
                        spd.Normalize();
                        Projectile.NewProjectile(npc.Center, spd * 5f, mod.ProjectileType<Barrowsdamageproj>(), 100, 0, Main.player[npc.target].whoAmI);
                    }
                }
            }
            return true;
        }
    }
}
