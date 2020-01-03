using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace OldSchoolRuneScape
{
    public class SlayerTokenCurrency : CustomCurrencySingleCoin
    {
        public Color SlayerTokenCurrencyTextColor = Colors.RarityAmber;

        public SlayerTokenCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        {
        }

        public override void GetPriceText(string[] lines, ref int currentLine, int price)
        {
            Color color = SlayerTokenCurrencyTextColor;
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
                {
                    color.R,
                    color.G,
                    color.B,
                    Language.GetTextValue("LegacyTooltip.50"),
                    price,
                    "Slayer Tokens"
                });
        }
    }
}
