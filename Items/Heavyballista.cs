using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class Heavyballista : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy ballista");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (10 second cooldown): Fires a charged javelin that takes enemies with it] \nUses stakes as ammunition");
        }
        public override void SetDefaults()
        {
            item.damage = 380;
            item.crit = 21;
            item.ranged = true;
            item.scale = 0.8f;
            item.width = 60;
            item.height = 56;
            item.useTime = 35;
            item.useAnimation = 35;
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 15);
            item.rare = 8;
            item.UseSound = SoundID.Item99;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = AmmoID.Stake;
            item.shoot = mod.ProjectileType<Projectiles.Ballistaproj>();
            item.shootSpeed = 22f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                Projectile.NewProjectile(position + new Vector2(speedX, speedY), new Vector2(speedX * 0.8f, speedY * 0.8f), mod.ProjectileType<Projectiles.Ballistaproj>(), damage, knockBack, player.whoAmI, 3, 0);
                player.AddBuff(mod.BuffType("SpecCD"), 600);
                return false;
            }
            Projectile.NewProjectile(position + new Vector2(speedX, speedY), new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.Ballistaproj>(), damage, knockBack, player.whoAmI, 0, 0);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 10);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Ballistalimbs");
            recipe.AddIngredient(null, "Ballistaspring");
            recipe.AddIngredient(null, "Monkeytail");
            recipe.AddIngredient(null, "Heavyframe");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}