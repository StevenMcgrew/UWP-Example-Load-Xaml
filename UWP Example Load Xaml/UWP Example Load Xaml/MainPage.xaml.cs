using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace UWP_Example_Load_Xaml
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            if (myStackPanel.Children.Count > 2)
            {
                // Do nothing, because another button has already been added
                return;
            }
            else
            {
                // Get your file
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(BaseUri, "/MyXamlFiles/anotherButtonXamlString.txt"));

                // Read your file and set it to a string variable
                string myXamlString = await FileIO.ReadTextAsync(file);

                // Create the object from that string
                object button = XamlReader.Load(myXamlString);

                // Initialize it and cast it to the proper type
                Button anotherButton = button as Button;

                // Add it to your view
                myStackPanel.Children.Add(anotherButton);

                // Create an event handler
                anotherButton.Click += new RoutedEventHandler(anotherButton_Click);

                //...Below, is a way to make changes to the element you added
                //...by walking the tree using XLinq and cast back to a XAML type
                //...in order to set a property on it at runtime
                //
                //FrameworkElement resultElement = (from someElement in myStackPanel.Children
                //                                  where (someElement is FrameworkElement)
                //                                  && ((FrameworkElement)someElement).Name == "anotherButton"
                //                                  select someElement as FrameworkElement).FirstOrDefault();
                //
                //((Button)resultElement).Background = new SolidColorBrush(Colors.Pink);
            }
        }

        private void anotherButton_Click(object sender, RoutedEventArgs e)
        {
            myTextBox.Text = "It worked!";
        }
    }
}