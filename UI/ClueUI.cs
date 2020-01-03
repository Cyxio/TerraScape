using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;

namespace OldSchoolRuneScape.UI
{
    internal class ClueUI: UIState
    {
        internal static bool visible = false;
        internal static string texture = "OldSchoolRuneScape/Items/ClueScroll/ClueTemp";
        public override void OnInitialize()
        {
            UIimage parent = new UIimage();
            parent.Height.Set(296, 0f);
            parent.Width.Set(324, 0f);
            parent.Left.Set(Main.screenWidth / 2 - parent.Width.Pixels / 2, 0f);
            parent.Top.Set(Main.screenHeight / 2 - parent.Height.Pixels / 2, 0f);
            parent.backgroundColor = new Color(255, 255, 255, 255);
            base.Append(parent);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player p = Main.player[Main.myPlayer];
            if (p.channel)
            {
                if (p.inventory[p.selectedItem].type == ModContent.ItemType<Items.ClueScroll.EasyClue>() ||
                    p.inventory[p.selectedItem].type == ModContent.ItemType<Items.ClueScroll.MediumClue>() ||
                    p.inventory[p.selectedItem].type == ModContent.ItemType<Items.ClueScroll.HardClue>() ||
                    p.inventory[p.selectedItem].type == ModContent.ItemType<Items.ClueScroll.EliteClue>() ||
                    p.inventory[p.selectedItem].type == ModContent.ItemType<Items.ClueScroll.MasterClue>())
                {
                    CalculatedStyle dimensions = new CalculatedStyle(Main.screenWidth / 2 - 324 / 2, Main.screenHeight / 2 - 296 / 2, 324, 296);
                    Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
                    int width = (int)Math.Ceiling(dimensions.Width);
                    int height = (int)Math.Ceiling(dimensions.Height);
                    spriteBatch.Draw(ModContent.GetTexture(texture), new Rectangle(point1.X, point1.Y, width, height), Color.White);
                }
            }
        }
    }
}
