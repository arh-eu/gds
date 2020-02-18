using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace gds.messages
{
    static class MsgPackUtils
    {
        public static void WriteNullableBool(bool? value, ref MessagePackWriter writer)
        {
            if (value != null)
            {
                writer.Write(value.Value);
            }
            else
            {
                writer.WriteNil();
            }
        }

        public static bool? ReadNullableBool(ref MessagePackReader reader)
        {
            if(!reader.IsNil)
            {
                return reader.ReadBoolean();
            }
            else
            {
                reader.ReadNil();
                return null;
            }
        }

        public static void WriteNullableInt(int? value, ref MessagePackWriter writer)
        {
            if (value != null)
            {
                writer.Write(value.Value);
            }
            else
            {
                writer.WriteNil();
            }
        }

        public static int? ReadNullableInt(ref MessagePackReader reader)
        {
            if (!reader.IsNil)
            {
                return reader.ReadInt32();
            }
            else
            {
                reader.ReadNil();
                return null;
            }
        }

    }
}
