/*
 * Copyright 2020 ARH Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using MessagePack;

namespace Gds.Messages
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
            if (!reader.IsNil)
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
