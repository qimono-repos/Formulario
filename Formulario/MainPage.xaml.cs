using System;
//using Plugin.Media.Abstractions;
//using Plugin.Media;
using Microsoft.Maui.Media;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.ComponentModel;

//using Android.App;


namespace Formulario;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
	int count = 0;
    //public event PropertyChangedEventHandler PropertyChanged;
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

	public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    public async void OpenAddPhoto_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    var newFile = Path.Combine(FileSystem.AppDataDirectory, $"{DateTime.Now:yyyyMMddHHmmss}.jpg");

                    using (var stream = await photo.OpenReadAsync())
                    using (var newStream = File.OpenWrite(newFile))
                        await stream.CopyToAsync(newStream);

                    FacePhoto1 = Path.GetFileName(newFile);
                    PhotoPath = newFile;

                    SaveFile(out byte[] fileImage, out string codeHash);

                }
            }
            else
            {
                await DisplayAlert("Error", "Camera not supported", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    public async void OpenAddGallery_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                await EcxecuteImport(photo);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async Task EcxecuteImport(object photo)
    {
        var newFile = Path.Combine(FileSystem.AppDataDirectory, $"{DateTime.Now:yyyyMMddHHmmss}.jpg");

        using (var stream = await photo.OpenReadAsync())
        using (var newStream = File.OpenWrite(newFile))
            await stream.CopyToAsync(newStream);

        FacePhoto1 = Path.GetFileName(newFile);
        PhotoPath = newFile;

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

  

    private void OnCounterCompleted(object s, EventArgs e)
	{
		CounterBtn.Text = $"Completado";
	}
}

