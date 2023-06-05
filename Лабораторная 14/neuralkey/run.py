__author_ = "Fariz Rahman"

from machine import Machine
import numpy as np
import time
import sys

#Machine parameters
k = 100
n = 10
l = 10

#Update rule
update_rules = ['hebbian', 'anti_hebbian', 'random_walk']
update_rule = update_rules[2]

#Create 3 machines : Alice, Bob and Eve. Eve will try to intercept the communication between
#Alice and Bob.
print("Создание машин : k=" + str(k) + ", n=" + str(n) + ", l=" + str(n))
print("Используем " + update_rule + " правило обновления.")
Alice = Machine(k, n, l)
Bob = Machine(k, n, l)
Eve = Machine(k, n, l)

#Random number generator
def random():
	return np.random.randint(-l, l + 1, [k, n])

#Function to evaluate the synchronization score between two machines.
def sync_score(m1, m2):
	return 1.0 - np.average(1.0 * np.abs(m1.W - m2.W)/(2 * l))

#Synchronize weights

sync = False # Flag to check if weights are sync
nb_updates = 0 # Update counter
nb_eve_updates = 0 # To count the number of times eve updated
start_time = time.time() # Start time
sync_history = [] # to store the sync score after every update

while(not sync):

	X = random() # Create random vector of dimensions [k, n]

	tauA = Alice(X) # Get output from Alice
	tauB = Bob(X) # Get output from Bob
	tauE = Eve(X) # Get output from Eve

	Alice.update(tauB, update_rule) # Update Alice with Bob's output
	Bob.update(tauA, update_rule) # Update Bob with Alice's output

	#Eve would update only if tauA = tauB = tauE
	if tauA == tauB == tauE:
		Eve.update(tauA, update_rule)
		nb_eve_updates += 1

	nb_updates += 1

	score = 100 * sync_score(Alice, Bob) # Calculate the synchronization of the 2 machines

	sync_history.append(score) # Add sync score to history, so that we can plot a graph later.

	sys.stdout.write('\r' + "Синхронизация = " + str(int(score)) + "%   /  Обновления = " + str(nb_updates) + " / Обновления машины Евы = " + str(nb_eve_updates)) 
	if score == 100: # If synchronization score is 100%, set sync flag = True
		sync = True

end_time = time.time()
time_taken = end_time - start_time # Calculate time taken

#Print results
print ('\nМашины синхронизированы!')
print ('Затраченное время = ' + str(time_taken)+ " секунд.")
print ('Обновления = ' + str(nb_updates) + ".")

#See if Eve got what she wanted:
eve_score = 100 * int(sync_score(Alice, Eve))
if eve_score > 100:
	print("Ой! Ева синхронизировала свою машину с машинами Алисы и Боба!")
else:
	print("Машина Евы только на " + str(eve_score) + " % " + "синхронизировала свою машину с машинами Алисы и Боба и она сделала " + str(nb_eve_updates) + " обновлений.") 

#Plot graph 
import matplotlib.pyplot as mpl
mpl.plot(sync_history)
mpl.show()
