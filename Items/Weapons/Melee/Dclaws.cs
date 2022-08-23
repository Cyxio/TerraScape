using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Dclaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Claws");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (30 second cooldown): Supercharges your claws for 8 seconds, making you able to dash with them]");
        }
        public override void SetDefaults()
        {
            Item.damage = 144;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.crit = 21;
            Item.width = 62;
            Item.height = 52;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.knockBack = 2f;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.shootSpeed = 20;
            Item.shoot = Mod.Find<ModProjectile>("Dclaws").Type;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                player.AddBuff(Mod.Find<ModBuff>("Clawbuff").Type, 480);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 1800);
                return false;
            }
            else if (player.altFunctionUse == 2)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
    }
}