from socket import socket, AF_INET, SOCK_DGRAM
import sys

TCP_IP = "localhost"
TCP_PORT = int(sys.argv[1])
BUFFER_SIZE = 1024
server_addr = (TCP_IP, TCP_PORT)

imagePath = sys.argv[2]

with socket(AF_INET, SOCK_DGRAM) as sock, open(imagePath, 'rb') as file:
    data = file.read(200)
    while data:
        sent = sock.sendto(data, server_addr)
        resp, address = sock.recvfrom(200)
        print("Response:", resp)
        data = file.read(200)
    sock.sendto(b'', server_addr)
