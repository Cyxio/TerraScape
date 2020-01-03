using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Runedagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Dagger");
        }
        public override void SetDefaults()
        {
            item.damage = 34;
            item.melee = true;
            item.crit = 4;
            item.scale = 0.75f;
            item.width = 21;
            item.height = 31;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 3;
            item.knockBack = 4f;
            item.value = 60000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runitebar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += 10 * player.direction;
        }
    }
}