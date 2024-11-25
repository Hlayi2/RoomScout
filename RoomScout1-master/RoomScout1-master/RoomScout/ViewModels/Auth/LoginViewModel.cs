using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RoomScout.ViewModels.Auth
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isPasswordHidden = true;

        [ObservableProperty]
        private string eyeIcon = "eyeclosed.png";

        [RelayCommand]
        private void TogglePassword()
        {
            IsPasswordHidden = !IsPasswordHidden;
            EyeIcon = IsPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
        }
    }
}