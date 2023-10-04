from struct import Struct
import socket

unpack = Struct('20s i')

with open('domains.bin', 'rb') as f:
    while(True):
        data = f.read(unpack.size)
        if not data:
            break
        domain, port = unpack.unpack(data)
        domain = domain.decode().strip('\x00')
        try:
            print(domain, socket.gethostbyname(domain))
        except:
            print(f'Wrong domain: {domain}')
        print(socket.getservbyport(port, 'tcp'))

