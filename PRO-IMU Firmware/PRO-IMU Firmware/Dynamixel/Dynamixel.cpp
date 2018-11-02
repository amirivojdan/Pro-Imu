/*
* Dynamixel.cpp
*
* Created: 11/5/2013 4:00:09 PM
*  Author: Ahmad Amiri
*/

#include "Dynamixel.h"

namespace Dynamixel{
	
	unsigned char ControlTable[CONTROL_TABLE_SIZE]={0};

	void Initialize(){
		
		ControlTable[ModelNumberL]= EEPROM::ReadByte(ModelNumberL);
		ControlTable[ModelNumberH]= EEPROM::ReadByte(ModelNumberH);
		ControlTable[FirmwareVersion]= EEPROM::ReadByte(FirmwareVersion);
		
		
		if ( (ControlTable[ModelNumberL]!=default_ModelNumberL) || (ControlTable[ModelNumberH]!=default_ModelNumberH) || (ControlTable[FirmwareVersion]!=default_FirmwareVersion) )
		{
			Reset();
		}
		
		for (unsigned char i = 0 ; i < CONTROL_TABLE_RAM_START; i++)
		{
			ControlTable[i] = EEPROM::ReadByte(i);
		}
		
		

		
		//ControlTable[BaudNum]=EEPROM::ReadByte(BaudNum);
		//ControlTable[ID]=EEPROM::ReadByte(ID);
		
		Uart::Initialize((2000000/(ControlTable[BaudNum]+1)),Uart::DoubleSpeed);
		Uart::EnableRxInterrupt(Interpreter::RxInterruptHandler);
	}
	
	void Reset(){

		EEPROM::WriteByte(ID,default_ID);
		EEPROM::WriteByte(FirmwareVersion,default_FirmwareVersion);
		EEPROM::WriteByte(ModelNumberL,default_ModelNumberL);
		EEPROM::WriteByte(ModelNumberH,default_ModelNumberH);
		EEPROM::WriteByte(BaudNum,default_BaudNum);
		EEPROM::WriteByte(AutoSend,default_autoSend);
		EEPROM::WriteByte(Gyro_X_Offset,default_Gyro_X_Offset);
		EEPROM::WriteByte(Gyro_Y_Offset,default_Gyro_Y_Offset);
		EEPROM::WriteByte(Gyro_Z_Offset,default_Gyro_Z_Offset);
		
		ByteArrayFloat initialBeta;
		initialBeta.FloatNumber = default_beta;
		
		EEPROM::WriteByte(Beta,initialBeta.bytes[0]);
		EEPROM::WriteByte(Beta+1,initialBeta.bytes[1]);
		EEPROM::WriteByte(Beta+2,initialBeta.bytes[2]);
		EEPROM::WriteByte(Beta+3,initialBeta.bytes[3]);
		
		
		ControlTable[ID]=default_ID;
		ControlTable[ModelNumberL] = default_ModelNumberL;
		ControlTable[ModelNumberH] = default_ModelNumberH;
		ControlTable[FirmwareVersion] = default_FirmwareVersion;
		ControlTable[BaudNum] = default_BaudNum;
		ControlTable[AutoSend] = default_autoSend;
		
		ControlTable[Gyro_X_Offset] = default_Gyro_X_Offset;
		ControlTable[Gyro_Y_Offset] = default_Gyro_Y_Offset;
		ControlTable[Gyro_Z_Offset] = default_Gyro_Z_Offset;
		
		ControlTable[Beta] = initialBeta.bytes[0];
		ControlTable[Beta+1] = initialBeta.bytes[1];
		ControlTable[Beta+2] = initialBeta.bytes[2];
		ControlTable[Beta+3] = initialBeta.bytes[3];
		
	}
	
	void SetBaudRate(unsigned char baudNum)
	{
		ControlTable[BaudNum] = baudNum;
		Uart::Initialize((2000000/(ControlTable[BaudNum]+1)),Uart::DoubleSpeed);
		Uart::EnableRxInterrupt(Interpreter::RxInterruptHandler); // Should Call to enable RX interrupt
		EEPROM::WriteByte(BaudNum,baudNum);
	}
	
	void SetId(unsigned char id){
		
		ControlTable[ID]=id;
		EEPROM::WriteByte(ID,id);
	}

}