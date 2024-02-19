import socket
import threading # 多线程
 
def dispose_client_request(tcp_client_1, tcp_client_address):
    # 循环接收和发送消息
    while True:
        recv_data = tcp_client_1.recv(4096)
        # 有消息就回复数据，如果消息长度为0表示客户端下线了
        if recv_data:
            print("客户端是:", tcp_client_address)
            print("客户端发来的消息是:", str(recv_data.decode()))
 
        else:
            print("%s 客户端下线了..." % tcp_client_address[1])
            tcp_client_1.close()
            break
 
if __name__ == '__main__':
    # 创建服务端套接字对象
    tcp_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # 端口复用
    tcp_server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, True)
    # 绑定端口
    tcp_server.bind(("192.168.31.73", 8266)) # 这里填局域网下的IPV4和自己设置的端口（这里是8266号端口）
    # 设置监听
    tcp_server.listen(128)
    # 循环等待客户端连接请求（最多128个）
    while True:
        tcp_client_1, tcp_client_address = tcp_server.accept()
        print  ('connected from:', tcp_client_address)
        socks = tcp_client_1
        # 创建多线程对象(读取)
        thd = threading.Thread(target=dispose_client_request, args=(tcp_client_1, tcp_client_address))
        # 设置守护主线程，如果主线程断了，子线程全部会销毁，防止主线程无法退出
        thd.setDaemon(True)
        # 启动子线程对象
        thd.start()