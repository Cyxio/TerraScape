using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Apprenticewand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice Wand");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 24;
            item.magic = true;
            item.mana = 8;
            item.crit = 4;
            item.width = 50;
            item.height = 58;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noUseGraphic = false;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FireboltP");
            item.shootSpeed = 16;
            item.scale = 0.8f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                item.shoot = mod.ProjectileType("WindboltP");
            }
            else if (s == 1)
            {
                item.shoot = mod.ProjectileType("WaterboltP");
            }
            else if (s == 2)
            {
                item.shoot = mod.ProjectileType("EarthboltP");
            }
            else
            {
                item.shoot = mod.ProjectileType("FireboltP");
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Beginnerwand");
            recipe.AddIngredient(null, "Windbolt");
            recipe.AddIngredient(null, "Waterbolt");
            recipe.AddIngredient(null, "Earthbolt");
            recipe.AddIngredient(null, "Firebolt");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}