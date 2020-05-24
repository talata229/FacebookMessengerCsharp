using System.ComponentModel;

namespace Facebook.DAL.Enum
{
    public enum EnumReactionType
    {
        [Description("LIKE")]
        LIKE,
        [Description("LOVE")]
        LOVE,
        [Description("WOW")]
        WOW,
        [Description("HAHA")]
        HAHA,
        [Description("SAD")]
        SAD,
        [Description("ANGRY")]
        ANGRY,
        [Description("THANKFUL")]
        THANKFUL,
        [Description("PRIDE")]
        PRIDE,
        [Description("CARE")]
        CARE
    }
}
