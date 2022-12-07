using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestClientLib
{
    public static partial class JsonSerializerExtensions
    {
        public static T? DeserializeAnonymousType<T>(this string json, T t)
        => JsonSerializer.Deserialize<T>(json);

        public static ValueTask<TValue?> DeserializeAnonymousTypeAsync<TValue>(this Stream stream, JsonSerializerOptions? options = default, CancellationToken cancellationToken = default)
            => JsonSerializer.DeserializeAsync<TValue>(stream, options, cancellationToken);
    }
}
