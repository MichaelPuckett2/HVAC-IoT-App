using HVAC_iot_App.Global.GPIO;
using HVAC_iot_App.Global.HVAC;
using Windows.Devices.Gpio;

namespace HVAC_iot_App.Models.HVAC
{
    public class FanModel : ModelBase
    {
        private readonly GpioPin PinLowSpeed;
        private readonly GpioPin PinMediumSpeed;
        private readonly GpioPin PinHighSpeed;
        private FanSpeed fanSpeed;

        #region Singleton
        static private FanModel thisModel = new FanModel();
        private FanModel()
        {
            var gpio = GpioController.GetDefault();

            if (gpio != null)
            {
                PinLowSpeed = gpio.OpenPin((int)GpioPinNumber.Gpio3, GpioSharingMode.Exclusive);
                PinMediumSpeed = gpio.OpenPin((int)GpioPinNumber.Gpio5, GpioSharingMode.Exclusive);
                PinHighSpeed = gpio.OpenPin((int)GpioPinNumber.Gpio7, GpioSharingMode.Exclusive);
            }
        }
        static public FanModel Instance => thisModel;
        #endregion

        public FanSpeed FanSpeed
        {
            get { return fanSpeed; }
            set
            {
                fanSpeed = value;
                setFanSpeed();
                Notify(nameof(FanSpeed));
            }
        }

        private void setFanSpeed()
        {
            switch(FanSpeed) //Write the High's last in attempt to prevent two of the pins from being High at the same time.
            {
                case FanSpeed.Low:
                    PinMediumSpeed?.Write(GpioPinValue.Low);
                    PinHighSpeed?.Write( GpioPinValue.Low);
                    PinLowSpeed?.Write(GpioPinValue.High);
                    break;

                case FanSpeed.Medium:
                    PinLowSpeed?.Write(GpioPinValue.Low);
                    PinHighSpeed?.Write(GpioPinValue.Low);
                    PinMediumSpeed?.Write(GpioPinValue.High);
                    break;

                case FanSpeed.High:
                    PinLowSpeed?.Write(GpioPinValue.Low);
                    PinMediumSpeed?.Write(GpioPinValue.Low);
                    PinHighSpeed?.Write(GpioPinValue.High);
                    break;

                case FanSpeed.Off:
                default:
                    PinLowSpeed?.Write(GpioPinValue.Low);
                    PinMediumSpeed?.Write(GpioPinValue.Low);
                    PinHighSpeed?.Write(GpioPinValue.Low);
                    break;
            }           
        }
    }
}
