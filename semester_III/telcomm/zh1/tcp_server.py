import json
import random
import string
import sys
import socket
import struct
from typing import Optional
import select

from screening import Screening


def id_generator(size=6, chars=string.ascii_uppercase + string.digits):
    return ''.join(random.choice(chars) for _ in range(size))


if len(sys.argv) == 4:
    server_addr = sys.argv[1]
    server_port = int(sys.argv[2])
    screenings_source = sys.argv[3]
else:
    server_addr = "localhost"
    server_port = 10001
    screenings_source = "screenings.json"

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
sock.bind((server_addr, server_port))

sock.listen(5)

packer = struct.Struct("15s 6s")
BUFFER = 1024
inputs = [sock]
timeout = 1.0

screenings_data = json.load(open(screenings_source, "r"))
screenings = []

for screening in screenings_data:
    screenings.append(Screening(screening["movie"], screening["room"], screening["time"]))

while True:
    try:
        readables, _, _ = select.select(inputs, [], [], timeout)

        for s in readables:
            if s is sock:
                connection, client_info = sock.accept()
                print(f"Someone has connected: {client_info[0]}:{client_info[1]}")
                inputs.append(connection)
            else:
                msg = s.recv(BUFFER)
                if not msg:
                    s.close()
                    print("The client has terminated the connection")
                    inputs.remove(s)
                    continue

                if msg.decode() == "GET_SCREENINGS":
                    response = json.dumps(screenings_data).encode()
                    s.sendall(response)

                else:
                    unpacked_data = packer.unpack(msg)
                    print(f"Movie: {unpacked_data[0].decode()}, time: {unpacked_data[1].decode()}")
                    for screen in screenings:
                        print(f"Movie: {screen.movie}, time: {screen.time}, booked: {screen.booked}")
                        if screen.movie == unpacked_data[0].decode().strip('\x00') and screen.time == unpacked_data[1].decode().strip('\x00') and screen.booked < 5:
                            screen.booked += 1
                            packed_response = packer.pack(*(b'SUCCESS', id_generator().encode()))
                            s.sendall(packed_response)
                        else:
                            packed_response = packer.pack(*(b'FAIL', b'000000'))
                            s.sendall(packed_response)
    except KeyboardInterrupt:
        for s in inputs:
            s.close()
        print("Server closing")
        break
