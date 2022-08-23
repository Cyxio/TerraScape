using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Beginnerwand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beginner Wand");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.width = 50;
            Item.height = 58;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 2, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("FirestrikeP").Type;
            Item.shootSpeed = 12;
            Item.scale = 0.8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                Item.shoot = Mod.Find<ModProjectile>("WaterstrikeP").Type;
            }
            else if (s == 1)
            {
                Item.shoot = Mod.Find<ModProjectile>("WindstrikeP").Type;
            }
            else if (s == 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("EarthstrikeP").Type;
            }
            else
            {
                Item.shoot = Mod.Find<ModProjectile>("FirestrikeP").Type;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Windstrike");
            recipe.AddIngredient(null, "Waterstrike");
            recipe.AddIngredient(null, "Earthstrike");
            recipe.AddIngredient(null, "Firestrike");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}