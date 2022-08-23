using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace OldSchoolRuneScape.Items.Weapons.Melee
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
            Item.damage = 80;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 4;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8f;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.shootSpeed = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, new Vector2(0, 2), Mod.Find<ModProjectile>("D2Hspec").Type, Item.damage, Item.knockBack, player.whoAmI, 0, 0); ;
                Item.UseSound = SoundID.Item45;
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 700);
            }
            else if (player.altFunctionUse == 2)
            {
                Item.UseSound = SoundID.Item1;
                return false;
            }
            else
            {
                Item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }
    }
}