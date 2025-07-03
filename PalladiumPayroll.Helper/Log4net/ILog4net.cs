namespace PalladiumPayroll.Helper.Log4net
{
    public interface ILog4net
    {
        void Warn(object message);
        void Info(object message);
        void Debug(object message);
        void Error(object message);
    }
}
