from socket import socket, AF_INET, SOCK_STREAM

server_addr = ('localhost', 10000)

client = socket(AF_INET, SOCK_STREAM)
client.connect(server_addr)
client.sendall("Kerek feladatot".encode())
data = client.recv(1024)
print(data.decode())
if data.decode() == "Meg nincs":
    client.close()
    exit(0)

if "Tessek a feladat" in data.decode():
    client.sendall("Koszonjuk".encode())
    data = client.recv(1024)
    print(data.decode())
    client.close()
    exit(0)

client.close()
