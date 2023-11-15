from socket import socket, AF_INET, SOCK_DGRAM

server_address = ('',10000)

with socket(AF_INET, SOCK_DGRAM) as server:
	server.bind(server_address)

	data, client_addr = server.recvfrom(256)
	print("Received:",data.decode(),"from:", client_addr)

	server.sendto("Hello client!".encode(), client_addr)
	
exit(0)
import socket
import struct
import sys

message = 'very important data'
multicast_group = ('224.1.1.1', 10000)

# Create the datagram socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Set a timeout so the socket does not block indefinitely when trying
# to receive data.
sock.settimeout(1.0)

# Set the time-to-live for messages to 1 so they do not go past the
# local network segment.
ttl = struct.pack('b', 1)
sock.setsockopt(socket.IPPROTO_IP, socket.IP_MULTICAST_TTL, ttl)

try:

    # Send data to the multicast group
    print ( 'sending "%s"' % message)
    sent = sock.sendto(message.encode(), multicast_group)

    # Look for responses from all recipients
    while True:
        print ('waiting to receive')
        try:
            data, server = sock.recvfrom(16)
        except socket.timeout:
            print ('timed out, no more responses')
        else:
            print ('received "%s" from %s' % (data.decode(), server))
            break

finally:
    print ('closing socket')
    sock.close()