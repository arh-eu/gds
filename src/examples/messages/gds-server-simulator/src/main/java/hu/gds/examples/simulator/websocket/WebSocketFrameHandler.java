/*
 * Intellectual property of ARH Inc.
 * This file belongs to the GDS 5.0 system in the gdsserversimulator project.
 * Budapest, 2020/01/27
 */


/*
 * Copyright 2012 The Netty Project
 *
 * The Netty Project licenses this file to you under the Apache License,
 * version 2.0 (the "License"); you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at:
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */
package hu.gds.examples.simulator.websocket;

import hu.gds.examples.simulator.GDSSimulator;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import io.netty.handler.codec.http.websocketx.TextWebSocketFrame;
import io.netty.handler.codec.http.websocketx.WebSocketFrame;

public class WebSocketFrameHandler extends SimpleChannelInboundHandler<WebSocketFrame> {

    private final GDSSimulator simulator = new GDSSimulator();

    @Override
    protected void channelRead0(ChannelHandlerContext ctx, WebSocketFrame frame) {
        if (frame instanceof TextWebSocketFrame) {
            byte[] request = ((TextWebSocketFrame) frame).text().getBytes();
            try {
                byte[] response = simulator.handleRequest(request);
                ctx.channel().writeAndFlush(new TextWebSocketFrame(new String(response)));
            } catch (Throwable t) {
                throw new IllegalStateException(t);
            }
        } else {
            String message = "unsupported frame type: " + frame.getClass().getName();
            throw new UnsupportedOperationException(message);
        }
    }
}
