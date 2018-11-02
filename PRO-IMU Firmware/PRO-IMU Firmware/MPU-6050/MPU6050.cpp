/*
* MPU6050.cpp
*
* Created: 11/13/2013 6:51:13 PM
*  Author: Ahmad Amiri
*/

#include "MPU6050.h"
namespace MPU6050{

	void Initialize(){
		I2C::Initialize(400000,0);
		
		WriteByte(MPU6050_REG_PWR_MGMT_1,0);
		WriteByte(MPU6050_REG_SMPLRT_DIV,0); // Set Sample Rate To 8kHz
		WriteByte(MPU6050_REG_CONFIG,0);     // FSYNC = OFF , DLPF = OFF
		WriteByte(MPU6050_REG_GYRO_CONFIG,(MPU6050_GYRO_FULL_SCALE_RANGE_2000<<3)); // = 000 11 XXX  Set Gyro Full Scale Range to 2000 , Self Test Disabled  
		WriteByte(MPU6050_REG_ACCEL_CONFIG,(MPU6050_ACCEL_FULL_SCALE_RANGE_16G<<3)); // = 000 11 XXX  Set Accelerometer Full Scale Range to 16g , Self Test Disabled
		
	}
	
	unsigned char GetByte(unsigned char reg){
		
		I2C::Start();
		I2C::WriteDeviceAddress(MPU6050_DEVICE_ADRESS,TW_WRITE); // Device address
		I2C::WriteByte(reg); // Reg address
		
		I2C::ReStart();
		
		I2C::WriteDeviceAddress(MPU6050_DEVICE_ADRESS,TW_READ); // Device address
		unsigned char temp = I2C::ReadLastByte();
		
		I2C::Stop();
		
		return temp;
	}
	
	void WriteByte(unsigned char reg,unsigned char value){
		
		I2C::Start();
		I2C::WriteDeviceAddress(MPU6050_DEVICE_ADRESS,TW_WRITE); // Device address
		I2C::WriteByte(reg); // Register address
		I2C::WriteByte(value);
		I2C::Stop();
	}
	
	void BurstRead(unsigned char startRegAddress,unsigned char count, unsigned char * buffer){
		if (count<=0)
		{
			return;
		}
		I2C::Start();
		I2C::WriteDeviceAddress(MPU6050_DEVICE_ADRESS,TW_WRITE); // Device address
		I2C::WriteByte(startRegAddress); // REg address
		I2C::ReStart();
		I2C::WriteDeviceAddress(MPU6050_DEVICE_ADRESS,TW_READ); // Device address
		
		for (unsigned char i=0; i<count-1; i++)
		{
			buffer[i] = I2C::ReadByte();
		}
		buffer[count-1] = I2C::ReadLastByte();
		I2C::Stop();
	}
}