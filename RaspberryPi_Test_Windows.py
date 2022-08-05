import socket
from _thread import *
import threading
import time
message="12*12*45*54";
connect=1
host = socket.gethostbyname(socket.gethostname())#socket.gethostname() #""
port = 12345
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((host, port))
print("socket binded to port", port)
s.listen(1)
print("socket is listening")
print_lock = threading.Lock()
def Sendmsj(c):
    i=0
    while True:
        if i>360:
            i=0
        i+=1
        message=str(i)+"*"+str(i)+"*"+str(i)+"*"
        try:
            c.send(message.encode('utf-8'))
        except:
            print_lock.release()
            break
        time.sleep(0.4)

def connction():
    global c,addr
    c, addr = s.accept()   
    print('Connected to :', addr[0], ':', addr[1])        
        
def Main():
    while True:     
        connction()
        print('Connected to :', addr[0], ':', addr[1])  
        print_lock.acquire()  
        start_new_thread(Sendmsj, (c,))
        while True:
            data = c.recv(1024)
            if not data:
                print("data yok")
            if data.decode('utf-8')=="43+j&f+3V+f2eCR=E%4h":
                print("Beklemede")
                break
            if data.decode('utf-8')=="10":
                print("Motor Thrik komutu")
            print('Received from the client :',str(data.decode('utf-8')))
        c.close()
        print("client disconnected")


if __name__ == '__main__':
	Main()
