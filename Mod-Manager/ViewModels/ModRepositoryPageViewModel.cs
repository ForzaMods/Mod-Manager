using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mod_Manager.Models;
using Wpf.Ui.Controls;
using static System.Windows.Application;
using static System.Windows.Threading.DispatcherPriority;

namespace Mod_Manager.ViewModels;

public partial class ModRepositoryPageViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private bool _isInitialized;
    
    [ObservableProperty]
    private bool _areModsLoaded;

    [ObservableProperty] 
    private string? _loadingText;

    [ObservableProperty] 
    private GridLength _modListHeight;

    [ObservableProperty] 
    private GridLength _loadingHeight;

    [ObservableProperty]
    private double _loadingSize;
    
    [ObservableProperty] 
    private ObservableCollection<DataMod> _dataMods = new();
    
    public void OnNavigatedTo()
    {
        if (IsInitialized)
        {
            return;
        }

        InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private async void InitializeViewModel()
    {
        await LoadModPages();
        AreModsLoaded = true;
        IsInitialized = true;
    }

    private async Task LoadModPages()
    {
        AreModsLoaded = false;
        LoadingStart();
        TextLoadingAnimation();
        _completionSource = new TaskCompletionSource<bool>();
        ParseModPages();
        await _completionSource.Task;
        LoadingEnd();
        AreModsLoaded = true;
    }
    
    private TaskCompletionSource<bool>? _completionSource;
    
    private async void TextLoadingAnimation()
    {
        var dotCount = 0;
        while (!IsInitialized)
        {
            dotCount = (dotCount + 1) % 4;
            var count = dotCount;
            LoadingText = $"Parsing Mod Pages{new string('.', count)}";
            await Task.Delay(250);
        }
    }

    private async void ParseModPages()
    {
        if (DataMods.Any())
        {
            DataMods.Clear();
        }
        
        var alphabet = GenerateRandomStrings(100, 5, 5);
        var enumerable = alphabet.ToArray();
        var dataMods = enumerable.Select(t => new DataMod
        {
            Name = t,
            Id = 5,
            ImageUrls = new List<string> { "https://media.discordapp.net/attachments/1034577082933592124/1181923291347288084/Screenshot_2023-12-06_052446.png" }
        }).ToList();
        var cpuThreads = Environment.ProcessorCount;
        var batchSize = cpuThreads * 2;
        var sleepTime = cpuThreads <= 4 ? cpuThreads : cpuThreads / 2;
        
        for (var i = 0; i < dataMods.Count; i += batchSize)
        {
            var batch = dataMods.Skip(i).Take(batchSize).ToList();

            await Task.Run(() =>
            {
                foreach (var t in batch)
                {
                    Current.Dispatcher.InvokeAsync(() => DataMods.Add(t), Render);
                }
            });

            await Task.Delay(sleepTime); 
        }
        
        _completionSource?.SetResult(true);
    }

    private static List<string> GenerateRandomStrings(int numberOfStrings, int minLength, int maxLength)
    {
        var randomStrings = new List<string>();
        var random = new Random();

        for (var i = 0; i < numberOfStrings; i++)
        {
            var stringLength = random.Next(minLength, maxLength + 1);
            var randomString = GenerateRandomString(stringLength);
            randomStrings.Add(randomString);
        }

        return randomStrings;
    }

    private void LoadingStart()
    {
        LoadingSize = 200d;
        ModListHeight = new GridLength(0, GridUnitType.Star);
        LoadingHeight = new GridLength(1, GridUnitType.Star);
    }

    private void LoadingEnd()
    {
        ModListHeight = new GridLength(1, GridUnitType.Star);
        LoadingHeight = new GridLength(0, GridUnitType.Star);
        LoadingSize = 0d;
        
        
        
    }
    
    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();

        var randomString = new char[length];
        for (var i = 0; i < length; i++)
        {
            randomString[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomString);
    }
    
    
    [RelayCommand]
    private async Task OnRefresh()
    {
        await LoadModPages();
    }
}