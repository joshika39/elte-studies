import random
from socket import socket, AF_INET, SOCK_DGRAM

server_address = ('localhost', 1234)
debug = False

with socket(AF_INET, SOCK_DGRAM) as server:
    server.bind(server_address)
    while True:
        try:
            data, client_addr = server.recvfrom(256)
            if debug:
                print("Received:", data.decode(), "from:", client_addr)

            if data.decode() == "Keres":
                num = random.randint(1, 11)
                server.sendto(f"feladat{num}".encode(), client_addr)
        except KeyboardInterrupt:
            break
