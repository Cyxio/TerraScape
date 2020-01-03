using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Beginnerwand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beginner Wand");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 14;
            item.magic = true;
            item.mana = 5;
            item.width = 50;
            item.height = 58;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.noUseGraphic = false;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FirestrikeP");
            item.shootSpeed = 12;
            item.scale = 0.8f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                item.shoot = mod.ProjectileType("WaterstrikeP");
            }
            else if (s == 1)
            {
                item.shoot = mod.ProjectileType("WindstrikeP");
            }
            else if (s == 2)
            {
                item.shoot = mod.ProjectileType("EarthstrikeP");
            }
            else
            {
                item.shoot = mod.ProjectileType("FirestrikeP");
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Windstrike");
            recipe.AddIngredient(null, "Waterstrike");
            recipe.AddIngredient(null, "Earthstrike");
            recipe.AddIngredient(null, "Firestrike");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}