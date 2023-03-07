using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using Avalonia.Interactivity;
using EditorPanelExample.Models;
using EditorPanelExample.ViewModels;
using EditorPanelExample.Views.Components.Common;
using EditorPanelExample.Views.Components.Templates;
using ReactiveUI;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;

namespace EditorPanelExample.Views
{
    public partial class MainWindow : Window
    {
        private ScrollViewer _mainScrollViewer;
        private Button _addComponentButton;
        private AddComponentListBox _addComponentListBox;
        
        public MainWindow()
        {
            InitializeComponent();

            _mainScrollViewer = this.FindControl<ScrollViewer>("mainScrollViewer");
            _addComponentButton = this.FindControl<Button>("addComponentButton");
            _addComponentListBox = this.FindControl<AddComponentListBox>("addComponentListBox");

            _addComponentButton.Flyout.Opening += AddComponentFlyoutOpening;
            _addComponentListBox.PointerReleased += AddComponentListBoxPointerReleased;
            _addComponentListBox.MyItemSelected += AddComponentMyItemSelected;
        }

        private void AddComponentFlyoutOpening(object sender, EventArgs e)
        {
            _addComponentListBox.SelectedItem = null;
        }

        private void AddComponentListBoxPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            _addComponentButton.Flyout.Hide();
        }

        // Assumes component was added causing a layout update
        // Otherwise event handler will remain subscribed until a random layout update
        private void AddComponentMyItemSelected(object sender, EventArgs e)
        {
            _mainScrollViewer.LayoutUpdated += ScrollViewerLayoutUpdated;
        }

        // Scrolls to end assuming a component was appended
        private void ScrollViewerLayoutUpdated(object sender, EventArgs e)
        {
            _mainScrollViewer.ScrollToEnd();
            _mainScrollViewer.LayoutUpdated -= ScrollViewerLayoutUpdated;
        }
    }
}
