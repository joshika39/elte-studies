import socket
import struct
import sys
import select

TCP_IP = "localhost"  # "127.0.0.1" or ""
TCP_PORT = int(sys.argv[1])
BUFFER_SIZE = 1024

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind((TCP_IP, TCP_PORT))

unpacker = struct.Struct('I I 1s')  # int, int, char[1]

while True:
    try:
        data, address = sock.recvfrom(4096)
        unp_data = unpacker.unpack(data)
        print("Unpack:", unp_data)
        x = eval(str(unp_data[0]) + unp_data[2].decode() + str(unp_data[1]))
        sent = sock.sendto(data, address)
        print("Sent response:", sent)

    except KeyboardInterrupt:
        print("Server closing")
        break
