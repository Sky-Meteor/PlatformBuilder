using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace PlatformBuilder
{
    public class PlatBuilder : ModItem
    {
        public static PlatBuilder pb;
        public static bool TorchOnly = false;
        public static bool ClearOnly = false;
        public static bool BlockMode = false;
        public static bool ActuatorMode = false;
        public static bool ActuatorRevert = false;
        private int mode = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("平台/物块构造");
            Tooltip.SetDefault("左键生成玻璃平台，附带超亮火把\n右键切换模式\n输入/pb命令和查看Mod Configuration获得更多信息");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Purple;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.shoot = ModContent.ProjectileType<Null>();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void HoldItem(Player player)
        {
            player.rulerLine = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                mode += 1;

                switch (mode)
                {
                    case 1:
                        Default:
                        CombatText.NewText(player.Hitbox, Color.Orange, "火把+平台");
                        TorchOnly = false;
                        ClearOnly = false;
                        BlockMode = false;
                        ActuatorMode = false;
                        ActuatorRevert = false;
                        break;
                    case 2:
                        CombatText.NewText(player.Hitbox, Color.Orange, "只放火把");
                        TorchOnly = true;
                        break;
                    case 3:
                        CombatText.NewText(player.Hitbox, Color.Orange, "只清理方块");
                        TorchOnly = false;
                        ClearOnly = true;
                        break;
                    case 4:
                        CombatText.NewText(player.Hitbox, Color.Orange, "放置方块");
                        BlockMode = true;
                        break;
                    case 5:
                        CombatText.NewText(player.Hitbox, Color.Orange, "虚化方块");
                        ActuatorMode = true;
                        break;
                    case 6:
                        CombatText.NewText(player.Hitbox, Color.Orange, "还原虚化方块");
                        ActuatorMode = false;
                        ActuatorRevert = true;
                        break;
                    default:
                        mode = 1;
                        goto Default;
                }
            }
            else
                Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, 0, 0f, player.whoAmI);

            return false;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Glass, 999);
            r.AddIngredient(ItemID.Torch, 999);
            r.AddIngredient(ItemID.Obsidian, 99);
            r.AddIngredient(ItemID.Bomb, 99);
            r.AddRecipeGroup("AnyDungeonBrick", 99);
            r.AddTile(TileID.CookingPots);
            r.Register();
        }
    }

}
