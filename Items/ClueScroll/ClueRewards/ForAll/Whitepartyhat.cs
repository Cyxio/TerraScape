using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.ForAll
{
    [AutoloadEquip(EquipType.Head)]
    public class Whitepartyhat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Partyhat");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.vanity = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(10);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].OverrideColor = new Color(252, 251, 251);
        }
    }
}
