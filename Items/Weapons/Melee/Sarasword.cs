using Terraria.DataStructures;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Sarasword : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saradomin Sword");
            Tooltip.SetDefault("The incredible blade of an Icyene\n[c/5cdb7d:Special attack (15 second cooldown): Calls down Saradomin's lightning]");
        }
        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.crit = 4;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shootSpeed = 3.5f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 shootpos = new Vector2(Main.MouseWorld.X, player.position.Y) + new Vector2(0, -600);
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(source, shootpos + new Vector2(-50 + 50 * i, Main.rand.Next(-100, 0)), new Vector2(0, 3.5f), Mod.Find<ModProjectile>("SaraswordSpec").Type, Item.damage / 2, Item.knockBack, player.whoAmI, 0, 0);
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.SaraswordSpec>();
                var thunderSound = SoundID.Thunder;
                thunderSound.Variants = new System.ReadOnlySpan<int>(new int[] { 0 });
                SoundEngine.PlaySound(thunderSound, player.position);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 60 * 15);
            }
            else if (player.altFunctionUse == 2)
            {
                Item.shoot = ProjectileID.None;
                return false;
            }
            else
            {
                Item.shoot = ProjectileID.None;
            }
            return base.CanUseItem(player);
        }

    }
}

