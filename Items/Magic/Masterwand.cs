using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Masterwand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master Wand");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 100;
            item.magic = true;
            item.mana = 12;
            item.crit = 16;
            item.width = 62;
            item.height = 64;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 5;
            item.noUseGraphic = false;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FirewaveP");
            item.shootSpeed = 20;
            item.scale = 0.8f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                item.shoot = mod.ProjectileType("WindwaveP");
            }
            else if (s == 1)
            {
                item.shoot = mod.ProjectileType("WaterwaveP");
            }
            else if (s == 2)
            {
                item.shoot = mod.ProjectileType("EarthwaveP");
            }
            else
            {
                item.shoot = mod.ProjectileType("FirewaveP");
            }
            return true;
        }

        public override Vector2? HoldoutOrigin()
        {
            return new Vector2(0, 16);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Teacherwand");
            recipe.AddIngredient(null, "Windwave");
            recipe.AddIngredient(null, "Waterwave");
            recipe.AddIngredient(null, "Earthwave");
            recipe.AddIngredient(null, "Firewave");
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}