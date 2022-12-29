using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PlatformBuilder;

namespace PlatformBuilder
{
    public class PBCommands : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "pb";

        public override string Usage => "使用“/pb plattype [类型]”改变平台类型（数字范围为0~35）\n" +
            "0.木    1.乌木    2.红木    3.珍珠木    4.骨头    5.暗影木    6.蓝砖    7.粉砖    8.绿砖    9.金属架\n" +
            "10.黄铜架    11.木架    12.地牢架    13.黑曜石    14.玻璃    15.南瓜    16.阴森    17.棕榈木    18.蘑菇    19.针叶木\n" +
            "20.史莱姆    21.蒸汽朋克    22.天域    23.生命木    24.蜂蜜    25.仙人掌    26.火星    27.陨石    28.花岗岩    29.大理石\n" +
            "30.水晶    31.金色    32.王朝    33.丛林蜥蜴    34.血肉    35.冰冻\n" +
            "teamB：蓝团队平台    teamG：绿团队平台    teamP：粉团队平台    teamR：红团队平台    teamW：白团队平台    teamY：黄团队平台\n" +
            "使用“/pb tiletype [类型]”改变物块类型（数字范围为0~33）\n" +
            "0.木材    1.绿砖    2.蓝砖    3.粉砖    4.丛林蜥蜴砖    5.黑曜石砖    6.石板    7.灰砖    8.红砖    9.沙岩砖\n" +
            "10.彩虹砖    11.光面花岗岩块    12.光面大理石块    13.乌木    14.红木    15.珍珠木    16.暗影木    17.阴森木    18.王朝木    19.针叶木\n" +
            "20.棕榈木    21.玻璃    22.蓝团队块    23.绿团队块    24.粉团队块    25.红团队块    26.白团队块    27.黄团队块    28.雪块    29.血砖\n" +
            "30.魔矿砖    31.猩红矿砖    32.夜明砖    33.叶绿砖";

