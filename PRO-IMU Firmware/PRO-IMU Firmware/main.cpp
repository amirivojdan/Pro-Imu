/*
* DynamixelFirmware.cpp
*
* Created: 4/1/2013 3:10:09 AM
*  Author: Ahmad Amiri
*/

#define F_CPU 16000000UL


#include <avr/io.h>

#include "Dynamixel/Dynamixel.h"
#include "MPU-6050/MPU6050.h"
#include "SerialPort/SerialPort.h"
#include "Timers/Timers.h"
#include "SensorFusion/Madgwick.h"
#define DEG2RAD (0.0174532925199432957)
volatile bool sample_flag=false;
void Timer0_ISR_Handler(void);
int main(void)
{
	sei();
	Dynamixel::Initialize();
	MPU6050::Initialize();
	timer0(TIMER0_PRESCALER_256,0xFA,Timer0_ISR_Handler); // 250 Hz - Error[%] = 0
	unsigned char data;
	
	union ByteArrayFloat gx;
	union ByteArrayFloat gy;
	union ByteArrayFloat gz;
	
	union ByteArrayFloat ax;
	union ByteArrayFloat ay;
	union ByteArrayFloat az;
	
	union ByteArrayFloat q0;
	union ByteArrayFloat q1;
	union ByteArrayFloat q2;
	union ByteArrayFloat q3;
	
	union ByteArrayFloat roll;
	union ByteArrayFloat pitch;
	union ByteArrayFloat yaw;
	
	union ByteArrayFloat beta;
	
	while(true)
	{
		if(RingBuffer::GetChar(data)){
			Dynamixel::Interpreter::Dispatch(data);
		}
		
		if (sample_flag)
		{
			
			beta.bytes[0] = Dynamixel::ControlTable[Dynamixel::Beta];
			beta.bytes[1] = Dynamixel::ControlTable[Dynamixel::Beta+1];
			beta.bytes[2] = Dynamixel::ControlTable[Dynamixel::Beta+2];
			beta.bytes[3] = Dynamixel::ControlTable[Dynamixel::Beta+3];
			
			SensorFusion::beta = beta.FloatNumber;
			
			
			MPU6050::BurstRead(MPU6050_REG_ACCEL_XOUT_H,14,Dynamixel::ControlTable+Dynamixel::Raw_Accel_X_H);
			
			gx.FloatNumber =  (((short)(Dynamixel::ControlTable[Dynamixel::Raw_Gyro_X_H]<<8)+Dynamixel::ControlTable[Dynamixel::Raw_Gyro_X_L] ) - (short)Dynamixel::ControlTable[Dynamixel::Gyro_X_Offset] )*MPU6050_GYRO_LSB_2000_Scale;
			gy.FloatNumber = (((short)(Dynamixel::ControlTable[Dynamixel::Raw_Gyro_Y_H]<<8)+Dynamixel::ControlTable[Dynamixel::Raw_Gyro_Y_L]  - (short)Dynamixel::ControlTable[Dynamixel::Gyro_Y_Offset]))*MPU6050_GYRO_LSB_2000_Scale;
			gz.FloatNumber =  (((short)(Dynamixel::ControlTable[Dynamixel::Raw_Gyro_Z_H]<<8)+Dynamixel::ControlTable[Dynamixel::Raw_Gyro_Z_L] - (short)Dynamixel::ControlTable[Dynamixel::Gyro_Z_Offset]))*MPU6050_GYRO_LSB_2000_Scale;
			
			ax.FloatNumber =  (((short)(Dynamixel::ControlTable[Dynamixel::Raw_Accel_X_H]<<8)+Dynamixel::ControlTable[Dynamixel::Raw_Accel_X_L]/* - Accel_X_Offset */));
			ay.FloatNumber = (((short)(Dynamixel::ControlTable[Dynamixel::Raw_Accel_Y_H]<<8)+Dynamixel::ControlTable[Dynamixel::Raw_Accel_Y_L]/* - Accel_Y_Offset */));
			az.FloatNumber = (((short)(Dynamixel::ControlTable[Dynamixel::Raw_Accel_Z_H]<<8)+Dynamixel::ControlTable[Dynamixel::Raw_Accel_Z_L]/* - Accel_Z_Offset */));
			
			SensorFusion::MadgwickAHRSupdateIMU(gx.FloatNumber*DEG2RAD,gy.FloatNumber*DEG2RAD,gz.FloatNumber*DEG2RAD,ax.FloatNumber,ay.FloatNumber,az.FloatNumber);
			SensorFusion::getRollPitchYaw(&roll.FloatNumber,&pitch.FloatNumber,&yaw.FloatNumber);
			
			q0.FloatNumber = SensorFusion::q0;
			Dynamixel::ControlTable[Dynamixel::Filtered_Q0]  = q0.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q0+1]= q0.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q0+2]= q0.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q0+3]= q0.bytes[3];
			
			q1.FloatNumber = SensorFusion::q1;
			Dynamixel::ControlTable[Dynamixel::Filtered_Q1]  = q1.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q1+1]= q1.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q1+2]= q1.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q1+3]= q1.bytes[3];
			
			q2.FloatNumber = SensorFusion::q2;
			Dynamixel::ControlTable[Dynamixel::Filtered_Q2]  = q2.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q2+1]= q2.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q2+2]= q2.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q2+3]= q2.bytes[3];
			
			q3.FloatNumber = SensorFusion::q3;
			Dynamixel::ControlTable[Dynamixel::Filtered_Q3]  = q3.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q3+1]= q3.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q3+2]= q3.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Q3+3]= q3.bytes[3];
			
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Roll]= roll.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Roll+1]= roll.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Roll+2]= roll.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Roll+3]= roll.bytes[3];
			
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Pitch]= pitch.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Pitch+1]= pitch.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Pitch+2]= pitch.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Pitch+3]= pitch.bytes[3];
			
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Yaw]= yaw.bytes[0];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Yaw+1]= yaw.bytes[1];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Yaw+2]= yaw.bytes[2];
			Dynamixel::ControlTable[Dynamixel::Filtered_Angle_Yaw+3]= yaw.bytes[3];
			
			sample_flag=false;
		}
	}
}

void Timer0_ISR_Handler(void){
	sample_flag=true;
}