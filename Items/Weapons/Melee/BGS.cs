using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class BGS : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bandos Godsword");
            Tooltip.SetDefault("A brutally heavy sword. \n[c/5cdb7d:Special attack (14 second cooldown): Warstrike]\n[c/5cdb7d:Strike forward dealing up to 500% increased damage based on target's defense with doubled critical strike chance]");
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
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
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, new Vector2(player.direction, 0), Mod.Find<ModProjectile>("BGSspec").Type, Item.damage, Item.knockBack, player.whoAmI, 0, 0);
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/AGSspec"), player.position);
                Item.noUseGraphic = true;
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 60 * 14);
            }
            return base.CanUseItem(player);
        }

    }
}

