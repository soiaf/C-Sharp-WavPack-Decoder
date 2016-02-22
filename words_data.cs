using System;
/*
** words_data.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class words_data
{
	public words_data()
	{
		InitBlock();
	}
	private void  InitBlock()
	{
		c = new entropy_data[]{temp_ed1, temp_ed2};
	}
	internal long[] bitrate_delta = new long[2]; // was uint32_t  in C
	internal long[] bitrate_acc = new long[2]; // was uint32_t  in C
	internal long holding_one, zeros_acc; // was uint32_t  in C
	internal int holding_zero;
	
	internal entropy_data temp_ed1 = new entropy_data();
	internal entropy_data temp_ed2 = new entropy_data();
	internal entropy_data[] c;
}
