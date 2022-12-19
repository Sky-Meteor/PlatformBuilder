using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PlatformBuilder
{
    public class PBSystem : ModSystem
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
