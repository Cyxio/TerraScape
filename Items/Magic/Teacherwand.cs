using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Teacherwand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Teacher Wand");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 44;
            item.magic = true;
            item.mana = 9;
            item.crit = 8;
            item.width = 50;
            item.height = 58;
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = 5;
            item.noUseGraphic = false;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FireblastP");
            item.shootSpeed = 19;
            item.scale = 0.8f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                item.shoot = mod.ProjectileType("WindblastP");
            }
            else if (s == 1)
            {
                item.shoot = mod.ProjectileType("WaterblastP");
            }
            else if (s == 2)
            {
                item.shoot = mod.ProjectileType("EarthblastP");
            }
            else
            {
                item.shoot = mod.ProjectileType("FireblastP");
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Apprenticewand");
            recipe.AddIngredient(null, "Windblast");
            recipe.AddIngredient(null, "Waterblast");
            recipe.AddIngredient(null, "Earthblast");
            recipe.AddIngredient(null, "Fireblast");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}