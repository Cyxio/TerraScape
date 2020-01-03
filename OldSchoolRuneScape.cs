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
using Terraria.GameContent.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using System;

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
        public static int elvargMusic;
        public static int chaoseleMusic;
        public static int barrowsMusic;
        public static int olmMusic;
        public static int slayerMusic;

        public static int SlayerTokenCurrencyInt;

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
            slayerMusic = GetSoundSlot(SoundType.Music, "Sounds/Music/UpperDepths");
            elvargMusic = GetSoundSlot(SoundType.Music, "Sounds/Music/Attack2");
            chaoseleMusic = GetSoundSlot(SoundType.Music, "Sounds/Music/EverlastingFire");
            barrowsMusic = GetSoundSlot(SoundType.Music, "Sounds/Music/DangerousWay");
            olmMusic = GetSoundSlot(SoundType.Music, "Sounds/Music/MonkeyBadness");
            /*slayerMusic = MusicID.Boss1;
            elvargMusic = MusicID.Boss1;
            chaoseleMusic = MusicID.Boss2;
            barrowsMusic = MusicID.Boss3;
            olmMusic = MusicID.LunarBoss;*/
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MainTheme"), ItemType("MainThemeBoxItem"), TileType("MainThemeBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MonkeyBadness"), ItemType("OlmMusicBoxItem"), TileType("OlmMusicBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Attack2"), ItemType("ElvargMusicBoxItem"), TileType("ElvargMusicBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/EverlastingFire"), ItemType("ChaosMusicBoxItem"), TileType("ChaosMusicBox"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DangerousWay"), ItemType("BarrowsMusicBoxItem"), TileType("BarrowsMusicBox"));
            SlayerTokenCurrencyInt = CustomCurrencyManager.RegisterCurrency(new SlayerTokenCurrency(ModContent.ItemType<Items.SlayerToken>(), 999L));
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

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBoss", 1.9f, ModContent.NPCType<NPCs.Slayer.CrushingHand>(), this, "Crushing Hand", (Func<bool>)(() => OSRSworld.slayBossProgress >= 1),
                    ModContent.ItemType<NPCs.Slayer.CrawlingSummon>(), null, ModContent.ItemType<Items.SlayerToken>(), 
                    "Fought at night. Summoning item obtained from any slayer master after completing 10 slayer tasks.");
                bossChecklist.Call("AddBoss", 4.1f, ModContent.NPCType<NPCs.Slayer.ScreamingBanshee>(), this, "Screaming Banshee", (Func<bool>)(() => OSRSworld.slayBossProgress >= 2),
                    ModContent.ItemType<NPCs.Slayer.BansheeSummon>(), null, ModContent.ItemType<Items.SlayerToken>(),
                    "Fought at night. Summoning item obtained from any slayer master after completing 20 slayer tasks.");
                bossChecklist.Call("AddBoss", 6.1f, ModContent.NPCType<NPCs.Slayer.AbhorrentSpectre>(), this, "Abhorrent Spectre", (Func<bool>)(() => OSRSworld.slayBossProgress >= 3),
                    ModContent.ItemType<NPCs.Slayer.SpectreSummon>(), null, ModContent.ItemType<Items.SlayerToken>(),
                    "Fought at night. Summoning item obtained from any slayer master after completing 30 slayer tasks.");
                bossChecklist.Call("AddBoss", 9.9f, ModContent.NPCType<NPCs.Slayer.MarbleGargoyle>(), this, "Marble Gargoyle", (Func<bool>)(() => OSRSworld.slayBossProgress >= 4),
                    ModContent.ItemType<NPCs.Slayer.MarbleSummon>(), null, ModContent.ItemType<Items.SlayerToken>(),
                    "Fought at night. Summoning item obtained from any slayer master after completing 40 slayer tasks.");
                bossChecklist.Call("AddBoss", 5.9f, ModContent.NPCType<NPCs.Elvarg.Elvarg>(), this, "Elvarg", (Func<bool>)(() => OSRSworld.downedElvarg),
                    ModContent.ItemType<NPCs.Elvarg.CrandorMap>(), ModContent.ItemType<Tiles.ElvargMusicBoxItem>(), 
                    new List<int> { ModContent.ItemType<Items.Greendhide>(), ModContent.ItemType<Items.Mysticcomponents>(), ModContent.ItemType<Items.Excalibur>(), ModContent.ItemType<Items.Accessories.Boltenchant>(), ModContent.ItemType<NPCs.Elvarg.ElvargBag>() },
                    "Use a [i:" + ModContent.ItemType<NPCs.Elvarg.CrandorMap>() + "].");
                bossChecklist.Call("AddBoss", 6.5f, ModContent.NPCType<NPCs.Chaoselemental.Chaoselemental>(), this, "The Chaos Elemental", (Func<bool>)(() => OSRSworld.downedChaosEle),
                    ModContent.ItemType<NPCs.Chaoselemental.Chaossummon>(), ModContent.ItemType<Tiles.ChaosMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Dds>(), ModContent.ItemType<Items.Dragon2h>(), ModContent.ItemType<Items.Dragonscimitar>(), ModContent.ItemType<Items.Magic.Ibansstaff>(), ModContent.ItemType<Items.Dragonhuntercrossbow>(), ModContent.ItemType<Items.Dragonstone>(), ModContent.ItemType<Items.Dragonspear>(), ModContent.ItemType<Items.Magicshortbow>(), ModContent.ItemType<Items.Ammo.Dragonbolttips>(), ModContent.ItemType<NPCs.Chaoselemental.ChaosBag>() },
                    "Use a [i:" + ModContent.ItemType<NPCs.Chaoselemental.Chaossummon>() + "] at night.");
                bossChecklist.Call("AddBoss", 9.1f, ModContent.NPCType<NPCs.Barrows.Ahrim.Ahrim>(), this, "Ahrim", (Func<bool>)(() => OSRSworld.downedAhrim),
                    new List<int> { ModContent.ItemType<NPCs.Barrows.Ahrimsummon>(), ModContent.ItemType<NPCs.Barrows.Cryptspade>() }, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Armor.Ahrimhelm>(), ModContent.ItemType<Items.Armor.Ahrimbody>(), ModContent.ItemType<Items.Armor.Ahrimlegs>(), ModContent.ItemType<Items.Magic.Ahrimstaff>(), ModContent.ItemType<NPCs.Barrows.Ahrim.Ahrimbag>() },
                    "Using a [i:" + ModContent.ItemType<NPCs.Barrows.Cryptspade>() + "] will summon one brother at random. Defeating a brother will drop its respective summoning item.");
                bossChecklist.Call("AddBoss", 9.1f, ModContent.NPCType<NPCs.Barrows.Dharok.Dharok>(), this, "Dharok", (Func<bool>)(() => OSRSworld.downedDharok),
                    new List<int> { ModContent.ItemType<NPCs.Barrows.Dharoksummon>(), ModContent.ItemType<NPCs.Barrows.Cryptspade>() }, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Armor.Dharokhelm>(), ModContent.ItemType<Items.Armor.Dharokbody>(), ModContent.ItemType<Items.Armor.Dharoklegs>(), ModContent.ItemType<Items.Dharokaxe>(), ModContent.ItemType<NPCs.Barrows.Dharok.Dharokbag>() },
                    "Using a [i:" + ModContent.ItemType<NPCs.Barrows.Cryptspade>() + "] will summon one brother at random. Defeating a brother will drop its respective summoning item.");
                bossChecklist.Call("AddBoss", 9.1f, ModContent.NPCType<NPCs.Barrows.Torag.Torag>(), this, "Torag", (Func<bool>)(() => OSRSworld.downedTorag),
                    new List<int> { ModContent.ItemType<NPCs.Barrows.Toragsummon>(), ModContent.ItemType<NPCs.Barrows.Cryptspade>() }, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Armor.Toraghelm>(), ModContent.ItemType<Items.Armor.Toragbody>(), ModContent.ItemType<Items.Armor.Toraglegs>(), ModContent.ItemType<Items.Toraghammers>(), ModContent.ItemType<NPCs.Barrows.Torag.Toragbag>() },
                    "Using a [i:" + ModContent.ItemType<NPCs.Barrows.Cryptspade>() + "] will summon one brother at random. Defeating a brother will drop its respective summoning item.");
                bossChecklist.Call("AddBoss", 9.1f, ModContent.NPCType<NPCs.Barrows.Karil.Karil>(), this, "Karil", (Func<bool>)(() => OSRSworld.downedKaril),
                    new List<int> { ModContent.ItemType<NPCs.Barrows.Karilsummon>(), ModContent.ItemType<NPCs.Barrows.Cryptspade>() }, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Armor.Karilhelm>(), ModContent.ItemType<Items.Armor.Karilbody>(), ModContent.ItemType<Items.Armor.Karillegs>(), ModContent.ItemType<Items.Karilcrossbow>(), ModContent.ItemType<Items.Ammo.Boltrack>(), ModContent.ItemType<NPCs.Barrows.Karil.Karilbag>() },
                    "Using a [i:" + ModContent.ItemType<NPCs.Barrows.Cryptspade>() + "] will summon one brother at random. Defeating a brother will drop its respective summoning item.");
                bossChecklist.Call("AddBoss", 9.1f, ModContent.NPCType<NPCs.Barrows.Guthan.Guthan>(), this, "Guthan", (Func<bool>)(() => OSRSworld.downedGuthan),
                    new List<int> { ModContent.ItemType<NPCs.Barrows.Guthansummon>(), ModContent.ItemType<NPCs.Barrows.Cryptspade>() }, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Armor.Guthanhelm>(), ModContent.ItemType<Items.Armor.Guthanbody>(), ModContent.ItemType<Items.Armor.Guthanlegs>(), ModContent.ItemType<Items.Guthanspear>(), ModContent.ItemType<NPCs.Barrows.Guthan.Guthanbag>() },
                    "Using a [i:" + ModContent.ItemType<NPCs.Barrows.Cryptspade>() + "] will summon one brother at random. Defeating a brother will drop its respective summoning item.");
                bossChecklist.Call("AddBoss", 9.1f, ModContent.NPCType<NPCs.Barrows.Verac.Verac>(), this, "Verac", (Func<bool>)(() => OSRSworld.downedVerac),
                    new List<int> { ModContent.ItemType<NPCs.Barrows.Veracsummon>(), ModContent.ItemType<NPCs.Barrows.Cryptspade>() }, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Armor.Verachelm>(), ModContent.ItemType<Items.Armor.Veracbody>(), ModContent.ItemType<Items.Armor.Veraclegs>(), ModContent.ItemType<Items.Veracflail>(), ModContent.ItemType<NPCs.Barrows.Verac.Veracbag>() },
                    "Using a [i:" + ModContent.ItemType<NPCs.Barrows.Cryptspade>() + "] will summon one brother at random. Defeating a brother will drop its respective summoning item.");
                bossChecklist.Call("AddBoss", 11.1f, ModContent.NPCType<NPCs.Barrows.Barrowsspirit>(), this, "Spirit of Barrows", (Func<bool>)(() => OSRSworld.downedBarSpirit),
                    ModContent.ItemType<NPCs.Barrows.Rockcluster>(), ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Accessories.Amuletdamned>(), ModContent.ItemType<NPCs.Barrows.Barrowschest>() },
                    "Use a [i:" + ModContent.ItemType<NPCs.Barrows.Rockcluster>() + "] at night.");
                bossChecklist.Call("AddBoss", 12.1f, ModContent.NPCType<NPCs.Olm.Olm>(), this, "The Great Olm", (Func<bool>)(() => OSRSworld.downedOlm),
                    ModContent.ItemType<NPCs.Olm.Darkrelic>(), ModContent.ItemType<Tiles.OlmMusicBoxItem>(),
                    new List<int> { ModContent.ItemType<Items.Magic.Kodaiinsignia>(), ModContent.ItemType<Items.Dinhbulwark>(), ModContent.ItemType<Items.Dclaws>(), ModContent.ItemType<Items.Twistedbow>(), ModContent.ItemType<Items.Dragonstone>(), ModContent.ItemType<Items.Onyx>(), ModContent.ItemType<Items.Zenyte>(), ModContent.ItemType<NPCs.Olm.OlmBag>() },
                    "Use a [i:" + ModContent.ItemType<NPCs.Olm.Darkrelic>() + "] at night.", null, "OldSchoolRuneScape/NPCs/Olm/OlmBossCheck");
            }
        }
    }
}
