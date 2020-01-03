using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class Runecrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Crossbow");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 24;
            item.ranged = true;
            item.scale = 0.75f;
            item.width = 28;
            item.height = 28;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            item.knockBack = 4f;
            item.value = 200000;
            item.rare = 3;
            item.UseSound = SoundID.Item99;
            item.autoReuse = false;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = 1;
            item.shoot = 10;
            item.shootSpeed = 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runitebar", 10);
            recipe.AddRecipeGroup("Wood", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}