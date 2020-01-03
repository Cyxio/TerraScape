using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Commands
{
    class ChatMessage : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.World; }
        }

        public override string Command
        {
            get { return "ClueMessage"; }
        }

        public override string Description
        {
            get { return "Used for displaying clue progression messages in multiplayer (automatic)"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            string[] inputt = input.Split(' ');
            string final = inputt[1].Replace('_', ' ');
            int colorVal = int.Parse(inputt[2]);
            Color messageColor = Color.Red;
            switch (colorVal)
            {
                case 0:
                case 1: messageColor = Colors.RarityGreen; break;
                case 2: messageColor = Colors.RarityBlue; break;
                case 3: messageColor = Colors.RarityOrange; break;
                case 4: messageColor = Colors.RarityPurple; break;
                case 5: messageColor = Colors.RarityAmber; break;
                case 6: messageColor = Colors.RarityLime; break;
                case 7: messageColor = Colors.RarityCyan; break;
                case 8: messageColor = Colors.RarityRed; break;
                case 9: messageColor = Colors.RarityPink; break;
                case 10: messageColor = new Color(225, 6, 67); break;
                default:
                    break;
            }
            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(final), messageColor);
        }
    }
}
