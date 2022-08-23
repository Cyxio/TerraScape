using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Item.damage = 55;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.crit = 5;
            Item.width = 58;
            Item.height = 60;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 10f;
            Item.scale = 0.8f;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation < 20 && player.itemAnimation > 9)
            {
                Item.shoot = ProjectileID.None;
                for (int j = 0; j < 8; j++)
                {
                    Vector2 velo = new Vector2(0, 4 * player.itemAnimation).RotatedBy(MathHelper.ToRadians(45 * j));
                    int dust = Dust.NewDust(player.MountedCenter + velo, 0, 0, DustID.Flare, -velo.X * 0.05f, -velo.Y * 0.05f, 0, Color.Red, 1.4f);
                    Main.dust[dust].noGravity = true;
                }
            }
            if (player.itemAnimation == 8)
            {
                SoundEngine.PlaySound(SoundID.Item20, player.MountedCenter);
                Item.shoot = ModContent.ProjectileType<Projectiles.Ibanblast>();
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}