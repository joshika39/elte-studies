import random
import socket
import struct
import sys
import time

packer = struct.Struct('c i')

EQUAL = b'='
LESS = b'<'
GTR = b'>'


def main():
    if len(sys.argv) != 3:
        print("Használat: python3 client.py <hostname> <port szám>")
        return

    host = sys.argv[1]
    port = int(sys.argv[2])

    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client_socket.connect((host, port))
    minimum = 0
    maximum = 100
    message = LESS
    previous_message = message
    guess = (minimum + maximum) // 2
    client_socket.sendall(packer.pack(message, guess))

    while True:
        data = client_socket.recv(1024)
        result, _ = packer.unpack(data)
        previous_guess = guess

        if data:
            if result == b'V' or result == b'Y' or result == b'K':
                client_socket.close()
                break
            print(f'{minimum} - {maximum}')

            if minimum >= maximum:
                message = EQUAL

            if minimum == maximum - 1:
                message = EQUAL
                guess = maximum - 1

            elif result == b'N':
                if previous_message == LESS:
                    minimum = previous_guess
                if previous_message == GTR:
                    maximum = previous_guess
            elif result == b'I':
                if previous_message == LESS:
                    maximum = previous_guess - 1
                if previous_message == GTR:
                    minimum = previous_guess + 1
            guess = (minimum + maximum) // 2
            client_socket.sendall(packer.pack(message, guess))


if __name__ == "__main__":
    main()
