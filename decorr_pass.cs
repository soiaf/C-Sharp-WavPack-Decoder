using System;
/*
** decorr_pass.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class decorr_pass
{
	public decorr_pass()
	{
		InitBlock();
	}
	private void  InitBlock()
	{
		samples_A = new int[Defines.MAX_TERM];
		samples_B = new int[Defines.MAX_TERM];
	}
	internal short term, delta, weight_A, weight_B;
	internal int[] samples_A;
	internal int[] samples_B;
}
