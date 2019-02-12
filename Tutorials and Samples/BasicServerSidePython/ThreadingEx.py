import threading
from queue import Queue
import time
import MariaDBConnection

print_lock = threading.Lock()
counter = 0 
def exampleJob(worker):
    time.sleep(.5) # pretend to do some work.
    if(counter == 0):
        MariaDBConnection.startServer
        with print_lock:
            print(threading.current_thread().name,worker)
        counter +=1
    elif(counter == 1):
        #thread 2 start
        print("Thread 2 Start")

# The threader thread pulls an worker from the queue and processes it
def threader():
    while True:
        # gets an worker from the queue
        worker = q.get()

        # Run the example job with the avail worker in queue (thread)
        exampleJob(worker)

        # completed with the job
        q.task_done()

# Create the queue and threader 
q = Queue()

# how many threads are we going to allow for
for x in range(2):
     t = threading.Thread(target=threader)

     # classifying as a daemon, so they will die when the main dies
     t.daemon = True

     # begins, must come after daemon definition
     t.start()

start = time.time()

# 20 jobs assigned.
for worker in range(2):
    q.put(worker)

# wait until the thread terminates.
q.join()

# with 10 workers and 20 tasks, with each task being .5 seconds, then the completed job
# is ~1 second using threading. Normally 20 tasks with .5 seconds each would take 10 seconds.
print('Entire job took:',time.time() - start)