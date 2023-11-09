import socket
import sys
import select

TCP_IP = "localhost"  # "127.0.0.1" or ""
TCP_PORT = 10000
BUFFER_SIZE = 1024

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind((TCP_IP, TCP_PORT))
sock.listen(5)

zh_server_address = ('localhost', 1234)
client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)


required_stud = int(sys.argv[1])
debug = False

inputs = [sock]
timeout = 1.0

student_count = 0

while True:
    try:
        readables, _, _ = select.select(inputs, [], [], timeout)

        for s in readables:
            if s is sock:
                connection, client_info = sock.accept()
                if debug:
                    print(f"Someone has connected: {client_info[0]}:{client_info[1]}")
                inputs.append(connection)
            else:
                msg = s.recv(BUFFER_SIZE)
                if not msg:
                    s.close()
                    if debug:
                        print("The client has terminated the connection")
                    inputs.remove(s)
                    continue
                if "Kerek feladatot" in msg.decode():
                    student_count += 1
                print("The student's message:", msg.decode())
                if msg.decode() == "Koszonjuk":
                    s.sendall(b"Szivesen")
                    continue
                if student_count < required_stud:
                    s.sendall(b"Meg nincs")
                else:
                    client.sendto(b"Keres", zh_server_address)
                    data, _ = client.recvfrom(1024)
                    s.sendall(f"Tessek a {data.decode()}".encode())

    except KeyboardInterrupt:
        for s in inputs:
            s.close()
        print("Server closing")
        break