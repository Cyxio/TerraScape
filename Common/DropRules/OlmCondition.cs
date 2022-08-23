using Terraria.GameContent.ItemDropRules;

namespace OldSchoolRuneScape.Common.DropRules
{
	public class OlmCondition : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
			{
				return OSRSworld.downedOlm;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops after defeating Olm";
		}
	}
}
