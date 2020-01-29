package hu.gds.examples.clientsimulator.websocket;

import io.netty.channel.*;
import io.netty.handler.codec.http.FullHttpResponse;
import io.netty.handler.codec.http.websocketx.*;
import io.netty.util.CharsetUtil;

import static hu.gds.examples.clientsimulator.ClientSimulator.logger;

class WebSocketClientHandler extends SimpleChannelInboundHandler<Object> {
    private final WebSocketClientHandshaker handshaker;
    private ChannelPromise handshakeFuture;
    private ResponseHandler responseHandler;

    public WebSocketClientHandler(WebSocketClientHandshaker handshaker,
                                  ResponseHandler responseHandler) {
        this.handshaker = handshaker;
        this.responseHandler = responseHandler;
    }

    public ChannelFuture handshakeFuture() {
        return handshakeFuture;
    }

    @Override
    public void handlerAdded(ChannelHandlerContext ctx) {
        handshakeFuture = ctx.newPromise();
    }

    @Override
    public void channelActive(ChannelHandlerContext ctx) {
        handshaker.handshake(ctx.channel());
    }

    @Override
    public void channelInactive(ChannelHandlerContext ctx) {
        logger.info("WebSocket Client disconnected!");
    }

    @Override
    public void channelRead0(ChannelHandlerContext ctx, Object msg) throws Exception {
        Channel ch = ctx.channel();
        if (!handshaker.isHandshakeComplete()) {
            try {
                handshaker.finishHandshake(ch, (FullHttpResponse) msg);
                logger.info("WebSocket Client connected!");
                handshakeFuture.setSuccess();
            } catch (WebSocketHandshakeException e) {
                logger.info("WebSocket Client failed to connect");
                handshakeFuture.setFailure(e);
            }
            return;
        }

        if (msg instanceof FullHttpResponse) {
            FullHttpResponse response = (FullHttpResponse) msg;
            throw new IllegalStateException(
                    "Unexpected FullHttpResponse (getStatus=" + response.status() +
                            ", content=" + response.content().toString(CharsetUtil.UTF_8) + ')');
        }

        WebSocketFrame frame = (WebSocketFrame) msg;
        if(frame instanceof BinaryWebSocketFrame) {
            logger.info("WebSocket Client received BinaryWebSocketFrame");
            byte[] binaryFrame = new byte[frame.content().readableBytes()];
            frame.content().readBytes(binaryFrame);
            responseHandler.handleResponse(binaryFrame);
        } else if (frame instanceof TextWebSocketFrame) {
            logger.info("WebSocket Client received TextWebSocketFrame");
            responseHandler.handleResponse(((TextWebSocketFrame) frame).text().getBytes());
        } else if (frame instanceof PongWebSocketFrame) {
            logger.info("WebSocket Client received pong");
        } else if (frame instanceof CloseWebSocketFrame) {
            logger.info("WebSocket Client received closing");
            ch.close();
        } else {
            logger.info("unsupported frame type: " + frame.getClass().getName());
        }
    }

    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) {
        cause.printStackTrace();
        if (!handshakeFuture.isDone()) {
            handshakeFuture.setFailure(cause);
        }
        ctx.close();
    }
}
