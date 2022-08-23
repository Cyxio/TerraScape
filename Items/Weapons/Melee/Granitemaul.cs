using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Granitemaul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Granite Maul");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (10 second cooldown): Strikes 3 times in quick succession]");
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useTurn = false;
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
            return base.CanUseItem(player);
        }
        private int specUse = 2;
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            Item.useAnimation = 50;
            Item.knockBack = 6f;
            if (specUse > 0)
            {
                if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
                {
                    Item.knockBack = 0f;
                    if (player.itemAnimation > 15)
                    {
                        player.itemAnimation = 15;
                    }
                    if (player.itemAnimation == 5)
                    {
                        player.itemAnimation = 15;
                        SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Gmaul"));
                        specUse--;
                    }
                }
            }
            else
            {
                specUse = 2;
                player.AddBuff(ModContent.BuffType<Buffs.SpecCD>(), 600);
            }
        }
    }
}