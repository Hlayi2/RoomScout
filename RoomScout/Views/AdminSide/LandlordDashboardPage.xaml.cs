using RoomScout.Services.Common;

namespace RoomScout.Views.AdminSide;

public partial class LandlordDashboardPage : ContentPage
{
    private readonly IFileUploadService _fileUploadService;
    private Dictionary<string, Models.AdminSide.UploadedFile> _uploadedFiles;

    public LandlordDashboardPage()
    {
        InitializeComponent();
        _fileUploadService = new FileUploadService();
        _uploadedFiles = new Dictionary<string, Models.AdminSide.UploadedFile>();
    }

    private async void OnUploadFileClicked(object sender, EventArgs e)
    {
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

    private async void OnAddRoomClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new AddListingPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to navigate to Add Listing page.", "OK");
        }

    }

}

