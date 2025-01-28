using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Formulario;

public class MainPageViewModel
{
        protected bool SetProperty<T>(
		ref T backingStore, T value,
		[CallerMemberName]string propertyName = "",
		Action onChanged = null)
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
			return false;

		backingStore = value;
		onChanged?.Invoke();
		OnPropertyChanged(propertyName);
		return true;
	}

    private void OnPropertyChanged(string propertyName)
    {
        throw new NotImplementedException();
    }

    private string _facePhoto1 = "";
	public string FacePhoto1 {
		get { return _facePhoto1; }
		set { SetProperty(ref _facePhoto1, value); }
	}
	private string _facePhotoPath1 = "";
	public string PhotoPath
	{
		get { return _facePhotoPath1; }
		set { SetProperty(ref _facePhotoPath1, value); }
	}
        private async Task EcxecuteImport(object photo)
    {
        // var newFile = Path.Combine(Microsoft.VisualBasic.FileSystem.AppDataDirectory, $"{DateTime.Now:yyyyMMddHHmmss}.jpg");

        // using (var stream = new File())//await photo.OpenReadAsync())
        // using (var newStream = File.OpenWrite(newFile))
        //     await stream.CopyToAsync(newStream);

        // FacePhoto1 = Path.GetFileName(newFile);
        // PhotoPath = newFile;

        SaveFile(out byte[] fileImage, out string codeHash);
    }
        private void SaveFile(out byte[] FileImage, out string CodeHash)
    {
        if (!string.IsNullOrEmpty(PhotoPath))
        {
            var img = File.ReadAllBytes(PhotoPath);
            using var objAlgoritmo = SHA512.Create();
            var imgHash = objAlgoritmo.ComputeHash(img);
            FileImage = img;
            CodeHash = Convert.ToBase64String(imgHash);
        }
        else
        {
            FileImage = Array.Empty<byte>();
            CodeHash = string.Empty;
        }
    }

}
