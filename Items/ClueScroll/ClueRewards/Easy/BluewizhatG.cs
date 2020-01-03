using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Head)]
    public class BluewizhatG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Wizard Hat (g)");
            Tooltip.SetDefault("Increases maximum mana by 20");
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BluewizbodyG") && legs.type == mod.ItemType("BluewizlegsG");
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases maximum mana by 80\n10% increased magic damage";
            player.statManaMax2 += 80;
            player.magicDamage += 0.1f;
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.defense = 2;
            item.rare = 1;
        }
    }
}