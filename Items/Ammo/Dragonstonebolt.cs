using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Dragonstonebolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Bolt");
            Tooltip.SetDefault("[c/5cdb7d:Special effect: 500% increased damage and a long-lasting fire effect]");
        }
        public override void SetDefaults()
        {
            item.damage = 18;
            item.ranged = true;
            item.width = 12;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 5;
            item.shoot = mod.ProjectileType("Dragonstonebolt");
            item.shootSpeed = 16f;
            item.ammo = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Dragonbolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
