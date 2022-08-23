using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class SGS : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saradomin Godsword");
            Tooltip.SetDefault("A gracious, heavy sword. \n[c/5cdb7d:Special attack (20 second cooldown): Healing Blade]\n[c/5cdb7d:Strike upward dealing 200% increased damage with doubled critical strike chance and heal for 10% of the damage dealt]");
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.crit = 4;
            Item.width = 76;
            Item.height = 86;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shootSpeed = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
            }
            else
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                return false;
            }
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                Item.noMelee = true;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, new Vector2(player.direction, 0), Mod.Find<ModProjectile>("SGSspec").Type, (int)(Item.damage * 2f), Item.knockBack, player.whoAmI, 0, 0);
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/AGSspec"), player.position);
                Item.noUseGraphic = true;
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 60 * 20);
            }
            return base.CanUseItem(player);
        }

    }
}

