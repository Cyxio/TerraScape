using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class Runecrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Crossbow");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.75f;
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = 200000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item99;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = 1;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 50;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runitebar", 10);
            recipe.AddRecipeGroup("Wood", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}