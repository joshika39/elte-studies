import struct
from struct import pack, unpack
import sys

if __name__ == '__main__':
    packers = ['c9si', 'if?', '?9sc', 'icf']
    if len(sys.argv) < 2:
        print("Missing input file")
        exit(1)
    files = sys.argv[1:len(sys.argv)]
    i = 0
    for fName in files:
        if i < 4:
            with open(fName, 'rb') as f:
                data = f.readline()
                try:
                    print(unpack(packers[i], data))
                except Exception as e:
                    print(e)
                    print(f"Error: {data}")
        i = i + 1
    print(pack('13si?', b'elso', 81, True))
    print(pack('f?c', 84.5, False,  b'X'))
    print(pack('i11sf', 72, b"masodik", 91.9))
    print(pack('ci14s', b'Z', 103, b"harmadik"))



