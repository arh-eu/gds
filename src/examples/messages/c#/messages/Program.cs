using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace messages
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageHeader header = Examples.GetMessageHeader(1);
            MessageDataConnectionAck data = Examples.GetMessageDataConnectionAck01();
            Message message = new Message(header, data);

            MemoryStream stream = MessageManager.GetBinaryMessage(message);

            stream.Position = 0;
            Message unpackedMessage = MessageManager.GetMessageFromBinary(stream);

            Console.WriteLine(unpackedMessage.Data.ToString());
        }

        private static void WriteToFile(MemoryStream stream)
        {
            stream.Position = 0;
            using (FileStream file = new FileStream("message", FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }
    }
}
