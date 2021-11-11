using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace fs_auth.DTOs
{
    public class BaseDto
    {
        public bool Status { get; set; } = false;
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }
        public object Message { get; set; } = "";
    }
}