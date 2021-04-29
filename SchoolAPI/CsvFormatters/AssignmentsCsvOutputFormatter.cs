using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace SchoolAPI.CsvFormatters
{
    public class AssignmentsCsvOutputFormatter : TextOutputFormatter
    {
        public AssignmentsCsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(AssignmentDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<AssignmentDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext
            context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<AssignmentDto>)
            {
                foreach (var item in (IEnumerable<AssignmentDto>)context.Object)
                {
                    FormatCsv(buffer, item);
                }
            }
            else
            {
                FormatCsv(buffer, (AssignmentDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, AssignmentDto item)
        {
            buffer.AppendLine($"{item.Id},\"{item.Name},\" Score: {item.Score}\"");
        }
    }
}
