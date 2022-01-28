using HVAC_iot_App.Global.HVAC;
using HVAC_iot_App.Models.HVAC;
using HVAC_iot_App.ViewModels.Commands;
using System;

namespace HVAC_iot_App.ViewModels.HVAC
{
    public class FanViewModel : ViewModelBase
    {
        private readonly FanModel FanModel;

        public FanViewModel()
        {
            SetFanSpeedCommand = new RelayCommand(obj => FanModel.FanSpeed = (FanSpeed)obj);
            FanModel = FanModel.Instance;
            FanModel.PropertyChanged += FanModel_PropertyChanged;
        }

        private void FanModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(FanModel.FanSpeed):
                    Notify(nameof(FanSpeed));
                    break;
            }
        }

        public FanSpeed FanSpeed => FanModel.FanSpeed;

        public RelayCommand SetFanSpeedCommand { get; private set; }
    }
}
