from struct import Struct

packer = Struct('20s i')


with open('test.bin', 'wb') as f:
    for i in range(10):
        data = packer.pack(b'localhost', i*10)
        f.write(data)

