using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OldSchoolRuneScape.UI
{
    [Autoload(Side = ModSide.Client)]
    public sealed class SpellbookUISystem : ModSystem
    {
        public static SpellbookUISystem Instance { get; private set; }

        internal static UserInterface spellbookInterface;
        internal SpellbookUI spellbookUI;

        public override void Load()
        {
            Instance = this;
            if (!Main.dedServ)
            {
                spellbookInterface = new UserInterface();
                spellbookUI = new SpellbookUI();
                spellbookUI.Activate();
            }
        }

        public override void Unload()
        {
            spellbookUI?.Deactivate();
            spellbookUI = null;
        }

        private GameTime _lastUpdateUiGameTime;
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (spellbookInterface?.CurrentState != null)
            {
                spellbookInterface.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && spellbookInterface?.CurrentState != null)
                        {
                            spellbookInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
        /*private static Asset<Texture2D>? spellbookBaseTexture;
        private static Asset<Texture2D>? spellbookActiveTexture;
        private static Asset<Texture2D>? spellbookIconTexture;
        private static LegacyGameInterfaceLayer? layer;
        public override void Load()
        {
            spellbookBaseTexture = Mod.Assets.Request<Texture2D>("UI/Spellbook");
            spellbookActiveTexture = Mod.Assets.Request<Texture2D>("UI/Spellbook_Active");
            spellbookIconTexture = Mod.Assets.Request<Texture2D>("UI/Spellbook_Active");

            layer = new LegacyGameInterfaceLayer($"{nameof(OldSchoolRuneScape)}: Spellbook", () =>
            {
                if (!spellbookBaseTexture.IsLoaded || !spellbookActiveTexture.IsLoaded)
                {
                    return true;
                }

                var player = Main.LocalPlayer;

                if (player?.active != true || !player.TryGetModPlayer(out OSRSplayer osrsPlayer))
                {
                    return true;
                }

                var baseTexture = spellbookBaseTexture.Value;
                var activeTexture = spellbookActiveTexture.Value;

                var basePosition = Vector2.Zero + new Vector2(20f, Main.screenHeight - baseTexture.Height - 20f);
                var drawColor = new Color(1f, 1f, 1f, 1f);

                Main.spriteBatch.Draw(baseTexture, basePosition, drawColor);
                if (player.velocity.Y < 0)
                {
                    Main.spriteBatch.Draw(activeTexture, basePosition, drawColor);
                }

                return true;
            });
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (layer == null)
            {
                return;
            }

            int preferredIndex = layers.FindIndex(l => l.Name == "Vanilla: Cursor");

            layers.Insert(preferredIndex < 0 ? 0 : preferredIndex, layer);
        }*/
    }
}
