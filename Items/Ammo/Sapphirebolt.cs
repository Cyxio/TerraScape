using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Sapphirebolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapphire Bolt");
            Tooltip.SetDefault("[c/5cdb7d:Special effect: Gains damage based on current mana]");
        }
        public override void SetDefaults()
        {
            item.damage = 11;
            item.ranged = true;
            item.width = 12;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 3;
            item.shoot = mod.ProjectileType("Sapphirebolt");
            item.shootSpeed = 16f;
            item.ammo = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Sapphirebolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
