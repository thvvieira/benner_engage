using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyFriends.Helpers;
using MyFriends.Model;
using PresenterBuddy.Helpers;

namespace MyFriends.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            Friends = new ObservableCollection<FriendViewModel>();

        #if DEBUG
            CreateDesignTimeData();
#endif
        }

        public ObservableCollection<FriendViewModel> Friends
        {
            get;
            private set;
        }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new RelayCommand(ExecuteRefreshCommand));
            }
        }

        private async void ExecuteRefreshCommand()
        {
            var friends = await _dataService.GetFriends();

            if (friends != null)
            {
                Friends.Clear();

                foreach (var friend in friends)
                {
                    Friends.Add(new FriendViewModel(friend, _schema));
                }
            }
        }

        private FullNameSchema _schema = FullNameSchema.FirstLast;

        public bool IsNameDisplayFirstLast
        {
            get
            {
                return _schema == FullNameSchema.FirstLast;
            }
            set
            {
                if (value)
                {
                    SetSchema(FullNameSchema.FirstLast);
                }
            }
        }

        private void SetSchema(FullNameSchema schema)
        {
            _schema = schema;
            RaisePropertyChanged(() => IsNameDisplayFirstLast);
            RaisePropertyChanged(() => IsNameDisplayLastFirstComma);
            Messenger.Default.Send(new ChangeFullNameSchemaMessage(_schema));
        }

        public bool IsNameDisplayLastFirstComma
        {
            get
            {
                return _schema == FullNameSchema.LastFirstComma;
            }
            set
            {
                if (value)
                {
                    SetSchema(FullNameSchema.LastFirstComma);
                }
            }
        }

        /// <summary>
        /// The <see cref="SelectedFriend" /> property's name.
        /// </summary>
        public const string SelectedFriendPropertyName = "SelectedFriend";

        private FriendViewModel _selectedFriend;

        /// <summary>
        /// Sets and gets the SelectedFriend property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public FriendViewModel SelectedFriend
        {
            get
            {
                return _selectedFriend;
            }
            set
            {
                if (Set(SelectedFriendPropertyName, ref _selectedFriend, value)
                    && value != null)
                {
                    _navigationService.Navigate(typeof (FriendView));
                }
            }
        }

#if DEBUG
        private void CreateDesignTimeData()
        {
            if (IsInDesignMode)
            {
                RefreshCommand.Execute(null);
                SelectedFriend = Friends[0];
            }
        }
#endif
    }
}