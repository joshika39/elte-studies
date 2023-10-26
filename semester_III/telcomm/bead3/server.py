import socket
import random
import struct
import sys
import select


def finish_game(clients: []):
    for s in clients:
        response = (b'V', 0)
        s.send(struct.pack('cI', *response))


def main():
    host = sys.argv[1]
    port = int(sys.argv[2])

    inputs = []
    target_number = random.randint(1, 100)

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((host, port))
    server_socket.listen(5)
    inputs.append(server_socket)

    while inputs:
        try:
            readable, _, _ = select.select(inputs, [], [])
            for sock in readable:
                if sock is server_socket:
                    client, _ = server_socket.accept()
                    inputs.append(client)
                else:
                    data = sock.recv(1024)
                    if not data:
                        sock.close()
                        inputs.remove(sock)
                        continue
                    message, guess = struct.unpack('cI', data)
                    if message == b'<':
                        result = b'I' if target_number < guess else b'N'
                    elif message == b'>':
                        result = b'I' if target_number > guess else b'N'
                    elif message == b'=':
                        result = b'Y' if guess == target_number else b'K'
                    else:
                        result = b'K'
                    sock.send(struct.pack('cI', *(result, 0)))
        except KeyboardInterrupt:
            for s in inputs:
                s.close()
            inputs = []
            break


if __name__ == "__main__":
    main()
