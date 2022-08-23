using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Apprenticewand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice Wand");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.crit = 4;
            Item.width = 50;
            Item.height = 58;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("FireboltP").Type;
            Item.shootSpeed = 16;
            Item.scale = 0.8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int s = Main.rand.Next(4);
            if (s == 0)
            {
                Item.shoot = Mod.Find<ModProjectile>("WindboltP").Type;
            }
            else if (s == 1)
            {
                Item.shoot = Mod.Find<ModProjectile>("WaterboltP").Type;
            }
            else if (s == 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("EarthboltP").Type;
            }
            else
            {
                Item.shoot = Mod.Find<ModProjectile>("FireboltP").Type;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Beginnerwand");
            recipe.AddIngredient(null, "Windbolt");
            recipe.AddIngredient(null, "Waterbolt");
            recipe.AddIngredient(null, "Earthbolt");
            recipe.AddIngredient(null, "Firebolt");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}