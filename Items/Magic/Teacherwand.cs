using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Teacherwand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Teacher Wand");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
            Item.crit = 8;
            Item.width = 50;
            Item.height = 58;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("FireblastP").Type;
            Item.shootSpeed = 19;
            Item.scale = 0.8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                Item.shoot = Mod.Find<ModProjectile>("WindblastP").Type;
            }
            else if (s == 1)
            {
                Item.shoot = Mod.Find<ModProjectile>("WaterblastP").Type;
            }
            else if (s == 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("EarthblastP").Type;
            }
            else
            {
                Item.shoot = Mod.Find<ModProjectile>("FireblastP").Type;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Apprenticewand");
            recipe.AddIngredient(null, "Windblast");
            recipe.AddIngredient(null, "Waterblast");
            recipe.AddIngredient(null, "Earthblast");
            recipe.AddIngredient(null, "Fireblast");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}