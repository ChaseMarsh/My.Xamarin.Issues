using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace DynamicResouceTriggerIssue
{
    public enum Theme
    {
        Default,
        Dark
    }

    public static class ThemeManager
    {
        private static Theme _currentTheme;
        public static Theme CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                _currentTheme = value;
                ApplyTheme(_currentTheme);
            }
        }
        private static void ApplyTheme(Theme theme)
        {
            if (Application.Current != null)
            {
                if (!Application.Current.Resources.MergedDictionaries.Any((rd) => rd is ColorResources))
                    Application.Current.Resources.MergedDictionaries.Add(new ColorResources());

                switch (CurrentTheme)
                {
                    case Theme.Default:
                        UpdateResources<Color>(new ColorResources());
                        break;
                    case Theme.Dark:
                        UpdateResources<Color>(new DarkColorResources());
                        break;
                }
            }
        }

        private static void UpdateResources<T>(ResourceDictionary newResources)
        {
            foreach (var newResourceKey in newResources.Keys)
            {
                if (Application.Current.Resources.TryGetValue(newResourceKey, out var appResource))
                {
                    if (appResource is T)
                    {
                        Application.Current.Resources[newResourceKey] = newResources[newResourceKey];
                    }
                }
            }
        }
    }
}