        public static bool team = false;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!int.TryParse(args[0], out int plattype))
            {
                if ((args[0] != "plattype" && args[0] != "tiletype") || args.Length > 2)
                    throw new UsageException("命令错误");
                if (args[0] == "plattype")
                {
                    if (int.TryParse(args[1], out int type))
                    {
                        if (type < 0 || type > 35)
                        {
                            throw new UsageException("输入的数字不在范围内");
                        }
                        else
                        {
                            Null.PlatformIDs = TileID.Platforms;
                            Null.PlatType = type;
                            Main.NewText("成功将平台更改为" + type);
                        }
                    }
                    else if (!int.TryParse(args[1], out int team))
                    {
                        if (args[1] == "teamB")
                        {
                            Null.PlatformIDs = TileID.TeamBlockBluePlatform;
                            Main.NewText("成功将平台更改为蓝团队平台");
                        }
                        else if (args[1] == "teamG")
                        {
                            Null.PlatformIDs = TileID.TeamBlockGreenPlatform;
                            Main.NewText("成功将平台更改为绿团队平台");
                        }
                        else if (args[1] == "teamP")
                        {
                            Null.PlatformIDs = TileID.TeamBlockPinkPlatform;
                            Main.NewText("成功将平台更改为粉团队平台");
                        }
                        else if (args[1] == "teamR")
                        {
                            Null.PlatformIDs = TileID.TeamBlockRedPlatform;
                            Main.NewText("成功将平台更改为红团队平台");
                        }
                        else if (args[1] == "teamW")
                        {
                            Null.PlatformIDs = TileID.TeamBlockWhitePlatform;
                            Main.NewText("成功将平台更改为白团队平台");
                        }
                        else if (args[1] == "teamY")
                        {
                            Null.PlatformIDs = TileID.TeamBlockYellowPlatform;
                            Main.NewText("成功将平台更改为黄团队平台");
                        }
                        else
                            throw new UsageException("命令错误");
                    }
                }
                else if (args[0] == "tiletype")
                {
                    if (int.TryParse(args[1], out int type))
                    {
                        if (type < 0 || type > 34)
                        {
                            throw new UsageException("输入的数字不在范围内");
                        }
                        else
                        {
                            if (type == 0)
                            {
                                Null.TileIDs = TileID.WoodBlock;
                                Main.NewText("成功将物块更改为木材");
                            }
                            else if (type == 1)
                            {
                                Null.TileIDs = TileID.GreenDungeonBrick;
                                Main.NewText("成功将物块更改为绿砖");
                            }
                            else if (type == 2)
                            {
                                Null.TileIDs = TileID.BlueDungeonBrick;
                                Main.NewText("成功将物块更改为蓝砖");
                            }
                            else if (type == 3)
                            {
                                Null.TileIDs = TileID.PinkDungeonBrick;
                                Main.NewText("成功将物块更改为粉砖");
                            }
                            else if (type == 4)
                            {
                                Null.TileIDs = TileID.LihzahrdBrick;
                                Main.NewText("成功将物块更改为丛林蜥蜴砖");
                            }
                            else if (type == 5)
                            {
                                Null.TileIDs = TileID.ObsidianBrick;
                                Main.NewText("成功将物块更改为黑曜石砖");
                            }
                            else if (type == 6)
                            {
                                Null.TileIDs = TileID.StoneSlab;
                                Main.NewText("成功将物块更改为石板");
                            }
                            else if (type == 7)
                            {
                                Null.TileIDs = TileID.GrayBrick;
                                Main.NewText("成功将物块更改为灰砖");
                            }
                            else if (type == 8)
                            {
                                Null.TileIDs = TileID.RedBrick;
                                Main.NewText("成功将物块更改为红砖");
                            }
                            else if (type == 9)
                            {
                                Null.TileIDs = TileID.SandstoneBrick;
                                Main.NewText("成功将物块更改为沙岩砖");
                            }
                            else if (type == 10)
                            {
                                Null.TileIDs = TileID.RainbowBrick;
                                Main.NewText("成功将物块更改为彩虹砖");
                            }
                            else if (type == 11)
                            {
                                Null.TileIDs = TileID.GraniteBlock;
                                Main.NewText("成功将物块更改为光面花岗岩块");
                            }
                            else if (type == 12)
                            {
                                Null.TileIDs = TileID.MarbleBlock;
                                Main.NewText("成功将物块更改为光面大理石块");
                            }
                            else if (type == 13)
                            {
                                Null.TileIDs = TileID.Ebonwood;
                                Main.NewText("成功将物块更改为乌木");
                            }
                            else if (type == 14)
                            {
                                Null.TileIDs = TileID.RichMahogany;
                                Main.NewText("成功将物块更改为红木");
                            }
                            else if (type == 15)
                            {
                                Null.TileIDs = TileID.Pearlwood;
                                Main.NewText("成功将物块更改为珍珠木");
                            }
                            else if (type == 16)
                            {
                                Null.TileIDs = TileID.Shadewood;
                                Main.NewText("成功将物块更改为暗影木");
                            }
                            else if (type == 17)
                            {
                                Null.TileIDs = TileID.SpookyWood;
                                Main.NewText("成功将物块更改为阴森木");
                            }
                            else if (type == 18)
                            {
                                Null.TileIDs = TileID.DynastyWood;
                                Main.NewText("成功将物块更改为王朝木");
                            }
                            else if (type == 19)
                            {
                                Null.TileIDs = TileID.BorealWood;
                                Main.NewText("成功将物块更改为针叶木");
                            }
                            else if (type == 20)
                            {
                                Null.TileIDs = TileID.PalmWood;
                                Main.NewText("成功将物块更改为棕榈木");
                            }
                            else if (type == 21)
                            {
                                Null.TileIDs = TileID.Glass;
                                Main.NewText("成功将物块更改为玻璃");
                            }
                            else if (type == 22)
                            {
                                Null.TileIDs = TileID.TeamBlockBlue;
                                Main.NewText("成功将物块更改为蓝团队块");
                            }
                            else if (type == 23)
                            {
                                Null.TileIDs = TileID.TeamBlockGreen;
                                Main.NewText("成功将物块更改为绿团队块");
                            }
                            else if (type == 24)
                            {
                                Null.TileIDs = TileID.TeamBlockPink;
                                Main.NewText("成功将物块更改为粉团队块");
                            }
                            else if (type == 25)
                            {
                                Null.TileIDs = TileID.TeamBlockRed;
                                Main.NewText("成功将物块更改为红团队块");
                            }
                            else if (type == 26)
                            {
                                Null.TileIDs = TileID.TeamBlockWhite;
                                Main.NewText("成功将物块更改为白团队块");
                            }
                            else if (type == 27)
                            {
                                Null.TileIDs = TileID.TeamBlockYellow;
                                Main.NewText("成功将物块更改为黄团队块");
                            }
                            else if (type == 28)
                            {
                                Null.TileIDs = TileID.SnowBlock;
                                Main.NewText("成功将物块更改为雪块");
                            }
                            else if (type == 29)
                            {
                                Null.TileIDs = TileID.SnowBrick;
                                Main.NewText("成功将物块更改为雪砖");
                            }
                            else if (type == 30)
                            {
                                Null.TileIDs = TileID.DemoniteBrick;
                                Main.NewText("成功将物块更改为魔矿砖");
                            }
                            else if (type == 31)
                            {
                                Null.TileIDs = TileID.CrimtaneBrick;
                                Main.NewText("成功将物块更改为猩红矿砖");
                            }
                            else if (type == 32)
                            {
                                Null.TileIDs = TileID.LunarBrick;
                                Main.NewText("成功将物块更改为夜明砖");
                            }
                            else if (type == 33)
                            {
                                Null.TileIDs = TileID.ChlorophyteBrick;
                                Main.NewText("成功将物块更改为叶绿砖");
                            }
                            else if (type == 34)
                            {
                                Null.TileIDs = TileID.MinecartTrack;
                                Main.NewText("成功将物块更改为矿车轨道");
                            }
                        }
                    }
                    else
                        throw new UsageException("命令错误");
                }
            }
        }
    }
}
