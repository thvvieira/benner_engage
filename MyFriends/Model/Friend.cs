using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.SimpleSerializer;

namespace MyFriends.Model
{
    [SimpleSerialize]
    public class Friend : ObservableObject
    {
        /// <summary>
        /// The <see cref="FirstName" /> property's name.
        /// </summary>
        public const string FirstNamePropertyName = "FirstName";

        private string _firstName;

        /// <summary>
        /// Sets and gets the FirstName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [SimpleSerialize(FieldName = "first_name")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                Set(FirstNamePropertyName, ref _firstName, value);
            }
        }

        /// <summary>
        /// The <see cref="LastName" /> property's name.
        /// </summary>
        public const string LastNamePropertyName = "LastName";

        private string _lastName;

        /// <summary>
        /// Sets and gets the LastName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [SimpleSerialize(FieldName = "last_name")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                Set(LastNamePropertyName, ref _lastName, value);
            }
        }

        /// <summary>
        /// The <see cref="DateOfBirth" /> property's name.
        /// </summary>
        public const string DateOfBirthPropertyName = "DateOfBirth";

        private string _dateOfBirthString;

        /// <summary>
        /// Sets and gets the DateOfBirth property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [SimpleSerialize(FieldName = "birthday")]
        public string DateOfBirthString
        {
            get
            {
                return _dateOfBirthString;
            }
            set
            {
                _dateOfBirthString = value;
                RaisePropertyChanged(() => DateOfBirth);
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                if (string.IsNullOrEmpty(_dateOfBirthString))
                {
                    return DateTime.MinValue;
                }

                return DateTime.ParseExact(DateOfBirthString, "d", CultureInfo.InvariantCulture);
            }
            set
            {
                _dateOfBirthString = value.ToString("d", CultureInfo.InvariantCulture);
            }
        }

        private string _imageUrl;

        [SimpleSerialize(FieldName = "picture")]
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
                RaisePropertyChanged(() => ImageUri);
            }
        }

        public Uri ImageUri
        {
            get
            {
                return new Uri(_imageUrl);
            }
        }
    }
}
