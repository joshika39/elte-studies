import json
import socket
import struct
import sys

if len(sys.argv) == 4:
    server_addr = sys.argv[1]
    server_port = int(sys.argv[2])
    screenings_source = sys.argv[3]
else:
    server_addr = "localhost"
    server_port = 10000
BUFFER_SIZE = 1024

packer = struct.Struct("15s 6s")

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((server_addr, server_port))
sock.sendall(b"GET_SCREENINGS")
reply = sock.recv(BUFFER_SIZE)
if reply:
    json_data = json.loads(reply.decode())
    print(json_data)
    packed_data = packer.pack(*(json_data[0]["movie"].encode(), json_data[0]["time"].encode()))
    sock.sendall(packed_data)
    reply = sock.recv(BUFFER_SIZE)
    unpacked_data = packer.unpack(reply)
    print(unpacked_data[0].decode(), unpacked_data[1].decode())

sock.close()
