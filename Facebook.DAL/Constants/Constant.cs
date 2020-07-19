using System.Collections.Generic;

namespace Facebook.DAL.Constants
{
    public class Constant
    {
        public static string Token = "EAAAAUaZA8jlABAAKgCvUUFTTG5VP7o90MymwjJuaTZC5RmiEszZBpMrulORQv3qG7GabZBAFwEx3UXw79EyAdocAJdZAgCOI0OvFV4unaf4TtMrYxVgW6V4Xt2OEB2au5Rimm2mg6rVmRfASqXMXQz4vRH1rZAeglPaRDEMmGs7pu91RjHF2xpsDc6itnK9DUZD";
        public static List<string> ListTroLyAoMessage = new List<string>
        {
            "Đây là trợ lý ảo của Quang.\nHiện tại Quang không đang online nên không thể trả lời bạn ngay được.\nTrong khi chờ đợi, các bạn có thể gõ tin nhắn \"trolyao\" để trao đổi với trợ lý ảo" ,
            "Chào bạn.\nHiện tại anh Quang không đang online nên không thể trả lời ngay tin nhắn của bạn được.\nCác bạn có thể viết tin nhắn \"trolyao\" trong khi chờ đợi để chat với Trợ lý ảo của anh Quang.\nKhi nào online, anh Quang sẽ trả lời bạn.",
            "Mình là trợ lý ảo của anh Quang.\nHiện tại anh Quang không đang online.\nTrong quá trình trợ đợi, các bạn có thể viết tin nhắn \"trolyao\" để nói chuyện với mình.\n" +
            "Khi nào online thì anh Quang sẽ trả lời cho bạn."
        };

        public static List<string> ListConfirmAgreeUseTroLyAoMessage = new List<string>
        {
            "Chào bạn. Mình là trợ lý ảo.\nRất hân hạn được nói chuyện với bạn.\nCác bạn có thể đặt ra bất cứ câu hỏi nào, mình sẽ trả lời cho bạn"
        };

        public static int TIME_SLEEP_REACTION = 60 * 1000;
    }
}
