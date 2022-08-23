using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class Heavyballista : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy ballista");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (10 second cooldown): Fires a charged javelin that takes enemies with it] \nUses stakes as ammunition");
        }
        public override void SetDefaults()
        {
            Item.damage = 380;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.8f;
            Item.width = 60;
            Item.height = 56;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.reuseDelay = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 15);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item99;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Stake;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ballistaproj>();
            Item.shootSpeed = 22f;
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
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                Projectile.NewProjectile(source, position + velocity, velocity, ModContent.ProjectileType<Projectiles.Ballistaproj>(), damage, knockback, player.whoAmI, 3, 0);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 600);
                return false;
            }
            Projectile.NewProjectile(source, position + velocity, velocity, ModContent.ProjectileType<Projectiles.Ballistaproj>(), damage, knockback, player.whoAmI, 0, 0);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 10);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Ballistalimbs");
            recipe.AddIngredient(null, "Ballistaspring");
            recipe.AddIngredient(null, "Monkeytail");
            recipe.AddIngredient(null, "Heavyframe");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}