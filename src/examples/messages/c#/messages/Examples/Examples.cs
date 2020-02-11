using System;
using System.Collections.Generic;
using System.Text;

namespace messages
{
    class Examples
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public static MessageHeader GetMessageHeader(int dataType)
        {
            return MessageManager.GetMessageHeader("user1", "249fe125-ce77-4414-8411-b43d8975111d", CurrentTimeMillis(),
                CurrentTimeMillis(), false, null, null, null, null, dataType);
        }

        public static MessageDataConnection GetMessageDataConnection01()
        {
            return MessageManager.GetConnectionMessageData(false, 1, false, null);
        }

        public static MessageDataConnection GetMessageDataConnection02()
        {
            return MessageManager.GetConnectionMessageData(true, 1, true, 10, "pass");
        }

        public static MessageDataConnectionAck GetMessageDataConnectionAck01()
        {
            return MessageManager.GetConnectionAckMessageData(200, GetMessageDataConnection02(), null);
        }

        public static MessageDataConnectionAck GetMessageDataConnectionAck02()
        {
            Dictionary<int, string> unauthorizedAckData = new Dictionary<int, string>();
            unauthorizedAckData.Add(0, "unknown user name");
            return MessageManager.GetConnectionAckMessageData(401, unauthorizedAckData, "unknown user name");
        }

        public static MessageDataConnectionAck GetMessageDataConnectionAck03()
        {
            return MessageManager.GetConnectionAckMessageData(500, "an internal server error occured");
        }
    }
}
