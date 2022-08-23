using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Diamondbolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond Bolt");
            Tooltip.SetDefault("[c/5cdb7d:Special effect: Gains damage based on the enemy's defense]");
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 20;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("Diamondbolt").Type;
            Item.shootSpeed = 16f;
            Item.ammo = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Diamondbolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
