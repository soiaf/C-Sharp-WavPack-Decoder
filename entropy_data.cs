using System;
/*
** entropy_data.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class entropy_data
{
	internal long slow_level;
	internal int[] median = new int[]{0, 0, 0}; // was uint32_t in C, we initialize in order to remove run time errors
	internal long error_limit; // was uint32_t in C
}
