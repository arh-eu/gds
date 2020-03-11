using Gds.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace messages.Gds
{
    class test
    {
        public static void Main(String[] args)
        {
            byte[] binary = File.ReadAllBytes(@"E:\php\src\message");
            MessageManager.GetMessageFromBinary(binary);
        }
    }
}
