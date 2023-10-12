from socket import socket, AF_INET, SOCK_STREAM, timeout, SOL_SOCKET, SO_REUSEADDR
import struct
import sys

TCP_IP = "80.240.30.249"
TCP_PORT = int(sys.argv[1])
BUFFER_SIZE = 1024
packer = struct.Struct('I I 1s')  # int, int, char[1]
server_addr = (TCP_IP, TCP_PORT)

with socket(AF_INET, SOCK_STREAM) as client:
    client.connect(server_addr)

    client.sendall(b'Joshua')
    client.sendall(b'Hello world')
    data = client.recv(16).decode()

    print("Result:", data)
