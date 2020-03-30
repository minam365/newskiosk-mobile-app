using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inamsoft.Newskiosk.Abstractions
{
    public static class ExtensionMethods
    {
        static readonly Encoding Utf8EncodingWithNoBom = new UTF8Encoding(false);

        public static ReadOnlySpan<byte> ToReadOnlySpan(this string value, Encoding encoding = null)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} is null or empty.", nameof(value));
            }

            if(encoding == null)
                encoding = Utf8EncodingWithNoBom;

            ReadOnlySpan<byte> result = encoding.GetBytes(value);

            return result;
        }

        public static Stream ToStream(this string value, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} is null or empty.", nameof(value));
            }

            if (encoding == null)
                encoding = Utf8EncodingWithNoBom;

            var bytes = encoding.GetBytes(value);
            var ms = new MemoryStream(bytes);
            ms.SeekToBegin();

            return ms;
        }


        /// <summary>
        /// Converts the string to a stream using the specified encoding.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <returns>A newly created <see cref="Stream"/> object containing the input string.</returns>
        public static async Task<Stream> ToStreamAsync(this string value, Encoding encoding)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} is null or empty.", nameof(value));
            }

            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, encoding, 8 * 1024, leaveOpen: true))
            {
                await writer.WriteAsync(value).ConfigureAwait(continueOnCapturedContext: false);
            }

            stream.SeekToBegin();

            return stream;
        }

        /// <summary>
        /// Sets the stream cursor to the beginning of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The stream</returns>
        public static Stream SeekToBegin(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), $"{nameof(stream)} is null.");
            }

            if (!stream.CanSeek) throw new InvalidOperationException("Stream does not support seeking.");

            if (stream.Position > 0)
                stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
