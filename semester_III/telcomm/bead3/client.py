from socket import socket, AF_INET, SOCK_DGRAM, timeout, SOL_SOCKET, SO_REUSEADDR
import struct
import sys

TCP_IP = "localhost"
TCP_PORT = int(sys.argv[1])
BUFFER_SIZE = 1024
packer = struct.Struct('ci')
server_addr = (TCP_IP, TCP_PORT)

with socket(AF_INET, SOCK_DGRAM) as client:
    client.connect(server_addr)

    data, address = client.recvfrom(4096)
    received = packer.unpack(data)
    print(received)
    while received != 'K' or received != 'N' or received != 'Y':
        packed_data = packer.pack()
        sent = client.sendto(packed_data, server_addr)

        data, address = client.recvfrom(4096)
        received = packer.unpack(data)
