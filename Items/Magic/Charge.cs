using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    /*public class Charge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charge");
            Tooltip.SetDefault("God spells gain 100% increased damage for 5 minutes");
        }
        public override void SetDefaults()
        {
            item.mana = 200;
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
            return !player.GetModPlayer<OSRSplayer>().GodCharge;
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Buffs.Charge>(), 18000);
            for (int o = 0; o < 36; o++)
            {
                Vector2 rotate = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
                int dust = Dust.NewDust(player.Center, 0, 0, 21);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = player.MountedCenter + 10*rotate;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].fadeIn = 2.5f;
                Main.dust[dust].velocity = -rotate;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Firerune", 33);
            recipe.AddIngredient(null, "Bloodrune", 33);
            recipe.AddIngredient(null, "Airrune", 33);
            recipe.AddIngredient(ItemID.Ectoplasm, 3);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }*/
}