using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace OldSchoolRuneScape.Common.DropRules
{
	public class SkeletronCondition : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return NPC.downedBoss3;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops after defeating Skeletron";
		}
	}
}
