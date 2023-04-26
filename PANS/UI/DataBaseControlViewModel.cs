using PANS.Entities;
using Server;
using System.Collections.ObjectModel;

namespace PANS.UI
{
    public class DataBaseControlViewModel : ViewModelBase
    {
        private readonly IDbService _dbService;
        private readonly ITechTableSqliteDataAccess _dataAccess;
        private ObservableCollection<TechData> _techDat;

        public ObservableCollection<TechData> TechData
        {
            get => _techDat;
            set
            {
                _techDat = value;
                OnPropertyChanged(nameof(TechData));
            }
        }

        public DataBaseControlViewModel(IDbService dbService, ITechTableSqliteDataAccess dataAccess)
        {
            _dbService = dbService;
            _dataAccess = dataAccess;
            Initializer();
        }

        private async void Initializer()
        {
            await _dbService.FillTableTech();

            TechData = new ObservableCollection<TechData>(_dataAccess.GetData());
        }
    }
}