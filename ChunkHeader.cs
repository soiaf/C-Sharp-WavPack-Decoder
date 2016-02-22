using System;
/*
** ChunkHeader.cs
**
** Copyright (c) 2010-2016 Peter McQuillan
**
** All Rights Reserved.
**                       
** Distributed under the BSD Software License (see license.txt)  */

class ChunkHeader
{
	internal char[] ckID = new char[4];
	internal long ckSize; // was uint32_t in C
}
