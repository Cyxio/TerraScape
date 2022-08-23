using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Vengeance : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vengeance");
            Tooltip.SetDefault("Getting hit will reflect 1000% damage back to the attacker");
        }
        public override void SetDefaults()
        {
            Item.mana = 50;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item77;
            Item.autoReuse = false;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<OSRSplayer>().Vengeance;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(ModContent.BuffType<Buffs.Vengeance>(), 360000);
            for (int o = 0; o < 36; o++)
            {
                Vector2 rotate = new Vector2(3).RotatedBy(MathHelper.ToRadians(10 * o));
                int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemRuby);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = player.MountedCenter + 10*rotate;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].fadeIn = 2f;
                Main.dust[dust].velocity = -rotate;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Deathrune", 20);
            recipe.AddIngredient(null, "Astralrune", 40);
            recipe.AddIngredient(null, "Earthrune", 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}