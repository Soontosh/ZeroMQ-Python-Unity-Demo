import time
import zmq

context = zmq.Context()
socket = context.socket(zmq.PUSH)
socket.bind('tcp://*:5555')

for _ in range(100):
    data = {
        'bool': True,
        'int': 123,
        'string_msg': 'Hello there!'
    }

    print('Sending data...')
    socket.send_json(data)
    print('Sent data')
    time.sleep(2)