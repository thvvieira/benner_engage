
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyFriends.Helpers;
using MyFriends.Model;
using Windows.UI.Xaml.Media.Imaging;

namespace MyFriends.ViewModel
{
    public class FriendViewModel : ViewModelBase
    {
        private FullNameSchema _schema = FullNameSchema.FirstLast;

        public Friend Model
        {
            get;
            private set;
        }

        public FriendViewModel(Friend model, FullNameSchema schema)
        {
            Model = model;

            Model.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Friend.FirstNamePropertyName
                    || e.PropertyName == Friend.LastNamePropertyName)
                {
                    RaisePropertyChanged(() => FullName);
                    return;
                }

                if (e.PropertyName == Friend.DateOfBirthPropertyName)
                {
                    RaisePropertyChanged(() => DateOfBirthFormatted);
                }
            };

            Messenger.Default.Register<ChangeFullNameSchemaMessage>(
                this,
                msg =>
                {
                    _schema = msg.Schema;
                    RaisePropertyChanged(() => FullName);
                });
        }

        public string DateOfBirthFormatted
        {
            get
            {
                return Model.DateOfBirth.ToString("d");
            }
        }

        public string FullName
        {
            get
            {
                switch (_schema)
                {
                    case FullNameSchema.LastFirstComma:
                        return string.Format("{0}, {1}", Model.LastName, Model.FirstName);
                    default:
                        return string.Format("{0} {1}", Model.FirstName, Model.LastName);
                }
            }
        }
    }
}
