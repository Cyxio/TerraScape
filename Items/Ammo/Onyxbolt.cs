using System;
using Terraria;
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
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 20;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = 10;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = Mod.Find<ModProjectile>("Onyxbolt").Type;
            Item.shootSpeed = 16f;
            Item.ammo = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runebolt");
            recipe.AddIngredient(null, "Onyxbolttips");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
