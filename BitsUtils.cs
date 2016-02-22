using System;
/*
** BitsUtils.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  
***/

class BitsUtils
{
	internal static Bitstream getbit(Bitstream bs)
	{		
		if (bs.bc > 0)
		{
			bs.bc--;
		}
		else
		{
			bs.ptr++;
			bs.buf_index++;
			bs.bc = 7;
			
			if (bs.ptr == bs.end)
			{
				// wrap call here
				bs = bs_read(bs);
			}
			bs.sr = (bs.buf[bs.buf_index] & 0xff);
		}
		
		bs.bitval =  (int)(bs.sr & 1); 
        bs.sr = bs.sr >> 1; 
		return bs;
	}
	
	internal static long getbits(int nbits, Bitstream bs)
	{
		int uns_buf;
		long retval;
		
		while ((nbits) > bs.bc)
		{
			bs.ptr++;
			bs.buf_index++;
			
			if (bs.ptr == bs.end)
			{
				bs = bs_read(bs);
			}
			uns_buf = (int) (bs.buf[bs.buf_index] & 0xff);
			bs.sr = bs.sr | (uns_buf << bs.bc); // values in buffer must be unsigned
			
			bs.sr = bs.sr & 0xFFFFFFFFL;        // sr is an unsigned 32 bit variable
			
			bs.bc += 8;
		}
		
		retval = bs.sr;
		
		if (bs.bc > 32)
		{
			bs.bc -= (nbits);
			bs.sr = (bs.buf[bs.buf_index] & 0xff) >> (8 - bs.bc);
		}
		else
		{
			bs.bc -= (nbits);
			bs.sr >>= (nbits);
		}
		
		return (retval);
	}
	
	internal static Bitstream bs_open_read(byte[] stream, short buffer_start, short buffer_end, System.IO.BinaryReader file, int file_bytes, int passed)
	{
		//   CLEAR (*bs);
		Bitstream bs = new Bitstream();
		
		bs.buf = stream;
		bs.buf_index = buffer_start;
		bs.end = buffer_end;
		bs.sr = 0;
		bs.bc = 0;
		
		if (passed != 0)
		{
			bs.ptr = (short) (bs.end - 1);
			bs.file_bytes = file_bytes;
			bs.file = file;
		}
		else
		{
			/* Strange to set an index to -1, but the very first call to getbit will iterate this */
			bs.buf_index = - 1;
			bs.ptr = (short) (- 1);
		}
		
		return bs;
	}
	
	internal static Bitstream bs_read(Bitstream bs)
	{
		if (bs.file_bytes > 0)
		{
			int bytes_to_read;
			int bytes_read;
			
			bytes_to_read = Defines.BITSTREAM_BUFFER_SIZE;
			
			if (bytes_to_read > bs.file_bytes)
				bytes_to_read = bs.file_bytes;

			try
			{
				bytes_read   = bs.file.BaseStream.Read(bs.buf, 0, bytes_to_read);

				bs.buf_index = 0;
			}
			catch (System.Exception e)
			{
				System.Console.Error.WriteLine("Big error while reading file: " + e);
				bytes_read = 0;
			}
			
			if (bytes_read > 0)
			{
				bs.end = (short) (bytes_read);
				bs.file_bytes -= bytes_read;
			}
			else
			{
				for (int i = 0; i < Defines.BITSTREAM_BUFFER_SIZE; i++)
				{
					bs.buf[i] = unchecked((byte)-1);
				}
				bs.error = 1;
			}
		}
		else
		{
			bs.error = 1;
			
			for (int i = 0; i < Defines.BITSTREAM_BUFFER_SIZE; i++)
			{
				bs.buf[i] = unchecked((byte)-1);
			}
		}
		
		
		bs.ptr = 0;
		bs.buf_index = 0;
		
		return bs;
	}
}
