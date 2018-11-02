/*
 * I2C.h
 *
 * Created: 11/10/2013 1:02:19 PM
 *  Author: Ahmad
 */ 


#ifndef I2C_H_
#define I2C_H_

#include <avr/io.h>
#include <util/twi.h>
#include <math.h>
namespace I2C{
	
	 
	#define TW_INSTRUCTION_BIT (0x01)
	void Initialize(unsigned long int bitrate,unsigned char prescale);
	void Start();
	void ReStart();
	void Stop();
	unsigned char ReadByte();
	unsigned char ReadLastByte();
	void WriteByte(unsigned char data);
	void WriteDeviceAddress(unsigned char data,unsigned char instruction);
}



#endif /* I2C_H_ */