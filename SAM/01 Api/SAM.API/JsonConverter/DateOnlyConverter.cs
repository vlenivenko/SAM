using System.Text.Json;
using System.Text.Json.Serialization;

namespace SAM.API.JsonConverter
{
    /// <summary>
    /// Converter for DateOnly type
    /// </summary>
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private readonly string serializationFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOnlyConverter"/> class.
        /// </summary>
        public DateOnlyConverter() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOnlyConverter"/> class.
        /// </summary>
        /// <param name="serializationFormat"></param>
        public DateOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        }

        /// <inheritdoc/>
        public override DateOnly Read(ref Utf8JsonReader reader,
                                Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DateOnly value,
                                            JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }
}