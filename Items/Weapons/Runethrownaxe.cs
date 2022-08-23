using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons
{
    public class Runethrownaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Thrownaxe");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (3 second cooldown): Throws an enchanted thrownaxe which seeks targets behind it after hit]");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.DamageType = DamageClass.Throwing;
            Item.consumable = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.maxStack = 999;
            Item.shoot = Mod.Find<ModProjectile>("Runethrow").Type;
            Item.shootSpeed = 8;
            Item.value = 600;
            Item.noUseGraphic = true;
            Item.damage = 20;
            Item.knockBack = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                Item.useAnimation = 16;
                Item.shootSpeed = 16f;
                Item.UseSound = SoundID.Item1;
                Item.shoot = Mod.Find<ModProjectile>("RunethrowS").Type;
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 180);
            }
            else if (player.altFunctionUse == 2)
            {
                Item.shootSpeed = 8;
                Item.shoot = ProjectileID.None;
                Item.useAnimation = 0;
                Item.UseSound = null;
            }
            else
            {
                Item.shootSpeed = 8;
                Item.useAnimation = 16;
                Item.UseSound = SoundID.Item1;
                Item.shoot = Mod.Find<ModProjectile>("Runethrow").Type;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(30);
            rec.AddIngredient(Mod.Find<ModItem>("Runitebar").Type);
            rec.AddTile(TileID.Anvils);
            rec.Register();
        }
    }
}