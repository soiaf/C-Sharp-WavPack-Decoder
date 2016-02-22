using System;
/*
** Bitstream.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)
***/

class Bitstream
{
	internal short end, ptr; // was uchar in c
	internal long sr;
	internal int file_bytes;
	internal int error, bc;
	internal System.IO.BinaryReader file;
	internal int bitval = 0;
	internal byte[] buf = new byte[Defines.BITSTREAM_BUFFER_SIZE];
	internal int buf_index = 0;
}
