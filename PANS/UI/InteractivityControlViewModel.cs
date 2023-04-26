using PANS.Services;

namespace PANS.UI
{
    public class InteractivityControlViewModel: ViewModelBase
    {
        private readonly ISHA1Encryptor _encryptor;
        private string _enteredStr;

        public InteractivityControlViewModel(ISHA1Encryptor encryptor)
        {
            _encryptor = encryptor;
        }

		public string EnteredStr
        {
			get => _enteredStr;
            set
            {
                _enteredStr = value;
                SymbolCount = _enteredStr.Length.ToString();
                EncryptedStr = _symbolCount;
                OnPropertyChanged(nameof(EnteredStr));
            }
		}

        private string _symbolCount;

        public string SymbolCount
        {
            get => _symbolCount;
            set
            {
                _symbolCount = value;
                OnPropertyChanged(nameof(SymbolCount));
            }
        }

        private string _encryptedStr;

        public string EncryptedStr
        {
            get => _encryptedStr;
            set
            {
                _encryptedStr += _encryptor.Encrypt(value, "sss");
                OnPropertyChanged(nameof(EncryptedStr));
            }
        }
    }
}