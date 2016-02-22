using System;
/*
** WavpackHeader.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class WavpackHeader
{
	
	internal char[] ckID = new char[4];
	internal long ckSize; // was uint32_t in C
	internal short version;
	internal short track_no, index_no; // was uchar in C
	internal long total_samples, block_index, block_samples, flags, crc; // was uint32_t in C
	internal int status = 0; // 1 means error
}
