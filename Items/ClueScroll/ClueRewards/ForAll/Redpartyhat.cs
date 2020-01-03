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
    public class Redpartyhat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Partyhat");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.vanity = true;
            item.rare = 1;
            item.value = Item.sellPrice(10);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(163, 35, 18);
        }
    }
}
