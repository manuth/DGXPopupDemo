// <copyright file="NumericFilterControl.cs" company="Anderes Finanzberatung">
// Copyright © Anderes Finanzberatung. All rights reserved.
// </copyright>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataGridExtensions;

namespace DGXPopupDemo.Interaction.Controls.Filtering
{
    /// <summary>
    /// Provides the functionality to filter numeric values.
    /// </summary>
    public partial class NumericFilterControl : Control
    {
        /// <summary>
        /// Identifies the <see cref="IsExpanded"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(NumericFilterControl), new FrameworkPropertyMetadata(false, (sender, e) => ((NumericFilterControl)sender).OnPopupVisibilityChanged((bool)e.NewValue)));

        /// <summary>
        /// Identifies the <see cref="IsActive"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(NumericFilterControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, _) => ((NumericFilterControl)sender).OnConfigChanged()));

        /// <summary>
        /// Identifies the <see cref="Minimum"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(NumericFilterControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, _) => ((NumericFilterControl)sender).OnConfigChanged()));

        /// <summary>
        /// Identifies the <see cref="Maximum"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(NumericFilterControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, _) => ((NumericFilterControl)sender).OnConfigChanged()));

        /// <summary>
        /// Identifies the <see cref="Filter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(Filter), typeof(IContentFilter), typeof(NumericFilterControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, _) => ((NumericFilterControl)sender).OnFilterChanged()));

        /// <summary>
        /// The <see cref="DataGrid"/> which contains this control.
        /// </summary>
        private DataGrid? dataGrid;

        static NumericFilterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericFilterControl), new FrameworkPropertyMetadata(typeof(NumericFilterControl)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericFilterControl"/> class.
        /// </summary>
        public NumericFilterControl()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the filter window is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the filter is active.
        /// </summary>
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        /// <summary>
        /// Gets or sets the specified minimum of the range to filter.
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Gets or sets the specified maximum of the range to filter.
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Gets or sets the filter of the control.
        /// </summary>
        public IContentFilter? Filter
        {
            get => (IContentFilter)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            dataGrid = FindAncestor<DataGrid>();
        }

        /// <summary>
        /// Finds an ancestor with the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the ancestor to find.</typeparam>
        /// <returns>The closest ancestor with type <typeparamref name="T"/>.</returns>
        protected T? FindAncestor<T>() where T : DependencyObject
        {
            DependencyObject item = this;

            while (item != null)
            {
                if (item is T result)
                {
                    return result;
                }
                else
                {
                    item = LogicalTreeHelper.GetParent(item) ?? VisualTreeHelper.GetParent(item);
                }
            }

            return null;
        }

        /// <summary>
        /// Handles the change of the visibility of the popup.
        /// </summary>
        /// <param name="visible">A value indicating whether the popup is visible.</param>
        protected void OnPopupVisibilityChanged(bool visible)
        {
            if (!visible)
            {
                this.MoveFocusToDataGrid(dataGrid);
            }
        }

        /// <summary>
        /// Handles the change of a configuration.
        /// </summary>
        protected void OnConfigChanged()
        {
            NumericFilter? filter;

            if (!IsActive ||
                Maximum > Minimum)
            {
                filter = null;
            }
            else
            {
                filter = new NumericFilter(Minimum, Maximum);
            }

            Filter = filter;
        }

        /// <summary>
        /// Handles the change of the filter.
        /// </summary>
        protected void OnFilterChanged()
        {
            if (Filter is NumericFilter filter)
            {
                Maximum = filter.Max;
                Minimum = filter.Min;
            }
        }

        /// <summary>
        /// Provides the functionality to filter numeric values based on pre-defined boundaries.
        /// </summary>
        public class NumericFilter : IContentFilter
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NumericFilter"/> class.
            /// </summary>
            /// <param name="min">The minimum of the value to pass the filter.</param>
            /// <param name="max">The maximum of the value to pass the filter.</param>
            public NumericFilter(double min, double max)
            {
                Min = min;
                Max = max;
            }

            /// <summary>
            /// Gets the minimum of the value to pass the filter.
            /// </summary>
            public double Min { get; }

            /// <summary>
            /// Gets the maximum of the value to pass the filter.
            /// </summary>
            public double Max { get; }

            /// <summary>
            /// Determines whether the specified <paramref name="value"/> applies to the filter.
            /// </summary>
            /// <param name="value">The value to check.</param>
            /// <returns>A value indicating whether the specified <paramref name="value"/> applies to the filter.</returns>
            public bool IsMatch(object? value)
            {
                if (double.TryParse(value?.ToString(), out double numericValue))
                {
                    return (numericValue >= Min) && (numericValue <= Max);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
