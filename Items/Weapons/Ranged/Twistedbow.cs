using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
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
            Item.damage = 280;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.scale = 0.9f;
            Item.width = 20;
            Item.height = 80;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 25);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 30f;
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
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<Projectiles.TwistedbowProj>(), 0, 0, player.whoAmI);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 1200);
                return false;
            }
            return true;
        }
    }
}