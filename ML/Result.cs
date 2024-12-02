using System.Reflection.Metadata;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; }
        public String? ErrorMessage { get; set; }
        public Exception? Ex { get; set; }
        public object? Object { get; set; }
        public List<object>? results { get; set; }
        public List<object>? Objects { get; set; }
    }
}
