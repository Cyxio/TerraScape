using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Head)]
    public class BluewizhatT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Wizard Hat (t)");
            Tooltip.SetDefault("Increases maximum mana by 20");
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("BluewizbodyT").Type && legs.type == Mod.Find<ModItem>("BluewizlegsT").Type;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases maximum mana by 80\n10% increased magic damage";
            player.statManaMax2 += 80;
            player.GetDamage(DamageClass.Magic) += 0.1f;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.defense = 2;
            Item.rare = ItemRarityID.Blue;
        }
    }
}