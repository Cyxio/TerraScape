using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Runebolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runite Bolt");
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 12;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 3;
            item.shoot = mod.ProjectileType("Runebolt");
            item.shootSpeed = 16f;
            item.ammo = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runitebar");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
