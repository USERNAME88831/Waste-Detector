from ClassModule import FindClass
import socket
from _thread import * 


ip = "127.0.0.1"
port = 22

addr = (ip, port)

socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
    socket.bind(addr)
except SystemError as e
    print("err:")
    print(e)


def HandleReq(conn):
    DataRecv = conn.recv(1024).decode("utf8")
    Data = DataRecv.decode("utf8")
    reply = ""

    if not Data:
        break
    else:
        try:
            reply = FindClass(Data)
        except:
            reply = "err"
    conn.sendall(reply.encode("utf8"))


def Start():
    conn, addr = socket.accept()

    while True:
        start_new_thread(HandleReq, (conn,))

Start()