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
            item.mana = 50;
            item.width = 40;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = 8;
            item.UseSound = SoundID.Item77;
            item.autoReuse = false;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<OSRSplayer>().Vengeance;
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Buffs.Vengeance>(), 360000);
            for (int o = 0; o < 36; o++)
            {
                Vector2 rotate = new Vector2(3).RotatedBy(MathHelper.ToRadians(10 * o));
                int dust = Dust.NewDust(player.Center, 0, 0, 90);
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Deathrune", 20);
            recipe.AddIngredient(null, "Astralrune", 40);
            recipe.AddIngredient(null, "Earthrune", 100);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}