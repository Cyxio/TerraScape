using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
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
            item.width = 26;
            item.height = 30;
            item.thrown = true;
            item.consumable = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.noMelee = true;
            item.rare = 3;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.maxStack = 999;
            item.shoot = mod.ProjectileType("Runethrow");
            item.shootSpeed = 8;
            item.value = 600;
            item.noUseGraphic = true;
            item.damage = 20;
            item.knockBack = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                item.useAnimation = 16;
                item.shootSpeed = 16f;
                item.UseSound = SoundID.Item1;
                item.shoot = mod.ProjectileType("RunethrowS");
                player.AddBuff(mod.BuffType("SpecCD"), 180);
            }
            else if (player.altFunctionUse == 2)
            {
                item.shootSpeed = 8;
                item.shoot = 0;
                item.useAnimation = 0;
                item.UseSound = null;
            }
            else
            {
                item.shootSpeed = 8;
                item.useAnimation = 16;
                item.UseSound = SoundID.Item1;
                item.shoot = mod.ProjectileType("Runethrow");
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe rec = new ModRecipe(mod);
            rec.AddIngredient(mod.ItemType("Runitebar"));
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this, 30);
            rec.AddRecipe();
        }
    }
}