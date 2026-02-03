using System;

namespace Opc
{
    public interface IServer : IDisposable
    {
        event ServerShutdownEventHandler ServerShutdown;

        string GetLocale();

        string SetLocale(string locale);

        string[] GetSupportedLocales();

        string GetErrorText(string locale, ResultID resultID);
    }
}