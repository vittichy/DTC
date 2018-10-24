using System.IO;
using System.Text;

namespace Dtc.Common.Text
{
    /// <summary>
    /// StringWriter with UTF8 encoding
    /// </summary>
    public sealed class StringWriterUtf8 : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        public StringWriterUtf8(StringBuilder sb) : base(sb)
        {
        }
    }
}
