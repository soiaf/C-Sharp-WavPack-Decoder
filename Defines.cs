using System;
/*
** Defines.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

public class Defines
{
	// Change the following value to an even number to reflect the maximum number of samples to be processed
	// per call to WavPackUtils.WavpackUnpackSamples
	
	internal static int SAMPLE_BUFFER_SIZE = 256;
	
	internal static int BITSTREAM_BUFFER_SIZE = 1024;
	
	internal static int FALSE = 0;
	internal static int TRUE = 1;
	
	// or-values for "flags"
	
	
	internal static int BYTES_STORED = 3; // 1-4 bytes/sample
	internal static int MONO_FLAG = 4; // not stereo
	internal static int HYBRID_FLAG = 8; // hybrid mode
	internal static int FALSE_STEREO = 0x40000000; // block is stereo, but data is mono
	
	internal static int SHIFT_LSB = 13;
	internal static long SHIFT_MASK = (0x1fL << SHIFT_LSB);
	
	internal static int FLOAT_DATA = 0x80; // ieee 32-bit floating point data
	
	internal static int SRATE_LSB = 23;
	internal static long SRATE_MASK = (0xfL << SRATE_LSB);
	
	internal static int FINAL_BLOCK = 0x1000; // final block of multichannel segment
	
	
	internal static int MIN_STREAM_VERS = 0x402; // lowest stream version we'll decode
	internal static int MAX_STREAM_VERS = 0x410; // highest stream version we'll decode
	
	
	internal const short ID_DUMMY = (short) (0x0);
	internal static short ID_ENCODER_INFO = (short) (0x1);
	internal const short ID_DECORR_TERMS = (short) (0x2);
	internal const short ID_DECORR_WEIGHTS = (short) (0x3);
	internal const short ID_DECORR_SAMPLES = (short) (0x4);
	internal const short ID_ENTROPY_VARS = (short) (0x5);
	internal const short ID_HYBRID_PROFILE = (short) (0x6);
	internal const short ID_SHAPING_WEIGHTS = (short) (0x7);
	internal const short ID_FLOAT_INFO = (short) (0x8);
	internal const short ID_INT32_INFO = (short) (0x9);
	internal const short ID_WV_BITSTREAM = (short) (0xa);
	internal const short ID_WVC_BITSTREAM = (short) (0xb);
	internal const short ID_WVX_BITSTREAM = (short) (0xc);
	internal const short ID_CHANNEL_INFO = (short) (0xd);
	
	internal static int JOINT_STEREO = 0x10; // joint stereo
	internal static int CROSS_DECORR = 0x20; // no-delay cross decorrelation
	internal static int HYBRID_SHAPE = 0x40; // noise shape (hybrid mode only)
	
	internal static int INT32_DATA = 0x100; // special extended int handling
	internal static int HYBRID_BITRATE = 0x200; // bitrate noise (hybrid mode only)
	internal static int HYBRID_BALANCE = 0x400; // balance noise (hybrid stereo mode only)
	
	internal static int INITIAL_BLOCK = 0x800; // initial block of multichannel segment
	
	internal static int FLOAT_SHIFT_ONES = 1; // bits left-shifted into float = '1'
	internal static int FLOAT_SHIFT_SAME = 2; // bits left-shifted into float are the same
	internal static int FLOAT_SHIFT_SENT = 4; // bits shifted into float are sent literally
	internal static int FLOAT_ZEROS_SENT = 8; // "zeros" are not all real zeros
	internal static int FLOAT_NEG_ZEROS = 0x10; // contains negative zeros
	internal static int FLOAT_EXCEPTIONS = 0x20; // contains exceptions (inf, nan, etc.)
	
	
	internal static short ID_OPTIONAL_DATA = (short) (0x20);
	internal static int ID_ODD_SIZE = 0x40;
	internal static int ID_LARGE = 0x80;
	
	internal static int MAX_NTERMS = 16;
	internal static int MAX_TERM = 8;
	
	internal static int MAG_LSB = 18;
	internal static long MAG_MASK = (0x1fL << MAG_LSB);
	
	internal const short ID_RIFF_HEADER = (short) (0x21);
	internal const short ID_RIFF_TRAILER = (short) (0x22);
	internal const short ID_REPLAY_GAIN = (short) (0x23);
	internal const short ID_CUESHEET = (short) (0x24);
	internal const short ID_CONFIG_BLOCK = (short) (0x25);
	internal const short ID_MD5_CHECKSUM = (short) (0x26);
	internal const short ID_SAMPLE_RATE = (short) (0x27);
	
	internal static long CONFIG_BYTES_STORED = 3; // 1-4 bytes/sample
	internal static long CONFIG_MONO_FLAG = 4; // not stereo
	internal static long CONFIG_HYBRID_FLAG = 8; // hybrid mode
	internal static long CONFIG_JOINT_STEREO = 0x10; // joint stereo
	internal static long CONFIG_CROSS_DECORR = 0x20; // no-delay cross decorrelation
	internal static long CONFIG_HYBRID_SHAPE = 0x40; // noise shape (hybrid mode only)
	internal static long CONFIG_FLOAT_DATA = 0x80; // ieee 32-bit floating point data
	internal static long CONFIG_FAST_FLAG = 0x200; // fast mode
	internal static long CONFIG_HIGH_FLAG = 0x800; // high quality mode
	internal static long CONFIG_VERY_HIGH_FLAG = 0x1000; // very high
	internal static long CONFIG_BITRATE_KBPS = 0x2000; // bitrate is kbps, not bits / sample
	internal static long CONFIG_AUTO_SHAPING = 0x4000; // automatic noise shaping
	internal static long CONFIG_SHAPE_OVERRIDE = 0x8000; // shaping mode specified
	internal static long CONFIG_JOINT_OVERRIDE = 0x10000; // joint-stereo mode specified
	internal static long CONFIG_CREATE_EXE = 0x40000; // create executable
	internal static long CONFIG_CREATE_WVC = 0x80000; // create correction file
	internal static long CONFIG_OPTIMIZE_WVC = 0x100000; // maximize bybrid compression
	internal static long CONFIG_CALC_NOISE = 0x800000; // calc noise in hybrid mode
	internal static long CONFIG_LOSSY_MODE = 0x1000000; // obsolete (for information)
	internal static long CONFIG_EXTRA_MODE = 0x2000000; // extra processing mode
	internal static long CONFIG_SKIP_WVX = 0x4000000; // no wvx stream w/ floats & big ints
	internal static long CONFIG_MD5_CHECKSUM = 0x8000000; // compute & store MD5 signature
	internal static long CONFIG_OPTIMIZE_MONO = 0x80000000; // optimize for mono streams posing as stereo
	
	internal static int MODE_WVC = 0x1;
	internal static int MODE_LOSSLESS = 0x2;
	internal static int MODE_HYBRID = 0x4;
	internal static int MODE_FLOAT = 0x8;
	internal static int MODE_VALID_TAG = 0x10;
	internal static int MODE_HIGH = 0x20;
	internal static int MODE_FAST = 0x40;
}
