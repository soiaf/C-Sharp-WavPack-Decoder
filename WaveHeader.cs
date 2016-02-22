using System;
/*
** WaveHeader.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)
***/

class WaveHeader
{
	internal int FormatTag, NumChannels; // was ushort in C
	internal long SampleRate, BytesPerSecond; // was uint32_t in C
	internal int BlockAlign, BitsPerSample; // was ushort in C
}
