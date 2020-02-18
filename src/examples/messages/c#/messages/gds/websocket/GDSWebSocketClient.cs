using gds.messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using WebSocket4Net;


namespace messages_api.gds.websocket
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
            if (MessageReceived != null)
            {
                MessageReceived(sender, message);
            }
        }
    }
}
