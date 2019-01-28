using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Runescim : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Scimitar");
        }
        public override void SetDefaults()
        {
            item.damage = 32;
            item.melee = true;
            item.scale = 0.8f;
            item.crit = 0;
            item.width = 30;
            item.height = 27;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.knockBack = 2f;
            item.value = 120000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runitebar", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += 0 * player.direction;
        }
    }
}