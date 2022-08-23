using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class Dorgeshuuncrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dorgeshuun Crossbow");
            Tooltip.SetDefault("A bone crossbow from the Lost Tribe \n[c/5cdb7d:Special attack (8 second cooldown): Fires a bolt that deals increased damage to enemies at full health]");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.75f;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = 50000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item99;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = 1;
            Item.shoot = Mod.Find<ModProjectile>("BoneSpec").Type;
            Item.shootSpeed = 40;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                Projectile.NewProjectile(source, position, velocity, Mod.Find<ModProjectile>("BoneSpec").Type, damage, knockback, Item.playerIndexTheItemIsReservedFor, 0, 0);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 480);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 45);
            recipe.AddIngredient(ItemID.Cobweb, 60);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}