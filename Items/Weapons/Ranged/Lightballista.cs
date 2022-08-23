using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class Lightballista : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light ballista");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (7 second cooldown): Fires a charged javelin that sticks to the enemy and explodes] \nUses stakes as ammunition");
        }
        public override void SetDefaults()
        {
            Item.damage = 160;
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.8f;
            Item.width = 60;
            Item.height = 56;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.reuseDelay = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 15);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item99;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Stake;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ballistaproj>();
            Item.shootSpeed = 22f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                Projectile.NewProjectile(source, position + velocity, velocity, ModContent.ProjectileType<Projectiles.Ballistaproj>(), damage, knockback, player.whoAmI, 1, 0);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 420);
                return false;
            }
            Projectile.NewProjectile(source, position + velocity, velocity, ModContent.ProjectileType<Projectiles.Ballistaproj>(), damage, knockback, player.whoAmI, 0, 0);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 10);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Ballistalimbs");
            recipe.AddIngredient(null, "Ballistaspring");
            recipe.AddIngredient(null, "Monkeytail");
            recipe.AddIngredient(null, "Lightframe");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}