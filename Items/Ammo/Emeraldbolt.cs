using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Emeraldbolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emerald Bolt");
            Tooltip.SetDefault("[c/5cdb7d:Special effect: Spawns a spore cloud, poisoning the enemy]");
        }
        public override void SetDefaults()
        {
            item.damage = 12;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 3;
            item.shoot = mod.ProjectileType("Emeraldbolt");
            item.shootSpeed = 16f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Emeraldbolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
