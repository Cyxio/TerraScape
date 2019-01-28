using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Bonebolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Bolt");
        }
        public override void SetDefaults()
        {
            item.damage = 6;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 2;
            item.shoot = mod.ProjectileType("Bonebolt");
            item.shootSpeed = 16f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
