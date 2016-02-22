using System;
/*
** WavpackContext.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

public class WavpackContext
{
	internal WavpackConfig config = new WavpackConfig();
	internal WavpackStream stream = new WavpackStream();
	
	
	internal byte[] read_buffer = new byte[1024]; // was uchar in C
	internal System.String error_message = "";
	internal bool error;
	
	internal System.IO.BinaryReader infile;
	internal long total_samples, crc_errors; // was uint32_t in C
	internal int open_flags, norm_offset;
	internal int reduced_channels = 0;
	internal int lossy_blocks;
	internal int status = 0; // 0 ok, 1 error
}
