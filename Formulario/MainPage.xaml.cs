using System;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

//using Android.App;


namespace Formulario;

public partial class MainPage : ContentPage
{
	int count = 0;
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
	}

	public async void OpenAddPhoto_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            // var boxPhoto = b.Parent as StackLayout;
            // var buttonAdd = (Button)boxPhoto.Children[0];
            // var ide = buttonAdd.ClassId;
			var ide = b.ClassId;

			//await Task.Run(()=> CounterBtn.Text = $"Clicked");

			//CounterBtn.Text = $"Clicked";

            //await viewModel.ExecuteTakePhotoCommand();
            //await viewModel.ExecuteTakePhotoCommand(ide);

			try
           {
            //    //Plugin.Media.Abstractions.Location _location = await GetGeolocation();
               await CrossMedia.Current.Initialize();

               if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
               {
					await DisplayAlert("Error", "Camara no detectada" , "Aceptar");
                   //await _dialogService.ShowError("Camara no detectada", "Error", "OK", null);
                   return;
               }
               var photoName = "";
               var varName = "";

            	photoName = $"PHOTO_{DateTime.Now.ToString()}";


               	var pName = "";
            	var pdirectory = $"Face_{new Random().Next(99).ToString()}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg";
               
                pdirectory = DateTime.Now.ToString("yyyyMMddHHmmss");

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
               {
                   Name = pName,
                   Directory = pdirectory,
                   CompressionQuality = 75,
                   CustomPhotoSize = 50,
                   PhotoSize = PhotoSize.MaxWidthHeight,
                   MaxWidthHeight = 2000
               });

               if (file == null)
               {
                   //await _dialogService.ShowError("La foto no se tomo correctamente", "Error", "OK", null);
                   return;
               }


				FacePhoto1 = pName;
				PhotoPath = file.Path;
				var UpdateFace1 = true;
				var NewoneFace1 = false;
				byte[] FileImage ;
				string CodeHash = string.Empty;


			if (PhotoPath != "")
            {
                var img = File.ReadAllBytes(PhotoPath);
                SHA512 objAlgoritmo = SHA512.Create();
                var imgHash = objAlgoritmo.ComputeHash(img);
                FileImage = img;
                var FileName = FacePhoto1;
                var Face = 1;//face;
            	CodeHash = Convert.ToBase64String(imgHash);
            }
            else
            {
                FileImage = new byte[0];
                CodeHash = "";
            }
  

               //MediaScannerConnection.ScanFile(Android.App.Application.Context, new string[] { file.Path }, null, null);
#if DEBUG
				await DisplayAlert("Error", $"Fin del proceso Multimedia {ide}" , "Aceptar");
#endif

           }
           catch (Exception ex)
           {
				await DisplayAlert("Error", ex.Message , "Aceptar");
               //new DisplayInfo();// _dialogService.ShowError(ex.Message, "Error", "Aceptar", null);""
           }
        }


	// private void OnCounterClicked(object sender, EventArgs e)
	// {
	// 	count++;

	// 	if (count == 1)
	// 		CounterBtn.Text = $"Evento : primera vez";
	// 	else
	// 		CounterBtn.Text = $"Eventos : {count} veces";

	// 	SemanticScreenReader.Announce(CounterBtn.Text);

	// }


	private void OnCounterCompleted(object s, EventArgs e)
	{
		CounterBtn.Text = $"Completado";
	}
}

