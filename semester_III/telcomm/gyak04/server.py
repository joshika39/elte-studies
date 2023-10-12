import socket
import struct
import sys
import select

TCP_IP = "localhost"  # "127.0.0.1" or ""
TCP_PORT = int(sys.argv[1])
BUFFER_SIZE = 1024
reply = "Hello kliens".encode()  # could be just b"Hello kliens"

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind((TCP_IP, TCP_PORT))
sock.listen(5)

inputs = [sock]
timeout = 1.0

unpacker = struct.Struct('I I 1s')  # int, int, char[1]

while True:
    try:
        readables, _, _ = select.select(inputs, [], [], timeout)

        for s in readables:
            if s is sock:
                connection, client_info = sock.accept()
                print(f"Someone has connected: {client_info[0]}:{client_info[1]}")
                inputs.append(connection)
            else:
                data = s.recv(BUFFER_SIZE)
                if not data:
                    s.close()
                    print("The client has terminated the connection")
                    inputs.remove(s)
                    continue
                unp_data = unpacker.unpack(data)
                print("Unpack:", unp_data)
                x = eval(str(unp_data[0]) + unp_data[2].decode() + str(unp_data[1]))
                s.sendall(str(x).encode())
                print("Sent response:", reply)

    except KeyboardInterrupt:
        for s in inputs:
            s.close()
        print("Server closing")
        break
