import socket

# host = socket.gethostbyname("spencerpi")
port = 13000                   # The same port as used by the server
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(("10.0.0.43", port))
s.sendall(b'Hello, world')
data = s.recv(1024)
s.close()
print('Received', repr(data))