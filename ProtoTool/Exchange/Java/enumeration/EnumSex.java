
			
// Generated by the SocketProtoGenerationTool.  DO NOT EDIT!
package com.mokylin.td.clientmsg.enumeration;

//
public enum EnumSex
{

			
		// 无性别
		NONE(0),

		
			
		// 男
		MALE(1),

		
			
		// 女
		FEMALE(2)

;

	private final int value;
	
	EnumSex(int value){
		this.value= value;
	}
	
	public int getValue() {
		return value;
	}

	public static EnumSex valueOf(int _value) {
		for (EnumSex each : EnumSex.values()) {
			if (each.value == _value) {
				return each;
			}
		}
		return null;
	}
}

		