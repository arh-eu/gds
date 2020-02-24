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

using Gds.Messages;
using MessagePack;
using System;
using System.Threading;
using WebSocket4Net;

namespace Gds.Websocket
{
    /// <summary>
    /// Simple websocket class with basic functionality for sending and receiving messages.
    /// </summary>
    public class GDSWebSocketClient
    {
        private readonly WebSocket client;
        private readonly AutoResetEvent messageReceiveEvent = new AutoResetEvent(false);
        private Tuple<Message, MessagePackSerializationException> lastMessageReceived;
        public event EventHandler<Tuple<Message, MessagePackSerializationException>> MessageReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="GDSWebSocketClient"/> class
        /// </summary>
        /// <param name="uri">websocket server uri</param>
        public GDSWebSocketClient(string uri)
        {
            client = new WebSocket(uri);
            client.Opened += new EventHandler(Opened);
            client.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(Error);
            client.Opened += new EventHandler(Closed);
            client.DataReceived += new EventHandler<DataReceivedEventArgs>(DataReceived);
        }

        /// <summary>
        /// Connect to the server (specified by the uri) asynchronously
        /// </summary>
        public void ConnectAsync()
        {
            client.Open();
        }

        /// <summary>
        /// Connect to the server (specified by the uri) synchronously
        /// </summary>
        /// <returns></returns>
        public bool ConnectSync()
        {
            ConnectAsync();
            while (client.State == WebSocketState.Connecting) { };
            return client.State == WebSocketState.Open;
        }

        /// <summary>
        /// Whether the client is connected
        /// </summary>
        public bool IsConnected()
        {
            return client.State == WebSocketState.Open;
        }

        /// <summary>
        /// Close the connection asynchronously
        /// </summary>
        public void CloseAsync()
        {
            client.Close();
        }

        /// <summary>
        /// Close the connection synchronously
        /// </summary>
        public bool CloseSync()
        {
            CloseAsync();
            while (client.State == WebSocketState.Closing) { };
            return client.State == WebSocketState.Closed;
        }

        /// <summary>
        /// Whether the connection is closed
        /// </summary>
        public bool IsClosed()
        {
            return client.State == WebSocketState.Closed;
        }

        /// <summary>
        /// Get the client state
        /// </summary>
        /// <returns></returns>
        public WebSocketState GetState()
        {
            return client.State;
        }

        /// <summary>
        /// Send a message to the server asynchronously
        /// </summary>
        /// <param name="message">The message to be send</param>
        public void SendAsync(Message message)
        {
            if (message == null)
            {
                throw new InvalidOperationException("Parameter 'message' cannot be null");
            }
            byte[] binary = MessageManager.GetBinaryFromMessage(message);
            client.Send(binary, 0, binary.Length);
        }

        /// <summary>
        /// Send a message to the server synchronously
        /// </summary>
        /// <param name="message">The message to be send</param>
        /// <param name="timeout">The timeout value in milliseconds</param>
        /// <returns></returns>
        public Tuple<Message, MessagePackSerializationException> SendSync(Message message, int timeout)
        {
            SendAsync(message);
            if (!messageReceiveEvent.WaitOne(timeout))
            {
                throw new TimeoutException("The timeout period elapsed");
            }
            return lastMessageReceived;
        }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                Message message = MessageManager.GetMessageFromBinary(e.Data);
                lastMessageReceived = new Tuple<Message, MessagePackSerializationException>(message, null);
            }
            catch (MessagePackSerializationException exception)
            {
                lastMessageReceived = new Tuple<Message, MessagePackSerializationException>(null, exception);
            }
            MessageReceived?.Invoke(sender, lastMessageReceived);
            messageReceiveEvent.Set();
        }

        private void Opened(object sender, EventArgs e) { }
        private void Closed(object sender, EventArgs e) { }
        private void Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) { }
    }
}
