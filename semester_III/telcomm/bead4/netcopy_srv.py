import socket
import sys


def crc16(data: bytes, poly=0x8408):
    data = bytearray(data)
    crc = 0xFFFF
    for b in data:
        cur_byte = 0xFF & b
        for _ in range(0, 8):
            if (crc & 0x0001) ^ (cur_byte & 0x0001):
                crc = (crc >> 1) ^ poly
            else:
                crc >>= 1
            cur_byte >>= 1
    crc = (~crc & 0xFFFF)
    crc = (crc << 8) | ((crc >> 8) & 0xFF)

    return crc & 0xFFFF


if len(sys.argv) < 7:
    print('Invalid number of arguments')
    exit(0)

server_address = sys.argv[1]
port = int(sys.argv[2])

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((server_address, port))
server.listen()

checksum_server_address = sys.argv[3]
checksum_port = int(sys.argv[4])
checksum = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
checksum.connect((checksum_server_address, checksum_port))

f_id = int(sys.argv[5])
filename = sys.argv[6]

connection, client_address = server.accept()
fileBytes = bytearray()
with open(filename, 'bw') as f:
    data = connection.recv(512)
    while data != b'':
        f.write(data)
        fileBytes.extend(data)
        data = connection.recv(512)
connection.close()

checksum.send('|'.join(['KI', str(f_id)]).encode())
response = checksum.recv(512)
response = response.decode()
response = response.split('|')

if response[0] == 0 or len(response) != 2:
    print('Invalid checksum')
    exit(0)

print('Valid checksum' if response[1] == str(crc16(fileBytes)) else 'Invalid checksum')
