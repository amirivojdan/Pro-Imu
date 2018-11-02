/*
* Queue.h
*
* Created: 1/15/2014 2:19:24 AM
*  Author: Ahmad Amiri
*/


#ifndef QUEUE_H_
#define QUEUE_H_

namespace Queue{
	#define QUEUE_SIZE 128

	void Enque(unsigned char data);        // ~insertion
	unsigned char  Dequeue();             // ~deletion
	bool isEmpty(void);
}

#endif /* QUEUE_H_ */