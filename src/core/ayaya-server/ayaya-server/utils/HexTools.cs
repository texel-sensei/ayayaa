﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ayaya_server.utils
{
    internal static class HexTools
    {
        public static string ByteArrayToHexString(byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length * 2);
            const string hexAlphabet = "0123456789ABCDEF";

            foreach (var b in bytes)
            {
                result.Append(hexAlphabet[(int)(b >> 4)]);
                result.Append(hexAlphabet[(int)(b & 0xF)]);
            }

            return result.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            var hexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
                0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
            {
                bytes[x] = (byte)(hexValue[char.ToUpper(hex[i + 0]) - '0'] << 4 |
                                  hexValue[char.ToUpper(hex[i + 1]) - '0']);
            }

            return bytes;
        }
    }
}
