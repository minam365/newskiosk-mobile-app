using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inamsoft.Newskiosk.Abstractions
{

    public class ObjectSerializer
    {

        static readonly Encoding Utf8EncodingWithNoBom = new UTF8Encoding(false);


        #region --- Static Instance ---
        /// <summary>
        /// Defines a static default instance.
        /// </summary>
        private static readonly Lazy<ObjectSerializer> _defaultInstance = new Lazy<ObjectSerializer>(() => new ObjectSerializer());

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectSerializer"/> class.
        /// </summary>
        /// <remarks>
        /// This is to prevent direct instantiation of the class.
        /// </remarks>
        private ObjectSerializer()
        { }

        /// <summary>
        /// Gets the default instance of the <see cref="ObjectSerializer"/> class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static ObjectSerializer Default
        {
            get { return _defaultInstance.Value; }
        }
        #endregion

        public async Task<string> SerializeAsync<T>(T value)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<T>(ms, value).ConfigureAwait(false);
            var bytes = ms.ToArray();
            var json = Utf8EncodingWithNoBom.GetString(bytes);

            return json;
        }

        public async Task<TResult> DeserializeAsync<TResult>(string json)
        {
            var encoding = System.Text.Encoding.UTF8;

            using var jsonStream = await json.ToStreamAsync(encoding).ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<TResult>(jsonStream).ConfigureAwait(false);

            return result;
        }

        public async Task<TResult> DeserializeAsync<TResult>(Stream json)
        {
            var result = await JsonSerializer.DeserializeAsync<TResult>(json).ConfigureAwait(false);

            return result;
        }

        #region Utility Methods



        #endregion
    }

}