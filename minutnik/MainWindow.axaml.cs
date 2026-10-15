using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;

namespace okno;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private int _remainingSeconds;
    private int _completedCycles;
    private bool _isWorkPhase = true;
    private bool _hasStarted;
    private readonly List<string> _history = new();
    
    public MainWindow()
    {
        InitializeComponent();

        _remainingSeconds = GetConfiguredDurationSeconds(isWorkPhase: true);
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;
        Closed += (_, _) => _timer.Stop();
        WorkDurationInput.ValueChanged += (_, _) => OnDurationChanged(isWorkPhase: true);
        BreakDurationInput.ValueChanged += (_, _) => OnDurationChanged(isWorkPhase: false);
        UpdateDisplay();
        _history.Add($"{DateTime.Now:HH:mm} — cykl {_completedCycles}");
        HistoryList.ItemsSource = _history.ToArray();
    }

    private void OnStartPauseClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_timer.IsEnabled)
        {
            _timer.Stop();
        }
        else
        {
            if (!_hasStarted)
            {
                _remainingSeconds = GetConfiguredDurationSeconds(_isWorkPhase);
                _hasStarted = true;
                UpdateDisplay();
            }

            _timer.Start();
        }

        UpdateControls();
    }

    private void OnResetClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _timer.Stop();
        _isWorkPhase = true;
        _hasStarted = false;
        _completedCycles = 0;
        _remainingSeconds = GetConfiguredDurationSeconds(isWorkPhase: true);
        UpdateDisplay();
        UpdateControls();
    }

    private void OnDurationChanged(bool isWorkPhase)
    {
        if (!_timer.IsEnabled && _isWorkPhase == isWorkPhase)
        {
            _remainingSeconds = GetConfiguredDurationSeconds(isWorkPhase);
            UpdateDisplay();
        }
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _remainingSeconds--;

        if (_remainingSeconds <= 0)
        {
            _isWorkPhase = !_isWorkPhase;
            if (_isWorkPhase)
            {
                _completedCycles++;
            }

            _remainingSeconds = GetConfiguredDurationSeconds(_isWorkPhase);
        }

        UpdateDisplay();
    }

    private int GetConfiguredDurationSeconds(bool isWorkPhase)
    {
        var minutes = isWorkPhase
            ? WorkDurationInput.Value ?? 25
            : BreakDurationInput.Value ?? 5;
        return decimal.ToInt32(minutes) * 60;
    }

    private void UpdateDisplay()
    {
        var minutes = _remainingSeconds / 60;
        var seconds = _remainingSeconds % 60;
        TimeText.Text = $"{minutes:00}:{seconds:00}";
        PhaseText.Text = _isWorkPhase ? "Czas na pracę" : "Czas na przerwę";
        CyclesText.Text = _completedCycles.ToString();
        UpdateControls();
    }

    private void UpdateControls()
    {
        StartPauseButton.Content = _timer.IsEnabled
            ? "Pauza"
            : _hasStarted
                ? "Wznów"
                : "Start";
    }
}