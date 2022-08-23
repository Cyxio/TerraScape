using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Hard
{
    [AutoloadEquip(EquipType.Head)]
    public class RunehelmAR : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armadyl Full Helm");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("RunebodyAR").Type && legs.type == Mod.Find<ModItem>("RunelegsAR").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased melee damage";
            player.GetDamage(DamageClass.Melee) += 0.2f;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }
    }
}