using Microsoft.AspNetCore.Mvc;

public class UploadController : Controller
{
    private readonly Services _blobService;

    public UploadController(Services blobService)
    {
        _blobService = blobService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        await _blobService.UploadAsync(file.FileName, stream);
        return Ok("Uploaded");
    }
}