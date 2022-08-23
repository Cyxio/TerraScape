using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Runedagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Dagger");
        }
        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.crit = 4;
            Item.scale = 0.75f;
            Item.width = 21;
            Item.height = 31;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 4f;
            Item.value = 60000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useTurn = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runitebar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += 10 * player.direction;
        }
    }
}