using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Zamorakstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zamorak Staff");
            Tooltip.SetDefault("'Burn with the flame of Zamorak'");
        }
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.crit = 5;
            Item.width = 52;
            Item.height = 56;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Zamorakflame").Type;
            Item.shootSpeed = 0;
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
            Projectile.NewProjectile(source, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y), new Vector2(0), Mod.Find<ModProjectile>("Groundproj").Type, damage, knockback, player.whoAmI, 0, 0);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}