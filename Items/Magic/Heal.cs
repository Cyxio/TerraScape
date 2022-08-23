using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Heal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heal");
            Tooltip.SetDefault("Channel for up to 10 seconds to restore up to half of your maximum health");
        }
        public override void SetDefaults()
        {
            Item.mana = 10;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = false;
        }
        int counter = 0;
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.channel && player.statMana > 9)
            {
                if (player.velocity.Y < 0)
                {
                    player.velocity.Y *= 0.5f;
                }
                player.velocity.X *= 0.5f;
                player.itemTime = 2;
                player.itemAnimation = 2;
                Item.mana = 10;
                counter++;
                if (counter >= 600)
                {
                    player.channel = false;

                }
                if (counter % 30 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item15, player.position);
                    player.statMana -= Item.mana;
                    for (int o = 0; o < counter / 30; o++)
                    {
                        int dust = Dust.NewDust(player.Center, 0, 0, DustID.RainbowMk2, 0, 0, 0, Color.LightGreen, 1f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].position = player.MountedCenter + new Vector2(0, -25).RotatedBy(MathHelper.ToRadians(18 * o));
                    }
                }
            }
            else
            {
                if (counter > 30)
                {
                    int hpRatio = player.statLifeMax2 / 40;
                    player.HealEffect((counter / 30) * hpRatio);
                    player.statLife += (counter / 30) * hpRatio;
                }
                player.itemTime = 0;
                player.itemAnimation = 0;
                counter = 0;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Cosmicrune", 20);
            recipe.AddIngredient(null, "Astralrune", 20);
            recipe.AddIngredient(null, "Lawrune", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}