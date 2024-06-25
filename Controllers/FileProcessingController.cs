using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ProcessUploadedFile.Controllers
{
    [Route("api/[controller]")]
    public class FileProcessingController : ControllerBase
    {
        private static List<ProcessLogs> logs = new List<ProcessLogs>();

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, string filter)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            var filePath = Path.Combine("uploads", file.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ProcessLogs processLogs = new ProcessLogs();
            processLogs.filename = Path.GetFileName(filePath).ToString();
            processLogs.dateposted = DateTime.Now;
            logs.Add(processLogs);

            if (Path.GetExtension(filePath).ToString().ToLower() == ".json")
            {
                var result = SearchJson(filePath, filter);
                return Ok(new { message = "Success", result });
            }
            else
            {
                return BadRequest("Invalid file.");
            }
        }

        [HttpGet("report")]
        public IActionResult GenerateReports()
        {
            return Ok(new { logs });
        }

        private List<string> SearchJson(string filePath, string search)
        {
            var json = System.IO.File.ReadAllText(filePath);
            List<string> jsonList = JsonConvert.DeserializeObject<List<string>>(json);
            List<string> result = new List<string>();

            result = (string.IsNullOrEmpty(search)) ? jsonList : jsonList.Where(x => x.ToLower().Contains(search)).ToList();

            return result;
        }

    }

    public class ProcessLogs()
    {
        public string filename { get; set; }

        public DateTime dateposted { get; set; }
    }
}
