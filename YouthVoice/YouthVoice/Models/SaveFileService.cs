namespace YouthVoice.Models
{
    public class SaveFileService
    {
        public async Task<string> SaveFile(IFormFile file, string folderPath)
        {
            string directory = "";
            try
            {
                if (file != null && file.Length > 0)
                {
                    directory = Path.Combine(folderPath, file.FileName);

                    using (var stream = new FileStream($"wwwroot/{directory}", FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Възникна грешка при качването на изображение.");
            }

            return directory;
        }
    }
}
