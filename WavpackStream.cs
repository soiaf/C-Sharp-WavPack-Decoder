using System;
/*
** WavpackStream.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class WavpackStream
{
	public WavpackStream()
	{
		InitBlock();
	}
	private void  InitBlock()
	{
		decorr_passes = new decorr_pass[]{dp1, dp2, dp3, dp4, dp5, dp6, dp7, dp8, dp9, dp10, dp11, dp12, dp13, dp14, dp15, dp16};
	}
	internal WavpackHeader wphdr = new WavpackHeader();
	internal Bitstream wvbits = new Bitstream();
	
	internal words_data w = new words_data();
	
	internal int num_terms = 0;
	internal int mute_error;
	internal long sample_index, crc; // was uint32_t in C
	
	internal short int32_sent_bits, int32_zeros, int32_ones, int32_dups; // was uchar in C
	internal short float_flags, float_shift, float_max_exp, float_norm_exp; // was uchar in C
	
	internal decorr_pass dp1 = new decorr_pass();
	internal decorr_pass dp2 = new decorr_pass();
	internal decorr_pass dp3 = new decorr_pass();
	internal decorr_pass dp4 = new decorr_pass();
	internal decorr_pass dp5 = new decorr_pass();
	internal decorr_pass dp6 = new decorr_pass();
	internal decorr_pass dp7 = new decorr_pass();
	internal decorr_pass dp8 = new decorr_pass();
	internal decorr_pass dp9 = new decorr_pass();
	internal decorr_pass dp10 = new decorr_pass();
	internal decorr_pass dp11 = new decorr_pass();
	internal decorr_pass dp12 = new decorr_pass();
	internal decorr_pass dp13 = new decorr_pass();
	internal decorr_pass dp14 = new decorr_pass();
	internal decorr_pass dp15 = new decorr_pass();
	internal decorr_pass dp16 = new decorr_pass();
	
	internal decorr_pass[] decorr_passes;
}
