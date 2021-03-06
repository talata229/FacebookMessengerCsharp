﻿using System.ComponentModel;

namespace Facebook.DAL.Enum
{
    public enum EnumFeature
    {
        [Description("NoSpecialFeature")]
        NoSpecialFeature,
        [Description("StopAll")]
        StopAll,
        [Description("RemoveStopAll")]
        RemoveStopAll,
        [Description("Stop5Min")]
        Stop5Min,
        [Description("TroLyAo")]
        TroLyAo,
        [Description("GirlXinh")]
        GirlXinh,
        [Description("TruyenCuoi")]
        TruyenCuoi,
        [Description("TinTuc")]
        TinTuc,
        [Description("NoiTu")]
        NoiTu,
        [Description("StopNoiTu")]
        StopNoiTu,
        [Description("NoiTuTiengAnh")]
        NoiTuTiengAnh,
        [Description("StopNoiTuTiengAnh")]
        StopNoiTuTiengAnh,
        [Description("Normal")]
        Normal
    }
}
