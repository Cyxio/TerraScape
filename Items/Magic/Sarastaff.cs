using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Sarastaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saradomin Staff");
            Tooltip.SetDefault("'Strike with the power of Saradomin'");
        }
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 16;
            Item.crit = 3;
            Item.width = 42;
            Item.height = 62;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Sarastrike").Type;
            Item.shootSpeed = 10f;
            Item.scale = 0.8f;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.GetModPlayer<OSRSplayer>().GodCharge)
            {
                damage.Flat *= 2;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 55), new Vector2(0, 5), Mod.Find<ModProjectile>("Sarastrike").Type, damage, knockback, player.whoAmI);
            SoundEngine.PlaySound(SoundID.Item72, Main.MouseWorld);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}