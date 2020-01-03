using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
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
            item.damage = 20;
            item.melee = true;
            item.crit = 6;
            item.width = 58;
            item.height = 58;
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 1;
            item.knockBack = 6f;
            item.value = Item.sellPrice(0, 1);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = false;
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
        int specUse = 2;
        public override void UseStyle(Player player)
        {
            item.useAnimation = 50;
            item.knockBack = 6f;
            if (specUse > 0)
            {
                if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
                {
                    item.knockBack = 0f;
                    if (player.itemAnimation > 15)
                    {
                        player.itemAnimation = 15;
                    }
                    if (player.itemAnimation == 5)
                    {
                        player.itemAnimation = 15;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Gmaul"));
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