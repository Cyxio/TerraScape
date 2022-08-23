using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using OldSchoolRuneScape;
using Terraria.Chat;
using Terraria.GameContent.UI.Elements;
using ReLogic;
using ReLogic.Graphics;
using System.Runtime.InteropServices;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;

namespace OldSchoolRuneScape.UI
{
    internal class UIclueImage : UIElement
    {
        internal Color backgroundColor = Color.Gray;
        internal static Texture2D _backgroundTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/Items/ClueScroll/ClueTemp").Value;
    }
}
