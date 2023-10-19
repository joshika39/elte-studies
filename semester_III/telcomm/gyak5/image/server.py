import socket
import sys

TCP_IP = "localhost"  # "127.0.0.1" or ""
TCP_PORT = int(sys.argv[1])

with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as sock, open('out.jpg', 'wb') as file:
    data, address = sock.recvfrom(200)
    while data:
        sock.sendto(b'ok', address)
        data, address = sock.recvfrom(200)
        file.write(data)
