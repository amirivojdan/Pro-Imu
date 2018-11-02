/*
* Dynamixel.h
*
* Created: 11/5/2013 3:59:51 PM
*  Author: Ahmad Amiri
*/


#ifndef DYNAMIXEL_H_
#define DYNAMIXEL_H_


#include "../SerialPort/SerialPort.h"
#include "Interpreter.h"
#include "ROM.h"
#include <avr/eeprom.h>
#include "../Utility/ByteArrayFloat.h"

namespace Dynamixel{
	
	
	#define CONTROL_TABLE_SIZE 128
	extern  unsigned char ControlTable[CONTROL_TABLE_SIZE];
	
	#define CONTROL_TABLE_RAM_START 30
	
	#define default_ID 0X01
	#define default_FirmwareVersion 23
	#define default_ModelNumber 0X001C
	#define default_ModelNumberL 0X1C
	#define default_ModelNumberH 0X00
	
	#define default_BaudNum 0x1
	#define default_autoSend 0
	#define default_Gyro_X_Offset 0
	#define default_Gyro_Y_Offset 0
	#define default_Gyro_Z_Offset 0
	
	#define default_beta 0.1f
	 
	enum Control_Table_Address{
		// ++++++++++++++++++++ Essential protocol registers ++++++++++++++
		ModelNumberL=0,
		ModelNumberH=1,
		FirmwareVersion=2,
		ID=3,
		BaudNum=4,
		AutoSend=5,
		
		// +++++++++++++++ custom registers +++++++++
		
		//EEPROM ( Register Address < RAMSTART(30) )
		Gyro_X_Offset=6,
		Gyro_Y_Offset=7,
		Gyro_Z_Offset=8,
		
		Beta=9,
		
		//RAM (Start at RAMSTART (30))
		Raw_Accel_X_H=30,
		Raw_Accel_X_L=31,
		Raw_Accel_Y_H=32,
		Raw_Accel_Y_L=33,
		Raw_Accel_Z_H=34,
		Raw_Accel_Z_L=35,
		
		Raw_Temprature_H=36,
		Raw_Temprature_L=37,
		
		Raw_Gyro_X_H=38,
		Raw_Gyro_X_L=39,
		Raw_Gyro_Y_H=40,
		Raw_Gyro_Y_L=41,
		Raw_Gyro_Z_H=42,
		Raw_Gyro_Z_L=43,
		
		// raw magnetometer should add here
		
		Filtered_Q0=44,
		Filtered_Q1=48,
		Filtered_Q2=52,
		Filtered_Q3=56,
		
		Filtered_Angle_Roll=60,
		Filtered_Angle_Pitch=64,
		Filtered_Angle_Yaw=68, 
	};

	void Initialize();
	void Reset();
	unsigned char GetByte(unsigned char address);
	unsigned int GetWord(unsigned char address);
	void SetByte(unsigned char address,unsigned char data);
	
	void SetBaudRate(unsigned char baudNum);
	void SetId(unsigned char id);	
	
}

#endif /* DYNAMIXEL_H_ */