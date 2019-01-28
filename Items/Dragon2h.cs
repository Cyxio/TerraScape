using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Dragon2h : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon 2h Sword");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (12 second cooldown): Strikes downward, creating damaging waves on the ground]");
        }
        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.crit = 4;
            item.width = 54;
            item.height = 54;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 1;
            item.knockBack = 8f;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = false;
            item.shootSpeed = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD == false)
            {
                Projectile.NewProjectile(player.Center, new Vector2(0, 2), mod.ProjectileType("D2Hspec"), item.damage, item.knockBack, player.whoAmI, 0, 0);
                item.UseSound = SoundID.Item45;
                player.AddBuff(mod.BuffType("SpecCD"), 700);
            }
            else if (player.altFunctionUse == 2)
            {
                item.UseSound = SoundID.Item1;
                return false;
            } else
            {
                item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }
    }
}