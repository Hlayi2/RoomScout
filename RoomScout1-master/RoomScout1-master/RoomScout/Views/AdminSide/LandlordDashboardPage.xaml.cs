using RoomScout.Services.Common;
using RoomScout.Models.AdminSide;

namespace RoomScout.Views.AdminSide;

public partial class LandlordDashboardPage : ContentPage
{
    private readonly IFileUploadService _fileUploadService;
    private Dictionary<string, Models.AdminSide.UploadedFile> _uploadedFiles;

    private Landlord _landlord;


    public LandlordDashboardPage()
    {
        InitializeComponent();
        _fileUploadService = new FileUploadService();
        _uploadedFiles = new Dictionary<string, Models.AdminSide.UploadedFile>();

        _landlord = new Landlord
        {
            FullNames = Preferences.Get("FullNames", string.Empty),
            Email = Preferences.Get("Email", string.Empty),
            IdOrPassportNo = Preferences.Get("IdOrPassportNo", string.Empty),
            AccommodationName = Preferences.Get("AccommodationName", string.Empty),
            Address = Preferences.Get("Address", string.Empty),
            ProfilePicture = Preferences.Get("ProfilePicture", "profiles.png")
        };

        BindingContext = _landlord;
    }

    private async void OnUploadFileClicked(object sender, EventArgs e)
    {
        var fileResult = await FilePicker.Default.PickAsync();
        if (fileResult != null)
        {
            // Handle file upload
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileResult.FileName);
            using (var stream = await fileResult.OpenReadAsync())
            {
                using (var newStream = File.Create(filePath))
                {
                    await stream.CopyToAsync(newStream);
                }
            }
            _landlord.ProfilePicture = filePath; // Save the new path
            Preferences.Set("ProfilePicture", filePath);
            await DisplayAlert("File Uploaded", "Your file has been uploaded successfully!", "OK");
        }


        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                        { DevicePlatform.iOS, new[] { "public.image", "public.pdf" } },
                        { DevicePlatform.Android, new[] { "image/*", "application/pdf" } },
                        { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".pdf" } },
                        { DevicePlatform.macOS, new[] { "public.image", "public.pdf" } }
                });

            var options = new PickOptions
            {
                PickerTitle = "Please select a file",
                FileTypes = customFileType,
            };

            var result = await FilePicker.PickAsync(options);
            if (result != null)
            {
                string purpose = ((Button)sender).ClassId;

                try
                {
                    var uploadedFile = await _fileUploadService.UploadFileAsync(result, purpose);
                    _uploadedFiles[purpose] = uploadedFile;

                    // Update button text to show filename
                    ((Button)sender).Text = Path.GetFileName(result.FileName);
                    await DisplayAlert("Success", "File uploaded successfully", "OK");
                }
                catch (InvalidOperationException ex)
                {
                    await DisplayAlert("Error", "Invalid file type or size. Please select a PDF or image file under 10MB.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Failed to upload file. Please try again.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to upload file: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }

    // Add this method to get uploaded files when saving
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_uploadedFiles.Count == 0)
        {
            await DisplayAlert("Warning", "Please upload all required documents before saving.", "OK");
            return;

        }

        // Handle saving the form data along with the uploaded files
        // Add your save logic here
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        Preferences.Set("FullNames", _landlord.FullNames);
        Preferences.Set("Email", _landlord.Email);
        Preferences.Set("IdOrPassportNo", _landlord.IdOrPassportNo);
        Preferences.Set("AccommodationName", _landlord.AccommodationName);
        Preferences.Set("Address", _landlord.Address);
        Preferences.Set("ProfilePicture", _landlord.ProfilePicture);

        // Add your submission logic here
        await DisplayAlert("Success", " Profile Information saved successfully! You will be redirected in 2 seconds", "OK");
        await Task.Delay(2000);

        // Navigate to the DashboardProfile page
        await Navigation.PushAsync(new DashboardProfile());

    }

    private async void OnProfileImageTapped(object sender, EventArgs e)
    {
        var action = await DisplayActionSheet("Profile Picture", "Cancel", null, "Upload", "Remove");

        if (action == "Upload")
        {
            // Handle image upload logic here, such as opening a file picker
            var pickedImage = await PickAndShowImage();
            if (pickedImage != null)
            {
                // Update the profile picture source
                ProfileImage.Source = pickedImage;
            }
        }
        else if (action == "Remove")
        {
            // Handle removing the profile picture
            ProfileImage.Source = "profiles.png"; // Set to default image
        }
    }

    private async Task<ImageSource> PickAndShowImage()
    {
        var fileResult = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (fileResult != null)
        {
            return ImageSource.FromFile(fileResult.FullPath);
        }

        return null;
    }



}

