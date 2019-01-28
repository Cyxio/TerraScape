using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Onyxbolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Onyx Bolt");
            Tooltip.SetDefault("[c/5cdb7d:Special effect: Spawns 4-6 healing orbs that heal 10% of the attack's damage]");
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 8;
            item.shoot = mod.ProjectileType("Onyxbolt");
            item.shootSpeed = 16f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Onyxbolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
