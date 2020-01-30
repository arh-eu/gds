#!/usr/bin/env python

import asyncio
import msgpack
import websockets

from datetime import datetime
from enum import Enum

class DataType(Enum):
    CONNECTION = 0
    CONNECTION_ACK = 1
    EVENT = 2
    EVENT_ACK = 3
    ATTACHMENT_REQUEST = 4
    ATTACHMENT_REQUEST_ACK = 5
    ATTACHMENT_RESPONSE = 6
    ATTACHMENT_RESPONSE_ACK = 7
    EVENT_DOCUMENT = 8
    EVENT_DOCUMENT_ACK = 9
    QUERY_REQUEST = 10
    QUERY_REQUEST_ACK = 11
    NEXT_QUERY_PAGE_REQUEST = 12
    


class WebsocketClient:
    def __init__(self, url="ws://127.0.0.1:8080/websocket"):
        self.url = url


    def send(self, data):
        return asyncio.get_event_loop().run_until_complete(self.async_send(data))

    
    async def async_send(self, data):
        async with websockets.connect(self.url) as ws:
            await ws.send(data)
            return await ws.recv()


class MessageUtil:
    @staticmethod
    def pack(data):
        return msgpack.packb(data, use_bin_type=True)
    
    @staticmethod
    def unpack(data):
        return msgpack.unpackb(data, raw=False)

    @staticmethod
    def create_dummy_header(type):
        now = int(datetime.now().timestamp())
        return ["user", "messageID-" + str(now), now, now, False, None, None, None, None, type.value]



class Example:
    def connection_message(self):
        msg    = MessageUtil.create_dummy_header(DataType.CONNECTION)
        msg.append([False, 1, False, None])
        return msg

    def event_message(self):
        msg    = MessageUtil.create_dummy_header(DataType.EVENT)
        msg.append(["INSERT INTO table('col1', col2') VALUES('a', 'b')"])
        return msg

    def query_message(self):
        msg    = MessageUtil.create_dummy_header(DataType.QUERY_REQUEST)
        msg.append(["SELECT * FROM table WHERE id IS NOT NULL","NONE",3600000])
        return msg    

    def next_query_message(self):
        msg    = MessageUtil.create_dummy_header(DataType.NEXT_QUERY_PAGE_REQUEST)
        msg.append([["query_context_str" for x in range (9)],3600000])
        return msg