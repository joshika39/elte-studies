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
    print(f"n={target_number}")
    game_finished = False
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((host, port))
    server_socket.listen(5)
    inputs.append(server_socket)

    while inputs and not game_finished:
        try:
            readable, writable, exceptional = select.select(inputs, [], inputs, 1)
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

                    if game_finished:
                        sock.send(struct.pack('cI', *(b'V', 0)))
                        continue

                    print(f'{message} {guess}')
                    if message == b'<':
                        result = b'I' if target_number < guess else b'N'
                    elif message == b'>':
                        result = b'I' if target_number > guess else b'N'
                    elif message == b'=':
                        if guess == target_number:
                            result = b'Y'
                            game_finished = True
                        else:
                            result = b'K'
                    else:
                        result = b'K'
                    sock.send(struct.pack('cI', *(result, 0)))
            for s in exceptional:
                print('Exceptional' + str(s.getpeername()))
                inputs.remove(s)
                s.close()
        except KeyboardInterrupt:
            for s in inputs:
                s.close()
            inputs = []
            break

    for s in inputs:
        if s is not server_socket:
            s.send(struct.pack('cI', *(b'V', 0)))
    server_socket.close()


if __name__ == "__main__":
    main()
