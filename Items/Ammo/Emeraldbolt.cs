using System;
using Terraria;
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
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 20;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("Emeraldbolt").Type;
            Item.shootSpeed = 16f;
            Item.ammo = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Emeraldbolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
