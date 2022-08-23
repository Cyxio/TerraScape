using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using OldSchoolRuneScape.Items.Magic;

namespace OldSchoolRuneScape.UI
{
    internal class SpellbookUI_old : UIState
    {
        internal static bool visible = true;
        internal static string texture = "OldSchoolRuneScape/UI/SpellbookBase";
        private bool bookVisible = false;
        private int left = 22;
        private int top = 23 + (int)(431 * Main.inventoryScale);
        public UIImageButton button;
        public override void OnInitialize()
        {
            button = new UIImageButton(Request<Texture2D>("OldSchoolRuneScape/UI/SpellbookIcon"));
            button.Height.Set(23, 0f);
            button.Width.Set(24, 0f);
            button.Left.Set(462, 0f);
            button.Top.Set(34, 0f);
            button.OnClick += new MouseEvent(Button_OnClick);
            base.Append(button);

            UIclueImage parent = new UIclueImage();
            parent.Height.Set(253, 0f);
            parent.Width.Set(202, 0f);
            parent.Left.Set(left, 0f);
            parent.Top.Set(top, 0f);
            parent.backgroundColor = new Color(255, 255, 255, 255);
            base.Append(parent);
        }

        private void Button_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (bookVisible)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                bookVisible = false;
            }
            else
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                bookVisible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Main.playerInventory)
            {
                button.Left.Set(462, 0f);
                button.Top.Set(34, 0f);
                Recalculate();
            }
            else
            {
                button.Left.Set(500, 0f);
                button.Top.Set(33, 0f);
                Recalculate();
            }
            Rectangle icon = new Rectangle((int)button.Left.Pixels, (int)button.Top.Pixels, (int)button.Width.Pixels, (int)button.Height.Pixels);
            if (icon.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (bookVisible)
            {
                Rectangle book = new Rectangle(left, top, 202, 253);
                if (!Main.playerInventory)
                {
                    book = new Rectangle(left, top - 198, 202, 253);
                }
                if (book.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y))
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Player player = Main.LocalPlayer;
                    player.cursorItemIconEnabled = true;
                    player.cursorItemIconID = ItemType<Items.ClueScroll.Cluespawner>();
                    Vector2 coords = ColumnAndRow(Main.MouseScreen.X - book.X, Main.MouseScreen.Y - book.Y);
                    if (coords.X >= 0 && coords.Y >= 0)
                    {
                        int x = (int)MathHelper.Clamp(coords.X, 0, 6);
                        int y = (int)MathHelper.Clamp(coords.Y, 0, 9);
                        int item = items[y, x];
                        string text = SpellText(item);
                        player.cursorItemIconText = text;
                    }
                }
            } 
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            int drawTop = top;
            if (!Main.playerInventory)
            {
                drawTop -= 198;
            }
            if (bookVisible)
            {
                Main.hidePlayerCraftingMenu = true;
                CalculatedStyle dimensions = new CalculatedStyle(left, drawTop, 202, 253);
                Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
                int width = (int)Math.Ceiling(dimensions.Width);
                int height = (int)Math.Ceiling(dimensions.Height);
                spriteBatch.Draw(Request<Texture2D>(texture).Value, new Vector2(point1.X, point1.Y), Color.White);
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    for (int j = 0; j < items.GetLength(1); j++)
                    {
                        int item = items[i, j];
                        if (runeRequirements.ContainsKey(item) && HasRunes(Main.LocalPlayer, runeRequirements[item]))
                        {
                            Texture2D t = Request<Texture2D>("OldSchoolRuneScape/Items/Magic/SpellbookActive").Value;
                            int x = 13 + 26 * j;
                            int y = 8 + 24 * i;
                            Vector2 position = new Vector2(point1.X + x, point1.Y + y);
                            spriteBatch.Draw(t, position, new Rectangle(x, y, 20, 20), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
        }
        public override void Click(UIMouseEvent evt)
        {
            if (bookVisible)
            {
                Rectangle book = new Rectangle(left, top, 202, 253);
                if (!Main.playerInventory)
                {
                    book = new Rectangle(left, top - 198, 202, 253);
                }
                if (book.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y))
                {
                    Vector2 coords = ColumnAndRow(evt.MousePosition.X - book.X, evt.MousePosition.Y - book.Y);
                    if (coords.X >= 0 && coords.Y >= 0 && Main.netMode != NetmodeID.Server)
                    {
                        Player player = Main.LocalPlayer;
                        int x = (int)MathHelper.Clamp(coords.X, 0, 6);
                        int y = (int)MathHelper.Clamp(coords.Y, 0, 9);
                        int item = items[y, x];
                        if (runeRequirements.ContainsKey(item) && HasRunes(player, runeRequirements[item]))
                        {
                            ConsumeRunes(player, runeRequirements[item]);
                            Main.playerInventory = true;
                            Item i = new Item();
                            i.SetDefaults(item);
                            Main.mouseItem = i;
                            SoundEngine.PlaySound(SoundID.Grab, player.position);
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                        }
                    }
                }
            }
        }
        private Vector2 ColumnAndRow(float x, float y)
        {
            if (x >= 13 && x <= 189 && y >= 8 && y <= 244)
            {
                int column = (int)((x - 13) / (176 / 7f));
                int row = (int)((y - 8) / (236 / 10f));
                return new Vector2(column, row);
            }
            return new Vector2(-1, -1);
        }
        private bool HasRunes(Player plr, Dictionary<int, int> runes)
        {
            foreach (KeyValuePair<int, int> pair in runes)
            {
                if (plr.HasItem(pair.Key))
                {
                    foreach (var item in plr.inventory)
                    {
                        if (item.type == pair.Key && item.stack < pair.Value)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        private void ConsumeRunes(Player plr, Dictionary<int, int> runes)
        {
            foreach (KeyValuePair<int, int> pair in runes)
            {
                for (int i = 0; i < plr.inventory.Length; i++)
                {
                    if (plr.inventory[i].type == pair.Key)
                    {
                        plr.inventory[i].stack -= pair.Value;
                    }
                }
            }
        }
        private Dictionary<int, string> toolTips = new Dictionary<int, string> 
        {
            { ItemType<VarrockTeleSpell>(), "Teleports you to a sky island" },
            { ItemType<CamelotTeleSpell>(), "Teleports you to the ocean" },
            { ItemType<FaladorTeleSpell>(), "Teleports you to the dungeon" },
            { ItemType<LumbridgeTeleSpell>(), "Teleports you to hell" },
            { ItemType<Bind>(), "Casts a bolt that slows the enemy for one second" },
            { ItemType<Charge>(), "God spells gain 100% increased damage for 5 minutes" },
            { ItemType<Confuse>(), "Casts a confusing bolt" },
            { ItemType<Entangle>(), "Casts a bolt that slows the enemy for three seconds" },
            { ItemType<Snare>(), "Casts a bolt that slows the enemy for two seconds" },
            { ItemType<Telegrab>(), "Casts a ball that grabs items from a distance" },
        };

        private Dictionary<int, Dictionary<int, int>> runeRequirements = new Dictionary<int, Dictionary<int, int>>
        {
            { ItemType<Windstrike>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 1 },  { ItemType<Mindrune>(), 1 },    { ItemType<Wrathrune>(), 1} } },
            { ItemType<Waterstrike>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 1 },  { ItemType<Waterrune>(), 1 },   { ItemType<Mindrune>(), 1 } } },
            { ItemType<Earthstrike>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 1 },  { ItemType<Earthrune>(), 2 },   { ItemType<Mindrune>(), 1 } } },
            { ItemType<Firestrike>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 2 },  { ItemType<Firerune>(), 3 },    { ItemType<Mindrune>(), 1 } } },
            { ItemType<Windbolt>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 2 },  { ItemType<Chaosrune>(), 1 },    } },
            { ItemType<Waterbolt>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 2 },  { ItemType<Waterrune>(), 2 },   { ItemType<Chaosrune>(), 1 } } },
            { ItemType<Earthbolt>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Earthrune>(), 1 },   { ItemType<Chaosrune>(), 1 } } },
            { ItemType<Firebolt>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Firerune>(), 4 },    { ItemType<Chaosrune>(), 1 } } },
            { ItemType<Windblast>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Deathrune>(), 1 },    } },
            { ItemType<Waterblast>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Waterrune>(), 3 },   { ItemType<Deathrune>(), 1 } } },
            { ItemType<Earthblast>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Earthrune>(), 4 },   { ItemType<Deathrune>(), 1 } } },
            { ItemType<Fireblast>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 4 },  { ItemType<Firerune>(), 5 },    { ItemType<Deathrune>(), 1 } } },
            { ItemType<Windwave>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 5 },  { ItemType<Bloodrune>(), 1 },    } },
            { ItemType<Waterwave>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 5 },  { ItemType<Waterrune>(), 7 },   { ItemType<Bloodrune>(), 1 } } },
            { ItemType<Earthwave>(),  new Dictionary<int, int> { { ItemType<Airrune>(), 5 },  { ItemType<Earthrune>(), 7 },   { ItemType<Bloodrune>(), 1 } } },
            { ItemType<Firewave>(),   new Dictionary<int, int> { { ItemType<Airrune>(), 5 },  { ItemType<Firerune>(), 7 },    { ItemType<Bloodrune>(), 1 } } },
            { ItemType<VarrockTeleSpell>(), new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Firerune>(), 1 },    { ItemType<Lawrune>(), 1 } } },
            { ItemType<FaladorTeleSpell>(), new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Waterrune>(), 1 },    { ItemType<Lawrune>(), 1 } } },
            { ItemType<LumbridgeTeleSpell>(), new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Earthrune>(), 1 },    { ItemType<Lawrune>(), 1 } } },
            { ItemType<CamelotTeleSpell>(), new Dictionary<int, int> { { ItemType<Airrune>(), 5 },  { ItemType<Lawrune>(), 1 } } },
            { ItemType<Confuse>(), new Dictionary<int, int> { { ItemType<Earthrune>(), 2 },  { ItemType<Waterrune>(), 3 },    { ItemType<Bodyrune>(), 1 } } },
            { ItemType<Bind>(), new Dictionary<int, int> { { ItemType<Earthrune>(), 3 },  { ItemType<Waterrune>(), 3 },    { ItemType<Naturerune>(), 2 } } },
            { ItemType<Snare>(), new Dictionary<int, int> { { ItemType<Earthrune>(), 4 },  { ItemType<Waterrune>(), 4 },    { ItemType<Naturerune>(), 3 } } },
            { ItemType<Entangle>(), new Dictionary<int, int> { { ItemType<Earthrune>(), 5 },  { ItemType<Waterrune>(), 5 },    { ItemType<Naturerune>(), 4 } } },
            { ItemType<Charge>(), new Dictionary<int, int> { { ItemType<Airrune>(), 3 },  { ItemType<Firerune>(), 3 },    { ItemType<Bloodrune>(), 3 } } },
            { ItemType<Telegrab>(), new Dictionary<int, int> { { ItemType<Airrune>(), 1 },  { ItemType<Lawrune>(), 1 } } },
        };
        private int[,] items = new int[,]
        {
            { ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Confuse>(), ItemType<Windstrike>(), ItemType<Waterstrike>(), ItemType<Windstrike>(), ItemType<Earthstrike>() },
            { ItemType<Windstrike>(), ItemType<Firestrike>(), ItemType<Windstrike>(), ItemType<Windbolt>(), ItemType<Windstrike>(), ItemType<Bind>(), ItemType<Windstrike>() },
            { ItemType<Waterbolt>(), ItemType<VarrockTeleSpell>(), ItemType<Windstrike>(), ItemType<Earthbolt>(), ItemType<LumbridgeTeleSpell>(), ItemType<Telegrab>(), ItemType<Firebolt>() },
            { ItemType<FaladorTeleSpell>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windblast>(), ItemType<Windstrike>(), ItemType<CamelotTeleSpell>(), ItemType<Waterblast>() },
            { ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Snare>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Earthblast>(), ItemType<Windstrike>() },
            { ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Fireblast>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>() },
            { ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windwave>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Waterwave>() },
            { ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Earthwave>(), ItemType<Windstrike>(), ItemType<Windstrike>() },
            { ItemType<Firewave>(), ItemType<Entangle>(), ItemType<Windstrike>(), ItemType<Charge>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>() },
            { ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>(), ItemType<Windstrike>() },
        };
        private string SpellText(int item)
        {
            Player player = Main.LocalPlayer;
            Item i = new Item();
            i.SetDefaults(item);
            int rare = (int)MathHelper.Clamp(i.rare - 1, 0, 10);
            string s = $"[c/{rarityColors[rare]}:{i.Name}] ";
            foreach (KeyValuePair<int, int> reqs in runeRequirements[item])
            {
                s += $"[i/s{reqs.Value}:{reqs.Key}]";
            }
            int damage = player.GetWeaponDamage(i);
            if (damage > 0)
            {
                s += "\n";
                s += damage + " magic damage\n";
                s += $"{player.GetCritChance(DamageClass.Magic) - player.inventory[player.selectedItem].crit + i.crit}% critical strike chance\n";
                s += GetSpeed(i.useTime) + " speed\n";
                s += GetKnockback(player.GetWeaponKnockback(i, i.knockBack)) + " knockback";
            }
            if (toolTips.ContainsKey(item))
            {
                s += "\n" + toolTips[item];
            }
            return s;
        }
        static string[] rarityColors = new string[]
        {
            "9696FF",
            "96FF96",
            "FFC896",
            "FF9696",
            "FF96FF",
            "D2A0FF",
            "96FF0A",
            "FFFF0A",
            "05C8FF",
            "FF2864",
            "B428FF",
        };
        static string[] useSpeeds = new string[]
        {
            "Insanely fast",
            "Very fast",
            "Fast",
            "Average",
            "Slow",
            "Very slow",
            "Extremely slow",
            "Snail"
        };
        public static string GetSpeed(int useSpeed)
        {
            if (useSpeed <= 8)
            {
                return useSpeeds[0];
            }
            else if (useSpeed <= 20)
            {
                return useSpeeds[1];
            }
            else if (useSpeed <= 25)
            {
                return useSpeeds[2];
            }
            else if (useSpeed <= 30)
            {
                return useSpeeds[3];
            }
            else if (useSpeed <= 35)
            {
                return useSpeeds[4];
            }
            else if (useSpeed <= 45)
            {
                return useSpeeds[5];
            }
            else if (useSpeed <= 55)
            {
                return useSpeeds[6];
            }
            else
            {
                return useSpeeds[7];
            }
        }

        static string[] knockbackPowers = new string[]
        {
                    "No",
                    "Extremely weak",
                    "Very weak",
                    "Weak",
                    "Average",
                    "Strong",
                    "Very strong",
                    "Extremely strong",
                    "Insane"
        };

        public static string GetKnockback(float knockback)
        {
            if (knockback == 0f)
            {
                return knockbackPowers[0];
            }
            else if ((double)knockback <= 1.5)
            {
                return knockbackPowers[1];
            }
            else if (knockback <= 3f)
            {
                return knockbackPowers[2];
            }
            else if (knockback <= 4f)
            {
                return knockbackPowers[3];
            }
            else if (knockback <= 6f)
            {
                return knockbackPowers[4];
            }
            else if (knockback <= 7f)
            {
                return knockbackPowers[5];
            }
            else if (knockback <= 9f)
            {
                return knockbackPowers[6];
            }
            else if (knockback <= 11f)
            {
                return knockbackPowers[7];
            }
            else
            {
                return knockbackPowers[8];
            }
        }
    }
}
