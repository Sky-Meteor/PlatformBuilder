using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace PlatformBuilder
{
	public class PlatformBuilder : Mod
	{
        public override void AddRecipeGroups()
        {
            RecipeGroup dungeonbrick = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "地牢砖", new int[]
            {
                ItemID.BlueBrick,
                ItemID.GreenBrick,
                ItemID.PinkBrick
            });
            RecipeGroup.RegisterGroup("AnyDungeonBrick", dungeonbrick);
        }
    }
}