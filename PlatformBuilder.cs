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

	public class PlatBuilder : ModItem
    {
        public static PlatBuilder pb;
        public static bool TorchOnly = false;
        public static bool ClearOnly = false;
        public static bool BlockMode = false;
        public int mode = 1;
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

                if (mode == 1)
                {
                    CombatText.NewText(player.Hitbox, Color.Orange, "火把+平台");

                    TorchOnly = false;
                    ClearOnly = false;
                    BlockMode = false;
                }
                else if (mode == 2)
                {
                    CombatText.NewText(player.Hitbox, Color.Orange, "只放火把");

                    TorchOnly = true;
                    ClearOnly = false;
                    BlockMode = false;
                }
                else if (mode == 3)
                {
                    CombatText.NewText(player.Hitbox, Color.Orange, "只清理方块");

                    TorchOnly = false;
                    ClearOnly = true;
                    BlockMode = false;
                }
                else if (mode == 4)
                {
                    CombatText.NewText(player.Hitbox, Color.Orange, "放置方块");

                    BlockMode = true;
                }
                else if (mode == 5)
                { 
                    mode = 1;
                    CombatText.NewText(player.Hitbox, Color.Orange, "火把+平台");

                    TorchOnly = false;
                    ClearOnly = false;
                    BlockMode = false;
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

    public class Null : ModProjectile
    {
        public static int PlatType = 14;
        public static int PlatformIDs = TileID.Platforms;
        public static int TileIDs = TileID.WoodBlock;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Null");
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.timeLeft = 1;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int x = 1; x <= Main.maxTilesX; x++)
            {
                for (int y = -PBConfig.Instance.TorchFromTile; y <= 0; y++)
                {
                    int xPosition = x;
                    int yPosition = (int)(y + Projectile.position.Y / 16.0f);
                    if (xPosition < 0 || xPosition >= Main.maxTilesX || yPosition < 0 || yPosition >= Main.maxTilesY)
                        continue;
                    Tile tile = Main.tile[xPosition, yPosition];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (y < 0 && !PlatBuilder.ClearOnly && !PlatBuilder.TorchOnly)
                    {
                        WorldGen.KillWall(xPosition, yPosition + 1);
                        WorldGen.KillTile(xPosition, yPosition + 1);
                    }
                    if (y == 0)
                    {
                        if (!PlatBuilder.BlockMode)
                        {
                            if (!PlatBuilder.TorchOnly && !PlatBuilder.ClearOnly)
                            {
                                if (PBConfig.Instance.WorldPlat)
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition + 1, PlatformIDs, false, false, -1, PBCommands.team ? 0 : PlatType);
                                    if (xPosition * 8 < Main.maxTilesX)
                                    {
                                        WorldGen.PlaceWall(xPosition * 8, yPosition - PBConfig.Instance.TorchFromTile, WallID.Glass);
                                        WorldGen.PlaceTile(xPosition * 8, yPosition - PBConfig.Instance.TorchFromTile, TileID.Torches, false, false, -1, PBConfig.Instance.IsUltraLight ? 12 : 0);
                                    }
                                }
                                else
                                {
                                    for (int X = 0; X < PBConfig.Instance.PlatLength; X++)
                                    {
                                        int PosX = (int)(Projectile.position.X / 16.0f) + 1 + (PBConfig.Instance.IsLeft ? -X : X);
                                        WorldGen.PlaceTile(PosX, yPosition + 1, PlatformIDs, false, false, -1, PBCommands.team ? 0 : PlatType);
                                        if (xPosition * 8 < Main.maxTilesX &&
                                            ((PBConfig.Instance.IsLeft && xPosition * 8 <= (int)(Projectile.position.X / 16.0f) + 1 && xPosition * 8 >= (int)(Projectile.position.X / 16.0f) + 1 - PBConfig.Instance.PlatLength) ||
                                            (!PBConfig.Instance.IsLeft && xPosition * 8 <= (int)(Projectile.position.X / 16.0f) + 1 + PBConfig.Instance.PlatLength && xPosition * 8 >= (int)(Projectile.position.X / 16.0f) + 1)))
                                        {
                                            WorldGen.PlaceWall(xPosition * 8, yPosition - PBConfig.Instance.TorchFromTile, WallID.Glass);
                                            WorldGen.PlaceTile(xPosition * 8, yPosition - PBConfig.Instance.TorchFromTile, TileID.Torches, false, false, -1, PBConfig.Instance.IsUltraLight ? 12 : 0);
                                        }
                                    }
                                }
                            }
                            else if (PlatBuilder.TorchOnly)
                            {
                                WorldGen.KillWall(xPosition, (int)(Projectile.position.Y / 16.0f));
                                WorldGen.KillTile(xPosition, (int)(Projectile.position.Y / 16.0f));

                                WorldGen.PlaceWall(xPosition * 8, (int)(Projectile.position.Y / 16.0f + 1), WallID.Glass);
                                WorldGen.PlaceTile(xPosition * 8, (int)(Projectile.position.Y / 16.0f + 1), TileID.Torches, false, false, -1, PBConfig.Instance.IsUltraLight ? 12 : 0);
                            }
                            else if (PlatBuilder.ClearOnly)
                            {
                                WorldGen.KillWall(xPosition, (int)(Projectile.position.Y / 16.0f + 1));
                                WorldGen.KillTile(xPosition, (int)(Projectile.position.Y / 16.0f + 1));
                            }
                        }
                        else
                        {
                            if (PBConfig.Instance.WorldPlat)
                            {
                                WorldGen.PlaceTile(xPosition, yPosition + 1, TileIDs, false, false, -1, 0);
                            }
                            else
                            {
                                for (int X = 0; X < PBConfig.Instance.PlatLength; X++)
                                {
                                    int PosX = (int)(Projectile.position.X / 16.0f) + 1 + (PBConfig.Instance.IsLeft ? -X : X);
                                    WorldGen.PlaceTile(PosX, yPosition + 1, TileIDs, false, false, -1, 0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}