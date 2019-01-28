using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class Twistedbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twisted Bow");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (20 second cooldown): Take aim for 3 seconds, then gain increased movement speed while barraging nearby enemies with arrows]");
        }
        public override void SetDefaults()
        {
            item.damage = 280;
            item.crit = 21;
            item.ranged = true;
            item.channel = true;
            item.scale = 0.9f;
            item.width = 20;
            item.height = 80;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 25);
            item.rare = 10;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = 10;
            item.shootSpeed = 30f;
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
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType<Projectiles.TwistedbowProj>(), 0, 0, player.whoAmI);
                player.AddBuff(mod.BuffType("SpecCD"), 1200);
                return false;
            }
            return true;
        }
    }
}