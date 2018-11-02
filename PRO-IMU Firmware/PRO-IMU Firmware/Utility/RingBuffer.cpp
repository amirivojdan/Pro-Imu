/*
* RingBuffer.cpp
*
* Created: 7/3/2013 8:51:32 PM
*  Author: Ahmad Amiri
*/
#include "RingBuffer.h"
namespace RingBuffer{
	
	#define BUFFER_SIZE	200
	volatile unsigned char Buffer[BUFFER_SIZE] = {0};
	volatile unsigned char Head = 0;
	volatile unsigned char Tail = 0;

	void ClearBuffer(void)
	{
		// Clear communication buffer
		Head = Tail;
	}

	int GetBufferState(void)
	{
		short NumByte;
		
		if( Head == Tail )
		{
			NumByte = 0;
		}
		else if( Head < Tail ){
			NumByte = Tail - Head;
		}
		else{
			NumByte = BUFFER_SIZE - (Head - Tail);
		}
		
		return (int)NumByte;
	}

	void PutChar( unsigned char data )
	{
		if( GetBufferState() == (BUFFER_SIZE-1) )
		{
			return;
		}
		
		Buffer[Tail] = data;

		if( Tail == (BUFFER_SIZE-1) )
		{
			Tail = 0;
		}
		else{
			Tail++;
		}
	}
	bool GetChar(unsigned char & data)
	{
		
		if( Head == Tail ){
			return false;
		}
		
		data = Buffer[Head];
		if( Head == (BUFFER_SIZE-1) )
		{
			Head = 0;
		}
		else{
			Head++;
		}
		return true;
	}
}