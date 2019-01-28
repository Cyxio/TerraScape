using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class Dorgeshuuncrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dorgeshuun Crossbow");
            Tooltip.SetDefault("A bone crossbow from the Lost Tribe \n[c/5cdb7d:Special attack (8 second cooldown): Fires a bolt that deals increased damage to enemies at full health]");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.scale = 0.75f;
            item.width = 56;
            item.height = 56;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = 50000;
            item.rare = 2;
            item.UseSound = SoundID.Item99;
            item.autoReuse = false;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = AmmoID.Dart;
            item.shoot = mod.ProjectileType("BoneSpec");
            item.shootSpeed = 40;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD == false)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("BoneSpec"), damage, knockBack, item.owner, 0, 0);
                player.AddBuff(mod.BuffType("SpecCD"), 480);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 45);
            recipe.AddIngredient(ItemID.Cobweb, 60);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}