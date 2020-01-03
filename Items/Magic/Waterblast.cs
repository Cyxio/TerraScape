using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Waterblast : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Blast");
        }
        public override void SetDefaults()
        {
            item.damage = 45;
            item.magic = true;
            item.mana = 7;
            item.crit = 8;
            item.width = 16;
            item.height = 16;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WaterblastP");
            item.shootSpeed = 22;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Waterrune", 100);
            recipe.AddIngredient(null, "Deathrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }
}