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

    clients = []
    target_number = random.randint(1, 100)

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((host, port))
    server_socket.listen(5)
    game_finished = False

    while not game_finished:
        try:
            readable, _, _ = select.select([server_socket] + clients, [], [])
            for sock in readable:
                if sock is server_socket:
                    client, _ = server_socket.accept()
                    clients.append(client)
                else:
                    data = sock.recv(1024)
                    if not data:
                        sock.close()
                        clients.remove(sock)
                        continue
                    message, guess = struct.unpack('cI', data)
                    if guess < 1 or guess > 100:
                        sock.send(struct.pack('cI', *(b'K', 0)))
                        continue
                    if message == b'>':
                        if guess > target_number:
                            response = (b'N', 0)
                        elif guess == target_number:
                            response = (b'Y', 0)
                            game_finished = True
                        else:
                            response = (b'I', 0)
                    elif message == b'<':
                        if guess < target_number:
                            response = (b'N', 0)
                        elif guess == target_number:
                            response = (b'Y', 0)
                            game_finished = True
                        else:
                            response = (b'I', 0)
                    elif message == b'=':
                        if guess == target_number:
                            response = (b'Y', 0)
                            game_finished = True
                        else:
                            response = (b'N', 0)
                    else:
                        response = (b'V', 0)
                    sock.send(struct.pack('cI', *response))
        except KeyboardInterrupt:
            for s in clients:
                s.close()
            break

    while True:
        try:
            readable, _, _ = select.select([server_socket] + clients, [], [])
            for sock in readable:
                if sock is server_socket:
                    client, _ = server_socket.accept()
                    clients.append(client)
                else:
                    data = sock.recv(1024)
                    if data:
                        message, guess = struct.unpack('cI', data)
                        response = (b'V', 0)
                        sock.send(struct.pack('cI', *response))
                    else:
                        sock.close()
                        clients.remove(sock)
        except KeyboardInterrupt:
            for s in clients:
                s.close()
            break


if __name__ == "__main__":
    main()
