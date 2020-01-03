using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Firebolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Bolt");
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.mana = 8;
            item.crit = 4;
            item.width = 15;
            item.height = 20;
            item.useTime = 23;
            item.useAnimation = 23;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("FireboltP");
            item.shootSpeed = 16;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Firerune", 50);
            recipe.AddIngredient(null, "Chaosrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}