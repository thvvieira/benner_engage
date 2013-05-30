using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresenterBuddy.Helpers;

namespace MyFriends.Design
{
    public class DesignNavigationService : INavigationService
    {
        // This class doesn't perform navigation, in order
        // to avoid issues in the designer at design time.

        public void Navigate(Type sourcePageType)
        {
        }

        public void Navigate(Type sourcePageType, object parameter)
        {
        }

        public void GoBack()
        {
        }
    }
}
