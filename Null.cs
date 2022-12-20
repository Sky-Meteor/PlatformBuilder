using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace PlatformBuilder
{

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
            Projectile.timeLeft = -1;
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
                                        WorldGen.PlaceTile(xPosition * 8, yPosition - PBConfig.Instance.TorchFromTile, TileID.Torches, false, false, -1, GetTorchType());
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
                                            WorldGen.PlaceTile(xPosition * 8, yPosition - PBConfig.Instance.TorchFromTile, TileID.Torches, false, false, -1, GetTorchType());
                                        }
                                    }
                                }
                            }
                            else if (PlatBuilder.TorchOnly)
                            {
                                WorldGen.KillWall(xPosition, (int)(Projectile.position.Y / 16.0f));
                                WorldGen.KillTile(xPosition, (int)(Projectile.position.Y / 16.0f));

                                WorldGen.PlaceWall(xPosition * 8, (int)(Projectile.position.Y / 16.0f + 1), WallID.Glass);
                                WorldGen.PlaceTile(xPosition * 8, (int)(Projectile.position.Y / 16.0f + 1), TileID.Torches, false, false, -1, GetTorchType());
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
        static int GetTorchType()
        {
            return PBConfig.Instance.TorchType switch
            {
                "超亮火把" => 12,
                "白火把" => 5,
                "火把" => 0,
                _ => 12,
            };
        }
    }
}
