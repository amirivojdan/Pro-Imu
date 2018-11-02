/*
 * ByteArrayFloat.h
 *
 * Created: 1/27/2014 6:52:30 AM
 *  Author: Ahmad Amiri
 */ 


#ifndef BYTEARRAYFLOAT_H_
#define BYTEARRAYFLOAT_H_

union ByteArrayFloat
{
	unsigned char bytes[4];
	float FloatNumber;
};



#endif /* BYTEARRAYFLOAT_H_ */