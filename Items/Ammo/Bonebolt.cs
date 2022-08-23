using System;
using Terraria;
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
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 20;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.shoot = Mod.Find<ModProjectile>("Bonebolt").Type;
            Item.shootSpeed = 16f;
            Item.ammo = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.Bone);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
