using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using Xabe.FFmpeg;

namespace Web_BTL.Services.UploadFile
{
    public class SaveImageVideo
    {
        public async Task<string> SaveImageAsync(IWebHostEnvironment _environment, string url, string oldName, string newName, IFormFile image)
        {
            validateEnvironment(_environment);
            validateFile(image);
            validateUrl(url);
            // Đường dẫn tới thư mục chứa ảnh trong wwwroot
            string uploadsFolder = Path.Combine(_environment.WebRootPath, url);
            // Lấy ảnh cũ từ cơ sở dữ liệu
            //string oldImagePath = Path.Combine(uploadsFolder, oldName);
            //// Kiểm tra nếu file ảnh cũ tồn tại thì xóa nó đi
            //if (File.Exists(oldImagePath) && oldName != "default.jpg") // cách trước
            //    File.Delete(oldImagePath);
            DeleteFile(_environment, url, oldName);
            // Lấy phần mở rộng của file (ví dụ .jpg, .png)
            string fileExtension = Path.GetExtension(image.FileName);
            // Tạo tên file tùy chỉnh, ở đây đang lấy theo tên đăng nhập
            string name = newName;
            string fileName = $"{name}{fileExtension}";
            // Đường dẫn file mới
            string newFilePath = Path.Combine(uploadsFolder, fileName);
            // Lưu ảnh mới vào thư mục với tên mới
            try
            {
                await SaveFileAsync(image, newFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("###################### Loi o phan luu anh" + ex.Message + " ######################");
            }
            // Cập nhật đường dẫn ảnh mới vào cơ sở dữ liệu
            return fileName;
        }
        public async Task<(string videoName, TimeSpan? duration)> SaveVideoAsync(IWebHostEnvironment _environment, string url, string oldName, string newName, IFormFile video, bool getDuration = false)
        {
            validateEnvironment(_environment);
            validateFile(video);
            validateUrl(url);
            string uploadsFolder = Path.Combine(_environment.WebRootPath, url);
            //string oldVideoPath = Path.Combine(uploadsFolder, oldName);
            //if (File.Exists(oldVideoPath) && oldName != "default.mp4") // kiểm tra xem cideo cũ đã tồn tại chưa
            //    File.Delete(oldVideoPath); // xoá video cũ đi
            DeleteFile(_environment, url, oldName);
            string fileExtension = Path.GetExtension(video.FileName);
            string name = newName;
            string fileName = $"{name}{fileExtension}";
            string newFilePath = Path.Combine(uploadsFolder, fileName);
            try
            {
                await SaveFileAsync(video, newFilePath);
                TimeSpan? duration = null;
                if (getDuration)
                {
                    var mediaInfo = await FFmpeg.GetMediaInfo(newFilePath);
                    duration = mediaInfo.Duration;
                }
                return (fileName, duration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Khong up duoc video: {ex.Message}");
            }
            return (fileName, null); // trả về 1 tuple(struct)
        }
        public void DeleteFile(IWebHostEnvironment _environment, string url, string name)
        {
            validateEnvironment(_environment);
            validateUrl(url);
            string filePath = Path.Combine(_environment.WebRootPath, url, name); // lấy đường dẫn từ wwwroot để xoá file
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        private async Task SaveFileAsync(IFormFile file, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        private void validateEnvironment(IWebHostEnvironment environment)
        {
            if (environment == null) throw new ArgumentNullException("Lỗi môi trường không tồn tại");
        }
        private void validateFile(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException("Lỗi file ko tồn tại");
        }
        private void validateUrl(string url)
        {
            if (url == null || url == "") throw new ArgumentNullException("url không tồn tại");
        }
    }
}