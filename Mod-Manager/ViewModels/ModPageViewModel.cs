using CommunityToolkit.Mvvm.ComponentModel;
using Mod_Manager.Models;
using Wpf.Ui.Controls;

namespace Mod_Manager.ViewModels;

public partial class ModPageViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private bool _isInitialized;
    
    [ObservableProperty]
    private DataMod _mod = new()
    {
        Name = "Mod Page Example Text", 
        Author = "Made By {Insert creator here}", 
        ModDescription = "Some basic ass info about the mod Idk insert like parts and shit here or smth",
        Version = "1.2.3.4",
        ImageUrls = new List<string> { "/Assets/ForzaMods.png", "/Assets/ForzaMods.png", "/Assets/ForzaMods.png", "/Assets/ForzaMods.png", "/Assets/ForzaMods.png", "/Assets/ForzaMods.png", "/Assets/ForzaMods.png" }
    };
    
    public void OnNavigatedTo()
    {
        if (IsInitialized)
        {
            return;
        }

        InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        IsInitialized = true;
    }
}