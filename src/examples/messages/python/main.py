#!/usr/bin/env python

import GDSClient

def run_example(pair):
    client = GDSClient.WebsocketClient()
    text, request = pair
    print("Trying to send " + text + "..") 
    message = GDSClient.MessageUtil.pack(request)
    try:
        result = client.send(message)
        response = GDSClient.MessageUtil.unpack(result)
        print("Got back the response!\n")
        print(response)
        print("\n\n")
    except Exception as e:
        print("Exception happend while trying to run the operation!\n\t" + str(e))

def main():
    example = GDSClient.Example()
    ops = [
        ("connection request", example.connection_message()),
        ("event request", example.event_message()),
        ("query request", example.query_message()),
        ("next query page request", example.next_query_message()),
    ]
    for op in ops: 
        run_example(op)


if __name__ == "__main__":
    main()
