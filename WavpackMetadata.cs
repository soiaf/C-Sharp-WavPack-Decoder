using System;
/*
** WavpackMetadata.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class WavpackMetadata
{
	internal int byte_length;
	internal byte[] data;
	internal short id; // was uchar in C
	internal int hasdata = 0; // 0 does not have data, 1 has data
	internal int status = 0; // 0 ok, 1 error
	internal long bytecount = 24; 	// we use this to determine if we have read all the metadata 
					// in a block by checking bytecount again the block length
					// ckSize is block size minus 8. WavPack header is 32 bytes long so we start at 24
}
