using System;
/*
** WvDemo.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

public class WvDemo
{
	public WvDemo()
	{
		InitBlock();
	}
	private void  InitBlock()
	{
		temp_buffer = new int[Defines.SAMPLE_BUFFER_SIZE];
		pcm_buffer = new byte[4 * Defines.SAMPLE_BUFFER_SIZE];
	}
	
	internal static int[] temp_buffer;
	
	internal static byte[] pcm_buffer;
	
	[STAThread]
	public static void  Main(System.String[] args)
	{
		ChunkHeader FormatChunkHeader = new ChunkHeader();
		ChunkHeader DataChunkHeader = new ChunkHeader();
		RiffChunkHeader myRiffChunkHeader = new RiffChunkHeader();
		WaveHeader WaveHeader = new WaveHeader();
		sbyte[] myRiffChunkHeaderAsByteArray = new sbyte[12];
		sbyte[] myFormatChunkHeaderAsByteArray = new sbyte[8];
		sbyte[] myWaveHeaderAsByteArray = new sbyte[16];
		sbyte[] myDataChunkHeaderAsByteArray = new sbyte[8];
		
		long total_unpacked_samples = 0, total_samples; // was uint32_t in C
		int num_channels, bps;
		WavpackContext wpc = new WavpackContext();
		System.IO.FileStream fistream;
		System.IO.FileStream fostream;
		System.IO.BinaryReader in_Renamed;
		long start, end;
		
		System.String inputWVFile;

		if (args.Length == 0)
		{
			inputWVFile = "input.wv";
		}
		else
		{
			inputWVFile = args[0];
		}
		
		try
		{
            fistream = new System.IO.FileStream(inputWVFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			System.IO.BufferedStream bstream = new System.IO.BufferedStream(fistream,16384); 
			in_Renamed = new System.IO.BinaryReader(bstream);
			wpc = WavPackUtils.WavpackOpenFileInput(in_Renamed);
		}
		catch (System.IO.FileNotFoundException)
		{
			System.Console.Error.WriteLine("Input file not found");
			System.Environment.Exit(1);
		}
		catch (System.IO.DirectoryNotFoundException)
		{
			System.Console.Error.WriteLine("Input file not found - invalid directory");
			System.Environment.Exit(1);
		}
		
		if (wpc.error)
		{
			System.Console.Error.WriteLine("Sorry an error has occured");
			System.Console.Error.WriteLine(wpc.error_message);
			System.Environment.Exit(1);
		}
		
		num_channels = WavPackUtils.WavpackGetReducedChannels(wpc);
		
		System.Console.Out.WriteLine("The WavPack file has " + num_channels + " channels");
		
		total_samples = WavPackUtils.WavpackGetNumSamples(wpc);
		
		System.Console.Out.WriteLine("The WavPack file has " + total_samples + " samples");
		
		bps = WavPackUtils.WavpackGetBytesPerSample(wpc);
		
		System.Console.Out.WriteLine("The WavPack file has " + bps + " bytes per sample");
		
		myRiffChunkHeader.ckID[0] = 'R';
		myRiffChunkHeader.ckID[1] = 'I';
		myRiffChunkHeader.ckID[2] = 'F';
		myRiffChunkHeader.ckID[3] = 'F';
		
		myRiffChunkHeader.ckSize = total_samples * num_channels * bps + 8 * 2 + 16 + 4;
		myRiffChunkHeader.formType[0] = 'W';
		myRiffChunkHeader.formType[1] = 'A';
		myRiffChunkHeader.formType[2] = 'V';
		myRiffChunkHeader.formType[3] = 'E';
		
		FormatChunkHeader.ckID[0] = 'f';
		FormatChunkHeader.ckID[1] = 'm';
		FormatChunkHeader.ckID[2] = 't';
		FormatChunkHeader.ckID[3] = ' ';
		
		FormatChunkHeader.ckSize = 16;
		
		WaveHeader.FormatTag = 1;
		WaveHeader.NumChannels = num_channels;
		WaveHeader.SampleRate = WavPackUtils.WavpackGetSampleRate(wpc);
		WaveHeader.BlockAlign = num_channels * bps;
		WaveHeader.BytesPerSecond = WaveHeader.SampleRate * WaveHeader.BlockAlign;
		WaveHeader.BitsPerSample = WavPackUtils.WavpackGetBitsPerSample(wpc);
		
		DataChunkHeader.ckID[0] = 'd';
		DataChunkHeader.ckID[1] = 'a';
		DataChunkHeader.ckID[2] = 't';
		DataChunkHeader.ckID[3] = 'a';
		DataChunkHeader.ckSize = total_samples * num_channels * bps;
		
		myRiffChunkHeaderAsByteArray[0] = (sbyte) myRiffChunkHeader.ckID[0];
		myRiffChunkHeaderAsByteArray[1] = (sbyte) myRiffChunkHeader.ckID[1];
		myRiffChunkHeaderAsByteArray[2] = (sbyte) myRiffChunkHeader.ckID[2];
		myRiffChunkHeaderAsByteArray[3] = (sbyte) myRiffChunkHeader.ckID[3];
		
		// swap endians here
		
		myRiffChunkHeaderAsByteArray[7] = (sbyte) (SupportClass.URShift(myRiffChunkHeader.ckSize, 24));
		myRiffChunkHeaderAsByteArray[6] = (sbyte) (SupportClass.URShift(myRiffChunkHeader.ckSize, 16));
		myRiffChunkHeaderAsByteArray[5] = (sbyte) (SupportClass.URShift(myRiffChunkHeader.ckSize, 8));
		myRiffChunkHeaderAsByteArray[4] = (sbyte) (myRiffChunkHeader.ckSize);
		
		myRiffChunkHeaderAsByteArray[8] = (sbyte) myRiffChunkHeader.formType[0];
		myRiffChunkHeaderAsByteArray[9] = (sbyte) myRiffChunkHeader.formType[1];
		myRiffChunkHeaderAsByteArray[10] = (sbyte) myRiffChunkHeader.formType[2];
		myRiffChunkHeaderAsByteArray[11] = (sbyte) myRiffChunkHeader.formType[3];
		
		myFormatChunkHeaderAsByteArray[0] = (sbyte) FormatChunkHeader.ckID[0];
		myFormatChunkHeaderAsByteArray[1] = (sbyte) FormatChunkHeader.ckID[1];
		myFormatChunkHeaderAsByteArray[2] = (sbyte) FormatChunkHeader.ckID[2];
		myFormatChunkHeaderAsByteArray[3] = (sbyte) FormatChunkHeader.ckID[3];
		
		// swap endians here
		myFormatChunkHeaderAsByteArray[7] = (sbyte) (SupportClass.URShift(FormatChunkHeader.ckSize, 24));
		myFormatChunkHeaderAsByteArray[6] = (sbyte) (SupportClass.URShift(FormatChunkHeader.ckSize, 16));
		myFormatChunkHeaderAsByteArray[5] = (sbyte) (SupportClass.URShift(FormatChunkHeader.ckSize, 8));
		myFormatChunkHeaderAsByteArray[4] = (sbyte) (FormatChunkHeader.ckSize);
		
		// swap endians
		myWaveHeaderAsByteArray[1] = (sbyte) (SupportClass.URShift(WaveHeader.FormatTag, 8));
		myWaveHeaderAsByteArray[0] = (sbyte) (WaveHeader.FormatTag);
		
		// swap endians
		myWaveHeaderAsByteArray[3] = (sbyte) (SupportClass.URShift(WaveHeader.NumChannels, 8));
		myWaveHeaderAsByteArray[2] = (sbyte) WaveHeader.NumChannels;
		
		
		// swap endians
		myWaveHeaderAsByteArray[7] = (sbyte) (SupportClass.URShift(WaveHeader.SampleRate, 24));
		myWaveHeaderAsByteArray[6] = (sbyte) (SupportClass.URShift(WaveHeader.SampleRate, 16));
		myWaveHeaderAsByteArray[5] = (sbyte) (SupportClass.URShift(WaveHeader.SampleRate, 8));
		myWaveHeaderAsByteArray[4] = (sbyte) (WaveHeader.SampleRate);
		
		// swap endians
		
		myWaveHeaderAsByteArray[11] = (sbyte) (SupportClass.URShift(WaveHeader.BytesPerSecond, 24));
		myWaveHeaderAsByteArray[10] = (sbyte) (SupportClass.URShift(WaveHeader.BytesPerSecond, 16));
		myWaveHeaderAsByteArray[9] = (sbyte) (SupportClass.URShift(WaveHeader.BytesPerSecond, 8));
		myWaveHeaderAsByteArray[8] = (sbyte) (WaveHeader.BytesPerSecond);
		
		// swap endians
		myWaveHeaderAsByteArray[13] = (sbyte) (SupportClass.URShift(WaveHeader.BlockAlign, 8));
		myWaveHeaderAsByteArray[12] = (sbyte) WaveHeader.BlockAlign;
		
		// swap endians
		myWaveHeaderAsByteArray[15] = (sbyte) (SupportClass.URShift(WaveHeader.BitsPerSample, 8));
		myWaveHeaderAsByteArray[14] = (sbyte) WaveHeader.BitsPerSample;
		
		myDataChunkHeaderAsByteArray[0] = (sbyte) DataChunkHeader.ckID[0];
		myDataChunkHeaderAsByteArray[1] = (sbyte) DataChunkHeader.ckID[1];
		myDataChunkHeaderAsByteArray[2] = (sbyte) DataChunkHeader.ckID[2];
		myDataChunkHeaderAsByteArray[3] = (sbyte) DataChunkHeader.ckID[3];
		
		// swap endians
		
		myDataChunkHeaderAsByteArray[7] = (sbyte) (SupportClass.URShift(DataChunkHeader.ckSize, 24));
		myDataChunkHeaderAsByteArray[6] = (sbyte) (SupportClass.URShift(DataChunkHeader.ckSize, 16));
		myDataChunkHeaderAsByteArray[5] = (sbyte) (SupportClass.URShift(DataChunkHeader.ckSize, 8));
		myDataChunkHeaderAsByteArray[4] = (sbyte) DataChunkHeader.ckSize;
		
		try
		{
			fostream = new System.IO.FileStream("output.wav", System.IO.FileMode.Create);
			SupportClass.WriteOutput(fostream, myRiffChunkHeaderAsByteArray);
			SupportClass.WriteOutput(fostream, myFormatChunkHeaderAsByteArray);
			SupportClass.WriteOutput(fostream, myWaveHeaderAsByteArray);
			SupportClass.WriteOutput(fostream, myDataChunkHeaderAsByteArray);
			
			start = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			
			while (true)
			{
				long samples_unpacked; // was uint32_t in C
				
				samples_unpacked = WavPackUtils.WavpackUnpackSamples(wpc, temp_buffer, Defines.SAMPLE_BUFFER_SIZE / num_channels);
				
				total_unpacked_samples += samples_unpacked;
				
				if (samples_unpacked > 0)
				{
					samples_unpacked = samples_unpacked * num_channels;
					
					pcm_buffer = format_samples(bps, temp_buffer, samples_unpacked);
					fostream.Write(pcm_buffer, 0, (int) samples_unpacked * bps);
				}
				
				if (samples_unpacked == 0)
					break;
			} // end of while
			
			end = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			
			System.Console.Out.WriteLine(end - start + " milli seconds to process WavPack file in main loop");
		}
		catch (System.Exception e)
		{
			System.Console.Error.WriteLine("Error when writing wav file, sorry: ");
			SupportClass.WriteStackTrace(e, Console.Error);
			System.Environment.Exit(1);
		}
		
		if ((WavPackUtils.WavpackGetNumSamples(wpc) != - 1) && (total_unpacked_samples != WavPackUtils.WavpackGetNumSamples(wpc)))
		{
			System.Console.Error.WriteLine("Incorrect number of samples");
			System.Environment.Exit(1);
		}
		
		if (WavPackUtils.WavpackGetNumErrors(wpc) > 0)
		{
			System.Console.Error.WriteLine("CRC errors detected");
			System.Environment.Exit(1);
		}
		
		System.Environment.Exit(0);
	}
	
	
	// Reformat samples from longs in processor's native endian mode to
	// little-endian data with (possibly) less than 4 bytes / sample.
	
	internal static byte[] format_samples(int bps, int[] src, long samcnt)
	{
		int temp;
		int counter = 0;
		int counter2 = 0;
		byte[] dst = new byte[4 * Defines.SAMPLE_BUFFER_SIZE];
		
		switch (bps)
		{
			
			case 1: 
				while (samcnt > 0)
				{
					dst[counter] = (byte) (0x00FF & (src[counter] + 128));
					counter++;
					samcnt--;
				}
				break;
			
			
			case 2: 
				while (samcnt > 0)
				{
					temp = src[counter2];
					dst[counter] = (byte) temp;
					counter++;
					//dst[counter] = (byte) (SupportClass.URShift(temp, 8));
					dst[counter] = (byte) (temp >> 8);
					counter++;
					counter2++;
					samcnt--;
				}
				
				break;
			
			
			case 3: 
				while (samcnt > 0)
				{
					temp = src[counter2];
					dst[counter] = (byte) temp;
					counter++;
                    dst[counter] = (byte)(temp >> 8);
					counter++;
                    dst[counter] = (byte)(temp >> 16);
					counter++;
					counter2++;
					samcnt--;
				}
				
				break;
			
			
			case 4: 
				while (samcnt > 0)
				{
					temp = src[counter2];
					dst[counter] = (byte) temp;
					counter++;
					dst[counter] = (byte) (SupportClass.URShift(temp, 8));
					counter++;
					dst[counter] = (byte) (SupportClass.URShift(temp, 16));
					counter++;
					dst[counter] = (byte) (SupportClass.URShift(temp, 24));
					counter++;
					counter2++;
					samcnt--;
				}
				
				break;
			}
		
		return dst;
	}
	static WvDemo()
	{
		temp_buffer = new int[Defines.SAMPLE_BUFFER_SIZE];
		pcm_buffer = new byte[4 * Defines.SAMPLE_BUFFER_SIZE];
	}
}
