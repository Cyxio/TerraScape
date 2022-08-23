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
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.crit = 16;
            Item.width = 62;
            Item.height = 64;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WindsurgeP").Type;
            Item.shootSpeed = 20;
            Item.scale = 0.8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            /*int s = Main.rand.Next(4);
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
            }*/
            return true;
        }

        public override Vector2? HoldoutOrigin()
        {
            return new Vector2(0, 16);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Teacherwand");
            recipe.AddIngredient(null, "Windwave");
            recipe.AddIngredient(null, "Waterwave");
            recipe.AddIngredient(null, "Earthwave");
            recipe.AddIngredient(null, "Firewave");
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}