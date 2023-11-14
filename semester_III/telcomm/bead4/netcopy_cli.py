import sys
import socket

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
checksum_server_address = sys.argv[3]
checksum_port = int(sys.argv[4])
f_id = int(sys.argv[5])
filename = sys.argv[6]

valid = 60
zokni = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

zokni.connect((server_address, port))
fileBytes = bytearray()
with open(filename, 'br') as file:
    data = file.read(512)
    while data != b'':
        zokni.send(data)
        fileBytes.extend(data)
        data = file.read(512)
    zokni.send(b'')

zokni.close()
checksum_client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
checksum_client.connect((checksum_server_address, checksum_port))
msg = ('|'.join(['BE', str(f_id), str(valid), str(crc16(fileBytes)), str(crc16(fileBytes))])).encode()
print(msg)
checksum_client.send(msg)
response = checksum_client.recv(2)
print(response.decode())
