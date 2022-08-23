using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.Items;
using OldSchoolRuneScape.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace OldSchoolRuneScape.Items.Magic
{
    public abstract class Spell : ModItem
    {
        public abstract void AdditionalDefaults();
        public abstract int Row();
        public abstract int Column();
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.consumable = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = 0;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Magic;
            AdditionalDefaults();
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            Item.TurnToAir();
        }
        public override string Texture => "OldSchoolRuneScape/UI/SpellbookIcon";
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D t = Request<Texture2D>("OldSchoolRuneScape/Items/Magic/SpellbookActive").Value;
            int x = 13 + 26 * Column();
            int y = 8 + 24 * Row();
            spriteBatch.Draw(t, position, new Rectangle(x, y, 20, 20), drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Item.active = false;
            return false;
        }
    }
    public class Windstrike : Spell
    {
        public override int Row() { return 0; }
        public override int Column() { return 1; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Strike");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 9;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Windcast");
            Item.shoot = Mod.Find<ModProjectile>("WindstrikeP").Type;
            Item.shootSpeed = 12;
        }
    }
    public class Waterstrike : Spell
    {
        public override int Row() { return 0; }
        public override int Column() { return 4; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Strike");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 9;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Watercast");
            Item.shoot = Mod.Find<ModProjectile>("WaterstrikeP").Type;
            Item.shootSpeed = 15;
        }
    }
    public class Earthstrike : Spell
    {
        public override int Row() { return 0; }
        public override int Column() { return 8; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Strike");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 11;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Earthcast");
            Item.shoot = Mod.Find<ModProjectile>("EarthstrikeP").Type;
            Item.shootSpeed = 8;
        }
    }
    public class Firestrike : Spell
    {
        public override int Row() { return 1; }
        public override int Column() { return 1; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Strike");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 9;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Firecast");
            Item.shoot = Mod.Find<ModProjectile>("FirestrikeP").Type;
            Item.shootSpeed = 12;
        }
    }
    public class Windbolt : Spell
    {
        public override int Row() { return 1; }
        public override int Column() { return 3; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Bolt");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 20;
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Windcast");
            Item.shoot = Mod.Find<ModProjectile>("WindboltP").Type;
            Item.shootSpeed = 16;
        }
    }
    public class Waterbolt : Spell
    {
        public override int Row() { return 2; }
        public override int Column() { return 0; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Bolt");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 20;
            Item.knockBack = 2.5f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Watercast");
            Item.shoot = Mod.Find<ModProjectile>("WaterboltP").Type;
            Item.shootSpeed = 19;
        }
    }
    public class Earthbolt : Spell
    {
        public override int Row() { return 2; }
        public override int Column() { return 3; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Bolt");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 20;
            Item.knockBack = 2.5f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Earthcast");
            Item.shoot = Mod.Find<ModProjectile>("EarthboltP").Type;
            Item.shootSpeed = 12;
        }
    }
    public class Firebolt : Spell
    {
        public override int Row() { return 2; }
        public override int Column() { return 6; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Bolt");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 20;
            Item.knockBack = 2.5f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Firecast");
            Item.shoot = Mod.Find<ModProjectile>("FireboltP").Type;
            Item.shootSpeed = 16;
        }
    }
    public class Windblast : Spell
    {
        public override int Row() { return 3; }
        public override int Column() { return 3; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Blast");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 45;
            Item.knockBack = 6f;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Windcast");
            Item.shoot = Mod.Find<ModProjectile>("WindblastP").Type;
            Item.shootSpeed = 19;
        }
    }
    public class Waterblast : Spell
    {
        public override int Row() { return 3; }
        public override int Column() { return 6; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Blast");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 45;
            Item.knockBack = 3f;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Watercast");
            Item.shoot = Mod.Find<ModProjectile>("WaterblastP").Type;
            Item.shootSpeed = 22;
        }
    }
    public class Earthblast : Spell
    {
        public override int Row() { return 4; }
        public override int Column() { return 5; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Blast");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 52;
            Item.knockBack = 3f;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Earthcast");
            Item.shoot = Mod.Find<ModProjectile>("EarthblastP").Type;
            Item.shootSpeed = 16;
        }
    }
    public class Fireblast : Spell
    {
        public override int Row() { return 5; }
        public override int Column() { return 3; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Blast");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 45;
            Item.knockBack = 3f;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Firecast");
            Item.shoot = Mod.Find<ModProjectile>("FireblastP").Type;
            Item.shootSpeed = 19;
        }
    }
    public class Windwave : Spell
    {
        public override int Row() { return 6; }
        public override int Column() { return 3; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Wave");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 60;
            Item.knockBack = 8f;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Windcast");
            Item.shoot = Mod.Find<ModProjectile>("WindwaveP").Type;
            Item.shootSpeed = 20;
        }
    }
    public class Waterwave : Spell
    {
        public override int Row() { return 6; }
        public override int Column() { return 6; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Wave");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 60;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Watercast");
            Item.shoot = Mod.Find<ModProjectile>("WaterwaveP").Type;
            Item.shootSpeed = 24;
        }
    }
    public class Earthwave : Spell
    {
        public override int Row() { return 7; }
        public override int Column() { return 4; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Wave");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 68;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Earthcast");
            Item.shoot = Mod.Find<ModProjectile>("EarthwaveP").Type;
            Item.shootSpeed = 17;
        }
    }
    public class Firewave : Spell
    {
        public override int Row() { return 8; }
        public override int Column() { return 0; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Wave");
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 60;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Firecast");
            Item.shoot = Mod.Find<ModProjectile>("FirewaveP").Type;
            Item.shootSpeed = 20;
        }
    }
    public class Bind : Spell
    {
        public override int Row() { return 1; }
        public override int Column() { return 5; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bind");
        }
        public override void AdditionalDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ProjectileType<Projectiles.Bind>();
            Item.shootSpeed = 12f;
        }
    }
    public class Snare : Spell
    {
        public override int Row() { return 4; }
        public override int Column() { return 2; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snare");
        }
        public override void AdditionalDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ProjectileType<Projectiles.Snare>();
            Item.shootSpeed = 12f;
        }
    }
    public class Entangle : Spell
    {
        public override int Row() { return 8; }
        public override int Column() { return 1; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entangle");
        }
        public override void AdditionalDefaults()
        {
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ProjectileType<Projectiles.Entangle>();
            Item.shootSpeed = 12f;
        }
    }
    public class Confuse : Spell
    {
        public override int Row() { return 0; }
        public override int Column() { return 2; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Confuse");
        }
        public override void AdditionalDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.Confuse>();
            Item.shootSpeed = 12f;
        }
    }
    public class Telegrab : Spell
    {
        public override int Row() { return 2; }
        public override int Column() { return 5; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Telekinetic grab");
        }
        public override void AdditionalDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = false;
            Item.shoot = ProjectileType<Projectiles.Telegrab>();
            Item.shootSpeed = 7.5f;
        }
    }
    public class Charge : Spell
    {
        public override int Row() { return 8; }
        public override int Column() { return 3; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charge");
        }
        public override void AdditionalDefaults()
        {
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item77;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<OSRSplayer>().GodCharge;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(ModContent.BuffType<Buffs.Charge>(), 18000);
            for (int o = 0; o < 36; o++)
            {
                Vector2 rotate = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
                int dust = Dust.NewDust(player.Center, 0, 0, DustID.VilePowder);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = player.MountedCenter + 10 * rotate;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].fadeIn = 2.5f;
                Main.dust[dust].velocity = -rotate;
            }
            return true;
        }
    }
}
