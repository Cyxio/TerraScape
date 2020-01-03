using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Earthwave : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Wave");
        }
        public override void SetDefaults()
        {
            item.damage = 68;
            item.magic = true;
            item.mana = 12;
            item.crit = 6;
            item.width = 17;
            item.height = 17;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("EarthwaveP");
            item.shootSpeed = 17;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Earthrune", 250);
            recipe.AddIngredient(null, "Bloodrune", 50);
            recipe.SetResult(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }
}