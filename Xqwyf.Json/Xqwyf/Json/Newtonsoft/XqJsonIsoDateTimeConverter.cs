using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xqwyf.DependencyInjection;
using Xqwyf.Timing;

namespace  Xqwyf.Json.Newtonsoft
{
    public class XqJsonIsoDateTimeConverter : IsoDateTimeConverter, ITransientDependency
    {
        private readonly IClock _clock;

        public XqJsonIsoDateTimeConverter(IClock clock, IOptions<XqJsonOptions> abpJsonOptions)
        {
            _clock = clock;

            if (abpJsonOptions.Value.DefaultDateTimeFormat != null)
            {
                DateTimeFormat = abpJsonOptions.Value.DefaultDateTimeFormat;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var date = base.ReadJson(reader, objectType, existingValue, serializer) as DateTime?;

            if (date.HasValue)
            {
                return _clock.Normalize(date.Value);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            base.WriteJson(writer, date.HasValue ? _clock.Normalize(date.Value) : value, serializer);
        }
    }
}
