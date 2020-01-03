using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Earthbolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Bolt");
        }
        public override void SetDefaults()
        {
            item.damage = 25;
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
            item.shoot = mod.ProjectileType("EarthboltP");
            item.shootSpeed = 12;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Earthrune", 50);
            recipe.AddIngredient(null, "Chaosrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}