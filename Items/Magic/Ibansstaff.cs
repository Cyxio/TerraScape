using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Ibansstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iban's Staff");
            Tooltip.SetDefault("'An ancient staff, formerly the property of Iban'");
        }
        public override void SetDefaults()
        {
            item.damage = 55;
            item.magic = true;
            item.mana = 12;
            item.crit = 5;
            item.width = 58;
            item.height = 60;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 6;
            item.autoReuse = true;
            item.shoot = 0;
            item.shootSpeed = 10f;
            item.scale = 0.8f;
        }
        public override void UseStyle(Player player)
        {
            if (player.itemAnimation < 20 && player.itemAnimation > 9)
            {
                item.shoot = 0;
                for (int j = 0; j < 8; j++)
                {
                    Vector2 velo = new Vector2(0, 4 * player.itemAnimation).RotatedBy(MathHelper.ToRadians(45 * j));
                    int dust = Dust.NewDust(player.MountedCenter + velo, 0, 0, 127, -velo.X * 0.05f, -velo.Y * 0.05f, 0, Color.Red, 1.4f);
                    Main.dust[dust].noGravity = true;
                }
            }
            if (player.itemAnimation == 8)
            {
                Main.PlaySound(SoundID.Item20, player.MountedCenter);
                item.shoot = ModContent.ProjectileType<Projectiles.Ibanblast>();
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}