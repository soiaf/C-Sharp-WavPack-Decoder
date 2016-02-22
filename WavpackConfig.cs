using System;
/*
** WavpackConfig.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class WavpackConfig
{
	internal int bits_per_sample, bytes_per_sample;
	internal int num_channels, float_norm_exp;
	internal long flags, sample_rate, channel_mask; // was uint32_t in C
}
