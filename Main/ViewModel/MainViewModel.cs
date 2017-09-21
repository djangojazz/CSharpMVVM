using CSharpControls;
using CSharpControls.Business;
using CSharpControls.Charting;
using CSharpControls.Converters.Instances;
using CSharpControls.Types;
using Main.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Main
{
  public class MainViewModel : BaseViewModel
  {
    private RelayCommand _testCommand;
    private List<PlotPoints> _lastPoints = new List<PlotPoints>();
    
    public MainViewModel()
    {
      AddingLinesForLineChart();
    }

    #region Properties
    public TrendChoices[] Array
    {
      get => (TrendChoices[])Enum.GetValues(typeof(TrendChoices));
    }

    public InstanceInSetToStringConverter InstanceConverter { get; }
    public ObservableCollectionContentNotifying<PlotTrend> ChartData { get; } = new ObservableCollectionContentNotifying<PlotTrend>();
    
    #region TestText
    private string _testText;
    public string TestText
    {
      get { return _testText; }
      set
      {
        _testText = value;
        OnPropertyChanged(nameof(TestText));
      }
    }
    #endregion

    #region LocationHeader
    private string _LocationHeader;
    public string LocationHeader
    {
      get { return _LocationHeader; }
      set
      {
        _LocationHeader = value;
        OnPropertyChanged(nameof(LocationHeader));
      }
    }
    #endregion
    
    #region XTicks
    private int _xTicks;
    public int XTicks
    {
      get { return _xTicks; }
      set
      {
        _xTicks = value;
        OnPropertyChanged(nameof(XTicks));
      }
    }
    #endregion

    #region Text
    private string _text;

    public string Text
    {
      get { return _text; }
      set
      {
        _text = value;
        OnPropertyChanged(nameof(Text));
      }
    }
    #endregion
    #endregion
    
    public ICommand TestCommand { get => (_testCommand == null) ? _testCommand = new RelayCommand(param => LinePlotAdding()) : _testCommand; }
    
    #region Methods

    #region "Line Graph parts"

    private void AddingLinesForLineChart()
    {
      _lastPoints = new List<PlotPoints>{
      new PlotPoints(new PlotPoint<DateTime>(new DateTime(2017, 2, 12)), new PlotPoint<double>(1200)),
      new PlotPoints(new PlotPoint<DateTime>(new DateTime(2017, 2, 12)), new PlotPoint<double>(1200))
      };

      var o = new ObservableCollection<PlotPoints>{
      new PlotPoints(new PlotPoint<DateTime>(new DateTime(2017, 2, 5)), new PlotPoint<double>(400)),
      new PlotPoints(new PlotPoint<DateTime>(new DateTime(2017, 2, 11)), new PlotPoint<double>(800)),
      _lastPoints[0]
      };

      var o2 = new List<PlotPoints>{
      new PlotPoints(new PlotPoint<DateTime>(new DateTime(2017, 2, 5)), new PlotPoint<double>(150)),
      new PlotPoints(new PlotPoint<DateTime>(new DateTime(2017, 2, 11)), new PlotPoint<double>(720)),
      _lastPoints[1]
      };
      
      ChartData.ClearAndAddRange(new List<PlotTrend>{
        new PlotTrend("First", Brushes.Blue, new Thickness(2), o),
        new PlotTrend("Second", Brushes.Red, new Thickness(2), o2)
      });

      var distinctCounts = (ChartData.SelectMany(x => x.Points).Select(x => x.XAsDouble).Distinct().Count() - 1);
      XTicks = distinctCounts > 0 ? distinctCounts : 1;
    }

    private void LinePlotAdding()
    {
      dynamic newPoints = new List<PlotPoints>();
      

      for (int i = 1; i <= _lastPoints.Count; i++)
      {
        newPoints.Add(new PlotPoints(new PlotPoint<DateTime>((((PlotPoint<DateTime>)_lastPoints[i - 1].X).Point).AddDays(1)), new PlotPoint<double>(((PlotPoint<double>)_lastPoints[i - 1].Y).Point * 1.95)));
      }

      _lastPoints = newPoints;
      ChartData[0].Points.Add(_lastPoints[0]);
      ChartData[1].Points.Add(_lastPoints[1]);

      dynamic distinctCounts = (ChartData.SelectMany(x => x.Points).Select(x => x.XAsDouble).Distinct().Count() - 1);
      XTicks = distinctCounts > 0 ? distinctCounts : 1;

      ChartData.ClearAndAddRange(new List<PlotTrend>{
        new PlotTrend("First", Brushes.Blue, new Thickness(2), ChartData[0].Points),
        new PlotTrend("Second", Brushes.Red, new Thickness(2), ChartData[1].Points)
      });
    }

    #endregion
    #endregion
  }
}
