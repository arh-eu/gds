package hu.gds.examples.clientsimulator.websocket;

import io.netty.bootstrap.Bootstrap;
import io.netty.buffer.Unpooled;
import io.netty.channel.*;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioSocketChannel;
import io.netty.handler.codec.http.DefaultHttpHeaders;
import io.netty.handler.codec.http.HttpClientCodec;
import io.netty.handler.codec.http.HttpObjectAggregator;
import io.netty.handler.codec.http.websocketx.*;
import io.netty.handler.codec.http.websocketx.extensions.compression.WebSocketClientCompressionHandler;
import io.netty.handler.ssl.SslContext;
import io.netty.handler.ssl.SslContextBuilder;
import io.netty.handler.ssl.util.InsecureTrustManagerFactory;

import javax.net.ssl.SSLException;
import java.net.URI;
import java.net.URISyntaxException;

import static hu.gds.examples.clientsimulator.ClientSimulator.logger;

public class WebSocketClient {
    private final URI URI;
    private SslContext sslCtx;
    private final String host;
    private final int port;
    private EventLoopGroup group = new NioEventLoopGroup();
    private Channel ch;

    private ResponseHandler responseHandler;

    public WebSocketClient(String url, ResponseHandler responseHandler) throws SSLException, URISyntaxException {
        this.responseHandler = responseHandler;
        this.URI = new URI(url);
        String scheme = URI.getScheme() == null ? "ws" : URI.getScheme();
        host = URI.getHost() == null ? "127.0.0.1" : URI.getHost();
        if (URI.getPort() == -1) {
            if ("ws".equalsIgnoreCase(scheme)) {
                port = 80;
            } else if ("wss".equalsIgnoreCase(scheme)) {
                port = 443;
            } else {
                port = -1;
            }
        } else {
            port = URI.getPort();
        }

        if (!"ws".equalsIgnoreCase(scheme) && !"wss".equalsIgnoreCase(scheme)) {
            System.err.println("Only WS(S) is supported.");
            return;
        }

        final boolean ssl = "wss".equalsIgnoreCase(scheme);
        if (ssl) {
            sslCtx = SslContextBuilder.forClient()
                    .trustManager(InsecureTrustManagerFactory.INSTANCE).build();
        } else {
            sslCtx = null;
        }
    }

    public void connect() throws Throwable {
        if (!isActive()) {
            if(isOpen()) {
                close();
            }

            try {
                group = new NioEventLoopGroup();
                WebSocketClientHandler webSocketClientHandler = new WebSocketClientHandler(
                        WebSocketClientHandshakerFactory.newHandshaker(
                                URI, WebSocketVersion.V13, null, true,
                                new DefaultHttpHeaders()), responseHandler);
                Bootstrap bootstrap = new Bootstrap();
                bootstrap.group(group)
                        .channel(NioSocketChannel.class)
                        .handler(new ChannelInitializer<SocketChannel>() {
                            @Override
                            protected void initChannel(SocketChannel ch) {
                                ChannelPipeline p = ch.pipeline();
                                if (sslCtx != null) {
                                    p.addLast(sslCtx.newHandler(ch.alloc(), host, port));
                                }
                                p.addLast(
                                        new HttpClientCodec(),
                                        new HttpObjectAggregator(8192),
                                        WebSocketClientCompressionHandler.INSTANCE,
                                        webSocketClientHandler);
                            }
                        });
                logger.info("WebSocket Client connecting to server...");
                ch = bootstrap.connect(URI.getHost(), port).sync().channel();
                webSocketClientHandler.handshakeFuture().sync();
            } catch (Throwable throwable) {
                group.shutdownGracefully();
                throw throwable;
            }
        }
    }

    public void send(byte[] msg) throws Throwable {
        if (!isActive()) {
            connect();
        }
        WebSocketFrame frame = new BinaryWebSocketFrame(Unpooled.wrappedBuffer(msg));
        logger.info("WebSocket Client sending BinaryWebSocketFrame...");
        ch.writeAndFlush(frame);
    }

    public void close() throws InterruptedException {
        if(isOpen()) {
            logger.info("WebSocket Client closing channel...");
            ch.writeAndFlush(new CloseWebSocketFrame());
            ch.closeFuture().sync();
            group.shutdownGracefully();
        }
    }

    private boolean isActive() {
        if(ch == null) {
            return false;
        } else {
            return ch.isActive();
        }
    }

    private boolean isOpen() {
        if(ch == null) {
            return false;
        } else {
            return ch.isOpen();
        }
    }
}

