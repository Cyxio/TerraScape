using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;

namespace OldSchoolRuneScape
{
	internal class OldSchoolRuneScape : Mod
	{
		public OldSchoolRuneScape()
		{
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
		}
        public static int easyClueCount = 31;
        public static int mediumClueCount = 36;
        public static int hardClueCount = 39;
        public static int eliteClueCount = 38;
        public static int masterClueCount = 33;
        internal ClueUI clueUI;
        internal UserInterface clueInterface;
        internal ClueRewardUI clueRewardUI;
        internal UserInterface clueRewardInterface;
        public override void Load()
        {
            /*AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MainTheme"), ItemType("MainThemeBoxItem"), TileType("MainThemeBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MonkeyBadness"), ItemType("OlmMusicBoxItem"), TileType("OlmMusicBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Attack2"), ItemType("ElvargMusicBoxItem"), TileType("ElvargMusicBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/EverlastingFire"), ItemType("ChaosMusicBoxItem"), TileType("ChaosMusicBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DangerousWay"), ItemType("BarrowsMusicBoxItem"), TileType("BarrowsMusicBox"));*/
            if (!Main.dedServ)
            {
                clueInterface = new UserInterface();
                clueUI = new ClueUI();
                ClueUI.visible = false;
                clueInterface.SetState(clueUI);
                clueRewardInterface = new UserInterface();
                clueRewardUI = new ClueRewardUI();
                ClueRewardUI.visible = false;
                clueRewardInterface.SetState(clueRewardUI);
            }
            ModTranslation text = CreateTranslation("ClueStepMP");
            text.SetDefault("Fuck Off");
            AddTranslation(text);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(layers.Count - 14, new LegacyGameInterfaceLayer("ClueScroll",
                delegate
                {
                    if (ClueUI.visible)
                    {
                        clueInterface.Update(Main._drawInterfaceGameTime);
                        clueUI.Draw(Main.spriteBatch);
                    }
                    return true;
                },
                InterfaceScaleType.UI)
            );
            layers.Insert(layers.Count - 14, new LegacyGameInterfaceLayer("ClueReward",
                delegate
                {
                    if (ClueRewardUI.visible)
                    {
                        clueRewardInterface.Update(Main._drawInterfaceGameTime);
                        clueRewardUI.Draw(Main.spriteBatch);
                    }
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}
