using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Sarasword : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saradomin Sword");
            Tooltip.SetDefault("The incredible blade of an Icyene \n[c/5cdb7d:Special attack (15 second cooldown): Calls down Saradomin's lightning]");
        }
        public override void SetDefaults()
        {
            item.damage = 120;
            item.melee = true;
            item.crit = 4;
            item.width = 58;
            item.height = 58;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 3.5f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 shootpos = new Vector2(Main.MouseWorld.X, player.position.Y) + new Vector2(0, -600);
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(shootpos + new Vector2(-50 + 50*i, Main.rand.Next(-100, 0)), new Vector2(0, 3.5f), mod.ProjectileType("SaraswordSpec"), item.damage / 2, item.knockBack, player.whoAmI, 0, 0);
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                item.shoot = ModContent.ProjectileType<Projectiles.SaraswordSpec>();
                item.UseSound = SoundID.Item72;
                player.AddBuff(mod.BuffType("SpecCD"), 60 * 15);
            }
            else if (player.altFunctionUse == 2)
            {
                item.shoot = 0;
                item.UseSound = SoundID.Item1;
                return false;
            }
            else
            {
                item.shoot = 0;
                item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }
        
    }
}

