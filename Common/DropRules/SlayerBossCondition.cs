using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace OldSchoolRuneScape.Common.DropRules
{
	public class SlayerBossFirstKillCondition : IItemDropRuleCondition
	{
		private int bossNumber = 0;

		public SlayerBossFirstKillCondition(int bossNumber)
        {
            this.bossNumber = bossNumber;
        }

        public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
                return OSRSworld.slayBossProgress < this.bossNumber;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops on first kill of the slayer boss";
		}
	}
}
