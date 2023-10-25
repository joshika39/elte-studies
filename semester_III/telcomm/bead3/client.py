import random
import socket
import struct
import sys
import time


def main():
    if len(sys.argv) != 3:
        print("Használat: python3 client.py <hostname> <port szám>")
        return

    host = sys.argv[1]
    port = int(sys.argv[2])

    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client_socket.connect((host, port))
    guess = 50
    multiplier = 25
    while True:
        time_to_wait = random.randint(1, 3)
        print(f"Sleeping for {time_to_wait}, then guessing: {guess} -> ({round(guess)})")
        time.sleep(time_to_wait)
        if multiplier <= 1:
            client_socket.send(struct.pack('cI', b'=', round(guess)))
        else:
            client_socket.send(struct.pack('cI', b'<', round(guess)))
        response = client_socket.recv(1024)
        result, _ = struct.unpack('cI', response)
        if result == b'Y':
            print("Nyertél!")
            break
        elif result == b'N':
            guess = guess + multiplier
            multiplier = multiplier / 2
        elif result == b'I':
            guess = guess - multiplier
            multiplier = multiplier / 2
        elif result == b'V':
            print("A játék már véget ért.")
            break
        elif result == b'K':
            print("Kiestél")
            break
    client_socket.close()


if __name__ == "__main__":
    main()
