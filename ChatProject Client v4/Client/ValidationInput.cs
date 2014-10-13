using System;
using System.Text.RegularExpressions;

namespace Client
{
    public static class ValidationInput
    {
        public static bool ValidatedIP { get; set; }
        public static bool ValidatedPort { get; set; }
        public static bool ValidatedUserName { get; set; }
        public static bool ValidatedColor { get; set; }

        public static bool ValidateIP(string IPinput, out string ErrorMessage)
        {
            Regex IP = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            if (IP.IsMatch(IPinput))
            {
                ErrorMessage = IPinput;
                ValidatedIP= true;
            }
            else
            {
                ErrorMessage = "Invalid IP number! The format is : 0-255:0-255:0-255:0-255.";
                ValidatedIP= false;
            }
            return ValidatedIP;
        }

        public static bool ValidatePort(string portInput, out string ErrorMessage)
        {
            try
            {
                ushort PortNum = ushort.Parse(portInput);
                if (PortNum > 256)
                {
                    ErrorMessage = portInput;
                    ValidatedPort = true;
                }
                else
                {
                    ErrorMessage = "Port number is out of range! (256-65536)";
                    ValidatedPort = false;
                }
            }
            catch (ArgumentException)
            {
                ErrorMessage = "Incorrect port number conversion";
                ValidatedPort = false;
            }
            catch (OverflowException)
            {
                ErrorMessage = "Port number is out of range";
                ValidatedPort = false;
            }
            return ValidatedPort;
        }

        public static bool ValidateUserName(string userNameInput, out string ErrorMessage)
        {
            if (userNameInput == "" || userNameInput == "Enter your user name here!")
            {
                ErrorMessage = "Wrong user name";
                ValidatedUserName= false;
            }
            else
            {
                ErrorMessage = userNameInput;
                ValidatedUserName= true;
            }
            return ValidatedUserName;
        }

        public static bool ValidateColor(string colorInput, out string ErrorMessage)
        {
            if (colorInput == "Choose your colour here!")
            {
                ErrorMessage = "Please, choose your colour";
                ValidatedColor= false;
            }
            else
            {
                ErrorMessage = colorInput;
                ValidatedColor= true;
            }
            return ValidatedColor;
        }
    }
}
