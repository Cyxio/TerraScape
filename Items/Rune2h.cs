using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Rune2h : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune 2h Sword");
        }
        public override void SetDefaults()
        {
            item.damage = 65;
            item.melee = true;
            item.crit = 11;
            item.width = 42;
            item.height = 60;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 1;
            item.knockBack = 8f;
            item.value = 180000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runitebar", 9);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
                hitbox.X += 15 * player.direction;
        }
    }
}