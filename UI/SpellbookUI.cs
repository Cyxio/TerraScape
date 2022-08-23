using ReLogic.Graphics;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using ReLogic.OS;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OldSchoolRuneScape.Items.Magic;
using System;

namespace OldSchoolRuneScape.UI
{
    internal class SpellbookUI : UIState
    {
        public static Asset<Texture2D>? spellbookBaseTexture;
        public static Asset<Texture2D>? spellbookActiveTexture;
        public static Asset<Texture2D>? spellbookInactiveTexture;
        public static Asset<Texture2D>? spellbookSelectedTexture;
        public static Asset<Texture2D>? spellbookLockTexture;
        public static Asset<Texture2D>? spellbookIconTexture;

        private int[] spellList = new int[] { 
            ModContent.ItemType<WindStrike>(),
            ModContent.ItemType<WaterStrike>(),
            ModContent.ItemType<EarthStrike>(),
            ModContent.ItemType<FireStrike>() 
        };
        public static bool Visible
        {
            get { return SpellbookUISystem.spellbookInterface.CurrentState == SpellbookUISystem.Instance.spellbookUI; }
            set { SpellbookUISystem.spellbookInterface.SetState(value ? SpellbookUISystem.Instance.spellbookUI : null); }
        }
        public override void OnInitialize()
        {
            spellbookBaseTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookBase", AssetRequestMode.ImmediateLoad);
            spellbookActiveTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookActive", AssetRequestMode.ImmediateLoad);
            spellbookInactiveTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookInactive", AssetRequestMode.ImmediateLoad);
            spellbookSelectedTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookSelected", AssetRequestMode.ImmediateLoad);
            spellbookLockTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookLock", AssetRequestMode.ImmediateLoad);
            spellbookIconTexture = ModContent.Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookIcon", AssetRequestMode.ImmediateLoad);
            UIImage image = new UIImage(spellbookBaseTexture);
            image.Top.Pixels = Main.screenHeight - spellbookBaseTexture.Height() - 20;
            image.Left.Pixels = 20;
            Append(image); 
            foreach (var item in spellList)
            {
                SpellCopy spell = (SpellCopy)ModContent.GetModItem(item);
                spell.AdditionalDefaults();
                SpellButton spell2 = new SpellButton(spellbookIconTexture,
                    spell, spell.Column, spell.Row);
                //InvisibleItemSlot itemSlot = new InvisibleItemSlot(
                  //  new Item[] { ModContent.GetModItem(item).Item }, 0, 14);
                //spell2.Append(itemSlot);
                image.Append(spell2);
            }
        }
        public override void Update(GameTime gameTime)
        {
            Main.hidePlayerCraftingMenu = true;
            base.Update(gameTime);
        }
    }

    internal class InvisibleItemSlot : UIItemSlot
    {
        public InvisibleItemSlot(Item[] itemArray, int itemIndex, int itemSlotContext) : base(itemArray, itemIndex, itemSlotContext)
        {
            Width.Pixels = 22;
            Height.Pixels = 22;
        }
        public override void RightClick(UIMouseEvent evt)
        {
            // disable
        }
    }

    internal class SpellButton : UIImageButton
    {
        private bool _runes;
        private bool _locked;
        private SpellCopy _spell;
        public SpellButton(Asset<Texture2D> texture, SpellCopy spell, int column, int row) : base(texture)
        {
            _runes = false;
            _locked = true;
            _spell = spell;
            MarginLeft = 13 + (26 * column);
            MarginTop = 8 + (24 * row);
        }
        public override void Update(GameTime gameTime)
        {
            _locked = !_spell.Unlocked();
            _runes = _spell.PlayerHasRunes(Main.LocalPlayer);
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            base.Update(gameTime);
        }
        public override void RightClick(UIMouseEvent evt)
        {
            Player player = Main.LocalPlayer;
            if (player.HeldItem.ModItem.GetType().IsSubclassOf(typeof(SpellStaff)))
            {
                ((SpellStaff)player.HeldItem.ModItem).SelectSpell(_spell);
                SoundEngine.PlaySound(SoundID.Item29);
            }
            base.RightClick(evt);
        }
        public override void Click(UIMouseEvent evt)
        {
            Player player = Main.LocalPlayer;
            OSRSplayer osrsPlayer = player.GetModPlayer<OSRSplayer>();
            int spellCooldown = osrsPlayer.spellCooldown;
            if (spellCooldown == 0 && _runes && !_locked)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);
                osrsPlayer.selectedSpell = _spell;
            }
            base.Click(evt);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (base.IsMouseHovering) 
            {
                Main.LocalPlayer.mouseInterface = true;
                Item inv = _spell.Item;
                ItemSlot.OverrideHover(ref inv, 21);
                ItemSlot.MouseHover(ref inv, 21);
            }
            var x = (int)MarginLeft - 1;
            var y = (int)MarginTop - 1;
            var size = 22;
            if (_runes) 
            {
                spriteBatch.Draw(SpellbookUI.spellbookActiveTexture.Value, new Vector2(Parent.Left.Pixels + MarginLeft, Parent.Top.Pixels + MarginTop),
                    new Rectangle(x, y, size, size), new Color(1f, 1f, 1f, 1f));
            }
            else
            {
                spriteBatch.Draw(SpellbookUI.spellbookInactiveTexture.Value, new Vector2(Parent.Left.Pixels + MarginLeft, Parent.Top.Pixels + MarginTop),
                    new Rectangle(x, y, size, size), new Color(1f, 1f, 1f, 1f));
            }

            if (_locked)
            {
                spriteBatch.Draw(SpellbookUI.spellbookLockTexture.Value, new Vector2(Parent.Left.Pixels + MarginLeft + 12, Parent.Top.Pixels + MarginTop),
                    new Color(1f, 1f, 1f, 1f));
            }
            else if (Main.LocalPlayer.GetModPlayer<OSRSplayer>().selectedSpell == _spell)
            {
                spriteBatch.Draw(SpellbookUI.spellbookSelectedTexture.Value, new Vector2(Parent.Left.Pixels + MarginLeft, Parent.Top.Pixels + MarginTop),
                    new Rectangle(x, y, size, size), new Color(1f, 1f, 1f, 1f));
            }
        }
    }
}
