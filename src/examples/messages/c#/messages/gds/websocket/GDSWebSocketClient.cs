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

using gds.message;
using System;
using System.Threading;
using WebSocket4Net;

namespace gds.websocket
{
    public class GDSWebSocketClient
    {
        private readonly WebSocket client;
        private readonly AutoResetEvent messageReceiveEvent = new AutoResetEvent(false);
        private Message lastMessageReceived;
        public event EventHandler<Message> MessageReceived;

        public GDSWebSocketClient(string uri)
        {
            client = new WebSocket(uri);
            client.Opened += new EventHandler(Opened);
            client.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(Error);
            client.Opened += new EventHandler(Closed);
            client.DataReceived += new EventHandler<DataReceivedEventArgs>(DataReceived);
            Connect();
        }

        public void Connect()
        {
            client.Open();
            while (client.State == WebSocketState.Connecting) { };

            if (client.State != WebSocketState.Open)
            {
                throw new Exception("Connection is not opened.");
            }
        }

        public void Close()
        {
            client.Close();
        }

        public void SendAsync(Message message)
        {
            if (message == null)
            {
                throw new InvalidOperationException("Parameter 'message' cannot be null");
            }
            byte[] binary = MessageManager.GetBinaryFromMessage(message);
            client.Send(binary, 0, binary.Length);

        }

        public Message SendSync(Message message, int timeout)
        {
            if (message == null)
            {
                throw new InvalidOperationException("Parameter 'message' cannot be null");
            }
            byte[] binary = MessageManager.GetBinaryFromMessage(message);
            {
                client.Send(binary, 0, binary.Length);
                if (!messageReceiveEvent.WaitOne(timeout))
                {
                    return null;
                }
                return lastMessageReceived;
            }
        }

        private void Opened(object sender, EventArgs e) { }

        private void Closed(object sender, EventArgs e) { }

        private void Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) { }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Message message = MessageManager.GetMessageFromBinary(e.Data);
            lastMessageReceived = message;
            messageReceiveEvent.Set();
            MessageReceived?.Invoke(sender, message);
        }
    }
}
