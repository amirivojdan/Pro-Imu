/*
* Queue.cpp
*
* Created: 1/15/2014 2:19:37 AM
*  Author: Ahmad Amiri
*/

#include "Queue.h"

namespace Queue{
	volatile unsigned char queue_beffer[QUEUE_SIZE], head=0, tail=0;
	void Enque(unsigned char data){

		queue_beffer[tail] = data;
		tail =(tail+1)%QUEUE_SIZE ;       //OR tail =  (tail==MAX) ? 0 : tail+1 ; */
		}

		unsigned char  Dequeue(){

		unsigned char temp =queue_beffer[head];
		head =(head+1)%QUEUE_SIZE ;       //OR head =  (head==MAX) ? 0 : head+1 ; */
		return temp;
		}
		
		bool isEmpty(){
		
		if(head == tail)
		return true;
		else
		return false;
		}
		}