import socket
from time import time
import select
import sys

server_address = sys.argv[1]
port = int(sys.argv[2])
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.settimeout(1.0)
server.bind((server_address, port))
server.listen()
inputs = [server]
validators = dict()
previous_time = time()
current_time = previous_time

while inputs:
    try:
        readable, writable, ex = select.select(inputs, [], inputs, server.timeout)
        if not (readable or writable or ex):
            continue
        for sock in readable:
            if sock is server:
                connection, client_address = server.accept()
                connection.setblocking(False)
                inputs.append(connection)
            else:
                previous_time = current_time
                current_time = time()

                for key in list(validators.keys()):
                    if current_time - previous_time > validators[key][3]:
                        del validators[key]
                data = sock.recv(512)
                if data:
                    print(data.decode())
                    if data.decode().split('|')[0] == 'BE':
                        msg = data.decode().split('|')
                        msg = [msg[0], msg[1], msg[2], msg[3], msg[4]]
                        validators[msg[1]] = (msg[1], int(msg[2]), msg[4], int(msg[3]))
                        sock.sendall("OK".encode())
                    elif data.decode().split('|')[0] == 'KI':
                        msg = data.decode().split('|')
                        if msg[1] in validators.keys():
                            record = validators[msg[1]]
                            sock.sendall(f'{str(record[1])}|{str(record[2])}'.encode())
                        else:
                            sock.sendall('0|'.encode())
                else:
                    inputs.remove(sock)
                    sock.close()
        for s in ex:
            inputs.remove(s)
            s.close()
    except KeyboardInterrupt:
        for client in inputs:
            client.close()
        inputs.clear()
