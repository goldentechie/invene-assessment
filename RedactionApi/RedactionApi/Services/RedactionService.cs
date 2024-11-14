using RedactionApi.Models;
using System.Text.RegularExpressions;

namespace RedactionApi.Services
{
    public interface IRedactionService
    {
        string RedactContent(FileContent file);
    }
    public class RedactionService : IRedactionService
    {
        public string RedactContent(FileContent file)
        {
            var content = file.Content;

            // Patterns to match and replace the entire line or specific formats
            content = Regex.Replace(content, @"Patient Name:.*", "Patient Name: [REDACTED]");
            content = Regex.Replace(content, @"Date of Birth:.*", "Date of Birth: [REDACTED]");
            content = Regex.Replace(content, @"Social Security Number:.*", "Social Security Number: [REDACTED]");
            content = Regex.Replace(content, @"Address:.*", "Address: [REDACTED]");
            content = Regex.Replace(content, @"Phone Number:.*", "Phone Number: [REDACTED]");
            content = Regex.Replace(content, @"Email:.*", "Email: [REDACTED]");
            content = Regex.Replace(content, @"Medical Record Number:.*", "Medical Record Number: [REDACTED]");

            return content;
        }
    }
}
