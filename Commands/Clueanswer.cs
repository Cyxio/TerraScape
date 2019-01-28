using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Commands
{
    public class Clueanswer : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "answer"; }
        }

        public override string Usage
        {
            get { return "/answer Answer"; }
        }

        public override string Description
        {
            get { return "Answer a challenge clue"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!args[0].Equals(0))
            {
                Player player = caller.Player;
                if (player.GetModPlayer<OSRSplayer>().challengeAns == int.Parse(args[0]))
                {
                    player.GetModPlayer<OSRSplayer>().ClueAnswer(player);
                }
                else
                {
                    Main.NewText("Wrong answer!", Color.Red);
                }
            }
        }
    }
}