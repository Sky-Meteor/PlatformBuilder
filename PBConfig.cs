using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace PlatformBuilder
{
    public class PBConfig : ModConfig
    {
        [Label("设置")]
        public override ConfigScope Mode
        {
            get
            {
                return 0;
            }
        }
        public static PBConfig Instance;

        [Header("火把与平台设置")]
        [Label("火把与平台间隔（清除）物块数")]
        [DefaultValue(5)]
        [Range(0, 50)]
        public int TorchFromTile;

        [Label("是否为世界平台/物块")]
        [DefaultValue(true)]
        public bool WorldPlat;

        [Label("向左延伸（是）向右延伸（否）")]
        [DefaultValue(true)]
        public bool IsLeft;

        [Label("平台长度")]
        [DefaultValue(200)]
        [Range(1, 1000)]
        public int PlatLength;

        [Label("火把种类")]
        [OptionStrings(new string[]
        {
            "超亮火把",
            "白火把", 
            "火把"
        })]
        [DefaultValue("超亮火把")]
        public string TorchType;

    }
}
