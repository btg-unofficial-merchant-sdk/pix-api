using PixApi.Models.Responses.Bacen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Helpers.Utility
{
    public static class Extensions
    {
        public static Errors ToError(this string responseError)
        {
            var serialization = new Serialization.JsonEntitySerializer();
            var error = new Errors();

            if (string.IsNullOrWhiteSpace(responseError))
            {
                error.type = "empty response";
                error.status = 500;
                error.detail = "There is a empty response on this request.";
                error.raw = responseError;

                return error;
            }

            try
            {
                error = serialization.Deserialize<Errors>(responseError);
                error.raw = responseError;
            }
            catch (Exception ex)
            {
                error.type = "not mapped error";
                error.status = 500;
                error.detail = ex.Message;
                error.raw = responseError;
            }

            return error;
        }

        public static string GetUriWithQueryString(string requestUri, Dictionary<string, string> queryStringParams)
        {
            bool startingQuestionMarkAdded = false;
            var sb = new StringBuilder();
            sb.Append(requestUri);
            foreach (var parameter in queryStringParams)
            {
                if (parameter.Value == null)
                {
                    continue;
                }

                sb.Append(startingQuestionMarkAdded ? '&' : '?');
                sb.Append(parameter.Key);
                sb.Append('=');
                sb.Append(parameter.Value);
                startingQuestionMarkAdded = true;
            }
            return sb.ToString();
        }

        internal static bool HasAttribute<T>(this MemberInfo property) where T : Attribute
        {
            return property.GetCustomAttributes<T>().Any();
        }

        internal static T GetAttribute<T>(this MemberInfo property) where T : Attribute
        {
            return property.GetCustomAttributes<T>().FirstOrDefault();
        }

        public static string GetStringValue(this Enum enumObj)
        {
            var type = enumObj.GetType();
            var memberInfo = type.GetMember(enumObj.ToString()).First();

            if (memberInfo.HasAttribute<EnumMemberAttribute>())
                return memberInfo.GetAttribute<EnumMemberAttribute>().Value;
            else
                return enumObj.ToString();
        }

        internal static async Task<byte[]> ToBytes(this Stream stream)
        {
            var memory = new MemoryStream();
            await stream.CopyToAsync(memory).ConfigureAwait(false);
            memory.Position = 0;

            var bytes = new byte[(int)memory.Length];
            await memory.ReadAsync(bytes, 0, bytes.Length).ConfigureAwait(false);

            return bytes;
        }

        internal static async Task<string> ToText(this Stream stream)
        {
            var reader = new StreamReader(stream, Encoding.UTF8);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        internal static async Task<FileInfo> ToFile(this Stream stream, string path)
        {
            var file = File.Create(path);
            await stream.CopyToAsync(file).ConfigureAwait(false);

            return new FileInfo(path);
        }

        internal static async Task WriteText(this Stream stream, string value)
        {
            var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteAsync(value).ConfigureAwait(false);
        }

        internal static async Task WriteBytes(this Stream stream, byte[] data)
        {
            await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
        }

        internal static async Task<byte[]> ToBytes(this FileInfo fileInfo)
        {
            var path = fileInfo.FullName;

            var file = File.OpenRead(path);
            return await file.ToBytes().ConfigureAwait(false);
        }

        internal static async Task<FileInfo> ToFile(this byte[] data, string path)
        {
            var file = File.Create(path);
            await file.WriteAsync(data, 0, data.Length).ConfigureAwait(false);

            return new FileInfo(path);
        }
    }
}
