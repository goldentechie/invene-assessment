using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedactionApi.Models;
using RedactionApi.Services;

namespace RedactionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedactionController : ControllerBase
    {
        private readonly IRedactionService _redactionService;

        public RedactionController(IRedactionService redactionService)
        {
            _redactionService = redactionService;
        }

        [HttpPost("redact")]
        public async Task<IActionResult> RedactFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            var responses = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    using var reader = new StreamReader(file.OpenReadStream());
                    var content = await reader.ReadToEndAsync();
                    var redactedContent = _redactionService.RedactContent(new FileContent { FileName = file.FileName, Content = content });
                    var filePath = Path.Combine("Output", file.FileName + "_sanitized.txt");
                    System.IO.File.WriteAllText(filePath, redactedContent); // Ensure to write the Content property

                    responses.Add($"File {file.FileName} processed and saved to {filePath}");
                }
                catch (Exception ex)
                {
                    // Log the error (consider using a logging framework like NLog, Serilog, or .NET Core Logging)
                    var error = $"Failed to process file {file.FileName}: {ex.Message}";
                    responses.Add(error);
                    // Optionally, log the stack trace or other details if needed
                }
            }

            // If all files fail, consider returning a BadRequest instead of Ok
            bool allFailed = responses.All(r => r.StartsWith("Failed to process file"));
            if (allFailed)
                return BadRequest(responses);

            return Ok(responses);
        }
    }
}
